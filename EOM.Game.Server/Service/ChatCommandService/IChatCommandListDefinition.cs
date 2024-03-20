using System.Collections.Generic;

namespace EOM.Game.Server.Service.ChatCommandService
{
    public interface IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands();
    }
}
