using System.Collections.Generic;

namespace EOM.Game.Server.Service.SpaceService
{
    public interface ISpaceObjectListDefinition
    {
        public Dictionary<string, SpaceObjectDetail> BuildSpaceObjects();
    }
}
