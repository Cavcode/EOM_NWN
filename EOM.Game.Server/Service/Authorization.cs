﻿using System;
using System.Linq;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service.DBService;
using AuthorizationLevel = EOM.Game.Server.Enumeration.AuthorizationLevel;

namespace EOM.Game.Server.Service
{
    public class Authorization
    {
        /// <summary>
        /// Retrieves the authorization level of a given player.
        /// </summary>
        /// <param name="player">The player whose authorization level we're checking</param>
        /// <returns>The authorization level (player, DM, or admin)</returns>
        public static AuthorizationLevel GetAuthorizationLevel(uint player)
        {
            var cdKey = GetPCPublicCDKey(player);

            // Check environment variable for super admin CD Key
            var superAdminCDKey = Environment.GetEnvironmentVariable("EOM_SUPER_ADMIN_CD_KEY");
            if (!string.IsNullOrWhiteSpace(superAdminCDKey))
            {
                if (cdKey == superAdminCDKey)
                    return AuthorizationLevel.Admin;
            }

            var query = new DBQuery<AuthorizedDM>()
                .AddFieldSearch(nameof(AuthorizedDM.CDKey), cdKey, false);
            var existing = DB.Search(query).FirstOrDefault();
            if (existing == null)
                return AuthorizationLevel.Player;

            return existing.Authorization;
        }
    }
}
