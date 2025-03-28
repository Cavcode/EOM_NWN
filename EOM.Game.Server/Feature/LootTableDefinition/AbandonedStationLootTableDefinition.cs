﻿using System.Collections.Generic;
using EOM.Game.Server.Service.LootService;

namespace EOM.Game.Server.Feature.LootTableDefinition
{
    public class AbandonedStationLootTableDefinition: ILootTableDefinition
    {
        private readonly LootTableBuilder _builder = new LootTableBuilder();

        public Dictionary<string, LootTable> BuildLootTables()
        {
            ZombieRancor();

            return _builder.Build();
        }

        private void ZombieRancor()
        {
            _builder.Create("ABANDONED_STATION_ZOMBIE_RANCOR");
        }
    }
}
