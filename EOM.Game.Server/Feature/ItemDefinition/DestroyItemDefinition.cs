using System.Collections.Generic;
using EOM.Game.Server.Feature.DialogDefinition;
using EOM.Game.Server.Service.ItemService;
using Dialog = EOM.Game.Server.Service.Dialog;

namespace EOM.Game.Server.Feature.ItemDefinition
{
    public class DestroyItemDefinition: IItemListDefinition
    {
        private readonly ItemBuilder _builder = new ItemBuilder();

        public Dictionary<string, ItemDetail> BuildItems()
        {
            _builder.Create("player_guide", "survival_knife")
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    SetLocalObject(user, "DESTROY_ITEM", item);
                    Dialog.StartConversation(user, user, nameof(DestroyItemDialog));
                });

            return _builder.Build();
        }
    }
}
