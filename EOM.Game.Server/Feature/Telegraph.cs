using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service;



namespace EOM.Game.Server.Feature
{
    public struct TelegraphData
    {
        public int Shape { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public float Rotation { get; set; }
        public float SizeX { get; set; }
        public float SizeY { get; set; }
        public float Duration { get; set; }
        public string OnStart { get; set; }
        public string OnUpdate { get; set; }
        public string OnFinish { get; set; }


        public TelegraphData(int shape, float x, float y, float z, float rotation, float sizeX, float sizeY, float duration, string onStart, string onUpdate, string onFinish)
        {
            Shape = shape;
            X = x;
            Y = y;
            Z = z;
            Rotation = rotation;
            SizeX = sizeX;
            SizeY = sizeY;
            Duration = duration;
            OnStart = onStart;
            OnUpdate = onUpdate;
            OnFinish = onFinish;
        }
    };


    public class Telegraph
    {
        const string _TELEGRAPH_NULL = "<null>";
        const int TELEGRAPH_SHAPE_NONE = 0;
        const int TELEGRAPH_SHAPE_SPHERE = 1;
        const int TELEGRAPH_SHAPE_CONE = 2;
        const int TARGET_FPS = 30;


        public static Effect TelegraphCreate(int shape,
            float x,
            float y,
            float z,
            float rotation,
            float sizeX,
            float sizeY,
            float duration,
            string onStart,
            string onUpdate,
            string onFinish,
            float updateInterval)
        {
            var telegraph = TelegraphStruct(shape, x, y, z, rotation, sizeX, sizeY, duration, onStart, onUpdate,
                onFinish);

            // TODO: Perhaps it would be faster to store the serialized telegraph as a JSON instead of a packed string, and only put the effect link ID in the string.
            //       Another option: Local variable cache in format "{linkID}.{fieldname}" stored on the module.
            //       I suspect it doesn't matter, but if dear reader spots this (or lots of string copies) in a profiler, now you know why.
            return EffectRunScript("inc_tele_e", "inc_tele_e", "inc_tele_e", updateInterval, TelegraphPack(telegraph));
        }

        public static TelegraphData TelegraphStruct(int shape,
            float x,
            float y,
            float z,
            float rotation,
            float sizeX,
            float sizeY,
            float duration,
            string onStart,
            string onUpdate,
            string onFinish)
        {
            TelegraphData telegraph = new TelegraphData(shape,
                 x,
                 y,
                 z,
                 rotation,
                 sizeX,
                 sizeY,
                 duration,
                 onStart,
                 onUpdate,
                 onFinish);
            return telegraph;
        }
        public static string TelegraphPack(TelegraphData unpacked)
        {
            return IntToString(unpacked.Shape) + " " + 
                   FloatToString(unpacked.X) + " " +
                   FloatToString(unpacked.Y) + " " +
                   FloatToString(unpacked.Z) + " " +
                   FloatToString(unpacked.Rotation) + " " + 
                   FloatToString(unpacked.SizeX) + " " + 
                   FloatToString(unpacked.SizeY) + " " + 
                   FloatToString(unpacked.Duration) + " " +
                   unpacked.OnStart + " " +
                   unpacked.OnUpdate + " " +
                   unpacked.OnFinish;
        }

        public static TelegraphData TelegraphUnpack(string packed)
        {
            string[] unpackedTelegraph = packed.Split(" ");

            string shape = unpackedTelegraph[0];
            string x = unpackedTelegraph[1];
            string y = unpackedTelegraph[2];
            string z = unpackedTelegraph[3];
            string rotation = unpackedTelegraph[4];
            string sizeX = unpackedTelegraph[5];
            string sizeY = unpackedTelegraph[6];
            string duration = unpackedTelegraph[7];
            string onStart = unpackedTelegraph[8];
            string onUpdate = unpackedTelegraph[9];
            string onFinish = unpackedTelegraph[10];

            return TelegraphStruct(
                StringToInt(shape),
                StringToFloat(x),
                StringToFloat(y),
                StringToFloat(z),
                StringToFloat(rotation),
                StringToFloat(sizeX),
                StringToFloat(sizeY),
                StringToFloat(duration),
                onStart == _TELEGRAPH_NULL ? "" : onStart,
                onUpdate == _TELEGRAPH_NULL ? "" : onUpdate,
                onFinish == _TELEGRAPH_NULL ? "" : onFinish);
        }
        public static string OnApply(uint area, uint telegrapher, Effect telegraphEff)
        {
            string telegraphId = GetEffectLinkId(telegraphEff);
            string packed = GetEffectString(telegraphEff, 0);
        TelegraphData unpacked = TelegraphUnpack(packed);

        int start = GetMicrosecondCounter();
        int end = start + FloatToInt(unpacked.Duration * 1000 * 1000);

        Json telegraphData = JsonObject();
        telegraphData = JsonObjectSet(telegraphData, "start", JsonInt(start));
        telegraphData = JsonObjectSet(telegraphData, "end", JsonInt(end));
        telegraphData = JsonObjectSet(telegraphData, "packed", JsonString(packed));

        Json telegraphIds = GetLocalJson(area, "TELEGRAPHS");
        telegraphIds = JsonGetType(telegraphIds) == JsonType.Null? JsonArray() : telegraphIds;
        telegraphIds = JsonArrayInsert(telegraphIds, JsonString(telegraphId));

        SetLocalString(area, telegraphId, unpacked.OnUpdate);
        SetLocalJson(area, telegraphId, telegraphData);
        SetLocalJson(area, "TELEGRAPHS", telegraphIds);

        return unpacked.OnStart;
        }

        public static string OnUpdate(uint area, uint telegrapher, Effect telegraphEff)
        {
            string telegraphId = GetEffectLinkId(telegraphEff);
            return GetLocalString(area, telegraphId);
        }

        public static string OnRemove(uint area, uint telegrapher, Effect telegraphEff)
        {
            string telegraphId = GetEffectLinkId(telegraphEff);
            Json telegraphData = GetLocalJson(area, telegraphId);

            string packed = JsonGetString(JsonObjectGet(telegraphData, "packed"));
            TelegraphData unpacked = TelegraphUnpack(packed);

        Json telegraphIds = GetLocalJson(area, "TELEGRAPHS");
        telegraphIds = JsonArrayDel(telegraphIds, JsonGetInt(JsonFind(telegraphIds, JsonString(telegraphId))));

        DeleteLocalString(area, telegraphId);
        DeleteLocalJson(area, telegraphId);
        SetLocalJson(area, "TELEGRAPHS", telegraphIds);


        return unpacked.OnFinish;
        }

        public static void UpdateShaderForAllPcs()
        {
            uint pc = GetFirstPC();
            while (pc != OBJECT_INVALID)
            {
                // TODO: This updates all telegraphs for all PCs when one of them starts or finished.
                // It's PROBABLY fine, but might be worth visiting in a profiler.
                UpdateShaderForPc(pc);
                pc = GetNextPC();
            }
        }

        public static void UpdateShaderForPc(uint pc)
        {
            uint area = GetArea(pc);
        
            Json telegraphs = GetLocalJson(area, "TELEGRAPHS");
            int telegraphCount = JsonGetLength(telegraphs);
            int telegraphCountToRender = telegraphCount > 8 ? 8 : telegraphCount;
            int telegraphCountToReset = 8 - telegraphCountToRender;
        
            int i;
            for (i = 0; i < telegraphCountToRender; ++i)
            {
                string telegraphId = JsonGetString(JsonArrayGet(telegraphs, i));
                Json telegraphJson = GetLocalJson(area, telegraphId);
        
                int start = JsonGetInt(JsonObjectGet(telegraphJson, "start"));
                int end = JsonGetInt(JsonObjectGet(telegraphJson, "end"));
                string packed = JsonGetString(JsonObjectGet(telegraphJson, "packed"));
                TelegraphData unpacked = TelegraphUnpack(packed);
        
                var uniformIdx = ShaderUniformType.Type1 + i;
                SetShaderUniformInt(pc, ShaderUniformType.Type1 + i, unpacked.Shape);
                SetShaderUniformVec(pc, ShaderUniformType.Type1 + (i * 2) + 0, unpacked.X, unpacked.Y, unpacked.Z, unpacked.Rotation);
                SetShaderUniformVec(pc, ShaderUniformType.Type1 + (i * 2) + 1, IntToFloat(start), IntToFloat(end), unpacked.SizeX, unpacked.SizeY);
            }

            for (i = 0; i < telegraphCountToReset; ++i)
            {
                var uniformIdx = ShaderUniformType.Type1 + telegraphCountToRender + i;
                SetShaderUniformInt(pc, uniformIdx, TELEGRAPH_SHAPE_NONE);
            }
        }

        [NWNEventHandler("mod_load")]
        public void UpdateShaderLerpTimer()
        {
            var counter = GetMicrosecondCounter();

            var pc = GetFirstPC();
            while (pc != OBJECT_INVALID)
            {
                SetShaderUniformInt(pc, ShaderUniformType.Type16, counter);
                pc = GetNextPC();
            }

            DelayCommand(1.0f, () => UpdateShaderLerpTimer());
        }
        [NWNEventHandler("TELEGRAPH_HOOK")]
        public void TelegraphHook()
        {
            Effect eff = GetLastRunScriptEffect();
            var evt = GetLastRunScriptEffectScriptType();

            var telegrapher = OBJECT_SELF;
            var area = GetArea(telegrapher);

            var script = "";

            if (evt == (int)RunScriptEffectScriptType.OnApplied)
            {
                script = OnApply(area, telegrapher, eff);
                UpdateShaderForAllPcs();
            }
            else if (evt == (int)RunScriptEffectScriptType.OnInterval)
            {
                script = OnUpdate(area, telegrapher, eff);
            }
            else if (evt == (int)RunScriptEffectScriptType.OnRemoved)
            {
                script = OnRemove(area, telegrapher, eff);
                UpdateShaderForAllPcs();
            }

            if (script != "")
            {
                ExecuteScript(script, telegrapher);
            }
        }
    }
}