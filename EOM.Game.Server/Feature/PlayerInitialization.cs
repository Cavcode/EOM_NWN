using System;
using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.LogService;
using EOM.Game.Server.Service.SkillService;
using Player = EOM.Game.Server.Entity.Player;
using Race = EOM.Game.Server.Service.Race;

namespace EOM.Game.Server.Feature
{
    public class PlayerInitialization
    {
        /// <summary>
        /// Handles 
        /// </summary>
        [NWNEventHandler("mod_enter")]
        public static void InitializePlayer()
        {
            var player = GetEnteringObject();

            if (!GetIsPC(player) || GetIsDM(player)) return;

            var playerId = GetObjectUUID(player);
            Console.WriteLine("UUID: " + playerId);
            var dbPlayer = DB.Get<Player>(playerId) ?? new Player(playerId);
            Console.WriteLine("dbPlayer ID: "+ dbPlayer);
            // Already been initialized. Don't do it again.
            if (dbPlayer.Version >= 1) // Note: -1 signifies legacy characters. The Migration service handles upgrading legacy characters.
            {
                ExecuteScript("char_init_after", OBJECT_SELF);
                return;
            }

            ClearInventory(player);
            AutoLevelPlayer(player);
            InitializeSkills(player);
            RemoveNWNSpells(player);
            ClearFeats(player);
            GrantBasicFeats(player);
            InitializeHotBar(player);
            AdjustStats(player, dbPlayer);
            AdjustAlignment(player);
            InitializeSavingThrows(player);
            AssignRacialAppearance(player, dbPlayer);
            GiveStartingItems(player);
            AssignCharacterType(player, dbPlayer);
            RegisterDefaultRespawnPoint(dbPlayer);
            ApplyMovementRate(player);

            DB.Set(dbPlayer);

            ExecuteScript("char_init_after", OBJECT_SELF);
        }

        private static void AutoLevelPlayer(uint player)
        {
            // Capture original stats before we level up the player.
            var str = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            var con = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            var dex = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            var @int = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Agility);
            var wis = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            var cha = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Intellect);

            GiveXPToCreature(player, 800000);
            var @class = GetClassByPosition(1, player);

            for (var level = 1; level <= 40; level++)
            {
                LevelUpHenchman(player, @class);
            }

            // Set stats back to how they were on entry.
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Might, str);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Vitality, con);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Perception, dex);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Agility, @int);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Willpower, wis);
            CreaturePlugin.SetRawAbilityScore(player, AbilityType.Intellect, cha);
        }

        /// <summary>
        /// Wipes a player's equipped items and inventory.
        /// </summary>
        /// <param name="player">The player to wipe an inventory for.</param>
        private static void ClearInventory(uint player)
        {
            for (var slot = 0; slot < NumberOfInventorySlots; slot++)
            {
                var item = GetItemInSlot((InventorySlot)slot, player);
                if (!GetIsObjectValid(item)) continue;

                DestroyObject(item);
            }

            var inventory = GetFirstItemInInventory(player);
            while (GetIsObjectValid(inventory))
            {
                DestroyObject(inventory);
                inventory = GetNextItemInInventory(player);
            }

            TakeGoldFromCreature(GetGold(player), player, true);
        }

        /// <summary>
        /// Initializes all player NWN skills to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        public static void InitializeSkills(uint player)
        {
            for (var iCurSkill = 1; iCurSkill <= 27; iCurSkill++)
            {
                var skill = (NWNSkillType) (iCurSkill - 1);
                CreaturePlugin.SetSkillRank(player, skill, 0);
            }
        }

        /// <summary>
        /// Initializes all player saving throws to zero.
        /// </summary>
        /// <param name="player">The player to modify</param>
        public static void InitializeSavingThrows(uint player)
        {
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Fortitude, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Will, 0);
            CreaturePlugin.SetBaseSavingThrow(player, SavingThrow.Reflex, 0);
        }

        /// <summary>
        /// Removes all NWN spells from a player.
        /// </summary>
        /// <param name="player">The player to modify.</param>
        private static void RemoveNWNSpells(uint player)
        {
            var @class = GetClassByPosition(1, player);
            for (var index = 0; index <= 255; index++)
            {
                CreaturePlugin.RemoveKnownSpell(player, @class, 0, index);
            }
        }

        public static void ClearFeats(uint player)
        {
            var numberOfFeats = CreaturePlugin.GetFeatCount(player);
            for (var currentFeat = numberOfFeats; currentFeat >= 0; currentFeat--)
            {
                CreaturePlugin.RemoveFeat(player, CreaturePlugin.GetFeatByIndex(player, currentFeat - 1));
            }
        }

        public static void GrantBasicFeats(uint player)
        {
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyLight, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyMedium, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ArmorProficiencyHeavy, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.ShieldProficiency, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyExotic, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencyMartial, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.WeaponProficiencySimple, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.UncannyDodge1, 1);
            CreaturePlugin.AddFeatByLevel(player, FeatType.PropertyMenu, 1);
        }

        public static void InitializeHotBar(uint player)
        {
            var structureTool = PlayerQuickBarSlot.UseFeat(FeatType.PropertyMenu);
            
            PlayerPlugin.SetQuickBarSlot(player, 0, structureTool);
        }

        /// <summary>
        /// Modifies the player's unallocated SP, version, max HP, and other assorted stats.
        /// </summary>
        /// <param name="player">The player object</param>
        /// <param name="dbPlayer">The player entity.</param>
        private static void AdjustStats(uint player, Player dbPlayer)
        {
            dbPlayer.UnallocatedSP = 10;
            dbPlayer.UnallocatedJP = 1;
            dbPlayer.Version = Migration.GetLatestPlayerVersion();
            dbPlayer.Name = GetName(player);
            dbPlayer.BAB = 1;
            Stat.AdjustPlayerMaxHP(dbPlayer, player, Stat.BaseHP);
            Stat.AdjustPlayerMaxMP(dbPlayer, Stat.BaseMP, player);
            CreaturePlugin.SetBaseAttackBonus(player, 1);
            dbPlayer.HP = GetCurrentHitPoints(player);
            dbPlayer.Magick = Stat.GetMaxMagick(player, dbPlayer);

            dbPlayer.WarriorRank = 0;
            dbPlayer.MonkRank = 0;
            dbPlayer.GunbreakerRank = 0;
            dbPlayer.WhiteMageRank = 0;
            dbPlayer.BlackMageRank = 0;

            dbPlayer.BaseStats[AbilityType.Might] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Might);
            dbPlayer.BaseStats[AbilityType.Perception] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Perception);
            dbPlayer.BaseStats[AbilityType.Vitality] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Vitality);
            dbPlayer.BaseStats[AbilityType.Willpower] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Willpower);
            dbPlayer.BaseStats[AbilityType.Agility] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Agility);
            dbPlayer.BaseStats[AbilityType.Intellect] = CreaturePlugin.GetRawAbilityScore(player, AbilityType.Intellect);

            dbPlayer.RebuildComplete = true;
        }

        /// <summary>
        /// Modifies the player's alignment to Neutral/Neutral since we don't use alignment at all here.
        /// </summary>
        /// <param name="player">The player to object.</param>
        public static void AdjustAlignment(uint player)
        {
            CreaturePlugin.SetAlignmentLawChaos(player, 50);
            CreaturePlugin.SetAlignmentGoodEvil(player, 50);
        }


        /// <summary>
        /// Assigns and stores the player's original racial appearance.
        /// This value is primarily used in the space system for switching between space and character modes.
        /// </summary>
        /// <param name="player">The player object</param>
        /// <param name="dbPlayer">The database entity</param>
        private static void AssignRacialAppearance(uint player, Player dbPlayer)
        {
            DelayCommand(0.1f, () =>
            {
                Race.SetDefaultRaceAppearance(player);
            });
            dbPlayer.OriginalAppearanceType = GetAppearanceType(player);
        }

        /// <summary>
        /// Gives the starting items to the player.
        /// </summary>
        /// <param name="player">The player to receive the starting items.</param>
        private static void GiveStartingItems(uint player)
        {
            var race = GetRacialType(player);
            var item = CreateItemOnObject("survival_knife", player);
            SetName(item, GetName(player) + "'s Survival Knife");
            SetItemCursedFlag(item, true);

            item = CreateItemOnObject("fresh_bread", player);
            SetItemCursedFlag(item, true);

            var clothes = race == RacialType.Droid ? "dlarproto" : "travelers_clothes";
            item = CreateItemOnObject(clothes, player);
            AssignCommand(player, () =>
            {
                ClearAllActions();
                ActionEquipItem(item, InventorySlot.Chest);
            });

            GiveGoldToCreature(player, 200);
        }

        /// <summary>
        /// Sets the character type (either Standard or Force Sensitive) based on their character class.
        /// </summary>
        /// <param name="player">The player</param>
        /// <param name="dbPlayer">The player entity</param>
        private static void AssignCharacterType(uint player, Player dbPlayer)
        {
            var @class = GetClassByPosition(1, player);

            if (@class == ClassType.Standard)
                dbPlayer.CharacterType = CharacterType.Standard;
            else if (@class == ClassType.ForceSensitive)
                dbPlayer.CharacterType = CharacterType.ForceSensitive;
        }

        /// <summary>
        /// Sets the player's default respawn location to that of the waypoint with tag 'DTH_DEFAULT_RESPAWN_POINT'.
        /// If no waypoint by that tag can be found, an error will be logged.
        /// </summary>
        /// <param name="dbPlayer">The player's database entity.</param>
        private static void RegisterDefaultRespawnPoint(Player dbPlayer)
        {
            const string DefaultRespawnWaypointTag = "DTH_DEFAULT_RESPAWN_POINT";
            var waypoint = GetWaypointByTag(DefaultRespawnWaypointTag);

            if (!GetIsObjectValid(waypoint))
            {
                Log.Write(LogGroup.Error, $"Default respawn waypoint could not be located. Did you place a waypoint with the tag 'DTH_DEFAULT_RESPAWN_POINT'?");
                return;
            }

            var location = GetLocation(waypoint);
            var position = GetPositionFromLocation(location);
            var orientation = GetFacingFromLocation(location);
            var area = GetAreaFromLocation(location);
            var areaResref = GetResRef(area);

            dbPlayer.RespawnAreaResref = areaResref;
            dbPlayer.RespawnLocationX = position.X;
            dbPlayer.RespawnLocationY = position.Y;
            dbPlayer.RespawnLocationZ = position.Z;
            dbPlayer.RespawnLocationOrientation = orientation;
        }

        private static void ApplyMovementRate(uint player)
        {
            CreaturePlugin.SetMovementRate(player, MovementRate.PC);
        }

    }
}
