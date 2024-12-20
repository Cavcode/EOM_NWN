﻿using EOM.Game.Server.Core.Beamdog;
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
                .SetInitialGeometry(0, 0, 545f, 600f)
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
                        }).SetRowCount(5).SetHeight(220.0f).SetWidth(824.0f);
                        row.AddSpacer();


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
                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);

                    //.BindSelectedIndex(model => model.SelectedItemTypeIndex);
                    //.BindOnClicked(model => model.OnClickOutfits());
                });
                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);


                    row.AddOptions()
                        .AddOption("Big Papa Axe Slam")
                        .AddOption("Melee Nuke")
                        .AddOption("Ur mom")
                        .AddOption("Power up")
                        .AddOption("+1 Deez Nuts")
                        .SetWidth(5.0f)
                        .SetIsEnabled(true)
                        .SetIsEncouraged(true);
                    //.BindSelectedIndex(model => model.SelectedItemTypeIndex);
                    //.BindOnClicked(model => model.OnClickOutfits());
                });
                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);


                    row.AddOptions()
                        .AddOption("Big Papa Axe Slam")
                        .AddOption("Melee Nuke")
                        .AddOption("Ur mom")
                        .AddOption("Power up")
                        .AddOption("+2 Deez Nuts")
                        .SetWidth(5.0f)
                        .SetIsEnabled(false)
                        .SetDisabledTooltip("Need more points bruh");
                    //.BindSelectedIndex(model => model.SelectedItemTypeIndex);
                    //.BindOnClicked(model => model.OnClickOutfits());
                });
                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);


                    row.AddOptions()
                        .AddOption("Big Papa Axe Slam")
                        .AddOption("Melee Nuke")
                        .AddOption("Ur mom")
                        .AddOption("Power up")
                        .AddOption("+4 Deez Nuts")
                        .SetWidth(5.0f)
                        .SetIsEnabled(false)
                        .SetDisabledTooltip("Need more points bruh"); 
                    //.BindSelectedIndex(model => model.SelectedItemTypeIndex);
                    //.BindOnClicked(model => model.OnClickOutfits());
                });
                col.AddRow(row =>
                {
                    row.BindIsVisible(model => model.IsWarriorSelected);


                    row.AddOptions()
                        .AddOption("Daf suks")
                        .AddOption("Hey yo")
                        .AddOption("Ur f")
                        .AddOption("Pizza time")
                        .AddOption("+6 Deez Nuts")
                        .SetWidth(5.0f)
                        .SetIsEnabled(false)
                        .SetDisabledTooltip("Need more points bruh"); 
                    //.BindSelectedIndex(model => model.SelectedItemTypeIndex);
                    //.BindOnClicked(model => model.OnClickOutfits());
                });
                col.AddRow(row =>
                {
                    row.AddPartialView(JobsViewModel.JobWarriorPartial);
                });
            });
        }
    }
}
