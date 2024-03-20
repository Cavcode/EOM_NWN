﻿using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.AbilityDefinition.Force
{
    public class ForceMindAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            ForceMind1();
            ForceMind2();

            return _builder.Build();
        }

        private void ForceMind1()
        {
            _builder.Create(FeatType.ForceMind1, PerkType.ForceMind)
                .Name("Force Mind I")
                .Level(1)
                .HasRecastDelay(RecastGroup.ForceRestore, 60f * 3f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.ForceMind1, 60f);
                    ApplyEffectToObject(DurationType.Temporary, EffectAbilityDecrease(AbilityType.Willpower, 2), activator, 60f);
                });
        }
        private void ForceMind2()
        {
            _builder.Create(FeatType.ForceMind2, PerkType.ForceMind)
                .Name("Force Mind II")
                .Level(2)
                .HasRecastDelay(RecastGroup.ForceRestore, 60f * 3f)
                .IsCastedAbility()
                .UsesAnimation(Animation.LoopingConjure1)
                .DisplaysVisualEffectWhenActivating()
                .HasImpactAction((activator, target, level, location) =>
                {
                    StatusEffect.Apply(activator, activator, StatusEffectType.ForceMind2, 60f);
                    ApplyEffectToObject(DurationType.Temporary, EffectAbilityDecrease(AbilityType.Willpower, 4), activator, 60f);
                });
        }
    }
}