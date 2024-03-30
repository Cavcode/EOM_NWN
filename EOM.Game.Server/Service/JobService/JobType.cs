using System;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Service.JobService
{
    // Note: Corresponds to iprp_Job.2da
    // New additions or changes to this file should also be made to the 2DA.
    public enum JobType
    {
        [Job("Invalid",
            0,
            "Unused in-game.")]
        Invalid = 0,
        [Job("Bard",
            5,
            "Boi that sings.")]
        Bard = 1,
    }

    public class JobAttribute : Attribute
    {
        public string Name { get; set; }
        public int MaxRank { get; set; }
        public string Description { get; set; }

        public JobAttribute(
            string name,
            int maxRank,
            string description)
        {
            Name = name;
            MaxRank = maxRank;
            Description = description;
        }
    }
}
