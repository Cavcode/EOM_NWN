using EOM.Game.Server.Core.Beamdog;
using EOM.Game.Server.Feature.GuiDefinition.ViewModel;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.GuiService.Component;
using NRediSearch.Aggregation;
using System;
using System.Data.Common;

namespace EOM.Game.Server.Feature.GuiDefinition
{
    public class JobsDefinition : IGuiWindowDefinition
    {
        private readonly GuiWindowBuilder<JobsViewModel> _builder = new();

        public GuiConstructedWindow BuildWindow()
        {
            _builder.CreateWindow(GuiWindowType.Jobs)
                .SetIsResizable(true)
                .SetIsCollapsible(true)
                .SetInitialGeometry(0, 0, 100f, 600f)
                .SetTitle("Jobs")
                .DefinePartialView(JobsViewModel.JobWarriorPartial, BuildWarrior)
                .AddColumn(column =>
                {
                    column.AddRow(row =>
                    {
                        row.AddList(template =>
                        {
                            template.AddCell(cell =>
                            {
                                cell.AddToggleButton()
                                    .BindText(model => model.JobOptions)
                                    .BindIsToggled(model => model.JobSelected)
                                    .BindOnClicked(model => model.OnSelectJob());
                            });
                        }).SetRowCount(5);
                    });

                    column.AddRow(row =>
                    {
                        row.AddPartialView(JobsViewModel.JobWarriorPartial);
                    });
                }).
                AddColumn(column2 =>
                    {
                        column2.AddRow(row2 =>
                        {
                            row2.AddLabel()
                                .BindText(model => model.SelectedJob)
                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                .SetHorizontalAlign(NuiHorizontalAlign.Center)
                                .SetHeight(25.0f);
                        });
                        column2.AddRow(row3 =>
                        {
                            row3.AddLabel()
                                .SetText("Job Rank: ")
                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                .SetHeight(25.0f);

                              row3.AddLabel()
                                  .BindText(model => model.SelectedJobRank)
                                  .SetVerticalAlign(NuiVerticalAlign.Top)
                                  .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                  .SetWidth(25.0f);

                        });
                        column2.AddRow(row4 =>
                        {
                            row4.AddLabel()
                                .SetText("Available Job Points: ")
                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                .SetHeight(25.0f);

                            row4.AddLabel()
                                .BindText(model => model.JobPoints)
                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                .SetHeight(25.0f)
                                .SetWidth(25.0f);
                        });
                        column2.AddRow(row5 =>
                        {
                            row5.AddLabel()
                                .BindText(model => model.SelectedPerkName)
                                .SetVerticalAlign(NuiVerticalAlign.Top)
                                .SetHorizontalAlign(NuiHorizontalAlign.Left)
                                .SetHeight(25.0f);
                        });
                        column2.AddRow(row5 =>
                        {
                            row5.AddText()
                                .BindText(model => model.SelectedPerkDetails);
                        });
                        column2.AddRow(row6 =>
                        {
                            row6.AddButton()
                                .SetText("Finalize")
                                .SetHeight(32f)
                                .SetWidth(412.0f);
                        });
                    }
                );





            return _builder.Build();
        }

        private void BuildWarrior(GuiGroup<JobsViewModel> partial)
        {
            partial.AddColumn(col =>
            {
                col.AddRow(rowBef1 =>
                {
                    rowBef1.AddLabel()
                        .SetText("Tier 1")
                        .SetVerticalAlign(NuiVerticalAlign.Top)
                        .SetHorizontalAlign(NuiHorizontalAlign.Center)
                        .SetHeight(25.0f);
                });

                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);
                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                });

                col.AddRow(rowBef2 =>
                {
                    rowBef2.AddLabel()
                        .SetText("Tier 2")
                        .SetVerticalAlign(NuiVerticalAlign.Top)
                        .SetHorizontalAlign(NuiHorizontalAlign.Center)
                        .SetHeight(25.0f);
                });

                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);
                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();
                });
                col.AddRow(rowBef2 =>
                {
                    rowBef2.AddLabel()
                        .SetText("Tier 3")
                        .SetVerticalAlign(NuiVerticalAlign.Top)
                        .SetHorizontalAlign(NuiHorizontalAlign.Center)
                        .SetHeight(25.0f);
                });

                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);
                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();
                });
                col.AddRow(rowBef2 =>
                {
                    rowBef2.AddLabel()
                        .SetText("Tier 4")
                        .SetVerticalAlign(NuiVerticalAlign.Top)
                        .SetHorizontalAlign(NuiHorizontalAlign.Center)
                        .SetHeight(25.0f);
                });

                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);
                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();
                });
                col.AddRow(rowBef2 =>
                {
                    rowBef2.AddLabel()
                        .SetText("Tier 5")
                        .SetVerticalAlign(NuiVerticalAlign.Top)
                        .SetHorizontalAlign(NuiHorizontalAlign.Center)
                        .SetHeight(25.0f);
                });

                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);
                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();

                    row.AddButtonImage()
                        .SetIsEnabled(true)
                        .SetImageResref("gui_xp2_logo")
                        .SetHeight(50.0f)
                        .SetWidth(50.0f)
                        .SetTooltip("yoyoyo");

                    row.AddSpacer();
                });
        });
        }
    }
}
