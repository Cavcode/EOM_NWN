using System;

namespace EOM.Game.Server.Service.JobService
{
    public enum JobCategoryType
    {
        [JobCategory("Invalid", false, 0)]
        Invalid = 0,
        [JobCategory("Combat", true, 1)]
        Combat = 1,
        [JobCategory("Crafting", true, 4)]
        Crafting = 2,
        [JobCategory("Utility", true, 6)]
        Utility = 3,
        [JobCategory("Languages", true, 8)]
        Languages = 4,
    }

    public class JobCategoryAttribute : Attribute
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public int Sequence { get; set; }

        public JobCategoryAttribute(string name, bool isActive, int sequence)
        {
            Name = name;
            IsActive = isActive;
            Sequence = sequence;
        }
    }
}