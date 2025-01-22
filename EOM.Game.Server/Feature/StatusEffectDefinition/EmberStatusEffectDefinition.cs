using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.StatusEffectDefinition
{
    public class EmberStatusEffectDefinition: IStatusEffectListDefinition
    {
        private readonly StatusEffectBuilder _builder = new StatusEffectBuilder();

        public Dictionary<StatusEffectType, StatusEffectDetail> BuildStatusEffects()
        {
            Ember();

            return _builder.Build();
        }

        private void Ember()
        {
            const string EffectTag = "EMBER_EFFECT";
            _builder.Create(StatusEffectType.Ember1)
                .Name("Ember I")
                .EffectIcon(EffectIconType.DamageImmunityIncrease)
                .CannotReplace(StatusEffectType.Shielding2, StatusEffectType.Shielding3, StatusEffectType.Shielding4)
                .GrantAction((source, target, length, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                    Effect effect;
                    int percent = 5;
                    effect = EffectDamageImmunityIncrease(DamageType.Bludgeoning, percent);
                    var effectPiercing = EffectDamageImmunityIncrease(DamageType.Piercing, percent);
                    effect = EffectLinkEffects(effectPiercing, effect);
                    var slashing = EffectDamageImmunityIncrease(DamageType.Slashing, percent);
                    effect = EffectLinkEffects(slashing, effect);
                    effect = TagEffect(effect, EffectTag);
                    ApplyEffectToObject(DurationType.Permanent, effect, target);
                    
                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                });

            _builder.Create(StatusEffectType.Ember2)
                .Name("Ember II")
                .EffectIcon(EffectIconType.DamageImmunityIncrease)
                .Replaces(StatusEffectType.Ember1)
                .CannotReplace(StatusEffectType.Ember3, StatusEffectType.Ember4)
                .GrantAction((source, target, length, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                    Effect effect;
                    int percent = 10;
                    effect = EffectDamageImmunityIncrease(DamageType.Bludgeoning, percent);
                    var effectPiercing = EffectDamageImmunityIncrease(DamageType.Piercing, percent);
                    effect = EffectLinkEffects(effectPiercing, effect);
                    var slashing = EffectDamageImmunityIncrease(DamageType.Slashing, percent);
                    effect = EffectLinkEffects(slashing, effect);
                    effect = TagEffect(effect, EffectTag);
                    ApplyEffectToObject(DurationType.Permanent, effect, target);

                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                }); 


            _builder.Create(StatusEffectType.Ember3)
                .Name("Ember III")
                .EffectIcon(EffectIconType.DamageImmunityIncrease)
                .Replaces(StatusEffectType.Ember1, StatusEffectType.Ember2)
                .CannotReplace(StatusEffectType.Ember4)
                .GrantAction((source, target, length, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                    Effect effect;
                    int percent = 15;
                    effect = EffectDamageImmunityIncrease(DamageType.Bludgeoning, percent);
                    var effectPiercing = EffectDamageImmunityIncrease(DamageType.Piercing, percent);
                    effect = EffectLinkEffects(effectPiercing, effect);
                    var slashing = EffectDamageImmunityIncrease(DamageType.Slashing, percent);
                    effect = EffectLinkEffects(slashing, effect);
                    effect = TagEffect(effect, EffectTag);
                    ApplyEffectToObject(DurationType.Permanent, effect, target);

                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                });

            _builder.Create(StatusEffectType.Ember4)
                .Name("Ember IV")
                .EffectIcon(EffectIconType.DamageImmunityIncrease)
                .Replaces(StatusEffectType.Ember1, StatusEffectType.Ember2, StatusEffectType.Ember3)
                .CannotReplace(StatusEffectType.Ember5)
                .GrantAction((source, target, length, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                    Effect effect;
                    int percent = 20;
                    effect = EffectDamageImmunityIncrease(DamageType.Bludgeoning, percent);
                    var effectPiercing = EffectDamageImmunityIncrease(DamageType.Piercing, percent);
                    effect = EffectLinkEffects(effectPiercing, effect);
                    var slashing = EffectDamageImmunityIncrease(DamageType.Slashing, percent);
                    effect = EffectLinkEffects(slashing, effect);
                    effect = TagEffect(effect, EffectTag);
                    ApplyEffectToObject(DurationType.Permanent, effect, target);

                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                });

            _builder.Create(StatusEffectType.Ember5)
                .Name("Ember V")
                .EffectIcon(EffectIconType.DamageImmunityIncrease)
                .Replaces(StatusEffectType.Ember1, StatusEffectType.Ember2, StatusEffectType.Ember3, StatusEffectType.Ember4)
                .GrantAction((source, target, length, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                    Effect effect;
                    int percent = 25;
                    effect = EffectDamageImmunityIncrease(DamageType.Bludgeoning, percent);
                    var effectPiercing = EffectDamageImmunityIncrease(DamageType.Piercing, percent);
                    effect = EffectLinkEffects(effectPiercing, effect);
                    var slashing = EffectDamageImmunityIncrease(DamageType.Slashing, percent);
                    effect = EffectLinkEffects(slashing, effect);
                    effect = TagEffect(effect, EffectTag);
                    ApplyEffectToObject(DurationType.Permanent, effect, target);

                })
                .RemoveAction((target, data) =>
                {
                    RemoveEffectByTag(target, EffectTag);
                });
        }
    }
}
