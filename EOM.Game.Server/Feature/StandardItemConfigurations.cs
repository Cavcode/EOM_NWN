﻿using System;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.Bioware;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Item;
using EOM.Game.Server.Core.NWScript.Enum.Item.Property;
using EOM.Game.Server.Extension;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.PlayerMarketService;
using EOM.Game.Server.Service.PropertyService;

namespace EOM.Game.Server.Feature
{
    public static class StandardItemConfigurations
    {
        /// <summary>
        /// These are valid item types which will receive the OnHitCastSpell item property.
        /// Anything outside this set will not have this item property added automatically.
        /// </summary>
        private static readonly BaseItem[] _validItemTypes = {
                    BaseItem.Armor,
                    BaseItem.Arrow,
                    BaseItem.BastardSword,
                    BaseItem.BattleAxe,
                    BaseItem.Belt,
                    BaseItem.Bolt,
                    BaseItem.Boots,
                    BaseItem.Bracer,
                    BaseItem.Bullet,
                    BaseItem.Cloak,
                    BaseItem.Club,
                    BaseItem.Dagger,
                    BaseItem.Dart,
                    BaseItem.DireMace,
                    BaseItem.DoubleAxe,
                    BaseItem.DwarvenWarAxe,
                    BaseItem.Gloves,
                    BaseItem.GreatAxe,
                    BaseItem.GreatSword,
                    BaseItem.Grenade,
                    BaseItem.Halberd,
                    BaseItem.HandAxe,
                    BaseItem.Cannon,
                    BaseItem.HeavyFlail,
                    BaseItem.Helmet,
                    BaseItem.Kama,
                    BaseItem.Katana,
                    BaseItem.Kukri,
                    BaseItem.LargeShield,
                    BaseItem.Rifle,
                    BaseItem.LightFlail,
                    BaseItem.LightHammer,
                    BaseItem.LightMace,
                    BaseItem.Longbow,
                    BaseItem.Longsword,
                    BaseItem.MorningStar,
                    BaseItem.QuarterStaff,
                    BaseItem.Rapier,
                    BaseItem.Scimitar,
                    BaseItem.Scythe,
                    BaseItem.Pistol,
                    BaseItem.ShortSpear,
                    BaseItem.ShortSword,
                    BaseItem.Shuriken,
                    BaseItem.Sickle,
                    BaseItem.Sling,
                    BaseItem.SmallShield,
                    BaseItem.ThrowingAxe,
                    BaseItem.TowerShield,
                    BaseItem.Trident,
                    BaseItem.TwoBladedSword,
                    BaseItem.WarHammer,
                    BaseItem.Whip,
                    BaseItem.Katar,
                    BaseItem.Lightsaber,
                    BaseItem.Saberstaff,
                    BaseItem.Electroblade,
                    BaseItem.TwinElectroBlade
        };

        /// <summary>
        /// Whenever an item with an approved base item type is equipped, the OnHitCastSpell item property will be added to it.
        /// Arrows, bolts, and bullets will also receive this item property if they're equipped.
        /// </summary>
        [NWNEventHandler("mod_equip")]
        public static void AddOnHitProperty()
        {
            var player = GetPCItemLastEquippedBy();
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var item = GetPCItemLastEquipped();

            var baseItemType = GetBaseItemType(item);
            if (!_validItemTypes.Contains(baseItemType)) return;

            var arrows = GetItemInSlot(InventorySlot.Arrows, player);
            var bolts = GetItemInSlot(InventorySlot.Bolts, player);
            var bullets = GetItemInSlot(InventorySlot.Bullets, player);

            ApplyOnHitProperty(item);
            ApplyOnHitProperty(arrows);
            ApplyOnHitProperty(bolts);
            ApplyOnHitProperty(bullets);
        }

        [NWNEventHandler("mod_equip")]
        public static void SwitchPhenoEquip()
        {
            var player = GetPCItemLastEquippedBy();
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var itemPlayer = GetPCItemLastEquipped();

            var baseItemType = GetBaseItemType(itemPlayer);
            if (!_validItemTypes.Contains(baseItemType)) return;

            var items = Enum.GetValues(typeof(BaseItem)).Cast<BaseItem>();
            foreach (var item in items)
            {
                if (item == baseItemType)
                {
                    var attribute = item.GetAttribute<BaseItem, ItemAttribute>();
                    SetPhenoType(attribute.PhenoType, player);
                    Stat.ApplyAttacksPerRound(player, GetPCItemLastEquipped());
                }
            }
        }

        [NWNEventHandler("mod_unequip")]
        public static void SwitchPhenoUnequip()
        {
            var player = GetPCItemLastEquippedBy();
            if (!GetIsPC(player) || GetIsDM(player)) return;
            var itemPlayer = GetPCItemLastEquipped();

            SetPhenoType(PhenoType.Normal,player);
        }

        /// <summary>
        /// Applies the OnHitCastSpell item property to a specified item.
        /// </summary>
        /// <param name="item"></param>
        private static void ApplyOnHitProperty(uint item)
        {
            for(var ip = GetFirstItemProperty(item); GetIsItemPropertyValid(ip); ip = GetNextItemProperty(item))
            {
                if (GetItemPropertyType(ip) == ItemPropertyType.OnHitCastSpell)
                {
                    if (GetItemPropertySubType(ip) == (int)OnHitCastSpell.ONHIT_UNIQUEPOWER)
                    {
                        return;
                    }
                }
            }

            // No item property found. Add it to the item.
            BiowareXP2.IPSafeAddItemProperty(item, ItemPropertyOnHitCastSpell(OnHitCastSpellType.ONHIT_UNIQUEPOWER, 40), 0.0f, AddItemPropertyPolicy.ReplaceExisting, false, false);
        }
    }
}
