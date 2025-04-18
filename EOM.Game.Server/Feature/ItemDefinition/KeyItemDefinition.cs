﻿using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.ItemService;
using EOM.Game.Server.Service.KeyItemService;
using EOM.Game.Server.Service.LogService;

namespace EOM.Game.Server.Feature.ItemDefinition
{
    public class KeyItemDefinition: IItemListDefinition
    {
        private readonly ItemBuilder _builder = new ItemBuilder();
        public Dictionary<string, ItemDetail> BuildItems()
        {
            KeyItem();

            return _builder.Build();
        }

        private void KeyItem()
        {
            _builder.Create("KEY_ITEM")
                .Delay(1f)
                .ReducesItemCharge()
                .ValidationAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var keyItemId = GetLocalInt(item, "KEY_ITEM_ID");

                    if (keyItemId <= 0)
                    {
                        Log.Write(LogGroup.Error, $"KEY_ITEM_ID for item '{GetName(item)}' is not set properly.");
                        return "KEY_ITEM_ID is not configured properly on the item. Notify an admin.";
                    }

                    try
                    {
                        var keyItemType = (KeyItemType)keyItemId;

                        if (Service.KeyItem.HasKeyItem(user, keyItemType))
                        {
                            return $"You have already acquired this key item.";
                        }

                    }
                    catch
                    {
                        Log.Write(LogGroup.Error, $"KEY_ITEM_ID '{keyItemId}' for item '{GetName(item)}' is not assigned to a valid KeyItemType.");
                        return "KEY_ITEM_ID is not configured properly on the item. Notify an admin.";
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var area = GetArea(user);
                    var keyItemId = GetLocalInt(item, "KEY_ITEM_ID");
                    var keyItemType = (KeyItemType)keyItemId;
                    Service.KeyItem.GiveKeyItem(user, keyItemType);

                    // If the player is within an area associated with this map, instantly explore it and ensure the minimap can be toggled.
                    if (GetLocalInt(area, "MAP_KEY_ITEM_ID") == keyItemId)
                    {
                        ExploreAreaForPlayer(area, user);
                        SetGuiPanelDisabled(user, GuiPanel.Minimap, false);
                    }
                });
        }
    }
}
