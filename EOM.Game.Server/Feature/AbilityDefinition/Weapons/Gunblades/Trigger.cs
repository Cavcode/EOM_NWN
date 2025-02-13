using System;
using System.Collections.Generic;
using Discord;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.Bioware;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Item;
using EOM.Game.Server.Core.NWScript.Enum.Item.Property;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.CombatService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service.StatusEffectService;
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Feature.AbilityDefinition.Weapons.Gunblades
{
    public class TriggerAbilityDefinition : IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            var builder = new AbilityBuilder();
            Trigger(builder);

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

            var weapon = GetItemInSlot(InventorySlot.RightHand, activator);

            //AddItemProperty(DurationType.Temporary,ItemPropertyDamageBonus(ItemPropertyDamageType.Fire,DamageBonus.DAMAGEBONUS_2d6), weapon,30.0f);
            var prop = ItemPropertyCustom(ItemPropertyType.DMG, (int)CombatDamageType.Fire, 5);
            BiowareXP2.IPSafeAddItemProperty(weapon, prop, 30.0f, AddItemPropertyPolicy.ReplaceExisting, false, false);

            AssignCommand(activator, () => ActionPlayAnimation(Animation.PointForward));

        }

        private static void Trigger(AbilityBuilder builder)
        {
            builder.Create(FeatType.Trigger, PerkType.Trigger)
                .Name("Trigger")
                .Level(1)
                .RequirementMagick(1)
                .UnaffectedByHeavyArmor()
                //.HasCustomValidation(Validation)
                .HasImpactAction(ImpactAction);
        }

    }
}