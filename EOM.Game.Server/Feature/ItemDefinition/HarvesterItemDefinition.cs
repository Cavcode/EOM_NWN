﻿using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.ItemService;
using EOM.Game.Server.Service.LogService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using Random = EOM.Game.Server.Service.Random;

namespace EOM.Game.Server.Feature.ItemDefinition
{
    public class HarvesterItemDefinition: IItemListDefinition
    {
        private readonly ItemBuilder _builder = new();

        public Dictionary<string, ItemDetail> BuildItems()
        {
            Harvester("harvest_r_old", 0);
            Harvester("harvest_r_b", 1);
            Harvester("harvest_r_1", 2);
            Harvester("harvest_r_2", 3);
            Harvester("harvest_r_3", 4);
            Harvester("harvest_r_4", 5);

            return _builder.Build();
        }

        /// <summary>
        /// Whenever a resource despawns, if it has an associated prop placeable, destroy it from the game world.
        /// </summary>
        [NWNEventHandler("spawn_despawn")]
        public static void CleanupResourcePropPlaceables()
        {
            var resource = OBJECT_SELF;
            DestroyProp(resource);
        }

        private static void DestroyProp(uint resource)
        {
            var prop = GetLocalObject(resource, "RESOURCE_PROP_OBJ");
            if (GetIsObjectValid(prop))
            {
                SetPlotFlag(prop, false);
                ApplyEffectToObject(DurationType.Instant, EffectDeath(), prop);
            }
        }

        private void Harvester(string tag, int requiredLevel)
        {
            _builder.Create(tag)
                .Delay(5f)
                .PlaysAnimation(Animation.LoopingGetMid)
                .UserFacesTarget()
                .MaxDistance(3.0f)
                .ReducesItemCharge()
                .ValidationAction((user, item, target, location, itemPropertyIndex) =>
                {
                    var perkLevel = Perk.GetPerkLevel(user, PerkType.Harvesting);

                    if (perkLevel < requiredLevel)
                    {
                        return $"Your Harvesting perk level is too low to use this harvester. (Required: {requiredLevel})";
                    }

                    var lootTableName = GetLocalString(target, "HARVESTING_LOOT_TABLE");
                    if (string.IsNullOrWhiteSpace(lootTableName))
                    {
                        return "This harvester cannot be used on that target.";
                    }

                    if (!Loot.LootTableExists(lootTableName))
                    {
                        Log.Write(LogGroup.Error, $"Loot table '{lootTableName}' assigned to harvesting object '{GetName(target)}' does not exist.");
                        return $"ERROR: Harvesting loot table misconfigured. Please use /bug to report this issue.";
                    }

                    var harvesterLevel = requiredLevel < 1 ? 1 : requiredLevel;
                    var resourceLevel = GetLocalInt(target, "HARVESTER_REQUIRED_LEVEL");
                    if (resourceLevel > harvesterLevel)
                    {
                        return $"A level {resourceLevel} harvester or higher is required for this resource.";
                    }

                    return string.Empty;
                })
                .ApplyAction((user, item, target, location, itemPropertyIndex) =>
                {
                    if (!GetIsObjectValid(target))
                    {
                        SendMessageToPC(user, "You lose your target.");
                        return;
                    }

                    var lootTableName = GetLocalString(target, "HARVESTING_LOOT_TABLE");
                    var lootTable = Loot.GetLootTableByName(lootTableName);
                    var loot = lootTable.GetRandomItem();
                    var resourceLevel = GetLocalInt(target, "HARVESTER_REQUIRED_LEVEL");

                    var resourceCount = GetLocalInt(target, "RESOURCE_COUNT");

                    if (resourceCount <= 0)
                    {
                        resourceCount = Random.D4(1);
                    }

                    resourceCount--;

                    CreateItemOnObject(loot.Resref, user);

                    // Check against the user's Might; create a second item if they are 
                    // strong.  This is 'free' and does not count towards the limit in the resource point.
                    if (d100() <= 5 * GetAbilityModifier(AbilityType.Might, user) * 5)
                    {
                        loot = lootTable.GetRandomItem();
                        CreateItemOnObject(loot.Resref, user);
                    }

                    if (resourceCount <= 0)
                    {
                        // DestroyObject bypasses the OnDeath event, and removes the object so we can't send events.
                        // Use EffectDeath to ensure that we trigger death processing.
                        SetPlotFlag(target, false);
                        ApplyEffectToObject(DurationType.Instant, EffectDeath(), target);

                        DestroyProp(target);
                    }
                    else
                    {
                        SetLocalInt(target, "RESOURCE_COUNT", resourceCount);
                    }

                    ApplyEffectAtLocation(DurationType.Instant, EffectVisualEffect(VisualEffect.Vfx_Fnf_Summon_Monster_3), GetLocation(target));

                    if (GetIsPC(user) && !GetIsDM(user))
                    {
                        var playerId = GetObjectUUID(user);
                        var dbPlayer = DB.Get<Player>(playerId);
                        var dbSkill = dbPlayer.Skills[SkillType.Gathering];
                        var veinLevel = 10 * (resourceLevel - 1) + 5;
                        var delta = veinLevel - dbSkill.Rank;
                        var deltaXP = Skill.GetDeltaXP(delta);

                        Skill.GiveSkillXP(user, SkillType.Gathering, deltaXP, false, false);
                    }

                    ExecuteScript("harvester_used", user);
                });
        }
    }
}
