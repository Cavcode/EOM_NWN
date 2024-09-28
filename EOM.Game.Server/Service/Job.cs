using System.Collections.Generic;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.JobService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service.StatusEffectService;
using Player = EOM.Game.Server.Entity.Player;

namespace EOM.Game.Server.Service
{
    public static partial class Job
    {
        /// <summary>
        /// This is the maximum number of job points a single character can have at any time.
        /// </summary>
        public const int JobCap = 5;


        /// <summary>
        /// Gives the player an job point which can be distributed to the job of their choice
        /// from the character menu. One point is earned per 70 skill ranks
        /// </summary>
        /// <param name="player">The player to receive the JP.</param>
        /// <param name="dbPlayer">The database entity.</param>
        private static void ApplyJobPoint(uint player, Player dbPlayer)
        {
            // Total JP have been earned (350SP = 35AP)
            if (dbPlayer.TotalJPAcquired >= Skill.SkillCap / 10) return;

            if (dbPlayer.TotalJPAcquired % 10 == 0)
            {
                dbPlayer.UnallocatedJP++;
                dbPlayer.TotalJPAcquired++;

                SendMessageToPC(player, ColorToken.Green("You acquired 1 job point!"));
            }
        }

    }
}
