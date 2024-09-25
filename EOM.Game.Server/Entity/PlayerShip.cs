using System.Collections.Generic;

namespace EOM.Game.Server.Entity
{
    public class PlayerShip: EntityBase
    {
        public PlayerShip()
        {
            PlayerHotBars = new Dictionary<string, string>();
        }

        [Indexed]
        public string OwnerPlayerId { get; set; }
        [Indexed]
        public string PropertyId { get; set; }
        public string SerializedItem { get; set; }
        public Dictionary<string, string> PlayerHotBars { get; set; }
    }
}
