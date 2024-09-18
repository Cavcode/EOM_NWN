﻿using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.StatusEffectDefinition
{
    public class AuraStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            Charge();
            Dedication();
            FrenziedShout();
            Rejuvenation();
            SoldiersPrecision();
            SoldiersSpeed();
            SoldiersStrike();

            return _builder.Build();
        }

        private void Charge()
        {
            const string EffectTag = "AURA_CHARGE_EFFECT";
            _builder.Create(StatusEffectType.Charge)
                .Name("Charge")
                .EffectIcon(EffectIconType.Charge)
                .GrantAction((source, target, length, data) => 
                {
                    RemoveEffectByTag(target, EffectTag);
                    var effectiveLevel = Perk.GetPerkLevel(source, PerkType.Charge);
                    Effect effect;

                    switch (effectiveLevel)
                    {
                        case 1:
                            effect = EffectMovementSpeedIncrease(15);
                            effect = TagEffect(effect, EffectTag);
                            ApplyEffectToObject(DurationType.Permanent, effect, target);
                            break;
                        case 2:
                            effect = EffectMovementSpeedIncrease(30);
                            effect = TagEffect(effect, EffectTag);
                            ApplyEffectToObject(DurationType.Permanent, effect, target);
                            break;
                    }
                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                });
        }

        private void Dedication()
        {
            _builder.Create(StatusEffectType.Dedication)
                .Name("Dedication")
                .EffectIcon(EffectIconType.Dedication);
        }

        private void FrenziedShout()
        {
            _builder.Create(StatusEffectType.FrenziedShout)
                .Name("Frenzied Shout")
                .EffectIcon(EffectIconType.FrenziedShout);
        }

        private void Rejuvenation()
        {
            _builder.Create(StatusEffectType.Rejuvenation)
                .Name("Rejuvenation")
                .EffectIcon(EffectIconType.Rejuvenation)
                .TickAction((source, target, data) =>
                {
                    var level = Perk.GetPerkLevel(source, PerkType.Rejuvenation);
                    Stat.RestoreMagick(target, level);
                });
        }

        private void SoldiersPrecision()
        {
            _builder.Create(StatusEffectType.SoldiersPrecision)
                .Name("Soldier's Precision")
                .EffectIcon(EffectIconType.SoldiersPrecision);
        }

        private void SoldiersSpeed()
        {
            _builder.Create(StatusEffectType.SoldiersSpeed)
                .Name("Soldier's Speed")
                .EffectIcon(EffectIconType.SoldiersSpeed);
        }

        private void SoldiersStrike()
        {
            _builder.Create(StatusEffectType.SoldiersStrike)
                .Name("Soldier's Strike")
                .EffectIcon(EffectIconType.SoldiersStrike);
        }
    }
}
