using EOM.Game.Server.Service.SkillService;
using System;

namespace EOM.Game.Server.Core.NWScript.Enum.Item
{
    public enum BaseItem
    {
        Invalid = -1,
        ShortSword = 0,
        [Item("Gunblade",
            PhenoType.Custom1)]
        Longsword = 1,
        BattleAxe = 2,
        BastardSword = 3,
        LightFlail = 4,
        WarHammer = 5,
        Cannon = 6,
        Rifle = 7,
        Longbow = 8,
        LightMace = 9,
        Halberd = 10,
        Pistol = 11,
        TwoBladedSword = 12,
        GreatSword = 13,
        SmallShield = 14,
        Torch = 15,
        Armor = 16,
        Helmet = 17,
        [Item("Axes",
            PhenoType.Large)]
        GreatAxe = 18,
        Amulet = 19,
        Arrow = 20,
        Belt = 21,
        Dagger = 22,
        MiscSmall = 24,
        Bolt = 25,
        Boots = 26,
        Bullet = 27,
        Club = 28,
        MiscMedium = 29,
        Dart = 31,
        DireMace = 32,
        DoubleAxe = 33,
        MiscLarge = 34,
        HeavyFlail = 35,
        Gloves = 36,
        LightHammer = 37,
        HandAxe = 38,
        HealersKit = 39,
        Kama = 40,
        Katana = 41,
        Kukri = 42,
        MiscTall = 43,
        MagicRod = 44,
        MagicStaff = 45,
        MagicWand = 46,
        MorningStar = 47,
        Potions = 49,
        QuarterStaff = 50,
        Rapier = 51,
        Ring = 52,
        Scimitar = 53,
        Scroll = 54,
        Scythe = 55,
        LargeShield = 56,
        TowerShield = 57,
        ShortSpear = 58,
        Shuriken = 59,
        Sickle = 60,
        Sling = 61,
        ThievesTools = 62,
        ThrowingAxe = 63,
        TrapKit = 64,
        Key = 65,
        LargeBox = 66,
        MiscWide = 68,
        CreatureSlashWeapon = 69,
        CreaturePierceWeapon = 70,
        CreatureBludgeonWeapon = 71,
        CreatureSlashPierceWeapon = 72,
        CreatureItem = 73,
        Book = 74,
        SpellScroll = 75,
        Gold = 76,
        Gem = 77,
        Bracer = 78,
        MiscThin = 79,
        Cloak = 80,
        Grenade = 81,
        Encampment = 82,
        Lance = 92,
        Trumpet = 92,
        MoonOnAStick = 92,
        Trident = 95,
        BlankPotion = 101,
        BlankScroll = 102,
        BlankWand = 103,
        EnchantedPotion = 104,
        EnchantedScroll = 105,
        EnchantedWand = 106,
        DwarvenWarAxe = 108,
        CraftMaterialMedium = 109,
        CraftMaterialSmall = 110,
        Whip = 111,
        CraftBase = 112,
        OffHandPistol = 213,

        Katar = 310,

        Saberstaff = 511,
        Lightsaber = 512,
        Electronics1 = 513,

        BaseFuel = 515,
        Electronics2 = 516,
        Electronics3 = 517,
        Electronics4 = 518,
        Electronics5 = 519,
        Electronics6 = 520,
        Electronics7 = 521,
        Electronics8 = 522,
        Electronics9 = 523,

        Electroblade = 525,
        MiscMediumStackable = 526,
        ElectronicsStackable1 = 527,
        ElectronicsStackable2 = 528,
        ElectronicsStackable3 = 529,
        ElectronicsStackable4 = 530,
        ElectronicsStackable5 = 531,
        ElectronicsStackable6 = 532,
        ElectronicsStackable7 = 533,
        ElectronicsStackable8 = 534,
        ElectronicsStackable9 = 535,
        MiscellaneousSmallStackable = 536,
        TwinElectroBlade = 537,
        MiscellaneousThinStackable = 538,

    }
    public class ItemAttribute : Attribute
    {
        public string Name { get; set; }
        public PhenoType PhenoType { get; set; }


        public ItemAttribute(
            string name,
            PhenoType phenoType)

        {
            Name = name;
            PhenoType = phenoType;
        }
    }
}