using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;

namespace EOM.Game.Server.Feature.MigrationDefinition.PlayerMigration
{
    public class _12_RemoveDecayedPerks: PlayerMigrationBase
    {
        public override int Version => 12;
        public override void Migrate(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            foreach (var (perkType, level) in dbPlayer.Perks)
            {
                var effectiveLevel = Perk.GetPlayerEffectivePerkLevel(player, perkType);

                if (level != effectiveLevel)
                {
                    RefundPerk(player, perkType);
                }
            }
        }
    }
}
