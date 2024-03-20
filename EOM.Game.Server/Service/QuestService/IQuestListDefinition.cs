using System.Collections.Generic;

namespace EOM.Game.Server.Service.QuestService
{
    public interface IQuestListDefinition
    {
        public Dictionary<string, QuestDetail> BuildQuests();
    }
}
