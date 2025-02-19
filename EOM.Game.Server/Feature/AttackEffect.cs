using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using System.Reactive;
using EOM.Game.Server.Service.StatusEffectService;
using Discord;
using EOM.Game.Server.Core.NWScript.Enum.Item;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;

namespace EOM.Game.Server.Feature
{
    public static class AttackEffect
    {
        public static void PlayEffect(uint target)
        {
            var attacker = GetLastAttacker(target);
            var item = GetItemInSlot(InventorySlot.RightHand, attacker);
            var itemType = GetBaseItemType(item);

            GreviousWounds(target, attacker);
            Trigger(target, attacker);

        }

        private static void GreviousWounds(uint target, uint attacker)
        {
            var item = GetItemInSlot(InventorySlot.RightHand, attacker);
            var itemType = GetBaseItemType(item);



            if (GetHasFeat(FeatType.GrievingWoundsGunblade, attacker))
                if (itemType == BaseItem.Longsword)
                {
                    StatusEffect.Apply(attacker, target, StatusEffectType.Bleed, 12f);
                }
        }

        private static void Trigger(uint target, uint attacker)
        {
            if (StatusEffect.HasStatusEffect(attacker, StatusEffectType.Trigger))
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_sf_gunblade_bang), target); 
            }
        }
    }
}

