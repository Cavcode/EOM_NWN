﻿using EOM.Game.Server.Core;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature
{
    public class HolonetTerminal
    {
        /// <summary>
        /// When a user click the holonet terminal, a UI will open which lets them enter their message
        /// and pay a fee to send a message over the in-game holonet channel and discord.
        /// </summary>
        [NWNEventHandler("open_holonet")]
        public static void OpenHolonetUI()
        {
            var player = GetLastUsedBy();
            var terminal = OBJECT_SELF;
            Gui.TogglePlayerWindow(player, GuiWindowType.HoloNet, null, terminal);
        }
    }
}