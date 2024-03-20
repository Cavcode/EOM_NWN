using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.StatusEffectDefinition
{
    public class ItemStatusEffectDefinition: IStatusEffectListDefinition
    {
        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            var builder = new StatusEffectBuilder();
            ForcePackEffect(builder);
            
            return builder.Build();
        }

        private void ForcePackEffect(StatusEffectBuilder builder)
        {
            void CreateEffect(string name, int amount)
            {
                builder.Create(StatusEffectType.ForcePack1)
                    .Name(name)
                    .EffectIcon(EffectIconType.Regenerate)
                    .TickAction((source, target, effectData) =>
                    {
                        Stat.RestoreFP(target, amount);
                    });
            }

            CreateEffect("Force Pack I", 2);
            CreateEffect("Force Pack II", 3);
            CreateEffect("Force Pack III", 4);
            CreateEffect("Force Pack IV", 5);
            CreateEffect("Force Pack V", 6);
        }
    }
}
