using System.Collections.Generic;

namespace EOM.Game.Server.Service.SnippetService
{
    public interface ISnippetListDefinition
    {
        public Dictionary<string, SnippetDetail> BuildSnippets();
    }
}
