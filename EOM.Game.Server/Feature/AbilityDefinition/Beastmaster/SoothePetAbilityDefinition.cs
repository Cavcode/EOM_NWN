using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Associate;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Feature.AbilityDefinition.Beastmaster
{
    public class SoothePetAbilityDefinition : IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();


        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            SoothePet();

            return _builder.Build();
        }

        private void SoothePet()
        {
            _builder.Create(FeatType.SoothePet, PerkType.SoothePet)
                .Name("Soothe Pet")
                .Level(1)
                .HasRecastDelay(RecastGroup.Tame, 60f * 3)
                .UsesAnimation(Animation.LoopingGetMid)
                .HasActivationDelay(1f)
                .RequirementStamina(2)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation((activator, target, level, location) =>
                {
                    if (!GetIsPC(activator) || GetIsDM(activator) || GetIsDMPossessed(activator))
                    {
                        return "Only players may use this ability.";
                    }

                    var beast = GetAssociate(AssociateType.Henchman, activator);
                    if (!BeastMastery.IsPlayerBeast(beast))
                    {
                        return "You do not have an active beast.";
                    }

                    if (GetDistanceBetween(beast, activator) >= 15f)
                    {
                        return "Your beast is too far away.";
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, _, _, targetLocation) =>
                {
                    var beast = GetAssociate(AssociateType.Henchman, activator);

                    StatusEffect.Remove(beast, StatusEffectType.Bleed);
                    StatusEffect.Remove(beast, StatusEffectType.Poison);
                    StatusEffect.Remove(beast, StatusEffectType.Shock);
                    StatusEffect.Remove(beast, StatusEffectType.Burn);
                    StatusEffect.Remove(beast, StatusEffectType.Disease);

                    RemoveEffect(beast, 
                        EffectTypeScript.Disease, 
                        EffectTypeScript.Poison, 
                        EffectTypeScript.Confused,
                        EffectTypeScript.Paralyze,
                        EffectTypeScript.Stunned,
                        EffectTypeScript.Sleep,
                        EffectTypeScript.Slow);

                    ApplyEffectToObject(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Imp_Healing_G), beast);
                    Enmity.ModifyEnmityOnAll(activator, 500);
                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.BeastMastery);
                });
        }
    }
}