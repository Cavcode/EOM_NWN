using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.PlayerMarketService;

namespace EOM.Game.Server.Feature.GuiDefinition.Payload
{
    public class MarketPayload: GuiPayloadBase
    {
        public MarketRegionType RegionType { get; set; }

        public MarketPayload(MarketRegionType regionType)
        {
            RegionType = regionType;
        }
    }
}
