using System;

namespace EOM.Game.Server.Service.SkillService
{
    public enum SkillCategoryType
    {
        [SkillCategory("Invalid", false, 0)]
        Invalid = 0,
        [SkillCategory("Weapons", true, 1)]
        Combat = 1,
        [SkillCategory("Crafting", true, 4)]
        Crafting = 2,
        [SkillCategory("Utility", true, 6)]
        Utility = 3,
        [SkillCategory("Magic", true, 8)]
        Roleplay = 4,
    }

    public class SkillCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }

        public SkillCategoryAttribute(string name, bool isActive, int sequence)
        {
            Name = name;
            IsActive = isActive;
            Sequence = sequence;
        }
    }
}