using System;
using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.CombatService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Feature.AbilityDefinition.Weapons.Axes
{
    public class ChargeAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Cleave(builder);

            return builder.Build();
        }


        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            var dmg = 0;
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            dmg += Combat.GetAbilityDamageBonus(activator, SkillType.Axes);

            const float Delay = 0.8f;
            ClearAllActions();
            AssignCommand(activator, () =>
            {
                PlaySound("sf_charge");
                ActionPlayAnimation(Animation.LayOnSide, 2.0f, 1.0f);
                SetCommandable(false, activator);
            });
            ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Charge), activator);

            CombatPoint.AddCombatPoint(activator, target, SkillType.Axes, 3);

            var stat = AbilityType.Might;
            var attackerStat = Combat.GetPerkAdjustedAbilityScore(activator);
            var attack = Stat.GetAttack(activator, stat, SkillType.Axes);
            var defense = Stat.GetDefense(target, CombatDamageType.Physical, AbilityType.Vitality);
            var defenderStat = GetAbilityScore(target, AbilityType.Vitality);
            var damage = Combat.CalculateDamage(
                attack,
                dmg,
                attackerStat,
                defense,
                defenderStat,
                0);
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);
            var rightHandBaseItemType = GetBaseItemType(weapon);

            DelayCommand(Delay, () =>
            {
                const float Duration = 2f;
                SetCommandable(true, activator);
                ApplyEffectToObject(DurationType.Instant, EffectDamage(damage), target);
                ApplyEffectToObject(DurationType.Temporary, EffectStunned(), target, Duration);
                Ability.ApplyTemporaryImmunity(target, Duration, ImmunityType.Stun);
                AssignCommand(activator, () =>
                {
                    ActionJumpToObject(target);
                });
            });
        }

        private static void Cleave(AbilityBuilder builder)
        {
            builder.Create(FeatType.ACharge, PerkType.ACharge)
                .Name("Charge")
                .Level(1)
                .HasRecastDelay(RecastGroup.Charge, 2f)
                .RequirementMagick(3)
                .HasMaxRange(20f)
                .UnaffectedByHeavyArmor()  
                .HasImpactAction(ImpactAction);
        }

    } 
}