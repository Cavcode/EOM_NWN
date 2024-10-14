using System;
using EOM.Game.Server.Enumeration;

namespace EOM.Game.Server.Service.JobService
{
    public enum JobType
    {
        [Job("Warrior",
            Job.JobCap,
            "Cool guys with big axes.")]
         Warrior = 1,

        [Job("Monk",
            Job.JobCap,
            "Cast fist")]
        Monk = 2,

        [Job("White Mage",
            Job.JobCap,
            "Cast heal")]
        WhiteMage = 3,

        [Job("Black Mage",
            Job.JobCap,
            "Cast fire")]
        BlackMage = 4,

        [Job("Gunbreaker",
            Job.JobCap,
            "Whatever")]
        Gunbreaker = 5,
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
