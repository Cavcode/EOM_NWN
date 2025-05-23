﻿using EOM.Game.Server.Core;
using EOM.Game.Server.Core.Beamdog;

namespace EOM.Game.Server.Service.GuiService.Component
{
    public class GuiColor
    {
        /// <summary>
        /// The amount of red to use. 0-255
        /// </summary>
        public byte R { get; set; }

        /// <summary>
        /// The amount of green to use. 0-255
        /// </summary>
        public byte G { get; set; }

        /// <summary>
        /// The amount of blue to use. 0-255
        /// </summary>
        public byte B { get; set; }

        /// <summary>
        /// The amount of alpha (transparency) to use. 0-255
        /// </summary>
        public byte Alpha { get; set; }

        public GuiColor(byte red, byte green, byte blue, byte alpha = 255)
        {
            R = red;
            G = green;
            B = blue;
            Alpha = alpha;
        }

        public Json ToJson()
        {
            return Nui.Color(R, G, B, Alpha);
        }

        public static GuiColor Green => new(0, 255, 0);
        public static GuiColor Red => new(255, 0, 0);
        public static GuiColor RedDiff => new(255, 117, 24);
        public static GuiColor Cyan => new(0, 255, 255);
        public static GuiColor White => new(255, 255, 255);
        public static GuiColor Grey => new(169, 169, 169);

        public static GuiColor HPColor = new(139, 0, 0);
        public static GuiColor MPColor = new(0, 150, 255);
    }
}
