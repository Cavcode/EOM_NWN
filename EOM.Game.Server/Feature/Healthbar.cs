using System;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Service;
using System.Reactive;

namespace EOM.Game.Server.Feature
{
    public static class Healthbar
    {

        public static void RefreshHealthbar(uint target)
        {

            if (GetLocalInt(target, "IS_BOSS") != 1)
                return;


            float percentage = IntToFloat(GetLocalInt(target, "BOSS_CUR_HP")) / IntToFloat(GetLocalInt(target,"BOSS_MAX_HP")) * 100;
            var nColourGrey = -1;
            var nColourRed = -1671191;
            var bar = "";

            for (int i = 0; i < FloatToInt(percentage); i++)
            {
                bar += "k";
            }

            if (percentage < 0)
            {
                bar = "";
            }

            uint player = GetFirstObjectInShape(Shape.Sphere, 100.0f, GetLocation(target), false, ObjectType.Creature);
            while (GetIsObjectValid(player))
            {
                if (GetIsPC(player))
                {
                    SetTextureOverride(
                        "fnt_console",
                        "fnt_es_gui",
                        player
                    );
                    PostString(player, "abbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbbc", 1, 5, ScreenAnchor.TopLeft, 0.0f, nColourGrey, nColourGrey, 1);
                    PostString(player, "diiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiif", 1, 6, ScreenAnchor.TopLeft, 0.0f, nColourGrey, nColourGrey, 5);
                    PostString(player, "diiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiif", 1, 7, ScreenAnchor.TopLeft, 0.0f, nColourGrey, nColourGrey, 6);
                    PostString(player, " " + bar, 1, 7, ScreenAnchor.TopLeft, 0.0f, nColourRed, nColourRed, 2);
                    PostString(player, "heeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeg", 1, 8, ScreenAnchor.TopLeft, 0.0f, nColourGrey, nColourGrey, 3);
                    PostString(player, GetName(target), 2, 6, ScreenAnchor.TopLeft, 0.0f, nColourGrey, nColourGrey, 4, "fnt_narcon");
                }
                player = GetNextObjectInShape(Shape.Sphere, 6.0f, GetLocation(target), true, ObjectType.Creature);
            }


            

        }
    }
}
