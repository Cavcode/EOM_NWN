﻿using System.Collections.Generic;
using EOM.Game.Server.Service.CraftService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Feature.RecipeDefinition.SmitheryRecipeDefinition
{
    public class CloakRecipes : IRecipeListDefinition
    {
        private readonly RecipeBuilder _builder = new RecipeBuilder();

        public Dictionary<RecipeType, RecipeDetail> BuildRecipes()
        {
            Tier1();
            Tier2();
            Tier3();
            Tier4();
            Tier5();

            return _builder.Build();
        }

        private void Tier1()
        {
            // Battlemaster Cloak
            _builder.Create(RecipeType.BattlemasterCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("bm_cloak")
                .Level(8)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("lth_ruined", 5)
                .Component("fiberp_ruined", 3);

            // Spiritmaster Cloak
            _builder.Create(RecipeType.SpiritmasterCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("sm_cloak")
                .Level(8)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("lth_ruined", 5)
                .Component("fiberp_ruined", 3);

            // Combat Cloak
            _builder.Create(RecipeType.CombatCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("com_cloak")
                .Level(8)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("lth_ruined", 5)
                .Component("fiberp_ruined", 3);

            // Advent Cloak
            _builder.Create(RecipeType.AdventCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("advent_cloak")
                .Level(10)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("ref_veldite", 5)
                .Component("lth_ruined", 5)
                .Component("jade", 3);

            // Amateur Cloak
            _builder.Create(RecipeType.AmateurCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("engi_cloak_1")
                .Level(10)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("ref_veldite", 5)
                .Component("lth_ruined", 5)
                .Component("jade", 3);

            // Cloth Cloak
            _builder.Create(RecipeType.ClothCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("fabr_belt_1")
                .Level(10)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("ref_veldite", 5)
                .Component("lth_ruined", 5)
                .Component("jade", 3);

            // Chef Cloak
            _builder.Create(RecipeType.ChefCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("chef_cloak_1")
                .Level(10)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 1)
                .EnhancementSlots(RecipeEnhancementType.Armor, 1)
                .Component("ref_veldite", 5)
                .Component("lth_ruined", 5)
                .Component("jade", 3);
        }

        private void Tier2()
        {
            // Titan Cloak
            _builder.Create(RecipeType.TitanCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("tit_cloak")
                .Level(18)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_flawed", 5)
                .Component("fiberp_flawed", 3);

            // Vivid Cloak
            _builder.Create(RecipeType.VividCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("viv_cloak")
                .Level(18)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_flawed", 5)
                .Component("fiberp_flawed", 3);

            // Valor Cloak
            _builder.Create(RecipeType.ValorCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("val_cloak")
                .Level(18)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_flawed", 5)
                .Component("fiberp_flawed", 3);

            // Frontier Cloak
            _builder.Create(RecipeType.FrontierCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("frontier_cloak")
                .Level(20)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_scordspar", 5)
                .Component("lth_flawed", 5)
                .Component("agate", 3);

            // Worker Cloak
            _builder.Create(RecipeType.WorkerCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("engi_cloak_2")
                .Level(20)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_scordspar", 5)
                .Component("lth_flawed", 5)
                .Component("agate", 3);

            // Linen Cloak
            _builder.Create(RecipeType.LinenCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("fabr_cloak_2")
                .Level(20)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_scordspar", 5)
                .Component("lth_flawed", 5)
                .Component("agate", 3);

            // Velveteen Cloak
            _builder.Create(RecipeType.VelveteenCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("chef_cloak_2")
                .Level(20)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 2)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_scordspar", 5)
                .Component("lth_flawed", 5)
                .Component("agate", 3);
        }

        private void Tier3()
        {
            // Quark Cloak
            _builder.Create(RecipeType.QuarkCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("qk_cloak")
                .Level(28)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_good", 5)
                .Component("fiberp_good", 3);

            // Reginal Cloak
            _builder.Create(RecipeType.ReginalCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("reg_cloak")
                .Level(28)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_good", 5)
                .Component("fiberp_good", 3);

            // Forza Cloak
            _builder.Create(RecipeType.ForzaCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("for_cloak")
                .Level(28)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_good", 5)
                .Component("fiberp_good", 3);

            // Majestic Cloak
            _builder.Create(RecipeType.MajesticCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("majestic_cloak")
                .Level(30)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_plagionite", 5)
                .Component("lth_good", 5)
                .Component("citrine", 3);

            // Mechanic Cloak
            _builder.Create(RecipeType.MechanicCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("engi_cloak_3")
                .Level(30)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_plagionite", 5)
                .Component("lth_good", 5)
                .Component("citrine", 3);

            // Designer Cloak
            _builder.Create(RecipeType.DesignerCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("fabr_cloak_3")
                .Level(30)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_plagionite", 5)
                .Component("lth_good", 5)
                .Component("citrine", 3);

            // Silk Cloak
            _builder.Create(RecipeType.SilkCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("chef_cloak_3")
                .Level(30)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 3)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_plagionite", 5)
                .Component("lth_good", 5)
                .Component("citrine", 3);

        }

        private void Tier4()
        {
            // Argos Cloak
            _builder.Create(RecipeType.ArgosCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("ar_cloak")
                .Level(38)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_imperfect", 5)
                .Component("fiberp_imperfect", 3);

            // Grenada Cloak
            _builder.Create(RecipeType.GrenadaCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("gre_cloak")
                .Level(38)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_imperfect", 5)
                .Component("fiberp_imperfect", 3);

            // Survival Cloak
            _builder.Create(RecipeType.SurvivalCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("sur_cloak")
                .Level(38)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_imperfect", 5)
                .Component("fiberp_imperfect", 3);

            // Dream Cloak
            _builder.Create(RecipeType.DreamCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("dream_cloak")
                .Level(40)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_keromber", 5)
                .Component("lth_imperfect", 5)
                .Component("ruby", 3);

            // Devotion Cloak
            _builder.Create(RecipeType.DevotionCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("engi_cloak_4")
                .Level(40)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_keromber", 5)
                .Component("lth_imperfect", 5)
                .Component("ruby", 3);

            // Oasis Cloak
            _builder.Create(RecipeType.OasisCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("fabr_cloak_4")
                .Level(40)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_keromber", 5)
                .Component("lth_imperfect", 5)
                .Component("ruby", 3);

            // Vintage Cloak
            _builder.Create(RecipeType.VintageCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("chef_cloak_4")
                .Level(40)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 4)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_keromber", 5)
                .Component("lth_imperfect", 5)
                .Component("ruby", 3);
        }

        private void Tier5()
        {
            // Eclipse Cloak
            _builder.Create(RecipeType.EclipseCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("ec_cloak")
                .Level(48)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_high", 5)
                .Component("fiberp_high", 3);

            // Transcendent Cloak
            _builder.Create(RecipeType.TranscendentCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("tran_cloak")
                .Level(48)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_high", 5)
                .Component("fiberp_high", 3);

            // Supreme Cloak
            _builder.Create(RecipeType.SupremeCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("sup_cloak")
                .Level(48)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("lth_high", 5)
                .Component("fiberp_high", 3);

            // Eternal Cloak
            _builder.Create(RecipeType.EternalCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("eternal_cloak")
                .Level(50)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_jasioclase", 5)
                .Component("lth_high", 5)
                .Component("emerald", 3);

            // Skysteel Cloak
            _builder.Create(RecipeType.SkysteelCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("engi_cloak_5")
                .Level(50)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_jasioclase", 5)
                .Component("lth_high", 5)
                .Component("emerald", 3);

            // Rose Cloak
            _builder.Create(RecipeType.RoseCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("fabr_cloak_5")
                .Level(50)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_jasioclase", 5)
                .Component("lth_high", 5)
                .Component("emerald", 3);

            // Moonflame Cloak
            _builder.Create(RecipeType.MoonflameCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("chef_cloak_5")
                .Level(50)
                .Quantity(1)
                .RequirementPerk(PerkType.ArmorBlueprints, 5)
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_jasioclase", 5)
                .Component("lth_high", 5)
                .Component("emerald", 3);

            // Chaos Cloak
            _builder.Create(RecipeType.ChaosCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("ch_cloak")
                .Level(52)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .RequirementUnlocked()
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_arkoxit", 2)
                .Component("lth_high", 20)
                .Component("fiberp_high", 20)
                .Component("chiro_shard", 2)
                .Component("ref_veldite", 5)
                .Component("ref_scordspar", 5)
                .Component("ref_plagionite", 5)
                .Component("ref_keromber", 5);

            // Magus Cloak
            _builder.Create(RecipeType.MagusCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("mag_cloak")
                .Level(52)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .RequirementUnlocked()
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_arkoxit", 2)
                .Component("lth_high", 20)
                .Component("fiberp_high", 20)
                .Component("chiro_shard", 2)
                .Component("ref_veldite", 5)
                .Component("ref_scordspar", 5)
                .Component("ref_plagionite", 5)
                .Component("ref_keromber", 5);

            // Immortal Cloak
            _builder.Create(RecipeType.ImmortalCloak, SkillType.Blacksmithing)
                .Category(RecipeCategoryType.Cloak)
                .Resref("imm_cloak")
                .Level(52)
                .Quantity(1)
                .RequirementPerk(PerkType.AccessoryBlueprints, 5)
                .RequirementUnlocked()
                .EnhancementSlots(RecipeEnhancementType.Armor, 2)
                .Component("ref_arkoxit", 2)
                .Component("lth_high", 20)
                .Component("fiberp_high", 20)
                .Component("chiro_shard", 2)
                .Component("ref_veldite", 5)
                .Component("ref_scordspar", 5)
                .Component("ref_plagionite", 5)
                .Component("ref_keromber", 5);
        }
    }
}