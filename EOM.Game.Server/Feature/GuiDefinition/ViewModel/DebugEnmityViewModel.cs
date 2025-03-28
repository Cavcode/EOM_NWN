﻿using EOM.Game.Server.Core;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class DebugEnmityViewModel: GuiViewModelBase<DebugEnmityViewModel, GuiPayloadBase>,
        IGuiRefreshable<EnmityChangedRefreshEvent>
    {
        [NWNEventHandler("enmity_changed")]
        public static void OnEnmityChanged()
        {
            foreach (var member in Party.GetAllPartyMembers(OBJECT_SELF))
            {
                Gui.PublishRefreshEvent(member, new EnmityChangedRefreshEvent());
            }
        }

        public GuiBindingList<string> EnmityDetails
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        private void RefreshData()
        {
            var enmityDetails = new GuiBindingList<string>();

            foreach (var member in Party.GetAllPartyMembers(Player))
            {
                var enmityValues = Enmity.GetEnmityTowardsAllEnemies(member);

                foreach (var (enemy, value) in enmityValues)
                {
                    var detail = $"{GetName(enemy)} -> {GetName(member)} = {value}enm";
                    enmityDetails.Add(detail);
                }
            }

            EnmityDetails = enmityDetails;
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            RefreshData();
        }

        public void Refresh(EnmityChangedRefreshEvent payload)
        {
            RefreshData();
        }
    }
}
