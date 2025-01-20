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
    public class CleaveAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Cleave(builder);

            return builder.Build();
        }

        private static string SValidation(uint activator, uint target, int level, Location targetLocation)
        {
            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);
            var rightHandType = GetBaseItemType(weapon);

            if (Item.LightsaberBaseItemTypes.Contains(rightHandType))
            {
                return string.Empty;
            }
            else
                return "A lightsaber must be equipped in your right hand to use this ability.";
        }

        private static void ImpactAction(uint activator, uint target, int level, Location targetLocation)
        {
            // If activator is in stealth mode, force them out of stealth mode.
            if (GetActionMode(activator, ActionMode.Stealth) == true)
                SetActionMode(activator, ActionMode.Stealth, false);

            var damBase = 10;
            damBase += Combat.GetAbilityDamageBonus(activator, SkillType.Axes);
            var stat = AbilityType.Might;
            var attackerStat = Combat.GetPerkAdjustedAbilityScore(activator);
            var attack = Stat.GetAttack(activator, stat, SkillType.Axes);

            Console.Write(GetName(target));
            Console.Write("test");

            DelayCommand(0.5f, () => ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Crack), activator)); 
            var creature = GetFirstObjectInShape(Shape.SpellCone, RadiusSize.Large, targetLocation, true, ObjectType.Creature, GetPosition(activator));
            while (GetIsObjectValid(creature)) 
            {
                if (GetIsReactionTypeFriendly(activator) == 0 && creature != activator)
                {
                    Console.Write("In while");
                    var defense = Stat.GetDefense(creature, CombatDamageType.Physical, AbilityType.Vitality);
                    var defenderStat = GetAbilityScore(creature, AbilityType.Vitality);
                    var damage = Combat.CalculateDamage(
                        attack,
                        damBase,
                        attackerStat,
                        defense,
                        defenderStat,
                        0);
                    Console.Write(damage.ToString() + " damages");

                    var dTarget = creature;
                    DelayCommand(0.1f,
                        () =>
                        {
                            ApplyEffectToObject(DurationType.Instant, EffectDamage(damage, DamageType.Slashing),
                                dTarget);
                        });

                    CombatPoint.AddCombatPoint(activator, creature, SkillType.Axes, 3);
                }

                creature = GetNextObjectInShape(Shape.SpellCone, RadiusSize.Large, targetLocation, true, ObjectType.Creature, GetPosition(activator));
            }

            




            CombatPoint.AddCombatPoint(activator, target, SkillType.Axes, 3);

            AssignCommand(activator, () => ActionPlayAnimation(Animation.CrossArms));

        }

        private static void Cleave(AbilityBuilder builder)
        {
            builder.Create(FeatType.ACleave, PerkType.ACleave)
                .Name("Cleave")
                .Level(1)
                .HasRecastDelay(RecastGroup.Cleave, 2f)
                .RequirementMagick(3)
                .UnaffectedByHeavyArmor()
                //.HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }

    }
}