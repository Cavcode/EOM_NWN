﻿using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Associate;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Feature.AbilityDefinition.Beastmaster
{
    public class CallBeastAbilityDefinition: IAbilityListDefinition
    {
        private readonly AbilityBuilder _builder = new();

        public Dictionary<FeatType, AbilityDetail> BuildAbilities()
        {
            CallBeast();

            return _builder.Build();
        }

        private void CallBeast()
        {
            _builder.Create(FeatType.CallBeast, PerkType.Tame) // Intentionally tied to Tame
                .Name("Call Beast")
                .Level(1)
                .HasRecastDelay(RecastGroup.CallBeast, 60f * 10f)
                .UsesAnimation(Animation.LoopingGetMid)
                .HasActivationDelay(6f)
                .IsCastedAbility()
                .UnaffectedByHeavyArmor()
                .HasCustomValidation((activator, target, level, location) =>
                {
                    if (GetIsInCombat(activator) || Enmity.HasEnmity(activator))
                    {
                        return "You are in combat and cannot call your beast.";
                    }

                    var maxBeastLevel = Perk.GetPerkLevel(activator, PerkType.Tame) * 10;

                    if (!GetIsPC(activator) || GetIsDM(activator) || GetIsDMPossessed(activator))
                    {
                        return "Only players may use this ability.";
                    }

                    if (GetIsObjectValid(GetAssociate(AssociateType.Henchman, activator)))
                    {
                        return "You already have a companion active.";
                    }

                    var playerId = GetObjectUUID(activator);
                    var dbPlayer = DB.Get<Player>(playerId);

                    if (string.IsNullOrWhiteSpace(dbPlayer.ActiveBeastId))
                    {
                        return "You do not have an active beast.";
                    }

                    var dbBeast = DB.Get<Beast>(dbPlayer.ActiveBeastId);

                    if (dbBeast.IsDead)
                    {
                        return "Your beast is unconscious.";
                    }

                    if (dbBeast.Level > maxBeastLevel)
                    {
                        return $"Your Tame level is too low to call this beast. (Required: {maxBeastLevel/10})";
                    }

                    return string.Empty;
                })
                .HasImpactAction((activator, target, level, location) =>
                {
                    var playerId = GetObjectUUID(activator);
                    var dbPlayer = DB.Get<Player>(playerId);
                    
                    BeastMastery.SpawnBeast(activator, dbPlayer.ActiveBeastId, 50);

                    Enmity.ModifyEnmityOnAll(activator, 230);
                    CombatPoint.AddCombatPointToAllTagged(activator, SkillType.BeastMastery);
                });
        }
    }
}
