using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Extension;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.LogService;

namespace EOM.Game.Server.Feature
{
    public static class AreaConfiguration
    {
        /// <summary>
        /// When the module loads, load the tile magic configured on every area.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void ApplyAreaConfiguration()
        {
            for (var area = GetFirstArea(); GetIsObjectValid(area); area = GetNextArea())
            {
                ApplyTileMagic(area);
            }
        }

        /// <summary>
        /// Applies tile magic to a specific area, if configured.
        /// </summary>
        /// <param name="area">The area to apply to</param>
        private static void ApplyTileMagic(uint area)
        {
            var zPosition = GetLocalFloat(area, "TILE_MAGIC_Z");
            var tileTypeId = GetLocalInt(area, "TILE_MAGIC_TYPE");

            if (tileTypeId <= 0) return;

            try
            {
                var tile = (TileMagicType) tileTypeId;
                TileMagic.ChangeAreaGroundTiles(area, tile, zPosition);
            }
            catch (Exception ex)
            {
                Log.Write(LogGroup.Error, $"Area {GetName(area)} has an invalid tile magic type. Please fix the local variable. Exception: {ex.ToMessageAndCompleteStacktrace()}", true);
            }
        }
    }
}
