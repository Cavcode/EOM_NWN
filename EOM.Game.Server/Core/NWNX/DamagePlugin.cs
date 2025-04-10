using EOM.Game.Server.Core.NWNX.Enum;

namespace EOM.Game.Server.Core.NWNX
{
    public class DamagePlugin

    {


        /// @addtogroup damage Damage
        /// Run a script before damage and attack events allowing for modification. Includes function to arbitrarily apply damage.
        /// @{
        /// @file nwnx_damage.nss
        public const string NWNX_Damage = "NWNX_Damage";

        ///&lt; @private
        /// @struct NWNX_Damage_DamageEventData
        /// Damage Event Data
        /// @struct NWNX_Damage_AttackEventData
        /// Attack Event Data
        /// @struct NWNX_Damage_DamageData
        /// Used for DealDamage
        /// Sets the script to run with a damage event.
        /// <param name="sScript">The script that will handle the damage event.</param>
        /// <param name="oOwner">An object if only executing for a specific object or OBJECT_INVALID for global.</param>
        public static void SetDamageEventScript(string sScript, uint oOwner = OBJECT_INVALID)
        {
            NWNXPushObject(oOwner);
            NWNXPushString(sScript);
            NWNXPushString("DAMAGE");
            NWNXCall(NWNX_Damage, "SetEventScript");
        }

        /// Get Damage Event Data
        /// <returns>A NWNX_Damage_DamageEventData struct.</returns>
        /// @note To use only in the Damage Event Script.
        public static DamageEventData GetDamageEventData()
        {

            NWNXCall(NWNX_Damage, "GetDamageEventData");

            var data = new DamageEventData
            {
                Damager = NWNXPopObject(),
                Bludgeoning = NWNXPopInt(),
                Pierce = NWNXPopInt(),
                Slash = NWNXPopInt(),
                Magical = NWNXPopInt(),
                Acid = NWNXPopInt(),
                Cold = NWNXPopInt(),
                Divine = NWNXPopInt(),
                Electrical = NWNXPopInt(),
                Fire = NWNXPopInt(),
                Negative = NWNXPopInt(),
                Positive = NWNXPopInt(),
                Sonic = NWNXPopInt(),
                Base = NWNXPopInt(),
                Custom1 = NWNXPopInt(),
                Custom2 = NWNXPopInt(),
                Custom3 = NWNXPopInt(),
                Custom4 = NWNXPopInt(),
                Custom5 = NWNXPopInt(),
                Custom6 = NWNXPopInt(),
                Custom7 = NWNXPopInt(),
                Custom8 = NWNXPopInt(),
                Custom9 = NWNXPopInt(),
                Custom10 = NWNXPopInt(),
                Custom11 = NWNXPopInt(),
                Custom12 = NWNXPopInt(),
                Custom13 = NWNXPopInt(),
                Custom14 = NWNXPopInt(),
                Custom15 = NWNXPopInt(),
                Custom16 = NWNXPopInt(),
                Custom17 = NWNXPopInt(),
                Custom18 = NWNXPopInt(),
                Custom19 = NWNXPopInt(),
            };
            return data;
        }

        /// Set Damage Event Data
        /// <param name="data">A NWNX_Damage_DamageEventData struct.</param>
        /// @note To use only in the Damage Event Script.
        public static void SetDamageEventData(DamageEventData data)
        {
            NWNXPushInt(data.Custom19);
            NWNXPushInt(data.Custom18);
            NWNXPushInt(data.Custom17);
            NWNXPushInt(data.Custom16);
            NWNXPushInt(data.Custom15);
            NWNXPushInt(data.Custom14);
            NWNXPushInt(data.Custom13);
            NWNXPushInt(data.Custom12);
            NWNXPushInt(data.Custom11);
            NWNXPushInt(data.Custom10);
            NWNXPushInt(data.Custom9);
            NWNXPushInt(data.Custom8);
            NWNXPushInt(data.Custom7);
            NWNXPushInt(data.Custom6);
            NWNXPushInt(data.Custom5);
            NWNXPushInt(data.Custom4);
            NWNXPushInt(data.Custom3);
            NWNXPushInt(data.Custom2);
            NWNXPushInt(data.Custom1);
            NWNXPushInt(data.Base);
            NWNXPushInt(data.Sonic);
            NWNXPushInt(data.Positive);
            NWNXPushInt(data.Negative);
            NWNXPushInt(data.Fire);
            NWNXPushInt(data.Electrical);
            NWNXPushInt(data.Divine);
            NWNXPushInt(data.Cold);
            NWNXPushInt(data.Acid);
            NWNXPushInt(data.Magical);
            NWNXPushInt(data.Slash);
            NWNXPushInt(data.Pierce);
            NWNXPushInt(data.Bludgeoning);
            NWNXCall(NWNX_Damage, "SetDamageEventData");
        }

        /// Sets the script to run with an attack event.
        /// <param name="sScript">The script that will handle the attack event.</param>
        /// <param name="oOwner">An object if only executing for a specific object or OBJECT_INVALID for global.</param>
        public static void SetAttackEventScript(string sScript, uint oOwner = OBJECT_INVALID)
        {
            NWNXPushObject(oOwner);
            NWNXPushString(sScript);
            NWNXPushString("ATTACK");
            NWNXCall(NWNX_Damage, "SetEventScript");
        }

        /// Get Attack Event Data
        /// <returns>A NWNX_Damage_AttackEventData struct.</returns>
        /// @note To use only in the Attack Event Script.
        public static AttackEventData GetAttackEventData()
        {
            NWNXCall(NWNX_Damage, "GetAttackEventData");

            var data = new AttackEventData
            {
                Target = NWNXPopObject(),
                Bludgeoning = NWNXPopInt(),
                Pierce = NWNXPopInt(),
                Slash = NWNXPopInt(),
                Magical = NWNXPopInt(),
                Acid = NWNXPopInt(),
                Cold = NWNXPopInt(),
                Divine = NWNXPopInt(),
                Electrical = NWNXPopInt(),
                Fire = NWNXPopInt(),
                Negative = NWNXPopInt(),
                Positive = NWNXPopInt(),
                Sonic = NWNXPopInt(),
                Base = NWNXPopInt(),
                Custom1 = NWNXPopInt(),
                Custom2 = NWNXPopInt(),
                Custom3 = NWNXPopInt(),
                Custom4 = NWNXPopInt(),
                Custom5 = NWNXPopInt(),
                Custom6 = NWNXPopInt(),
                Custom7 = NWNXPopInt(),
                Custom8 = NWNXPopInt(),
                Custom9 = NWNXPopInt(),
                Custom10 = NWNXPopInt(),
                Custom11 = NWNXPopInt(),
                Custom12 = NWNXPopInt(),
                Custom13 = NWNXPopInt(),
                Custom14 = NWNXPopInt(),
                Custom15 = NWNXPopInt(),
                Custom16 = NWNXPopInt(),
                Custom17 = NWNXPopInt(),
                Custom18 = NWNXPopInt(),
                Custom19 = NWNXPopInt(),
                AttackNumber = NWNXPopInt(),
                AttackResult = NWNXPopInt(),
                WeaponAttackType = NWNXPopInt(),
                SneakAttack = NWNXPopInt(),
                IsKillingBlow = NWNXPopInt(),
                AttackType = NWNXPopInt(),
                ToHitRoll = NWNXPopInt(),
                ToHitModifier = NWNXPopInt()
            };


            return data;
        }

        /// Set Attack Event Data
        /// <param name="data">A NWNX_Damage_AttackEventData struct.</param>
        /// @note To use only in the Attack Event Script.
        /// @note Setting iSneakAttack will only change the attack roll message and floating text feedback. Immunities and damage will have already been resolved by the time the attack event script is ran.
        public static void SetAttackEventData(AttackEventData data)
        {
            NWNXPushInt(data.SneakAttack);
            NWNXPushInt(data.AttackResult);
            NWNXPushInt(data.Custom19);
            NWNXPushInt(data.Custom18);
            NWNXPushInt(data.Custom17);
            NWNXPushInt(data.Custom16);
            NWNXPushInt(data.Custom15);
            NWNXPushInt(data.Custom14);
            NWNXPushInt(data.Custom13);
            NWNXPushInt(data.Custom12);
            NWNXPushInt(data.Custom11);
            NWNXPushInt(data.Custom10);
            NWNXPushInt(data.Custom9);
            NWNXPushInt(data.Custom8);
            NWNXPushInt(data.Custom7);
            NWNXPushInt(data.Custom6);
            NWNXPushInt(data.Custom5);
            NWNXPushInt(data.Custom4);
            NWNXPushInt(data.Custom3);
            NWNXPushInt(data.Custom2);
            NWNXPushInt(data.Custom1);
            NWNXPushInt(data.Base);
            NWNXPushInt(data.Sonic);
            NWNXPushInt(data.Positive);
            NWNXPushInt(data.Negative);
            NWNXPushInt(data.Fire);
            NWNXPushInt(data.Electrical);
            NWNXPushInt(data.Divine);
            NWNXPushInt(data.Cold);
            NWNXPushInt(data.Acid);
            NWNXPushInt(data.Magical);
            NWNXPushInt(data.Slash);
            NWNXPushInt(data.Pierce);
            NWNXPushInt(data.Bludgeoning);
            NWNXCall(NWNX_Damage, "SetAttackEventData");
        }

        /// Deal damage to a target.
        /// @remark Permits multiple damage types and checks enhancement bonus for overcoming DR.
        /// <param name="data">A NWNX_Damage_DamageData struct.</param>
        /// <param name="oTarget">The target object on whom the damage is dealt.</param>
        /// <param name="oSource">The source of the damage.</param>
        /// <param name="iRanged">Whether the attack should be treated as ranged by the engine (for example when considering damage inflicted by Acid Sheath and other such effects)</param>
        public static void DealDamage(DamageData data, uint oTarget, uint oSource, int iRanged = 0)
        {
            NWNXPushInt(iRanged);
            NWNXPushInt(data.Power);
            NWNXPushInt(data.Custom19);
            NWNXPushInt(data.Custom18);
            NWNXPushInt(data.Custom17);
            NWNXPushInt(data.Custom16);
            NWNXPushInt(data.Custom15);
            NWNXPushInt(data.Custom14);
            NWNXPushInt(data.Custom13);
            NWNXPushInt(data.Custom12);
            NWNXPushInt(data.Custom11);
            NWNXPushInt(data.Custom10);
            NWNXPushInt(data.Custom9);
            NWNXPushInt(data.Custom8);
            NWNXPushInt(data.Custom7);
            NWNXPushInt(data.Custom6);
            NWNXPushInt(data.Custom5);
            NWNXPushInt(data.Custom4);
            NWNXPushInt(data.Custom3);
            NWNXPushInt(data.Custom2);
            NWNXPushInt(data.Custom1);
            NWNXPushInt(0);
            NWNXPushInt(data.Sonic);
            NWNXPushInt(data.Positive);
            NWNXPushInt(data.Negative);
            NWNXPushInt(data.Fire);
            NWNXPushInt(data.Electrical);
            NWNXPushInt(data.Divine);
            NWNXPushInt(data.Cold);
            NWNXPushInt(data.Acid);
            NWNXPushInt(data.Magical);
            NWNXPushInt(data.Slash);
            NWNXPushInt(data.Pierce);
            NWNXPushInt(data.Bludgeoning);
            NWNXPushObject(oTarget);
            NWNXPushObject(oSource);
            NWNXCall(NWNX_Damage, "DealDamage");
        }

        // @}
    }


}