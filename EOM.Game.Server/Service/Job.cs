using System.Collections.Generic;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.JobService;
using EOM.Game.Server.Service.StatusEffectService;
using Player = EOM.Game.Server.Entity.Player;
using System;
using EOM.Game.Server.Extension;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Service
{
    public static partial class Job
    {
        /// <summary>
        /// This is the maximum number of job points a single character can have at any time.
        /// </summary>
        public const int JobCap = 5;

        // All jobs
        private static readonly Dictionary<JobType, JobAttribute> _allJobs = new();

        /// <summary>
        /// Retrieves a list of all jobs.
        /// </summary>
        /// <returns>A list of all jobs.</returns>
        public static Dictionary<JobType, JobAttribute> GetAllJobs()
        {
            return _allJobs.ToDictionary(x => x.Key, y => y.Value);
        }


        /// <summary>
        /// When the module loads, jobs and categories are organized into dictionaries for quick look-ups later on.
        /// </summary>
        [NWNEventHandler("mod_cache")]
        public static void CacheData()
        {
            // Organize jobs to make later reads quicker.
            var jobs = Enum.GetValues(typeof(JobType)).Cast<JobType>();
            foreach (var jobType in jobs)
            {
                var jobsDetail = jobType.GetAttribute<JobType, JobAttribute>();

                // Add to the jobs cache
                _allJobs[jobType] = jobsDetail;
            }

            EventsPlugin.SignalEvent("EOM_CACHE_jobs_LOADED", GetModule());
            Console.WriteLine($"Loaded {_allJobs.Count} jobs.");
        }

        /// <summary>
        /// Gives the player an job point which can be distributed to the job of their choice
        /// from the character menu. One point is earned per 70 jobs ranks
        /// </summary>
        /// <param name="player">The player to receive the JP.</param>
        /// <param name="dbPlayer">The database entity.</param>
        private static void ApplyJobPoint(uint player, Player dbPlayer)
        {
            // Total JP have been earned (350SP = 35AP)
            if (dbPlayer.TotalJPAcquired >= Job.JobCap / 10) return;

            if (dbPlayer.TotalJPAcquired % 10 == 0)
            {
                dbPlayer.UnallocatedJP++;
                dbPlayer.TotalJPAcquired++;

                SendMessageToPC(player, ColorToken.Green("You acquired 1 job point!"));
            }
        }

    }
}
