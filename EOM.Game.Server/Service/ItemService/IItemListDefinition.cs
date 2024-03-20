using System.Collections.Generic;

namespace EOM.Game.Server.Service.ItemService
{
    public interface IItemListDefinition
    {
        public Dictionary<string, ItemDetail> BuildItems();
    }
}
