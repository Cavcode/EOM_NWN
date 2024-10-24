﻿using EOM.Game.Server.Feature.GuiDefinition.ViewModel;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition
{
    public class PlayerStatusDefinition: IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<PlayerStatusViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.PlayerStatus)
                .SetInitialGeometry(0, 0, 180f, 70f)
                .SetTitle(null)
                .SetIsClosable(false)
                .SetIsResizable(false)
                .SetIsCollapsible(false)
                .SetIsTransparent(false)
                .SetShowBorder(true)
                .SetAcceptsInput(false)
                .AddColumn(col =>
                {
                    col.AddRow(row =>
                    {
                        row.AddProgressBar()
                            .BindValue(model => model.Bar1Progress)
                            .BindColor(model => model.Bar1Color)
                            .SetHeight(20f)
                            .AddDrawList(drawList =>
                            {
                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar1Label);
                                    text.SetBounds(5, 2, 110f, 50f);
                                    text.SetColor(255, 255, 255);
                                });

                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar1Value);
                                    text.BindBounds(model => model.RelativeValuePosition);
                                    text.SetColor(255, 255, 255);
                                });
                            });
                    });

                    col.AddRow(row =>
                    {

                        row.AddProgressBar()
                            .BindValue(model => model.Bar2Progress)
                            .BindColor(model => model.Bar2Color)
                            .SetHeight(20f)
                            .AddDrawList(drawList =>
                            {
                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar2Label);
                                    text.SetBounds(5, 2, 110f, 50f);
                                    text.SetColor(255, 255, 255);
                                });

                                drawList.AddText(text =>
                                {
                                    text.BindText(model => model.Bar2Value);
                                    text.BindBounds(model => model.RelativeValuePosition);
                                    text.SetColor(255, 255, 255);
                                });
                            });
                    });

                });

            return _builder.Build();
        }
    }
}
