using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using Random = EOM.Game.Server.Service.Random;

namespace EOM.Game.Server.Feature.PerkDefinition.Weapons
{
    public class AxePerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            PowerfulGrip();
            WeaponExpertiseAxes();
            Stoke();
            ImprovedCriticalAxes();
            MightyCriticalAxes();
            Cleave();
            Charge();
            Overhead();
            MightySwings();

            return _builder.Build();
        }

        private void PowerfulGrip()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.PowerfulGrip)
                .Name("Powerful Grip")

                .AddPerkLevel()
                .Description(
                    "Grants the character a better grip on the axe, giving the wielder a large bonus to Damage.")
                .Price(3)
                .RequirementSkill(SkillType.Axes, 15)
                .GrantsFeat(FeatType.PowerfulGrip);

        }

        private void WeaponExpertiseAxes()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.WeaponExpertiseAxes)
                .Name("Weapon Expertise - Axes")

                .AddPerkLevel()
                .Description(
                    "Grants the character an excelent knowledge on the use of axe, giving the wielder a bonus to Attack.")
                .Price(4)
                .RequirementSkill(SkillType.Axes, 30)
                .GrantsFeat(FeatType.WeaponExpertiseAxes);
        }
        private void Stoke()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.Stoke)
                .Name("Stoke")

                .AddPerkLevel()
                .Description(
                    "Everytime the character uses an Axe skill, will build up one stack of ember, up to 5. Each stack reduces a 5% all incoming physical damage.")
                .Price(4)
                .RequirementSkill(SkillType.Axes, 40)
                .GrantsFeat(FeatType.Stoke);
        }
        private void ImprovedCriticalAxes()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.ImprovedCriticalAxes)
                .Name("Improved Critical - Axes")

                .AddPerkLevel()
                .Description(
                    "Grants the feature Improved Critical to Axes.")
                .Price(5)
                .RequirementSkill(SkillType.Axes, 45)
                .GrantsFeat(FeatType.ImprovedCriticalAxes);
        }
        private void MightyCriticalAxes()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.MightyCriticalAxes)
                .Name("Mighty Critical - Axes")

                .AddPerkLevel()
                .Description(
                    "Grants the feature Overwhelming Critical to Axes.")
                .Price(5)
                .RequirementSkill(SkillType.Axes, 50)
                .GrantsFeat(FeatType.MightyCriticalAxes);
        }
        private void Cleave()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.ACleave)
                .Name("Cleave")

                .AddPerkLevel()
                .Description(
                    "The character swings the axe with might forward in a cone, hitting all the targets inside the AoE.")
                .Price(2)
                .RequirementSkill(SkillType.Axes, 10)
                .GrantsFeat(FeatType.ACleave);
        }
        private void Charge()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.ACharge)
                .Name("Charge")

                .AddPerkLevel()
                .Description(
                    "The character rushes forward the target, hitting with the axe in a savage strike.")
                .Price(2)
                .RequirementSkill(SkillType.Axes, 25)
                .GrantsFeat(FeatType.ACharge);
        }
        private void Overhead()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.Overhead)
                .Name("Overhead")

                .AddPerkLevel()
                .Description(
                    "The character raises the axe over their head, hitting powerfully to the target, causing large phys dmg.")
                .Price(2)
                .RequirementSkill(SkillType.Axes, 35)
                .GrantsFeat(FeatType.Overhead);
        }
        private void MightySwings()
        {
            _builder.Create(PerkCategoryType.WeaponAxes, PerkType.MightySwings)
                .Name("Mighty Swings")

                .AddPerkLevel()
                .Description(
                    "The chracter swings the weapon in wild strikes. Causes large physical damage in several hits in a cone.")
                .Price(2)
                .RequirementSkill(SkillType.Axes, 50)
                .GrantsFeat(FeatType.MightySwings);
        }
    }
}
