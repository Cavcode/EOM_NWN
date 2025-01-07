using System.Collections.Generic;
using EOM.Game.Server.Service.LootService;

namespace EOM.Game.Server.Feature.LootTableDefinition
{
    public class TestLobbyTableDefinition: ILootTableDefinition
    {
        private readonly LootTableBuilder _builder = new LootTableBuilder();

        public Dictionary<string, LootTable> BuildLootTables()
        {
            TuskenCrate();

            return _builder.Build();
        }

        private void TuskenCrate()
        {
            _builder.Create("TEST_LOBBY_BOSS_CHEST")
                .AddItem("ether", 20)
                .AddItem("phoenix_down", 15)
                .AddItem("axe", 10)
                .AddItem("sword", 10)
                .AddGold(100, 30);

        }
    }
}
