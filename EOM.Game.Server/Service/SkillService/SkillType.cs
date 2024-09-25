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
            "Axes",
            50,
            true,
            "Ability to use axes.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Axes = 1,

        [Skill(SkillCategoryType.Combat,
            "Gunblades",
            50,
            true,
            "Ability to use gunblades.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Gunblades = 2,


        [Skill(SkillCategoryType.Combat,
            "Martial Arts",
            50,
            true,
            "Ability to use FIST",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        MartialArts = 3,

        [Skill(SkillCategoryType.Combat,
            "Staves",
            50,
            true,
            "Ability to use staves.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        Staves = 4,


        [Skill(SkillCategoryType.Combat,
            "White Magic",
            50,
            true,
            "Ability to use White magic.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        WhiteMagic = 5,

        [Skill(SkillCategoryType.Combat,
            "Black Magic",
            50,
            true,
            "Ability to use Black magic.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        BlackMagic = 6,

        [Skill(SkillCategoryType.Combat,
              "Summoning",
              50,
              true,
              "Ability to summon Guardian Forces.",
              true,
              false,
              CombatPointCategoryType.Weapon)]
        Summoning = 7,

        [Skill(SkillCategoryType.Crafting,
            "Blacksmithing",
            50,
            true,
            "Ability to Blacksmithing.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Blacksmithing = 8,


        [Skill(SkillCategoryType.Crafting,
            "Armorsmith",
            50,
            true,
            "Ability to Armorsmith.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Armorsmith = 9,


        [Skill(SkillCategoryType.Crafting,
            "Cooking",
            50,
            true,
            "Ability to Cooking.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Cooking = 10,

        [Skill(SkillCategoryType.Crafting,
            "Fishing",
            50,
            true,
            "Ability to Fishing.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Fishing = 11,

        [Skill(SkillCategoryType.Crafting,
            "Survivalism",
            50,
            true,
            "Ability to Survive.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Survivalism = 12,

        [Skill(SkillCategoryType.Crafting,
            "First Aid",
            50,
            true,
            "Ability to First Aid.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        FirstAid = 13,


        [Skill(SkillCategoryType.Crafting,
            "Animal Handling",
            50,
            true,
            "Ability to Animal Handling.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        AnimalHandling = 14,

        [Skill(SkillCategoryType.Crafting,
            "Stealth",
            50,
            true,
            "Ability to Stealth.",
            true,
            true,
            CombatPointCategoryType.Exempt)]
        Stealth = 15,
        [Skill(SkillCategoryType.Combat,
            "Armor",
            50,
            true,
            "Ability to use armor.",
            true,
            true,
            CombatPointCategoryType.Utility)]
        Armor = 16,
        [Skill(SkillCategoryType.Utility,
            "Gathering",
            50,
            true,
            "Ability to gather resources.",
            true,
            true,
            CombatPointCategoryType.Utility)]
        Gathering = 17,
        [Skill(SkillCategoryType.Combat,
            "Sword and Shield",
            50,
            true,
            "Ability to use swords and shields.",
            true,
            false,
            CombatPointCategoryType.Weapon)]
        SwordAndShield = 18,
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
