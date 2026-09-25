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
    // multiworld item exchange. Always renders through the game's own side ItemNotifications
    // popup so every vanilla popup for the underlying pickup is left untouched, and
    // this one is shown as an additional notification alongside it
    static class APExchangeNotifier
    {
        private const string itemColor = "#FFD27F";
        private const string playerColor = "#66CCFF";
        private const string deathlinkColor = "#fa3d3d";

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
        // current one has fully finished 
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
                // 'showing' flag prevents multiple displays from happening in the same FixedUpdate frame
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

        // Side popup for every send location or item, once APLocationScoutCache confirms the
        // location is part of the multiworld. Shows the normal "Sent" message when the item belongs
        // to someone else, or a self-congratulatory "Received" message when it's the player's own check
        public static void NotifyExchange(ScoutedItemInfo info)
        {
            if (IsForSelf(info))
            {
                ShowColorized(
                    new Segment("You found "),
                    new Segment(info.ItemDisplayName, itemColor),
                    new Segment("! Go "),
                    new Segment("you", playerColor),
                    new Segment("!"));
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

        public const string SentinelLocalizationKey = "ap_raw";

        public static void OverrideTutorialMessageWithPointerPopup(ScoutedItemInfo info)
        {
            string verb = IsForSelf(info) ? "received" : "sent";
            TutorialMessage.text = SentinelLocalizationKey;
            TutorialMessage.extraText = $"An <color={itemColor}>AP Item</color> was {verb}! Check the notification!";
        }

        // Connection status ("Disconnected...", "Reconnected!") through the same corner popup queue
        public static void NotifyStatus(string message)
        {
            ShowColorized(new Segment(message));
        }

        public static void NotifyDeathlink(string player)
        {
            ShowColorized(
                new Segment("Deathlink received by "),
                new Segment(player, deathlinkColor),
                new Segment("!"));
        }

        public static void NotifyReceived(ItemInfo item)
        {
            string itemName = item.ItemDisplayName;
            string playerName = item.Player.Alias;
            string sigilText = "Sigil ";
            
            if (InventoryUtils.ALL_SIGIL_ITEM_ID.Contains((int)item.ItemId))
            {
                ShowColorized(
                    new Segment("Received "),
                    new Segment(InventoryUtils.AP_SIGIL_ITEM_ID.Contains((int)item.ItemId) ? sigilText : ""),
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
                    new Segment("!"));
                    // new Segment($"!\n{BuildStatText(item.ItemId)}"));
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
        // delta for Health/Magic/Damage (Stamina shows no numeric delta in vanilla either)
        // Unused since it won't fit the notification message but leaving it here nonetheless
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
        // dequeued) to compute the delta, rather than relying on live Platformer3D state
        private static string BuildDeltaText(int baseValue, int perUnit, long itemId)
        {
            int countSoFar = APMomodoraMoonlitFarewell.session.Items.AllItemsReceived.Count(i => i.ItemId == itemId);
            int newValue = baseValue + perUnit * countSoFar;
            int previousValue = newValue - perUnit;
            return $"({previousValue} -> {newValue})";
        }
    }
}
