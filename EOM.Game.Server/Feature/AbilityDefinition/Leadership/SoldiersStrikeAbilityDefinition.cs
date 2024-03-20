using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.AbilityDefinition.Leadership
{
    public class SoldiersStrikeAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SoldiersStrike();

            return _builder.Build();
        }

        private void SoldiersStrike()
        {
            _builder.Create(FeatType.SoldiersStrike, PerkType.SoldiersStrike)
                .Name("Soldier's Strike")
                .Level(1)
                .HasRecastDelay(RecastGroup.SoldiersStrike, 60f)
                .HasActivationDelay(4f)
                .UnaffectedByHeavyArmor()
                .IsCastedAbility()
                .UsesAnimation(Animation.FireForgetTaunt)
                .HasActivationAction((activator, target, level, location) =>
                {
                    return Ability.ToggleAura(activator, StatusEffectType.SoldiersStrike);
                })
                .HasImpactAction((activator, target, level, location) =>
                {
                    Ability.ApplyAura(activator, StatusEffectType.SoldiersStrike, false, true, false);
                });
        }
    }
}
