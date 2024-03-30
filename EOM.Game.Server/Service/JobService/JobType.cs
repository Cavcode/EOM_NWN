using System;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Service.JobService
{
    // Note: Corresponds to iprp_Job.2da
    // New additions or changes to this file should also be made to the 2DA.
    public enum JobType
    {
        [Job(JobCategoryType.Invalid,
            "Invalid",
            0,
            false,
            "Unused in-game.",
            false,
            false)]
        Invalid = 0,

        [Job(JobCategoryType.Combat,
            "One-Handed",
            50,
            true,
            "Ability to use one-handed weapons like vibroblades, finesse vibroblades, and lightsabers.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        OneHanded = 1,

        [Job(JobCategoryType.Combat,
            "Two-Handed",
            50,
            true,
            "Ability to use heavy weapons like heavy vibroblades, polearms, and saberstaffs in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        TwoHanded = 2,

        [Job(JobCategoryType.Combat,
            "Martial Arts", 50,
            true,
            "Ability to fight using katars and staves in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        MartialArts = 3,

        [Job(JobCategoryType.Combat,
            "Ranged",
            50,
            true,
            "Ability to use ranged weapons like pistols, shurikens, and rifles in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Ranged = 4,

        [Job(JobCategoryType.Combat,
            "Force",
            50,
            true,
            "Ability to use Force abilities.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.ForceSensitive)]
        Force = 5,

        [Job(JobCategoryType.Combat,
            "Armor",
            50,
            true,
            "Ability to effectively wear and defend against attacks with armor.",
            true,
            false)]
        Armor = 6,

        [Job(JobCategoryType.Utility,
            "Piloting",
            50,
            true,
            "Ability to pilot starships, follow navigation charts, and control starship systems.",
            true,
            false)]
        Piloting = 7,

        [Job(JobCategoryType.Utility,
            "First Aid",
            50,
            true,
            "Ability to treat bodily injuries in the field with healing kits and stim packs.",
            true,
            false)]
        FirstAid = 8,

        [Job(JobCategoryType.Crafting,
            "Smithery",
            50,
            true,
            "Ability to create weapons and armor like vibroblades, blasters, and helmets.",
            true,
            true)]
        Smithery = 9,

        [Job(JobCategoryType.Crafting,
            "Fabrication",
            50,
            true,
            "Ability to create base structures and furniture.",
            true,
            true)]
        Fabrication = 10,

        [Job(JobCategoryType.Crafting,
            "Gathering",
            50,
            true,
            "Ability to harvest raw materials and scavenge for supplies.",
            true,
            false)]
        Gathering = 11,

        [Job(JobCategoryType.Utility,
            "Leadership",
            50,
            true,
            "Ability to handle people, negotiate, and manage relations.",
            true,
            false)]
        Leadership = 12,

        [Job(JobCategoryType.Combat,
            "Beast Mastery",
            50,
            true,
            "Ability to tame wild animals, raise them, and train them.",
            true,
            false)]
        BeastMastery = 13,

        [Job(JobCategoryType.Languages,
            "Mirialan",
            20,
            true,
            "Ability to speak the Mirialan language.",
            false,
            false)]
        Mirialan = 14,

        [Job(JobCategoryType.Languages,
            "Bothese",
            20,
            true,
            "Ability to speak the Bothese language.",
            false,
            false)]
        Bothese = 15,

        [Job(JobCategoryType.Languages,
            "Cheunh",
            20,
            true,
            "Ability to speak the Cheunh language.",
            false,
            false)]
        Cheunh = 16,


        [Job(JobCategoryType.Languages,
            "Zabraki",
            20,
            true,
            "Ability to speak the Zabraki language.",
            false,
            false)]
        Zabraki = 17,

        [Job(JobCategoryType.Languages,
            "Twi'leki (Ryl)",
            20,
            true,
            "Ability to speak the Twi'leki (Ryl) language.",
            false,
            false)]
        Twileki = 18,

        [Job(JobCategoryType.Languages,
            "Catharese", 20,
            true,
            "Ability to speak the Catharese language.",
            false,
            false)]
        Catharese = 19,

        [Job(JobCategoryType.Languages,
            "Dosh",
            20,
            true,
            "Ability to speak the Dosh language.",
            false,
            false)]
        Dosh = 20,

        [Job(JobCategoryType.Languages,
            "Shyriiwook",
            20,
            true,
            "Ability to speak the Shyriiwook (Wookieespeak) language.",
            false,
            false)]
        Shyriiwook = 21,

        [Job(JobCategoryType.Languages,
            "Droidspeak",
            20,
            true,
            "Ability to speak the Droidspeak language.",
            false,
            false)]
        Droidspeak = 22,

        [Job(JobCategoryType.Languages,
            "Basic",
            20,
            true,
            "Ability to speak the Galactic Basic language.",
            false,
            false)]
        Basic = 23,

        [Job(JobCategoryType.Languages,
            "Mandoa",
            20,
            true,
            "Ability to speak the Mandoa language.",
            false,
            false)]
        Mandoa = 24,

        [Job(JobCategoryType.Languages,
            "Huttese",
            20,
            true,
            "Ability to speak the Huttese language.",
            false,
            false)]
        Huttese = 25,

        [Job(JobCategoryType.Languages,
            "Mon Calamarian",
            20,
            true,
            "Ability to speak the Mon Calamarian language.",
            false,
            false)]
        MonCalamarian = 26,

        [Job(JobCategoryType.Languages,
            "Ugnaught",
            20,
            true,
            "Ability to speak the Ugnaught language.",
            false,
            false)]
        Ugnaught = 27,

        [Job(JobCategoryType.Languages,
            "Rodese",
            20,
            true,
            "Ability to speak the Rodese language.",
            false,
            false)]
        Rodese = 28,

        [Job(JobCategoryType.Languages,
            "Togruti",
            20,
            true,
            "Ability to speak the Togruti language.",
            false,
            false)]
        Togruti = 29,

        [Job(JobCategoryType.Languages,
            "Kel Dor",
            20,
            true,
            "Ability to speak the Kel Dor language.",
            false,
            false)]
        KelDor = 30,

        [Job(JobCategoryType.Crafting,
            "Agriculture",
            50,
            true,
            "Ability to farm, fish, and cook.",
            true,
            true)]
        Agriculture = 31,

        [Job(JobCategoryType.Crafting,
            "Engineering",
            50,
            true,
            "Ability to create starships, modules, droids, and other electronic & mechanical items.",
            true,
            true)]
        Engineering = 32,

        [Job(JobCategoryType.Combat,
            "Devices",
            50,
            true,
            "Ability to use grenades, bombs, and other electronics.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.Standard)]
        Devices = 33,

        [Job(JobCategoryType.Languages,
            "Nautila",
            20,
            true,
            "Ability to speak the Nautila language.",
            false,
            false)]
            Nautila = 34,
    }

    public class JobAttribute : Attribute
    {
        public JobCategoryType Category { get; set; }
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool ContributesToJobCap { get; set; }
        public bool IsShownInCraftMenu { get; set; }
        public CharacterType CharacterTypeRestriction { get; set; }

        public CombatPointCategoryType CombatPointCategory { get; set; } 

        public JobAttribute(
            JobCategoryType category,
            string name,
            int maxRank,
            bool isActive,
            string description,
            bool contributesToJobCap,
            bool isShownInCraftMenu,
            CombatPointCategoryType combatPointCategory = CombatPointCategoryType.Exempt,
            CharacterType characterTypeRestriction = CharacterType.Invalid)
        {
            Category = category;
            Name = name;
            MaxRank = maxRank;
            IsActive = isActive;
            Description = description;
            ContributesToJobCap = contributesToJobCap;
            IsShownInCraftMenu = isShownInCraftMenu;
            CharacterTypeRestriction = characterTypeRestriction;
            CombatPointCategory = combatPointCategory;
        }
    }
}
