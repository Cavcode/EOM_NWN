using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using System.Reactive;
using EOM.Game.Server.Service.StatusEffectService;
using Discord;
using EOM.Game.Server.Core.NWScript.Enum.Item;
using EOM.Game.Server.Entity;

namespace EOM.Game.Server.Feature
{
    public static class AttackEffect
    {
        public static void GreviousWounds(uint target)
        {
            var attacker = GetLastAttacker(target);
            var item = GetItemInSlot(InventorySlot.RightHand, attacker);
            var itemType = GetBaseItemType(item);

            

            if (GetHasFeat(FeatType.GrievingWoundsGunblade, attacker))
                if (itemType == BaseItem.Longsword)
                {
                    StatusEffect.Apply(attacker, target, StatusEffectType.Bleed, 12f);
                }

        }
    }
}

