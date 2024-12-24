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
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Feature.AIBossDefinition.Bosses
{
    public static class SpiderDefinition
    {
        public static void SpiderBoi(uint boss)
        {

            if (GetLocalInt(boss, "SHADOW_COMBAT_RUNNING") == 1)
                return;

            if (GetCurrentHitPoints(boss) == 1)
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


                var nRoundDelay = Random(2);
                var fDelay = IntToFloat(nRoundDelay);
                var index = Random(6);

                SendMessageToPC(oEnemy, IntToString(index));

                switch (index)
                {
                    case 0:
                    {
                        DelayCommand(fDelay, () => Enmity.AttackHighestEnmityTarget(boss));
                        DelayCommand(fDelay + 1.5f, () => ClearAllActions());
                        break;
                    }
                    case 1:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                    }
                    case 2:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                    }
                    case 3:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                    }
                    case 4:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                        }
                    case 5:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                        }
                    case 6:
                    {
                        DelayCommand(fDelay, () => CastStinkyStuff(boss, oEnemy));
                        DelayCommand(fDelay + 0.5f, () => ActionWait(1.5f));
                        break;
                        }


                    default:
                        DelayCommand(fDelay, () => SpeakString("hoobla schmoobla!"));
                        DelayCommand(fDelay + 1.5f, () => ClearAllActions());
                        break;
                }
            }

            DelayCommand(4.0f, () => SetLocalInt(boss, "SHADOW_COMBAT_RUNNING", 0));

            //SetCreatureOverrideAIScriptFinished(boss);
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