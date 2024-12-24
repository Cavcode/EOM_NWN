using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.AIDefinition;
using EOM.Game.Server.Service.AIService;
using EOM.Game.Server.Service;


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

                DelayCommand(1.2f, () => PhaseChange2(boss));
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

            DelayCommand(2.0f, () => ApplyEffectToObject(DurationType.Temporary, eRedFlash, boss, 0.5f));
            DelayCommand(3.5f, () => ApplyEffectToObject(DurationType.Temporary, eRedFlash, boss, 0.5f));
            //DelayCommand(4.5f, PlayAnimation(ANIMATION_LOOPING_CONJURE1, 1.3f, 120.0f));
            DelayCommand(4.8f, () => ApplyEffectToObject(DurationType.Permanent, eRed, boss));
            DelayCommand(6.3f, () => SetImmortal(boss, false));
            DelayCommand(6.8f, () => ApplyEffectToObject(DurationType.Instant, eDeath, boss));


        }
    }
}
