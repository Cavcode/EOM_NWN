using System;
using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Associate;
using EOM.Game.Server.Core.NWScript.Enum.Item;
using EOM.Game.Server.Core.NWScript.Enum.Item.Property;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.CurrencyService;
using EOM.Game.Server.Service.ItemService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service.StatusEffectService;
using Random = EOM.Game.Server.Service.Random;

namespace EOM.Game.Server.Feature.ItemDefinition
{
    public class ConsumableItemDefinition: IItemListDefinition
    {
        private readonly ItemBuilder _builder = new();
        public Dictionary<string, ItemDetail> BuildItems()
        {
            SlugShake();
            RebuildToken();

            return _builder.Build();
        }

        private void SlugShake()
        {
            _builder.Create("slug_shake")
                .Delay(1f)
                .PlaysAnimation(Animation.FireForgetDrink)
                .ReducesItemCharge()
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var ability = AbilityType.Invalid;
                    
                    switch (Random.Next(5) + 1)
                    {
                        case 1:
                            ability = AbilityType.Intellect;
                            break;
                        case 2:
                            ability = AbilityType.Vitality;
                            break;
                        case 3:
                            ability = AbilityType.Perception;
                            break;
                        case 4:
                            ability = AbilityType.Might;
                            break;
                        case 5:
                            ability = AbilityType.Willpower;
                            break;
                    }

                    var maxHP = GetMaxHitPoints(user);
                    ApplyEffectToObject(DurationType.Instant, EffectHeal(maxHP), user);
                    ApplyEffectToObject(DurationType.Temporary, EffectAbilityDecrease(ability, 50), user, 120f);

                });
        }

  
        private void RebuildToken()
        {
            _builder.Create("rebuild_token")
                .PlaysAnimation(Animation.LoopingGetMid)
                .ValidationAction((user, item, target, location, itemPropertyIndex) =>
                {
                    if (!GetIsPC(user) || GetIsDM(user) || GetIsDMPossessed(user))
                    {
                        return "Only players may use this item.";
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    Currency.GiveCurrency(user, CurrencyType.RebuildToken, 1);
                    Item.ReduceItemStack(item, 1);
                    SendMessageToPC(user, $"Total Rebuild Tokens: {Currency.GetCurrency(user, CurrencyType.RebuildToken)}");
                });
        }
    }
}
