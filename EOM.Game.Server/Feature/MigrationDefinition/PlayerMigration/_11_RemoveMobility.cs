using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;

namespace EOM.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _11_RemoveMobility : PlayerMigrationBase
    {
        public override int Version => 11;
        public override void Migrate(uint player)
        {
            CreaturePlugin.RemoveFeat(player, FeatType.Mobility);
        }
    }
}
