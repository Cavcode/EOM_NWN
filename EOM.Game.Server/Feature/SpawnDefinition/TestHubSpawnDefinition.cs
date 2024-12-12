using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Service.AnimationService;
using EOM.Game.Server.Service.SpawnService;

namespace EOM.Game.Server.Feature.SpawnDefinition
{
    public class TestHubSpawnDefinition: ISpawnListDefinition
    {
        public Dictionary<string, SpawnTable> BuildSpawnTables()
        {
            var builder = new SpawnTableBuilder();
            DroidSpawns(builder);

            return builder.Build();
        }

        private void DroidSpawns(SpawnTableBuilder builder)
        {
            builder.Create("TESTHUB_DUMMYS", "TestHub Dummies")
                .AddSpawn(ObjectType.Creature, "testdummy")
                .WithFrequency(100)
                .ReturnsHome()
                .RespawnDelay(4)
                .PlayAnimation(DurationType.Instant, AnimationEvent.CreatureOnDeath, VisualEffect.Fnf_Fireball);

        }

    }
}
