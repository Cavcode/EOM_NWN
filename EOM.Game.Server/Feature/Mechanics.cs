using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service;
using NWN.Native.API;
using EOM.Game.Server.Feature.AIBossDefinition.Bosses;
using static EOM.Game.Server.Feature.Telegraph;
using Pipelines.Sockets.Unofficial.Arenas;
using EOM.Game.Server.Service;
using ObjectType = EOM.Game.Server.Core.NWScript.Enum.ObjectType;


namespace EOM.Game.Server.Feature
{

    public class Mechanics
    {
        [NWNEventHandler("TELEGRAPH_HOOK")]
        public static void MechanicHook()
        {
            var evt = GetLastRunScriptEffectScriptType();
            var telegrapher = OBJECT_SELF;
            var area = GetArea(OBJECT_SELF);
            var eff = GetLastRunScriptEffect();
            var packed = GetEffectString(eff, 0);
            var unpacked = TelegraphUnpack(packed);
            var bossIndex = GetLocalInt(telegrapher,"BOSS_INDEX");

            Console.WriteLine("Name of PC in TelegraphHook: " + GetName(telegrapher));

            if (evt == (int)RunScriptEffectScriptType.OnApplied)
            {
                Console.WriteLine("Firing OnApply");
                OnApplied(telegrapher,area, eff, packed, unpacked, bossIndex);

            }
            else if (evt == (int)RunScriptEffectScriptType.OnInterval)
            {
                Console.WriteLine("Firing oninterval");
                OnInterval(telegrapher, area, eff, packed, unpacked, bossIndex);
            }
            else if (evt == (int)RunScriptEffectScriptType.OnRemoved)
            {
                Console.WriteLine("Firing onremoved");
                OnRemove(telegrapher, area, eff, packed, unpacked, bossIndex);
            }
        }

        public static void OnApplied(uint telegrapher, uint area, Effect eff, string packed, TelegraphData unpacked, int bossIndex)
        {
            var loc = Location(area, Vector3(unpacked.X, unpacked.Y, unpacked.Z), unpacked.Rotation);
            var invisO = CreateObject(ObjectType.Placeable, "inviso", loc);
            var effect = EffectVisualEffect(VisualEffect.Vfx_Mechanic_Marker, false, unpacked.SizeX);
            effect = TagEffect(effect, GetObjectUUID(telegrapher) + GetName(telegrapher));
            ApplyEffectToObject(DurationType.Temporary, effect, invisO, unpacked.Duration);
        }

        public static void OnInterval(uint telegrapher, uint area, Effect eff, string packed, TelegraphData unpacked, int bossIndex)
        {
            //var loc = Location(area, Vector3(unpacked.X, unpacked.Y, unpacked.Z), unpacked.Rotation);
            //ApplyEffectAtLocation(DurationType.Instant,
                //EffectVisualEffect(VisualEffect.Fnf_Dispel, false, unpacked.SizeX / 5.0f), loc);
        }
        public static void OnRemove(uint telegrapher, uint area, Effect eff, string packed, TelegraphData unpacked, int bossIndex)
        {
            Console.WriteLine("IN MECHANIC");

            var loc = Location(area, Vector3(unpacked.X, unpacked.Y, unpacked.Z), unpacked.Rotation);
            ApplyEffectAtLocation(DurationType.Instant,
                EffectVisualEffect((VisualEffect)unpacked.ImpEffect, false, unpacked.SizeX), loc);

            var overlapped = GetFirstObjectInShape(Shape.Sphere, unpacked.SizeX, loc);
            while (GetIsObjectValid(overlapped))
            {
                ApplyEffectToObject(DurationType.Instant, EffectVisualEffect((VisualEffect)unpacked.HitEffect), overlapped);
                //DelayCommand(0.0f,
                //ApplyEffectToObject(DurationType.Instant, EffectDamage(1, DAMAGE), overlapped));
                overlapped = GetNextObjectInShape(Shape.Sphere, unpacked.SizeX, loc);
            }
        }
    }
}