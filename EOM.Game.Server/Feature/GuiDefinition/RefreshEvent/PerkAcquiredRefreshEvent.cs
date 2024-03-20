using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.PerkService;

namespace EOM.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class PerkAcquiredRefreshEvent: IGuiRefreshEvent
    {
        public PerkType Type { get; set; }

        public PerkAcquiredRefreshEvent(PerkType type)
        {
            Type = type;
        }
    }
}
