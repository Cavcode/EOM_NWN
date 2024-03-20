using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.StatusEffectDefinition
{
    public class IronShellStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            IronShell();

            return _builder.Build();
        }

        private void IronShell()
        {
            _builder.Create(StatusEffectType.IronShell)
                .Name("Iron Shell")
                .EffectIcon(EffectIconType.ElementalShield);
        }
    }
}
