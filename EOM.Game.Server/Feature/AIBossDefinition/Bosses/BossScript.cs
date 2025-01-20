using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.AIDefinition;
using EOM.Game.Server.Service.AIService;
using EOM.Game.Server.Service;
using EOM.Game.Server.Core.NWNX;


namespace EOM.Game.Server.Feature.AIBossDefinition.Bosses
{
    public static class BossScript
    {
        public static void BossHook(int bossIndex, uint boss)
        {           

            switch (bossIndex)
            {
                case 0:
                    SpiderDefinition.SpiderBoi(boss);
                    break;
            }

        }


        public static void PhaseChange(uint boss)
        {
            var iChanging = GetLocalInt(boss, "PH");

            if (iChanging != 1)
            {
                var oArea = GetArea(boss);
                MusicBackgroundStop(oArea);

                PhaseChange2(boss);
                SetLocalInt(boss, "PH", 1);
            }
        }

        public static void PhaseChange2(uint boss)
        {
            // effect eBlast = EffectVisualEffect(VFX_FNF_MYSTICAL_EXPLOSION);
            Effect eRed = EffectVisualEffect(VisualEffect.Vfx_Dur_Glow_Grey);
            Effect eRedFlash = EffectVisualEffect(VisualEffect.Vfx_Monster_Death);
            Effect eDeath = EffectDeath(false, true);
            Effect ePara = EffectCutsceneParalyze();
            Effect eParaPC = EffectCutsceneImmobilize();
            Effect eParaPC2 = EffectSilence();
            

            Location lThis = GetLocation(boss);

            Vector3 bossPosition = GetPosition(boss);
            bossPosition.Z += 2.0f;
            Location placeableLoc = Location(GetArea(boss),bossPosition,GetFacing(boss));


            CombatPoint.DistributeSkillXPBoss(boss);
            CombatPoint.CleanUpCombatPointsBoss(boss);

            uint oTarget = GetFirstObjectInShape(Shape.Sphere, 80.0f, lThis, false, ObjectType.Creature);
            while (GetIsObjectValid(oTarget))
            {
                if (GetTag(oTarget) == "")
                {
                    ApplyEffectToObject(DurationType.Temporary, eParaPC, oTarget, 10.0f);
                    ApplyEffectToObject(DurationType.Temporary, eParaPC2, oTarget, 10.0f);
                }
                else
                {
                    ApplyEffectToObject(DurationType.Temporary, ePara, oTarget, 13.0f);
                }
                oTarget = GetNextObjectInShape(Shape.Sphere, 60.0f, lThis, false, ObjectType.Creature);
            }
            var chestTag = GetLocalString(boss, "BOSS_CHEST_TAG");
            
            
            uint invisPlaceable = CreateObject(ObjectType.Placeable, "invisplaceholder", placeableLoc);
            
            DelayCommand(2.5f, () => ApplyEffectToObject(DurationType.Temporary, eRedFlash, invisPlaceable, 5.0f));
            DelayCommand(2.5f, () => ApplyEffectToObject(DurationType.Permanent, eRed, boss));
            DelayCommand(4.5f, () => SetImmortal(boss, false));
            DelayCommand(4.5f, () => CreateObject(ObjectType.Placeable, chestTag, GetLocation(GetWaypointByTag("BOSS_CHEST"))));

            DelayCommand(5.0f, () => DestroyObject(boss));

            //DelayCommand(5.0f, () => ApplyEffectToObject(DurationType.Instant, eDeath, boss));
        }

        [NWNEventHandler("dam_hook")]
        public static void BossCalc()
        {
            var damData = DamagePlugin.GetDamageEventData();
            var target = OBJECT_SELF;
            var damageTotal = damData.Total;

            if (GetLocalInt(target, "IS_BOSS") == 1)
            {
                var bossCurHP = GetLocalInt(target,"BOSS_CUR_HP");
                bossCurHP -= damageTotal;
                SetLocalInt(target, "BOSS_CUR_HP", bossCurHP);
            }

            Console.WriteLine(IntToString(damageTotal));
        }
    }
}
