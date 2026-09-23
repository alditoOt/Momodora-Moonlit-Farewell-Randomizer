using APMomodoraMoonlitFarewell.Utils;

namespace APMomodoraMoonlitFarewell.Archipelago
{
    // Applies the effect of a single received Archipelago item to the game state (money, skills, sigils).
    static class APItemGranter
    {
        // Archipelago "filler" item id used for the money/currency pickup, and the amount it grants.
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
                GameData.inventory.Add(GameData.itemDatabase.GetItem(itemId), is_new_item: false);
            }
        }
    }
}
