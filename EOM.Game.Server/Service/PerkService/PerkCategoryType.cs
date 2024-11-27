using System;

namespace EOM.Game.Server.Service.PerkService
{
    public enum PerkCategoryType
    {
        [PerkCategory("Invalid", false)]
        Invalid = 0,

        [PerkCategory("One Handed - General", false)]
        OneHandedGeneral = 1,

        [PerkCategory("One Handed - Vibroblade", false)]
        OneHandedVibroblade = 2,

        [PerkCategory("One Handed - Finesse Vibroblade", false)]
        OneHandedFinesseVibroblade = 3,

        [PerkCategory("One Handed - Lightsaber", false)]
        OneHandedLightsaber = 4,

        [PerkCategory("Two Handed - General", false)]
        TwoHandedGeneral = 5,

        [PerkCategory("Two Handed - Heavy Vibroblade", false)]
        TwoHandedHeavyVibroblade = 6,

        [PerkCategory("Two Handed - Polearm", false)]
        TwoHandedPolearm = 7,

        [PerkCategory("Two Handed - Twin Blade", false)]
        TwoHandedTwinBlade = 8,

        [PerkCategory("Two Handed - Saberstaff", false)]
        TwoHandedSaberstaff = 9,

        [PerkCategory("Martial Arts - General", false)]
        MartialArtsGeneral = 10,

        [PerkCategory("Martial Arts - Katars", false)]
        MartialArtsKatars = 11,

        [PerkCategory("Martial Arts - Staff", false)]
        MartialArtsStaff = 12,

        [PerkCategory("Ranged - General", false)]
        RangedGeneral = 13,

        [PerkCategory("Ranged - Pistol", false)]
        RangedPistol = 14,

        [PerkCategory("Ranged - Throwing", false)]
        RangedThrowing = 15,

        [PerkCategory("Ranged - Cannon", false)]
        RangedCannon = 16,

        [PerkCategory("Ranged - Rifle", false)]
        RangedRifle = 17,

        [PerkCategory("Force - Universal", false)]
        ForceUniversal = 18,

        [PerkCategory("Armor - General", false)]
        ArmorGeneral = 19,

        [PerkCategory("Armor - Heavy", false)]
        ArmorHeavy = 20,

        [PerkCategory("Armor - Light", false)]
        ArmorLight = 21,

        [PerkCategory("Piloting", false)]
        Piloting = 22,

        [PerkCategory("First Aid", false)]
        FirstAid = 23,

        [PerkCategory("Smithery", false)]
        Smithery = 24,

        [PerkCategory("Cybertech", false)]
        Cybertech = 25,

        [PerkCategory("Fabrication", false)]
        Fabrication = 26,

        [PerkCategory("Gathering", false)]
        Gathering = 27,

        [PerkCategory("Leadership", false)]
        Leadership = 28,

        [PerkCategory("Force - Light", false)]
        ForceLight = 29,

        [PerkCategory("Force - Dark", false)]
        ForceDark = 30,

        [PerkCategory("General Perks", false)]
        General = 31,

        [PerkCategory("Agriculture", false)]
        Agriculture = 32,

        [PerkCategory("Engineering", false)]
        Engineering = 33,

        [PerkCategory("Devices", false)]
        Devices = 34,

        [PerkCategory("One Handed - Shield", false)]
        OneHandedShield = 35,

        [PerkCategory("Beast Mastery - Training", false)]
        BeastMasteryTraining = 36,

        [PerkCategory("Beast Mastery - Incubation", false)]
        BeastMasteryIncubation = 37,

        [PerkCategory("Beast - General", false)]
        BeastGeneral = 38,

        [PerkCategory("Beast - Damage", false)]
        BeastDamage = 39,

        [PerkCategory("Beast - Tank", false)]
        BeastTank = 40,

        [PerkCategory("Beast - Balanced", false)]
        BeastBalanced = 41,

        [PerkCategory("Beast - Bruiser", false)]
        BeastBruiser = 42,

        [PerkCategory("Beast - Evasion", false)]
        BeastEvasion = 43,

        [PerkCategory("Beast - Force", false)]
        BeastForce = 44,

        [PerkCategory("Job - Bard", false)]
        JobBard = 45,

        [PerkCategory("Job - Dark Knight", false)]
        JobDarkKnight = 46,

        [PerkCategory("Job - Black Mage", true)]
        JobBlackMage = 47,

        [PerkCategory("Job - Gunbreaker", true)]
        JobGunbreaker = 48,

        [PerkCategory("Job - Gunslinger", false)]
        JobGunslinger = 49,


        [PerkCategory("Job - Monk", true)]
        JobMonk = 50,

        [PerkCategory("Job - Ninja", false)]
        JobNinja = 51,

        [PerkCategory("Job - Warrior", true)]
        JobWarrior = 52,

        [PerkCategory("Job - White Mage", true)]
        JobWhiteMage = 53,

        [PerkCategory("Job - Paladin", false)]
        JobPaladin = 54,

        [PerkCategory("Job - Reaper", false)]
        JobReaper = 55,

        [PerkCategory("Job - Summoner", true)]
        JobSummoner = 56,
        [PerkCategory("Weapon - Axes", true)]
        WeaponAxes = 57,
    }

    public class PerkCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public PerkCategoryAttribute(string name, bool isActive)
        {
            Name = name;
            IsActive = isActive;
        }
    }
}
