﻿using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Service.AbilityService
{
    public delegate bool AbilityActivationAction(uint activator, uint target, int effectivePerkLevel, Location targetLocation);
    public delegate void AbilityImpactAction(uint activator, uint target, int effectivePerkLevel, Location targetLocation);
    public delegate float AbilityActivationDelayAction(uint activator, uint target, int effectivePerkLevel);
    public delegate float AbilityRecastDelayAction(uint activator);
    public delegate string AbilityCustomValidationAction(uint activator, uint target, int effectivePerkLevel, Location targetLocation);

    public class AbilityDetail
    {
        public string Name { get; set; }
        public AbilityActivationAction ActivationAction { get; set; }
        public AbilityImpactAction ImpactAction { get; set; }
        public AbilityActivationDelayAction ActivationDelay { get; set; }
        public AbilityRecastDelayAction RecastDelay { get; set; }
        public AbilityCustomValidationAction CustomValidation { get; set; }
        public List<IAbilityActivationRequirement> Requirements { get; set; }
        public VisualEffect ActivationVisualEffect { get; set; }
        public RecastGroup RecastGroup { get; set; }
        public AbilityActivationType ActivationType { get; set; }
        public PerkType EffectiveLevelPerkType { get; set; }
        public Animation AnimationType { get; set; }
        public StatusEffectType ConcentrationStatusEffectType { get; set; }
        public bool CanBeUsedInSpace { get; set; }
        public bool IgnoreHeavyArmorPenalty { get; set; }
        public float MaxRange { get; set; }
        public bool IsHostileAbility { get; set; }
        public bool DisplaysActivationMessage { get; set; }
        public int AbilityLevel { get; set; }

        public AbilityDetail()
        {
            ActivationVisualEffect = VisualEffect.None;
            AnimationType = Animation.Invalid;
            Requirements = new List<IAbilityActivationRequirement>();
            ConcentrationStatusEffectType = StatusEffectType.Invalid;
            MaxRange = 5.0f;
            IsHostileAbility = false;
            DisplaysActivationMessage = true;
            AbilityLevel = 1;
        }
    }
}
