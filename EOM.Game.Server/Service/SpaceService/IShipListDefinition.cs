using System.Collections.Generic;

namespace EOM.Game.Server.Service.SpaceService
{
    public interface IShipListDefinition
    {
        public Dictionary<string, ShipDetail> BuildShips();
    }
}
