﻿using EOM.Game.Server.Feature.GuiDefinition.Payload;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class ExamineItemViewModel: GuiViewModelBase<ExamineItemViewModel, ExamineItemPayload>
    {
        public string WindowTitle
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Description
        {
            get => Get<string>();
            set => Set(value);
        }

        public string ItemProperties
        {
            get => Get<string>();
            set => Set(value);
        }

        protected override void Initialize(ExamineItemPayload initialPayload)
        {
            WindowTitle = initialPayload.ItemName;
            Description = initialPayload.Description;
            ItemProperties = initialPayload.ItemProperties;
        }
    }
}
