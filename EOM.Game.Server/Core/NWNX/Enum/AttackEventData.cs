

namespace EOM.Game.Server.Core.NWNX.Enum
{
    public class AttackEventData
    {
        public uint? Target { get; set; }
        public int Bludgeoning { get; set; }
        public int Pierce { get; set; }
        public int Slash { get; set; }
        public int Magical { get; set; }
        public int Acid { get; set; }
        public int Cold { get; set; }
        public int Divine { get; set; }
        public int Electrical { get; set; }
        public int Fire { get; set; }
        public int Negative { get; set; }
        public int Positive { get; set; }
        public int Sonic { get; set; }
        public int Base { get; set; }
        public int AttackNumber { get; set; } // 1-based index of the attack in current combat round
        public int AttackResult { get; set; } // 1=hit, 3=critical hit, 4=miss, 8=concealed
        public int WeaponAttackType { get; set; }//< 1=main hand, 2=offhand, 3-5=creature, 6=extra(haste), 7=unarmed, 8=unarmed extra
        public int SneakAttack { get; set; } // 0=neither, 1=sneak attack, 2=death attack, 3=both
        public int AttackType { get; set; } // 1=main hand, 2=offhand, 3-5=creature, 6=haste
        public int IsKillingBlow { get; set; }
        public int ToHitRoll { get; set; }
        public int ToHitModifier { get; set; }
        public int Custom1 { get; set; }
        public int Custom2 { get; set; }
        public int Custom3 { get; set; }
        public int Custom4 { get; set; }
        public int Custom5 { get; set; }
        public int Custom6 { get; set; }
        public int Custom7 { get; set; }
        public int Custom8 { get; set; }
        public int Custom9 { get; set; }
        public int Custom10 { get; set; }
        public int Custom11 { get; set; }
        public int Custom12 { get; set; }
        public int Custom13 { get; set; }
        public int Custom14 { get; set; }
        public int Custom15 { get; set; }
        public int Custom16 { get; set; }
        public int Custom17 { get; set; }
        public int Custom18 { get; set; }
        public int Custom19 { get; set; }
    }
}