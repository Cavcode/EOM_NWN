﻿using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;

namespace EOM.Game.Server.Feature
{
    public static class DisableAttackOfOpportunity
    {
        /// <summary>
        /// Whenever an attack of opportunity is broadcast, skip the event to disable it.
        /// This should effectively disable AOOs across the board.
        /// </summary>
        [NWNEventHandler("brdcast_aoo_bef")]
        public static void OnAttackOfOpportunity()
        {
            EventsPlugin.SkipEvent();
        }
    }
}
