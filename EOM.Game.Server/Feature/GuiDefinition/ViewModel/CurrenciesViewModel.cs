﻿using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class CurrenciesViewModel: GuiViewModelBase<CurrenciesViewModel, GuiPayloadBase>,
        IGuiRefreshable<CurrencyRefreshEvent>
    {

        public GuiBindingList<string> CurrencyNames
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public GuiBindingList<int> CurrencyValues
        {
            get => Get<GuiBindingList<int>>();
            set => Set(value);
        }

        private void LoadData()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);

            var currencyNames = new GuiBindingList<string>();
            var currencyValues = new GuiBindingList<int>();

            foreach (var (currency, value) in dbPlayer.Currencies)
            {
                var detail = Currency.GetCurrencyDetail(currency);

                currencyNames.Add(detail.Name);
                currencyValues.Add(value);
            }

            CurrencyNames = currencyNames;
            CurrencyValues = currencyValues;
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            LoadData();
        }

        public void Refresh(CurrencyRefreshEvent payload)
        {
            LoadData();
        }
    }
}
