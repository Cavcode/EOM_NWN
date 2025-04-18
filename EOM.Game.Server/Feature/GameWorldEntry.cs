﻿using EOM.Game.Server.Core;

namespace EOM.Game.Server.Feature
{
    public class GameWorldEntry
    {
        [NWNEventHandler("enter_world")]
        public static void EnterGameWorld()
        {
            var player = GetPCSpeaker();
            var waypoint = GetObjectByTag("ENTRY_STARTING_WP");
            var location = GetLocation(waypoint);

            AssignCommand(player, () =>
            {
                ActionJumpToLocation(location);
            });
        }
    }
}
