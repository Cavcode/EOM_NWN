using System.Collections.Generic;

namespace EOM.Game.Server.Service.BeastMasteryService
{
    public interface IBeastListDefinition
    {
        public Dictionary<BeastType, BeastDetail> Build();
    }
}
