using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Applies the effect of a single received Archipelago item to the game state (money, skills, sigils)
    static class APItemGranter
    {
        private const int moneyFillerItemId = 999;
        private const int moneyFillerAmount = 100;

        public static void GiveItem(int itemId)
        {
            if (itemId == moneyFillerItemId)
            {
                Platformer3D.player_money += moneyFillerAmount;
                return;
            }
            if (MomoEventUtils.SKILLEVENTS.Contains(itemId))
            {
                GameData.current.MomoEvent[itemId] = 1;
            }
            else if (InventoryUtils.AP_SIGIL_ITEM_ID.Contains(itemId))
            {
                // 1.8.0 documentation: is_new_item is set to false for a specific reason I can't remember right now
                // but if I set it to true, the logic for receiving the Sigil didn't work properly 
                // (I believe it was so the in world Sigil still appeared even if you had the item?)
                GameData.inventory.Add(GameData.itemDatabase.GetItem(itemId), is_new_item: false);
            }
        }
    }
}
