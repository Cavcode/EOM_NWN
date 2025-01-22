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
using EOM.Game.Server.Service.StatusEffectService;
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Feature.AbilityDefinition.Weapons.Axes
{
    public class OverheadAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Overhead(builder);

            return builder.Build();
        }

        private static string SValidation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);
            var rightHandType = GetBaseItemType(weapon);

            if (Item.WeaponBaseItemTypes.Contains(rightHandType))
            {
                return string.Empty;
            }
            else
                return "A Axe must be equipped in your right hand to use this ability.";
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            var damBase = 25;
            damBase += Combat.GetAbilityDamageBonus(activator, SkillType.Axes);
            var stat = AbilityType.Might;
            var attackerStat = Combat.GetPerkAdjustedAbilityScore(activator);
            var attack = Stat.GetAttack(activator, stat, SkillType.Axes);

            Console.Write(GetName(target));
            Console.Write("test");

            ApplyEffectToObject(DurationType.Instant,
                    EffectVisualEffect(VisualEffect.Vfx_Dur_OverheadSound), activator);
            DelayCommand(0.5f, () => ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffect.Vfx_Dur_Slashl), activator, 0.2f));
            DelayCommand(1.0f, () => ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffect.Vfx_Dur_Slashr), activator, 0.2f));
            DelayCommand(1.5f, () => ApplyEffectToObject(DurationType.Temporary, EffectVisualEffect(VisualEffect.Vfx_Dur_Overhead,false,2), target, 0.5f));
            var creature = GetFirstObjectInShape(Shape.SpellCone, RadiusSize.Large, targetLocation, true, ObjectType.Creature, GetPosition(activator));

            var defense = Stat.GetDefense(creature, CombatDamageType.Physical, AbilityType.Vitality);
            var defenderStat = GetAbilityScore(creature, AbilityType.Vitality);
            var damage = Combat.CalculateDamage(
                attack,
                damBase,
                attackerStat,
                defense,
                defenderStat,
                0);
            Console.Write(damage.ToString() + " damage");

            var dTarget = creature;
            DelayCommand(1.5f,
                () =>
                {
                    ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing),
                        dTarget);
                });

            CombatPoint.AddCombatPoint(activator, creature, SkillType.Axes, 3);

            if (StatusEffect.HasStatusEffect(activator, StatusEffectType.Ember1))
                StatusEffect.Apply(target, activator, StatusEffectType.Ember2, 12f);
            else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.Ember2))
                StatusEffect.Apply(target, activator, StatusEffectType.Ember3, 12f);
            else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.Ember3))
                StatusEffect.Apply(target, activator, StatusEffectType.Ember4, 12f);
            else if (StatusEffect.HasStatusEffect(activator, StatusEffectType.Ember4))
                StatusEffect.Apply(target, activator, StatusEffectType.Ember5, 12f);
            else
                StatusEffect.Apply(target, activator, StatusEffectType.Ember1, 12f);

            AssignCommand(activator, () => ActionPlayAnimation(Animation.PointForward));

        }

        private static void Overhead(AbilityBuilder builder)
        {
            builder.Create(FeatType.Overhead, PerkType.Overhead)
                .Name("Overhead")
                .Level(1)
                .HasRecastDelay(RecastGroup.Overhead, 2f)
                .RequirementMagick(10)
                .UnaffectedByHeavyArmor()
                //.HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }

    }
}