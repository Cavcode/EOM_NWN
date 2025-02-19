using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.StatusEffectDefinition
{
    public class TriggerStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            Trigger();

            return _builder.Build();
        }

        private void Trigger()
        {
            _builder.Create(StatusEffectType.Trigger)
                .Name("Trigger")
                .EffectIcon(EffectIconType.AttackIncrease);
        }
    }
}
