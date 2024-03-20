using EOM.Game.Server.Core;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AnimationService;

namespace EOM.Game.Server.Feature
{
    public static class CreatureDeathAnimation
    {
        [NWNEventHandler("crea_death_aft")]
        public static void OnDeath()
        {
            var creature = OBJECT_SELF;
            AnimationPlayer.Play(creature, AnimationEvent.CreatureOnDeath);
        }
    }
}
