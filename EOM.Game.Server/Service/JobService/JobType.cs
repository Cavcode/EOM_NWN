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

        [Job("Dark Knight",
            5,
            "A savior shrouded in shadows.")]
        DarkKnight = 2,

        [Job("Black Mage",
         5,
         "Michael Bay's wet dream.")]
        BlackMage = 3,

        [Job("Gunbreaker",
         5,
         "Heedless hero of the people.")]
        Gunbreaker = 4,

        [Job("Ninja",
         5,
         "Sneaky boi.")]
        Ninja = 5,

        [Job("Gunslinger",
         5,
         "Do ya, punk?")]
        Gunslinger = 6,

        [Job("Warrior",
         5,
         "A frontline of Heroes.")]
        Warrior = 7,

        [Job("White Mage",
         5,
         "Caretakers of Life")]
        WhiteMage = 8,

        [Job("Monk",
         5,
         "A master of balance.")]
        Monk = 9,

        [Job("Reaper",
         5,
         "A champion of the Void.")]
        Reaper = 10,

        [Job("Paladin",
         5,
         "Saints of the Sword")]
        Paladin = 11,

        [Job("Summoner",
         5,
         "Yuna wannabes.")]
        Summoner = 12,
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
