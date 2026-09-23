using Archipelago.MultiClient.Net.Models;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Builds and displays the "Sent X to Player Y!" / "Received X from Player Y!" popups for the
    // multiworld item exchange. Always renders through the game's own corner ItemNotifications
    // popup -- never TutorialMessage or the DialogueManager/DialogueText dialogue-box system used
    // for NPC cutscenes -- so every vanilla popup for the underlying pickup is left untouched, and
    // this one is shown as an additional notification alongside it.
    static class APExchangeNotifier
    {
        private const string itemColor = "#FFD27F";
        private const string playerColor = "#66CCFF";

        // A run of plain text, or of text wrapped in a <color> tag whose alpha we re-embed every
        // frame (see ReapplyColorForFrame) -- ItemNotifications renders through a legacy
        // UnityEngine.UI.Text component, whose fade-out animation does not apply to colored spans
        // on its own (a <color> tag pins that span to whatever alpha it names, ignoring the
        // component's own fading color), so a static color tag would leave that span stuck on screen.
        private readonly struct Segment
        {
            public readonly string Text;
            public readonly string ColorHex;
            public Segment(string text, string colorHex = null)
            {
                Text = text;
                ColorHex = colorHex;
            }
        }

        private static Segment[] trackedSegments;
        private static string trackedPlainText;

        // ItemNotifications only has one visual slot, so a notification that arrives while another
        // is still showing is queued here instead of overwriting it, and is displayed once the
        // current one has fully finished (see `showing` and ReapplyColorForFrame below).
        private const int maxQueuedNotifications = 5;
        private static readonly Queue<Segment[]> pendingNotifications = new Queue<Segment[]>();
        private static bool showing;

        private static readonly FieldInfo myTextField = AccessTools.Field(typeof(ItemNotifications), "my_text");

        private static bool IsForSelf(ScoutedItemInfo info) =>
            info.Player.Slot == APMomodoraMoonlitFarewell.session.ConnectionInfo.Slot;

        private static void ShowColorized(params Segment[] segments)
        {
            if (showing)
            {
                // Several exchanges can be reported synchronously in the same call stack (e.g. draining
                // multiple received items in one loop) before Unity's next FixedUpdate ever runs, so
                // `showing` -- set synchronously in Display below -- is what keeps a same-frame burst
                // from collapsing together the way relying on ItemNotifications' own static state would.
                if (pendingNotifications.Count >= maxQueuedNotifications)
                {
                    pendingNotifications.Dequeue();
                }
                pendingNotifications.Enqueue(segments);
                return;
            }

            Display(segments);
        }

        private static void Display(Segment[] segments)
        {
            showing = true;
            trackedSegments = segments;
            trackedPlainText = string.Concat(segments.Select(s => s.Text));
            ItemNotifications.SetNotification(trackedPlainText, null);
        }

        // Postfix target for ItemNotifications.FixedUpdate (see Patches/APExchangeNotificationPatcher.cs):
        // re-renders the currently showing notification every frame with each colored span's alpha
        // matched to the popup's own fade animation, so colored text fades out along with everything else.
        // Also drains the notification queue once the popup has fully finished (faded back out), so a
        // queued notification only appears after the previous one has visually disappeared.
        public static void ReapplyColorForFrame(ItemNotifications instance)
        {
            if (trackedSegments != null)
            {
                if (ItemNotifications.text != trackedPlainText)
                {
                    trackedSegments = null;
                }
                else
                {
                    byte alphaByte = (byte)Mathf.Clamp(instance.alpha.a * 255f, 0f, 255f);
                    StringBuilder builder = new StringBuilder();
                    foreach (Segment segment in trackedSegments)
                    {
                        builder.Append(segment.ColorHex == null
                            ? segment.Text
                            : $"<color={segment.ColorHex}{alphaByte:X2}>{segment.Text}</color>");
                    }

                    if (myTextField?.GetValue(instance) is Text textComponent)
                    {
                        textComponent.text = builder.ToString();
                    }
                }
            }

            if (showing && !ItemNotifications.active && instance.alpha.a <= 0f)
            {
                showing = false;
                if (pendingNotifications.Count > 0)
                {
                    Display(pendingNotifications.Dequeue());
                }
            }
        }

        // Corner popup for every send-side pickup (bosses, fairies, narrative skills, stat berries,
        // Lily/Atk flower, sigils, Dash, Double Jump) once APLocationScoutCache confirms the
        // location is part of the multiworld. Shows the normal "Sent" message when the item belongs
        // to someone else, or a self-congratulatory "Received" message when it's our own check.
        public static void NotifyExchange(ScoutedItemInfo info)
        {
            if (IsForSelf(info))
            {
                ShowColorized(
                    new Segment("Received "),
                    new Segment(info.ItemDisplayName, itemColor),
                    new Segment(" by yourself! Go you!"));
            }
            else
            {
                ShowColorized(
                    new Segment("Sent "),
                    new Segment(info.ItemDisplayName, itemColor),
                    new Segment(" to "),
                    new Segment(info.Player.Alias, playerColor),
                    new Segment("!"));
            }
        }

        // Sentinel localization id: MainScr.GetString is patched (see APExchangeNotificationPatcher)
        // to resolve this to an empty string, so a TutorialMessage override can carry its entire
        // message in extraText without needing a real localization-XML entry.
        public const string SentinelLocalizationKey = "ap_raw";

        // Stat berries, sigils, the Lily/Atk flower, and Dash/Double Jump all show their own vanilla
        // TutorialMessage popup as well as the corner NotifyExchange popup above -- rather than
        // showing the full item/player details twice, replace the vanilla text with a short pointer
        // so the player knows to look at the corner notification for the actual exchange details.
        public static void OverrideTutorialMessageWithPointerPopup(ScoutedItemInfo info)
        {
            string verb = IsForSelf(info) ? "received" : "sent";
            TutorialMessage.text = SentinelLocalizationKey;
            TutorialMessage.extraText = $"An <color={itemColor}>AP Item</color> was {verb}! Check the notification!";
        }

        public static void NotifyReceived(ItemInfo item)
        {
            string itemName = item.ItemDisplayName;
            string playerName = item.Player.Alias;

            if (InventoryUtils.TRUE_SIGIL_ITEM_ID.Contains((int)item.ItemId))
            {
                ShowColorized(
                    new Segment("Received Sigil "),
                    new Segment(itemName, itemColor),
                    new Segment(" from "),
                    new Segment(playerName, playerColor),
                    new Segment("!"));
            }
            else if (IsStat(item.ItemId))
            {
                ShowColorized(
                    new Segment("Received "),
                    new Segment(itemName, itemColor),
                    new Segment(" from "),
                    new Segment(playerName, playerColor),
                    new Segment($"!\n{BuildStatText(item.ItemId)}"));
            }
            else if (item.ItemId == InventoryUtils.FAIRY_ID)
            {
                ShowColorized(
                    new Segment(playerName, playerColor),
                    new Segment(" freed a Lumen Fairy!"));
            }
            else
            {
                ShowColorized(
                    new Segment("Received "),
                    new Segment(itemName, itemColor),
                    new Segment(" from "),
                    new Segment(playerName, playerColor),
                    new Segment("!"));
            }
        }

        private static bool IsStat(long itemId) =>
            itemId == InventoryUtils.HEALTH_ID || itemId == InventoryUtils.MAGIC_ID ||
            itemId == InventoryUtils.DAMAGE_ID || itemId == InventoryUtils.STAMINA_ID;

        // Mirrors the vanilla ItemFruit.Notif() popup: a flavor line, plus a "(previous -> new)"
        // delta for Health/Magic/Damage (Stamina shows no numeric delta in vanilla either).
        private static string BuildStatText(long itemId)
        {
            if (itemId == InventoryUtils.HEALTH_ID)
            {
                return BuildDeltaText(APPlayerStatUpdater.baseMaxHealth, APPlayerStatUpdater.healthPerBerry, itemId);
            }
            if (itemId == InventoryUtils.MAGIC_ID)
            {
                return BuildDeltaText(APPlayerStatUpdater.baseMaxMagic, APPlayerStatUpdater.magicPerUpgrade, itemId);
            }
            if (itemId == InventoryUtils.DAMAGE_ID)
            {
                return BuildDeltaText(APPlayerStatUpdater.baseAttack, APPlayerStatUpdater.attackPerLily, itemId);
            }
            return "";
        }

        // Counts how many of this stat item we've received in total (including the one just
        // dequeued) to compute the delta, rather than relying on live Platformer3D state --
        // that value may already reflect a full resync of every received item by the time this runs.
        private static string BuildDeltaText(int baseValue, int perUnit, long itemId)
        {
            int countSoFar = APMomodoraMoonlitFarewell.session.Items.AllItemsReceived.Count(i => i.ItemId == itemId);
            int newValue = baseValue + perUnit * countSoFar;
            int previousValue = newValue - perUnit;
            return $"({previousValue} -> {newValue})";
        }
    }
}
