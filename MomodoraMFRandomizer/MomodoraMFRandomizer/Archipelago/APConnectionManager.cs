using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Packets;
using MelonLoader;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Owns every network write and reconnect loop, so nothing on Unity's main thread ever waits on the socket. 
    // Gameplay code enters a queue (location checks, goal); a background worker sends them, 
    // and if the socket drops it reconnects with backoff and takes care of whatever piled up
    static class APConnectionManager
    {
        private const int sendTimeoutMs = 10000;
        private const int workerTickMs = 1000;
        private const int firstRetryDelayMs = 2000;
        private const int maxRetryDelayMs = 30000;

        private static readonly object stateLock = new object();
        private static readonly HashSet<long> pendingLocations = new HashSet<long>();
        private static bool goalPending;
        private static bool goalSent;

        private static readonly ConcurrentQueue<Action> mainThreadActions = new ConcurrentQueue<Action>();
        private static readonly AutoResetEvent wake = new AutoResetEvent(false);

        private static volatile bool connected;
        private static Thread worker;

        public static bool IsConnected => connected;

        // Call once after the first successful login
        public static void Start()
        {
            connected = true;
            if (worker != null)
            {
                return;
            }
            worker = new Thread(WorkerLoop) { IsBackground = true, Name = "AP connection worker" };
            worker.Start();
        }

        public static void QueueLocation(long locationId)
        {
            lock (stateLock)
            {
                pendingLocations.Add(locationId);
            }
            wake.Set();
        }

        // A check made while offline hasn't reached the session's AllLocationsChecked yet
        public static bool IsPending(long locationId)
        {
            lock (stateLock)
            {
                return pendingLocations.Contains(locationId);
            }
        }

        public static void QueueGoal()
        {
            lock (stateLock)
            {
                if (goalSent)
                {
                    return;
                }
                goalPending = true;
            }
            wake.Set();
        }

        // Fire-and-forget network work (i.e. DeathLink) that must not run on the game thread.
        // aka try to shoot this method when available but don't block the game (which could freeze the game)
        public static void RunInBackground(Action action)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    MelonLogger.Warning($"Background Archipelago send failed: {e.GetBaseException().Message}");
                    MarkDisconnected("send failed");
                }
            });
        }

        public static void RunOnMainThread(Action action) => mainThreadActions.Enqueue(action);

        // Called from OnFixedUpdate
        // Every frame try to run the established threads
        // Main thread handles game stuff, background threads handle mostly networking stuff
        public static void DrainMainThreadActions()
        {
            while (mainThreadActions.TryDequeue(out Action action))
            {
                try
                {
                    action();
                }
                catch (Exception e)
                {
                    MelonLogger.Error($"Error running queued main-thread action: {e}");
                }
            }
        }

        public static void MarkDisconnected(string reason)
        {
            lock (stateLock)
            {
                if (!connected)
                {
                    return;
                }
                connected = false;
            }
            MelonLogger.Warning($"Disconnected from Archipelago ({reason}). Checks are saved and will be sent once the connection is back; trying to reconnect...");
            RunOnMainThread(() => APExchangeNotifier.NotifyStatus("Disconnected from Archipelago. Reconnecting..."));
            wake.Set();
        }

        private static void WorkerLoop()
        {
            int retryDelayMs = firstRetryDelayMs;
            DateTime nextAttempt = DateTime.MinValue;

            while (true)
            {
                wake.WaitOne(workerTickMs);

                try
                {
                    if (connected)
                    {
                        // Catches a closed socket even if no event fired for it
                        if (APMomodoraMoonlitFarewell.session?.Socket == null || !APMomodoraMoonlitFarewell.session.Socket.Connected)
                        {
                            MarkDisconnected("socket closed");
                            continue;
                        }
                        Flush();
                        continue;
                    }

                    if (DateTime.UtcNow < nextAttempt)
                    {
                        continue;
                    }

                    if (APMomodoraMoonlitFarewell.TryReconnectSession())
                    {
                        retryDelayMs = firstRetryDelayMs;
                        connected = true;
                        int queued;
                        lock (stateLock)
                        {
                            queued = pendingLocations.Count;
                        }
                        MelonLogger.Msg("Reconnected to Archipelago.");
                        RunOnMainThread(() =>
                        {
                            APLocationHandler.UpdateItemsForTheSession(null);
                            APExchangeNotifier.NotifyStatus(queued > 0
                                ? $"Reconnected to Archipelago! Sending {queued} pending check(s)."
                                : "Reconnected to Archipelago!");
                        });
                    }
                    else
                    {
                        nextAttempt = DateTime.UtcNow.AddMilliseconds(retryDelayMs);
                        retryDelayMs = Math.Min(retryDelayMs * 2, maxRetryDelayMs);
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Warning($"Archipelago connection worker error: {e.GetBaseException().Message}");
                    MarkDisconnected("worker error");
                }
            }
        }

        private static void Flush()
        {
            long[] ids;
            bool sendGoal;
            lock (stateLock)
            {
                ids = pendingLocations.ToArray();
                sendGoal = goalPending && !goalSent;
            }

            try
            {
                if (ids.Length > 0)
                {
                    if (!APMomodoraMoonlitFarewell.session.Locations.CompleteLocationChecksAsync(ids).Wait(sendTimeoutMs))
                    {
                        throw new TimeoutException("sending location checks timed out");
                    }
                    lock (stateLock)
                    {
                        foreach (long id in ids)
                        {
                            pendingLocations.Remove(id);
                        }
                    }
                }
                if (sendGoal)
                {
                    StatusUpdatePacket packet = new StatusUpdatePacket { Status = ArchipelagoClientState.ClientGoal };
                    if (!APMomodoraMoonlitFarewell.session.Socket.SendPacketAsync(packet).Wait(sendTimeoutMs))
                    {
                        throw new TimeoutException("sending goal status timed out");
                    }
                    lock (stateLock)
                    {
                        goalSent = true;
                        goalPending = false;
                    }
                }
            }
            catch (Exception e)
            {
                MarkDisconnected(e.GetBaseException().Message);
            }
        }
    }
}
