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
    public class GunbladePerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            SwiftSlashes();
            WeaponExpertiseGunblades();
            WeaponExpertiseGunbladesII();
            ImprovedCriticalAxes();
            GrievingWoundsGunblades();
            Trigger();
            SonicSlash();
            AethericalAmmo();
            AethericalBulletSlug();

            return _builder.Build();
        }

        private void SwiftSlashes()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.SwiftSlashes)
                .Name("Swift Slashes")

                .AddPerkLevel()
                .Description(
                    "Grants the character one extra attack with a penalty bonus to hit")
                .Price(3)
                .RequirementSkill(SkillType.Gunblades, 15)
                .GrantsFeat(FeatType.SwiftSlashes);

        }

        private void WeaponExpertiseGunblades()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.WeaponExpertiseGunblade)
                .Name("Weapon Expertise - Gunblades")

                .AddPerkLevel()
                .Description(
                    "Grants the character an excelent knowledge on the use of the Gunblade, giving the wielder a bonus to attack")
                .Price(4)
                .RequirementSkill(SkillType.Gunblades, 30)
                .GrantsFeat(FeatType.WeaponExpertiseGunblade);
        }
        private void WeaponExpertiseGunbladesII()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.WeaponExpertiseGunbladeII)
                .Name("Weapon Expertise II - Gunblades")

                .AddPerkLevel()
                .Description(
                    "Grants the character an excelent knowledge on the use of the Gunblade, giving the wielder a further bonus")
                .Price(4)
                .RequirementSkill(SkillType.Gunblades, 40)
                .GrantsFeat(FeatType.WeaponExpertiseGunbladeII);
        }
        private void ImprovedCriticalAxes()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.ImprovedCriticalGunblade)
                .Name("Improved Critical - Gunblades")

                .AddPerkLevel()
                .Description(
                    "Grants the feature Improved Critical to Gunblades.")
                .Price(5)
                .RequirementSkill(SkillType.Gunblades, 45)
                .GrantsFeat(FeatType.ImprovedCriticalGunblade);
        }
        private void GrievingWoundsGunblades()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.GrievingWoundsGunblade)
                .Name("Grieving Wounds - Gunblades")

                .AddPerkLevel()
                .Description(
                    "Grants the character the ability to apply a DoT to enemies everytime they get hit.")
                .Price(5)
                .RequirementSkill(SkillType.Gunblades, 50)
                .GrantsFeat(FeatType.GrievingWoundsGunblade);
        }
        private void Trigger()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.Trigger)
                .Name("Trigger")

                .AddPerkLevel()
                .Description(
                    "The standard use of trigger of a gunblade. Adds Fire damage to each hit for x rounds")
                .Price(2)
                .RequirementSkill(SkillType.Gunblades, 10)
                .GrantsFeat(FeatType.Trigger);
        }
        private void SonicSlash()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.SonicSlash)
                .Name("Sonic Slash")

                .AddPerkLevel()
                .Description(
                    "The user slashes with aether using the Trigger. Deals minor Force ranged damage to the target")
                .Price(2)
                .RequirementSkill(SkillType.Gunblades, 25)
                .GrantsFeat(FeatType.SonicSlash);
        }
        private void AethericalAmmo()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.AethericalAmmo)
                .Name("Aetherical Ammo")

                .AddPerkLevel()
                .Description(
                    "The user learns how to infuse the gunblades' bullets with aether. The user can change the elemental type of Trigger")
                .Price(2)
                .RequirementSkill(SkillType.Gunblades, 35)
                .GrantsFeat(FeatType.AethericalAmmo);
        }
        private void AethericalBulletSlug()
        {
            _builder.Create(PerkCategoryType.WeaponGunblades, PerkType.AethericalBulletSlug)
                .Name("Aetherical Bullet - Slug")

                .AddPerkLevel()
                .Description(
                    "The user uses a special type of bullet for one hit, deal a great deal of Force Damage, which also causes Slow status")
                .Price(2)
                .RequirementSkill(SkillType.Gunblades, 50)
                .GrantsFeat(FeatType.AethericalBulletSlug);
        }
    }
}
