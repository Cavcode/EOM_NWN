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
        public int HitEffect { get; set; }

        public int ImpEffect { get; set; }
        public int Damage { get; set; }
        public TelegraphData(int shape, float x, float y, float z, float rotation, float sizeX, float sizeY, float duration, string onStart, string onUpdate, string onFinish, int hitEffect, int impEffect, int damage)
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
            HitEffect = hitEffect;
            ImpEffect = impEffect;
            Damage = damage;
        }
    };


    public class Telegraph
    {
        const string _TELEGRAPH_NULL = "<null>";
        public const int TELEGRAPH_SHAPE_NONE = 0;
        public const int TELEGRAPH_SHAPE_SPHERE = 1;
        public const int TELEGRAPH_SHAPE_CONE = 2;
        public const int TARGET_FPS = 30;


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
            float updateInterval,
            int hitEffect,
            int impEffect,
            int damage)
        {
            var telegraph = TelegraphStruct(shape, x, y, z, rotation, sizeX, sizeY, duration, onStart, onUpdate,
                onFinish, hitEffect, impEffect,damage);

            Console.WriteLine(telegraph.Shape);
            Console.WriteLine(telegraph.X);
            Console.WriteLine(telegraph.Y);
            Console.WriteLine(telegraph.Z);
            Console.WriteLine(telegraph.Rotation);
            Console.WriteLine(telegraph.SizeX);
            Console.WriteLine(telegraph.SizeY);
            Console.WriteLine(telegraph.Duration);
            Console.WriteLine(telegraph.OnStart);
            Console.WriteLine(telegraph.OnUpdate);
            Console.WriteLine(telegraph.OnFinish);

     

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
            string onFinish,
            int hitEffect,
            int impEffect,
            int damage)
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
                 onFinish,
                 hitEffect,
                 impEffect,
                 damage);
            return telegraph;
        }
        public static string TelegraphPack(TelegraphData unpacked)
        {
           var packedTelegraph = IntToString(unpacked.Shape) + " " +
                FloatToString(unpacked.X) + " " +
                FloatToString(unpacked.Y) + " " +
                FloatToString(unpacked.Z) + " " +
                FloatToString(unpacked.Rotation) + " " +
                FloatToString(unpacked.SizeX) + " " +
                FloatToString(unpacked.SizeY) + " " +
                FloatToString(unpacked.Duration) + " " +
                unpacked.OnStart + " " +
                unpacked.OnUpdate + " " +
                unpacked.OnFinish + " " + 
                unpacked.HitEffect + " " +
                unpacked.ImpEffect + " " + 
                unpacked.Damage;
           Console.WriteLine("Packed telegraph: " + packedTelegraph);

           return packedTelegraph;
        }

        public static string StringParse(string sSource, string sDelimiter = " ", int bRightToLeft = 0)
        {
            // Find the first delimiter.
            int nDelimIndex = FindSubString(sSource, sDelimiter);
            if (nDelimIndex < 0)
                // Delimiter not found; return the whole source string.
                return sSource;

            // For left-to-right, we're basically done.
            if (bRightToLeft == 0)
                return GetStringLeft(sSource, nDelimIndex);

            // For right-to-left, we need to find the last delimiter.
            int nLastDelim = 0;
            while (nDelimIndex >= 0)
            {
                nLastDelim = nDelimIndex;
                nDelimIndex = FindSubString(sSource, sDelimiter, nLastDelim + 1);
            }
            // Return everything after the last delimiter.
            int nRetLength = GetStringLength(sSource) - nLastDelim - GetStringLength(sDelimiter);
            return GetStringRight(sSource, nRetLength);
        }

       public static string StringRemoveParsed(string sSource, string sParsed, string sDelimiter = " ", int bRightToLeft = 0)
        {
            int nDelimLength = GetStringLength(sDelimiter);

            if (bRightToLeft == 0)
            {
                // Start after a string the length of sParsed.
                int nStart = GetStringLength(sParsed);
                if (nDelimLength > 0)
                    // Locate excess delimiters.
                    while (GetSubString(sSource, nStart, nDelimLength) == sDelimiter)
                        nStart += nDelimLength;
                // Remove the delimiters and the string the length of sParsed.
                return GetStringRight(sSource, GetStringLength(sSource) - nStart);
            }
            else
            {
                // Stop before a string the length of sParsed.
                int nEnd = GetStringLength(sSource) - GetStringLength(sParsed);
                if (nDelimLength > 0)
                    // Locate excess delimiters.
                    while (GetSubString(sSource, nEnd - nDelimLength, nDelimLength) == sDelimiter)
                        nEnd -= nDelimLength;
                // Remove the delimiters and the string the length of sParsed.
                return GetStringLeft(sSource, nEnd);
            }
        }

        public static TelegraphData TelegraphUnpack(string packed)
        {

            string shape = StringParse(packed);
            packed = StringRemoveParsed(packed, shape);

            string x = StringParse(packed);
            packed = StringRemoveParsed(packed, x);

            string y = StringParse(packed);
            packed = StringRemoveParsed(packed, y);

            string z = StringParse(packed);
            packed = StringRemoveParsed(packed, z);

            string rotation = StringParse(packed);
            packed = StringRemoveParsed(packed, rotation);

            string sizeX = StringParse(packed);
            packed = StringRemoveParsed(packed, sizeX);

            string sizeY = StringParse(packed);
            packed = StringRemoveParsed(packed, sizeY);

            string duration = StringParse(packed);
            packed = StringRemoveParsed(packed, duration);

            string onStart = StringParse(packed);
            packed = StringRemoveParsed(packed, onStart);

            string onUpdate = StringParse(packed);
            packed = StringRemoveParsed(packed, onUpdate);

            string onFinish = StringParse(packed);
            packed = StringRemoveParsed(packed, onFinish);

            string hitEffect = StringParse(packed);
            packed = StringRemoveParsed(packed, hitEffect);

            string impEffect = StringParse(packed);
            packed = StringRemoveParsed(packed, impEffect);

            string damage = StringParse(packed);
;

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
                onFinish == _TELEGRAPH_NULL ? "" : onFinish,
                StringToInt(hitEffect),
                StringToInt(impEffect),
                StringToInt(damage));
        }
        public static string OnApply(uint area, uint telegrapher, Effect telegraphEff)
        {
            string telegraphId = GetEffectLinkId(telegraphEff);
            string packed = GetEffectString(telegraphEff, 0);
            Console.WriteLine("only apply after geteffectstring: " + packed);
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

        Console.WriteLine("only apply after geteffectstring: before sendoff");
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

        public static void UpdateShaderLerpTimer()
        {
            var counter = GetMicrosecondCounter();

            var pc = GetFirstPC();
           while (pc != OBJECT_INVALID)
            {
                SetShaderUniformInt(pc, ShaderUniformType.Type16, counter);
                pc = GetNextPC();
            }

            DelayCommand(1.0f / 30, () => UpdateShaderLerpTimer());
        }

    }
}