using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    internal class PlayerStatusRefreshEvent: IGuiRefreshEvent
    {
        internal enum StatType
        {
            HP = 1,
            MP = 2,

        }

        public StatType Type { get; set; }

        public PlayerStatusRefreshEvent(StatType type)
        {
            Type = type;
        }
    }
}
