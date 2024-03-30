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
        Invalid = 0
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
