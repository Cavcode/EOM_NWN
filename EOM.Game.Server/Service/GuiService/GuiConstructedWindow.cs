﻿using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Service.GuiService.Component;

namespace EOM.Game.Server.Service.GuiService
{
    public class GuiConstructedWindow
    {
        public GuiWindowType Type { get; set; }
        public string WindowId { get; set; }
        public Json Window { get; set; }
        public CreatePlayerWindowDelegate CreatePlayerWindowAction { get; set; }
        public GuiRectangle InitialGeometry { get; set; }
        public Dictionary<string, Json> PartialViews { get; set; }

        public GuiConstructedWindow(
            GuiWindowType type, 
            string windowId, 
            Json window,
            GuiRectangle initialGeometry,
            Dictionary<string, Json> partialViews,
            CreatePlayerWindowDelegate createPlayerWindowAction)
        {
            Type = type;
            WindowId = windowId;
            Window = window;
            InitialGeometry = initialGeometry;
            PartialViews = partialViews;
            CreatePlayerWindowAction = createPlayerWindowAction;
        }
    }
}
