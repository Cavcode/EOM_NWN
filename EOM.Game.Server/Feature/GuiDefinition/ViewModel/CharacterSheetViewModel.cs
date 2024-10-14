﻿using System;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.DialogDefinition;
using EOM.Game.Server.Feature.GuiDefinition.Payload;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.CombatService;
using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.SkillService;
using EOM.Game.Server.Service.StatusEffectService;
using Skill = EOM.Game.Server.Service.Skill;

namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class CharacterSheetViewModel: GuiViewModelBase<CharacterSheetViewModel, CharacterSheetPayload>,
        IGuiRefreshable<ChangePortraitRefreshEvent>,
        IGuiRefreshable<SkillXPRefreshEvent>,
        IGuiRefreshable<EquipItemRefreshEvent>,
        IGuiRefreshable<UnequipItemRefreshEvent>,
        IGuiRefreshable<StatusEffectReceivedRefreshEvent>,
        IGuiRefreshable<StatusEffectRemovedRefreshEvent>
    {
        private const int MaxUpgrades = 10;
        private uint _target;

        public bool IsPlayerMode
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool ShowSP
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool ShowJP
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool ShowAPOrLevel
        {
            get => Get<bool>();
            set => Set(value);
        }

        public string PortraitResref
        {
            get => Get<string>();
            set => Set(value);
        }

        public string HP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string MP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string APOrLevelLabel
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Might
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Perception
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Vitality
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Willpower
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Agility
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Intellect
        {
            get => Get<int>();
            set => Set(value);
        }

        public string SavingThrows
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Name
        {
            get => Get<string>();
            set => Set(value);
        }

        public string MainHandDMG
        {
            get => Get<string>();
            set => Set(value);
        }

        public string OffHandDMG
        {
            get => Get<string>();
            set => Set(value);
        }

        public string MainHandTooltip
        {
            get => Get<string>();
            set => Set(value);
        }

        public string OffHandTooltip
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Attack
        {
            get => Get<int>();
            set => Set(value);
        }

        public int DefensePhysical
        {
            get => Get<int>();
            set => Set(value);
        }

        public int DefenseForce
        {
            get => Get<int>();
            set => Set(value);
        }

        public string DefenseElemental
        {
            get => Get<string>();
            set => Set(value);
        }

        public int Accuracy
        {
            get => Get<int>();
            set => Set(value);
        }

        public int Evasion
        {
            get => Get<int>();
            set => Set(value);
        }

        public string CharacterType
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Race
        {
            get => Get<string>();
            set => Set(value);
        }

        public string SP
        {
            get => Get<string>();
            set => Set(value);
        }
        public string JP
        {
            get => Get<string>();
            set => Set(value);
        }

        public string APOrLevel
        {
            get => Get<string>();
            set => Set(value);
        }

        public string APOrLevelTooltip
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Ingenuity
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Craftsmanship
        {
            get => Get<string>();
            set => Set(value);
        }

        public bool IsMightUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsPerceptionUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsVitalityUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsWillpowerUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsAgilityUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsSocialUpgradeAvailable
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsHolocomEnabled
        {
            get => Get<bool>();
            set => Set(value);
        }

        public Action OnClickSkills() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Skills);
        };

        public Action OnClickPerks() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Perks);
        };
        public Action OnClickJobs() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Jobs);
        };
        public Action OnClickChangePortrait() => () =>
        {
            var payload = new ChangePortraitPayload(_target);
            Gui.TogglePlayerWindow(Player, GuiWindowType.ChangePortrait, payload);
        };

        public Action OnClickQuests() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Quests);
        };

        public Action OnClickRecipes() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Recipes);
        };

        public Action OnClickHoloCom() => () =>
        {
            Dialog.StartConversation(Player, Player, nameof(HoloComDialog));
        };

        public Action OnClickKeyItems() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.KeyItems);
        };

        public Action OnClickCurrencies() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Currencies);
        };

        public Action OnClickAchievements() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Achievements);
        };

        public Action OnClickNotes() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Notes);
        };

        public Action OnClickOpenTrash() => () =>
        {
            var location = GetLocation(Player);
            var trash = CreateObject(ObjectType.Placeable, "reo_trash_can", location);
            AssignCommand(Player, () => ActionInteractObject(trash));
            DelayCommand(0.2f, () => SetUseableFlag(trash, false));
        };

        public Action OnClickAppearance() => () =>
        {
            var payload = new AppearanceEditorPayload(Player);
            Gui.TogglePlayerWindow(Player, GuiWindowType.AppearanceEditor, payload);
        };

        public Action OnClickSettings() => () =>
        {
            Gui.TogglePlayerWindow(Player, GuiWindowType.Settings);
        };

        private void UpgradeAttribute(AbilityType ability, string abilityName)
        {
            var playerId = GetObjectUUID(_target);
            var dbPlayer = DB.Get<Player>(playerId);
            var isRacial = dbPlayer.RacialStat == AbilityType.Invalid;
            var promptMessage = isRacial
                ? "WARNING: You are about to spend your one-time racial stat bonus. Once spent, this action can only be undone with a stat rebuild. Are you SURE you want to upgrade this stat?"
                : $"Upgrading your {abilityName} attribute will consume 1 AP. Are you sure you want to upgrade it?";

            ShowModal(promptMessage, () =>
            {
                if (GetResRef(GetArea(_target)) == "char_migration")
                {
                    FloatingTextStringOnCreature($"Stats cannot be upgraded in this area.", _target, false);
                    return;
                }

                playerId = GetObjectUUID(_target);
                dbPlayer = DB.Get<Player>(playerId);
                isRacial = dbPlayer.RacialStat == AbilityType.Invalid;

                // Racial upgrades do not count toward the 10 cap and they don't reduce AP.
                if (!isRacial)
                {
                    if (dbPlayer.UnallocatedAP <= 0)
                    {
                        FloatingTextStringOnCreature("You do not have enough AP to purchase this upgrade.", _target, false);
                        return;
                    }

                    if (dbPlayer.UpgradedStats[ability] >= MaxUpgrades)
                    {
                        FloatingTextStringOnCreature("You cannot upgrade this attribute any further.", _target, false);
                        return;
                    }

                    dbPlayer.UnallocatedAP--;
                    dbPlayer.UpgradedStats[ability]++;
                }
                else
                {
                    dbPlayer.RacialStat = ability;
                }

                CreaturePlugin.ModifyRawAbilityScore(_target, ability, 1);

                DB.Set(dbPlayer);

                FloatingTextStringOnCreature($"Your {abilityName} attribute has increased!", _target, false);
                LoadData();
            });
        }

        public Action OnClickUpgradeMight() => () =>
        {
            UpgradeAttribute(AbilityType.Might, "Might");
        };

        public Action OnClickUpgradePerception() => () =>
        {
            UpgradeAttribute(AbilityType.Perception, "Perception");
        };

        public Action OnClickUpgradeVitality() => () =>
        {
            UpgradeAttribute(AbilityType.Vitality, "Vitality");
        };

        public Action OnClickUpgradeWillpower() => () =>
        {
            UpgradeAttribute(AbilityType.Willpower, "Willpower");
        };

        public Action OnClickUpgradeAgility() => () =>
        {
            UpgradeAttribute(AbilityType.Agility, "Agility");
        };

        public Action OnClickUpgradeSocial() => () =>
        {
            UpgradeAttribute(AbilityType.Intellect, "Intellect");
        };


        private void RefreshStats()
        {
            HP = GetCurrentHitPoints(_target) + " / " + GetMaxHitPoints(_target);


            var currentSTM = Stat.GetCurrentMagick(_target);
            var maxSTM = Stat.GetMaxMagick(_target);
            if (currentSTM < 0)
                currentSTM = 0;
            if (maxSTM < 0)
                maxSTM = 0;

            MP = $"{currentSTM} / {maxSTM}";
            Name = GetName(_target);
            Might = GetAbilityScore(_target, AbilityType.Might);
            Perception = GetAbilityScore(_target, AbilityType.Perception);
            Vitality = GetAbilityScore(_target, AbilityType.Vitality);
            Willpower = GetAbilityScore(_target, AbilityType.Willpower);
            Agility = GetAbilityScore(_target, AbilityType.Agility);
            Intellect = GetAbilityScore(_target, AbilityType.Intellect);
            SavingThrows = GetFortitudeSavingThrow(_target) + "/" +
                           GetReflexSavingThrow(_target) + "/" +
                           GetWillSavingThrow(_target);

            if (IsPlayerMode)
            {
                var playerId = GetObjectUUID(_target);
                var dbPlayer = DB.Get<Player>(playerId);

                var isRacialBonusAvailable = dbPlayer.RacialStat == AbilityType.Invalid;
                IsMightUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Might] < MaxUpgrades) || isRacialBonusAvailable;
                IsPerceptionUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Perception] < MaxUpgrades) || isRacialBonusAvailable;
                IsVitalityUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Vitality] < MaxUpgrades) || isRacialBonusAvailable;
                IsWillpowerUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Willpower] < MaxUpgrades) || isRacialBonusAvailable;
                IsAgilityUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Agility] < MaxUpgrades) || isRacialBonusAvailable;
                IsSocialUpgradeAvailable = (dbPlayer.UnallocatedAP > 0 && dbPlayer.UpgradedStats[AbilityType.Intellect] < MaxUpgrades) || isRacialBonusAvailable;
            }
        }
        
        private void RefreshEquipmentStats()
        {
            // Builds a damage estimate using the player's stats as a baseline.
            (string, string) GetCombatInfo( uint item)
            {
                var itemType = GetBaseItemType(item);
                var skill = Skill.GetSkillTypeByBaseItem(itemType);
                int skillRank;

                if (GetIsPC(_target))
                {
                    var playerId = GetObjectUUID(_target);
                    var dbPlayer = DB.Get<Player>(playerId);
                    skillRank = dbPlayer.Skills[skill].Rank;
                }
                else
                {
                    var npcStats = Stat.GetNPCStats(_target);
                    skillRank = npcStats.Level;
                }

                var damageAbility = Item.GetWeaponDamageAbilityType(itemType);
                var damageStat = GetAbilityScore(_target, damageAbility);
                var dmg = Item.GetDMG(item) + Combat.GetMiscDMGBonus(_target, itemType);
                var dmgText = $"{dmg} DMG";
                var attack = Stat.GetAttack(_target, damageAbility, skill);
                var defense = Stat.CalculateDefense(damageStat, skillRank, 0);
                var (min, max) = Combat.CalculateDamageRange(attack, dmg, damageStat, defense, damageStat, 0);
                var tooltip = $"Est. Damage: {min} - {max}";

                return (dmgText, tooltip);
            }

            var mainHand = GetItemInSlot(InventorySlot.RightHand, _target);
            var offHand = GetItemInSlot(InventorySlot.LeftHand, _target);
            var mainHandType = GetBaseItemType(mainHand);

            if (GetIsObjectValid(mainHand))
            {
                var dmgInfo = GetCombatInfo(mainHand);
                MainHandDMG = dmgInfo.Item1;
                MainHandTooltip = dmgInfo.Item2;
            }
            else
            {
                MainHandDMG = "-";
                MainHandTooltip = "Est. Damage: N/A";
            }

            if (GetIsObjectValid(offHand))
            {
                var dmgInfo = GetCombatInfo(offHand);
                OffHandDMG = dmgInfo.Item1;
                OffHandTooltip = dmgInfo.Item2;
            }
            else
            {
                OffHandDMG = "-";
                OffHandTooltip = "Est. Damage: N/A";
            }

            AbilityType damageStat;
            AbilityType accuracyStatOverride;

                damageStat = Item.GetWeaponDamageAbilityType(mainHandType);
                accuracyStatOverride = AbilityType.Invalid;

                // Strong Style (Lightsaber)
                if (Item.LightsaberBaseItemTypes.Contains(mainHandType) &&
                    Ability.IsAbilityToggled(_target, AbilityToggleType.StrongStyleLightsaber))
                {
                    damageStat = AbilityType.Might;
                    accuracyStatOverride = AbilityType.Perception;
                }
                // Strong Style (Saberstaff)
                if (Item.SaberstaffBaseItemTypes.Contains(mainHandType) &&
                    Ability.IsAbilityToggled(_target, AbilityToggleType.StrongStyleSaberstaff))
                {
                    damageStat = AbilityType.Might;
                    accuracyStatOverride = AbilityType.Perception;
                }

                // Flurry Style (Staff)
                if (Item.StaffBaseItemTypes.Contains(mainHandType) &&
                    GetHasFeat(FeatType.CrushingStyle, _target))
                {
                    damageStat = AbilityType.Perception;
                    accuracyStatOverride = AbilityType.Agility;
                }

                var mainHandSkill = Skill.GetSkillTypeByBaseItem(mainHandType);
            Attack = Stat.GetAttack(_target, damageStat, mainHandSkill);
            DefensePhysical = Stat.GetDefense(_target, CombatDamageType.Physical, AbilityType.Vitality);
            
            if (GetIsPC(_target))
            {
                var playerId = GetObjectUUID(_target);
                var dbPlayer = DB.Get<Player>(playerId);

                var fireDefense = (dbPlayer.Defenses[CombatDamageType.Fire]).ToString();
                var waterDefense = (dbPlayer.Defenses[CombatDamageType.Water]).ToString();
                var electricalDefense = (dbPlayer.Defenses[CombatDamageType.Electrical]).ToString();
                var iceDefense = (dbPlayer.Defenses[CombatDamageType.Ice]).ToString();

                var lightDefense = (dbPlayer.Defenses[CombatDamageType.Light]).ToString();
                var darknessDefense = (dbPlayer.Defenses[CombatDamageType.Darkness]).ToString();
                var windDefense = (dbPlayer.Defenses[CombatDamageType.Wind]).ToString();

                DefenseElemental = $"{fireDefense}/{waterDefense}/{electricalDefense}/{iceDefense}/{windDefense}/{lightDefense}/{darknessDefense}";
            }
            else
            {
                var npcStats = Stat.GetNPCStats(_target);
                var fireDefense = npcStats.Defenses.ContainsKey(CombatDamageType.Fire) ? npcStats.Defenses[CombatDamageType.Fire] : 0;
                var waterDefense = npcStats.Defenses.ContainsKey(CombatDamageType.Water) ? npcStats.Defenses[CombatDamageType.Water] : 0;
                var electricalDefense = npcStats.Defenses.ContainsKey(CombatDamageType.Electrical) ? npcStats.Defenses[CombatDamageType.Electrical] : 0;
                var iceDefense = npcStats.Defenses.ContainsKey(CombatDamageType.Ice) ? npcStats.Defenses[CombatDamageType.Ice] : 0;

                DefenseElemental = $"{fireDefense}/{waterDefense}/{electricalDefense}/{iceDefense}";
            }

            Accuracy = Stat.GetAccuracy(_target, mainHand, accuracyStatOverride, SkillType.Invalid);
            Evasion = Stat.GetEvasion(_target, SkillType.Invalid);

            ///var smithery = Stat.CalculateControl(_target, SkillType.Smithery);
            //var fabrication = Stat.CalculateControl(_target, SkillType.Fabrication);


            //smithery = Stat.CalculateCraftsmanship(_target, SkillType.Smithery);
            //fabrication = Stat.CalculateCraftsmanship(_target, SkillType.Fabrication);
        }

        private void RefreshAttributes()
        {
            if (GetIsPC(_target))
            {
                var playerId = GetObjectUUID(_target);
                var dbPlayer = DB.Get<Player>(playerId);

                SP = $"{dbPlayer.TotalSPAcquired} / {Skill.SkillCap} ({dbPlayer.UnallocatedSP})";
                JP = $"{dbPlayer.TotalJPAcquired}";
                APOrLevel = $"{dbPlayer.TotalAPAcquired} / {Skill.APCap} ({dbPlayer.UnallocatedAP})";
            }
        }

        private void RefreshPortrait()
        {
            PortraitResref = GetPortraitResRef(_target) + "l";
        }

        private void LoadData()
        {
            CharacterType =  "Standard";
            Race = GetStringByStrRef(Convert.ToInt32(Get2DAString("racialtypes", "Name", (int)GetRacialType(_target))), GetGender(_target));

            if (IsPlayerMode)
            {
                APOrLevelLabel = "AP";
                APOrLevelTooltip = "Ability Points - Used to increase your attributes.";
            }
            else
            {
                APOrLevelLabel = "Level";
            }

            RefreshPortrait();
            RefreshStats();
            RefreshEquipmentStats();
            RefreshAttributes();
        }
        
        protected override void Initialize(CharacterSheetPayload initialPayload)
        {
            _target = GetIsObjectValid(initialPayload.Target) ? initialPayload.Target : Player;
            IsPlayerMode = initialPayload.IsPlayerMode;
            ShowSP = IsPlayerMode;
            ShowAPOrLevel = ShowSP;

            LoadData();
        }

        public void Refresh(ChangePortraitRefreshEvent payload)
        {
            RefreshPortrait();
        }

        public void Refresh(SkillXPRefreshEvent payload)
        {
            if (!GetIsPC(_target))
                return;

            var playerId = GetObjectUUID(_target);
            var dbPlayer = DB.Get<Player>(playerId);

            SP = $"{dbPlayer.TotalSPAcquired}";
            APOrLevel = $"{dbPlayer.TotalAPAcquired} / {Skill.APCap} ({dbPlayer.UnallocatedAP})";
            
            RefreshStats();
        }



        public void Refresh(EquipItemRefreshEvent payload)
        {
            RefreshEquipmentStats();
        }

        public void Refresh(UnequipItemRefreshEvent payload)
        {
            RefreshStats();
            RefreshEquipmentStats();
        }

        public void Refresh(StatusEffectReceivedRefreshEvent payload)
        {
            RefreshStats();
            RefreshEquipmentStats();
        }

        public void Refresh(StatusEffectRemovedRefreshEvent payload)
        {
            RefreshStats();
            RefreshEquipmentStats();
        }
    }
}
