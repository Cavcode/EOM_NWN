using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.KeyItemService;

namespace EOM.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class KeyItemReceivedRefreshEvent: IGuiRefreshEvent
    {
        public KeyItemType Type { get; set; }

        public KeyItemReceivedRefreshEvent(KeyItemType type)
        {
            Type = type;
        }
    }
}
