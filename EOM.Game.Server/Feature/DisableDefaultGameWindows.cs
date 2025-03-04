﻿using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;

namespace EOM.Game.Server.Feature
{
    public static class DisableDefaultGameWindows
    {
        /// <summary>
        /// When the player enters the server, disable default game windows.
        /// In most cases, these windows are replaced with custom versions.
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void DisableWindows()
        {
            var player = GetEnteringObject();
            if (!GetIsPC(player) || GetIsDM(player))
                return;

            // Spell Book - Completely unused
            SetGuiPanelDisabled(player, GuiPanel.SpellBook, true);

            // Character Sheet - A NUI replacement is used
            SetGuiPanelDisabled(player, GuiPanel.CharacterSheet, true);

            // Journal - A NUI replacement is used
            SetGuiPanelDisabled(player, GuiPanel.Journal, true);

            // Compass - Space is used by HP/FP/MP bars.
            SetGuiPanelDisabled(player, GuiPanel.Compass, true);
        }
    }
}
