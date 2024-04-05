using System;
using EOM.Game.Server.Enumeration;

namespace EOM.Game.Server.Service.SkillService
{
    // Note: Corresponds to iprp_skill.2da
    // New additions or changes to this file should also be made to the 2DA.
    public enum SkillType
    {
        [Skill(SkillCategoryType.Invalid,
            "Invalid",
            0,
            false,
            "Unused in-game.",
            false,
            false)]
        Invalid = 0,

        [Skill(SkillCategoryType.Combat,
            "One-Handed",
            50,
            true,
            "Ability to use one-handed weapons like vibroblades, finesse vibroblades, and lightsabers.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        OneHanded = 1,

        [Skill(SkillCategoryType.Combat,
            "Two-Handed",
            50,
            true,
            "Ability to use heavy weapons like heavy vibroblades, polearms, and saberstaffs in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        TwoHanded = 2,

        [Skill(SkillCategoryType.Combat,
            "Martial Arts", 50,
            true,
            "Ability to fight using katars and staves in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        MartialArts = 3,

        [Skill(SkillCategoryType.Combat,
            "Ranged",
            50,
            true,
            "Ability to use ranged weapons like pistols, shurikens, and rifles in combat.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Ranged = 4,

        [Skill(SkillCategoryType.Combat,
            "Force",
            50,
            true,
            "Ability to use Force abilities.",
            true,
            false,
            CombatPointCategoryType.Utility,
            CharacterType.ForceSensitive)]
        Force = 5,

        [Skill(SkillCategoryType.Combat,
            "Armor",
            50,
            true,
            "Ability to effectively wear and defend against attacks with armor.",
            true,
            false)]
        Armor = 6,

        [Skill(SkillCategoryType.Utility,
            "Piloting",
            50,
            true,
            "Ability to pilot starships, follow navigation charts, and control starship systems.",
            true,
            false)]
        Piloting = 7,

        [Skill(SkillCategoryType.Utility,
            "First Aid",
            50,
            true,
            "Ability to treat bodily injuries in the field with healing kits and stim packs.",
            true,
            false)]
        FirstAid = 8,

        [Skill(SkillCategoryType.Crafting,
            "Smithery",
            50,
            true,
            "Ability to create weapons and armor like vibroblades, blasters, and helmets.",
            true,
            true)]
        Smithery = 9,

        [Skill(SkillCategoryType.Crafting,
            "Fabrication",
            50,
            true,
            "Ability to create base structures and furniture.",
            true,
            true)]
        Fabrication = 10,

        [Skill(SkillCategoryType.Crafting,
            "Gathering",
            50,
            true,
            "Ability to harvest raw materials and scavenge for supplies.",
            true,
            false)]
        Gathering = 11,

        [Skill(SkillCategoryType.Utility,
            "Leadership",
            50,
            true,
            "Ability to handle people, negotiate, and manage relations.",
            true,
            false)]
        Leadership = 12,

        [Skill(SkillCategoryType.Combat,
            "Beast Mastery",
            50,
            true,
            "Ability to tame wild animals, raise them, and train them.",
            true,
            false)]
        BeastMastery = 13,

        [Skill(SkillCategoryType.Jobs,
            "Bard",
            50,
            true,
            "Singer homie.",
            true,
            true)]
        Bard = 14,

        [Skill(SkillCategoryType.Jobs,
          "Dark Knight",
            50,
            true,
            "Saviour in a shroud of shadows.",
            true,
            true)]
        DarkKnight = 15,

        [Skill(SkillCategoryType.Jobs,
            "Black Mage",
            50,
            true,
            "Michael Bay's wet dream.",
            true,
            true)]
        BlackMage = 16,

        [Skill(SkillCategoryType.Jobs,
            "Gunbreaker",
            50,
            true,
            "Heedless hero of the people.",
            true,
            true)]
        Gunbreaker = 17,

        [Skill(SkillCategoryType.Jobs,
             "Ninja",
             50,
             true,
             "Sneaky boi.",
             true,
             true)]
        Ninja = 18,

        [Skill(SkillCategoryType.Jobs,
             "Gunslinger",
             50,
             true,
             "Do ya, punk?",
             true,
             true)]
        Gunslinger = 19,

        [Skill(SkillCategoryType.Jobs,
            "White Mage",
            50,
            true,
            "Caretakers of Life",
            true,
            true)]
        WhiteMage = 20,

        [Skill(SkillCategoryType.Jobs,
            "Monk",
            50,
            true,
            "A master of balance.",
            true,
            true)]
        Monk = 21,

        [Skill(SkillCategoryType.Jobs,
            "Reaper",
            50,
            true,
            "A champion of the Void.",
            true,
            true)]
        Reaper = 22,

        [Skill(SkillCategoryType.Jobs,
            "Paladin",
            50,
            true,
            "Saints of the Sword",
            true,
            true)]
        Paladin = 23,

        [Skill(SkillCategoryType.Jobs,
            "Summoner",
            50,
            true,
            "Yuna wannabes.",
            true,
            true)]
        Summoner = 24,

    }

    public class SkillAttribute : Attribute
    {
        public SkillCategoryType Category { get; set; }
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public bool IsActive { get; set; }
        public string Description { get; set; }
        public bool ContributesToSkillCap { get; set; }
        public bool IsShownInCraftMenu { get; set; }
        public CharacterType CharacterTypeRestriction { get; set; }

        public CombatPointCategoryType CombatPointCategory { get; set; } 

        public SkillAttribute(
            SkillCategoryType category,
            string name,
            int maxRank,
            bool isActive,
            string description,
            bool contributesToSkillCap,
            bool isShownInCraftMenu,
            CombatPointCategoryType combatPointCategory = CombatPointCategoryType.Exempt,
            CharacterType characterTypeRestriction = CharacterType.Invalid)
        {
            Category = category;
            Name = name;
            MaxRank = maxRank;
            IsActive = isActive;
            Description = description;
            ContributesToSkillCap = contributesToSkillCap;
            IsShownInCraftMenu = isShownInCraftMenu;
            CharacterTypeRestriction = characterTypeRestriction;
            CombatPointCategory = combatPointCategory;
        }
    }
}
