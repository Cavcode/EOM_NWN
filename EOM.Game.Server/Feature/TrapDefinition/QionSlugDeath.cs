﻿using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Creature;
using EOM.Game.Server.Service;


namespace EOM.Game.Server.Feature
{
    public class SpawnLarvaeOnSlugDeath
    {
        /// <summary>
        /// When this creature dies, he'll spawn more creatures - for example, a large worm exploding into swarms of small bugs.
        /// </summary>
        [NWNEventHandler("crea_death_bef")]
        public static void CreatureDeath()
        {
            if (GetTag(OBJECT_SELF) != "qion_hive_slug")
                return;

            var creatureLocation = GetLocation(OBJECT_SELF);

            // Four lines create four creatures, more lines create more creatures
            CreateObject(ObjectType.Creature, "qionhivebugswarm", creatureLocation, true);
            CreateObject(ObjectType.Creature, "qionhivebugswarm", creatureLocation, true);
            CreateObject(ObjectType.Creature, "qionhivebugswarm", creatureLocation, true);
            CreateObject(ObjectType.Creature, "qion_larvae", creatureLocation, true);
        }

        /// <summary>
        /// When the creatures spawn, this will broadcasts an environmental message describing the narrative circumstances of that spawn in.
        /// It has to be ChatChannel.DMTalk - it won't work if it's ChatChannel.PlayerTalk.
        /// </summary>
        [NWNEventHandler("crea_spawn_aft")]
        public static void MessageOnDeath()
        {
            if (GetTag(OBJECT_SELF) != "qion_hive_larvae")
                return;
                
            Messaging.SendMessageNearbyToPlayers(OBJECT_SELF, "A ravenous larvae that had been clinging onto the Qion Hive Slug dislodges itself upon its host's demise; and with it, clouds of buzzing flesh flies.", 30f);
        }
    }
}
