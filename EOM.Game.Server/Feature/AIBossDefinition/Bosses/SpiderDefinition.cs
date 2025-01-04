using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Creature;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.AIDefinition;
using EOM.Game.Server.Service.AIService;
using EOM.Game.Server.Service;
using EOM.Game.Server.Feature.AIBossDefinition;
using EOM.Game.Server.Feature.AIBossDefinition;
using EOM.Game.Server.Service.SpawnService;
using static EOM.Game.Server.Feature.Telegraph;
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Feature.AIBossDefinition.Bosses
{
    public static class SpiderDefinition
    {
        public static void SpiderBoi(uint boss)
        {

            if (GetLocalInt(boss, "SHADOW_COMBAT_RUNNING") == 1)
                return;

            if (GetLocalInt(boss, "BOSS_CUR_HP") <= 0)
            {
                SetLocalInt(GetArea(boss), "StopMechanics", 1);
                SetLocalInt(boss, "SHADOW_COMBAT_RUNNING", 1);
                BossScript.PhaseChange(boss);
                return;
            }

            var oEnemy = Enmity.GetHighestEnmityTarget(boss);

            if (GetIsObjectValid(oEnemy) == true)
            {
                /*
                 object oWP = GetNearestObject(OBJECT_TYPE_WAYPOINT, boss);
                 if(GetDistanceToObject(oWP) > 1.0)
                 {
                   ActionMoveToObject(oWP, TRUE, 0.1f);
                   DelayCommand(1.0, TurnToFaceObject(oEnemy));
                 }
                */
                SetLocalInt(boss, "SHADOW_COMBAT_RUNNING", 1);


                
                System.Random random = new System.Random();

                var index = random.Next(0,2);

                SendMessageToPC(oEnemy, IntToString(index));

                switch (index)
                {
                    case 0:
                    {
                        Enmity.AttackHighestEnmityTarget(boss);
                        break;
                    }
                    case 1:
                    {
                        ClearAllActions();
                        SpawnAngryOrbs(boss, 5);
                        CastStinkyStuff(boss, oEnemy);
                        break;
                    }


                    default:
                        ClearAllActions();
                        SpeakString("hoobla schmoobla!");
                        break;
                }
            }

            DelayCommand(4.0f, () => SetLocalInt(boss, "SHADOW_COMBAT_RUNNING", 0));

            //SetCreatureOverrideAIScriptFinished(boss);
        }
       public static void SpawnAngryOrbs(uint pc, int count)
        {
            var pos = GetPosition(pc);

            int i;
            for (i = 0; i < count; ++i)
            {
                float telegraphRadius = 2.0f;
                float telegraphDuration = 5.0f;

                float x = pos.X + Random(150) / 10.0f - 7.5f;
                float y = pos.Y + Random(150) / 10.0f - 7.5f;

                Effect telegraph = TelegraphCreate(TELEGRAPH_SHAPE_SPHERE, 
                    x, 
                    y, 
                    pos.Z, 
                    0.0f, 
                    telegraphRadius, 
                    0.0f, 
                    telegraphDuration,
                    "inc_tele_e",
                    "inc_tele_e",
                    "inc_tele_e", 
                    0.0f,
                    (int)VisualEffect.Vfx_Com_Hit_Acid, 
                    (int)VisualEffect.Vfx_Imp_Pulse_Nature,
                    5);
                ApplyEffectToObject(DurationType.Temporary, telegraph, pc, telegraphDuration);
            }
        }
        public static void CastStinkyStuff(uint boss, uint target)
        {

            SpeakString("<cÿ¢0>~STICKY WEB!~</c>");
            // DelayCommand(fDelay+1.0, ActionCastSpellAtObject(906 , oEnemy, METAMAGIC_QUICKEN, TRUE));

            Effect eVis = EffectVisualEffect(VisualEffect.Dur_Web, false, 0.85f);

            AssignCommand(boss, () =>
            {
                ActionPlayAnimation(Animation.LoopingConjure1, 0.8f, 10.0f);

                ApplyEffectAtLocation(DurationType.Instant, eVis, GetLocation(target));
                DoAOE(target, 12);
        });

            float fDist1 = GetDistanceBetween(boss, target);
            float fDelay1 = fDist1 / (3.0f * log(fDist1) + 2.0f);


        }

        public static void DoAOE(uint target, int nDamage)
        {
            //effect eVisual = EffectVisualEffect(987);  //VFX_FLAME_TOSS
            // ApplyEffectAtLocation(DURATION_TYPE_INSTANT, eVisual, lPoint);
            Effect eDZ = EffectSlow();
            Effect eDamage;
            var lWhere = GetLocation(target);
            uint oTarget1 = GetFirstObjectInShape(Shape.Sphere, 6.0f, lWhere, false, ObjectType.Creature);
            while (GetIsObjectValid(oTarget1))
            {

                    eDamage = EffectDamage(nDamage + d2(1), DamageType.Acid, DamagePower.PlusSix);
                    ApplyEffectToObject(DurationType.Instant, eDamage, oTarget1);
                    ApplyEffectToObject(DurationType.Temporary, eDZ, oTarget1, 4.0f);
                
                oTarget1 = GetNextObjectInShape(Shape.Sphere, 6.0f, lWhere, false, ObjectType.Creature);
            }
        }
    }
}