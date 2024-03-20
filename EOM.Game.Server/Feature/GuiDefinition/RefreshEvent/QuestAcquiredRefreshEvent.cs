using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class QuestAcquiredRefreshEvent: IGuiRefreshEvent
    {
        public string QuestId { get; set; }

        public QuestAcquiredRefreshEvent(string questId)
        {
            QuestId = questId;
        }
    }
}
