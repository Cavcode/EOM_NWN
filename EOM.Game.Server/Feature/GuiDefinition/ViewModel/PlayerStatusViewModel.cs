using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.GuiService.Component;


namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    internal class PlayerStatusViewModel: GuiViewModelBase<PlayerStatusViewModel, GuiPayloadBase>,
        IGuiRefreshable<PlayerStatusRefreshEvent>
    {
        private int _screenHeight;
        private int _screenWidth;
        private int _screenScale;

        private static readonly GuiColor _hpColor = new GuiColor(139, 0, 0);
        private static readonly GuiColor _mpColor = new GuiColor(0, 0, 255);
        
        public GuiColor Bar1Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }
        
        public GuiColor Bar2Color
        {
            get => Get<GuiColor>();
            set => Set(value);
        }

        public string Bar1Label
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Label
        {
            get => Get<string>();
            set => Set(value);
        }

        public string Bar1Value
        {
            get => Get<string>();
            set => Set(value);
        }
        public string Bar2Value
        {
            get => Get<string>();
            set => Set(value);
        }


        public float Bar1Progress
        {
            get => Get<float>();
            set => Set(value);
        }
        public float Bar2Progress
        {
            get => Get<float>();
            set => Set(value);
        }


        public GuiRectangle RelativeValuePosition
        {
            get => Get<GuiRectangle>();
            set => Set(value);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            _screenHeight = -1;
            _screenScale = -1;
            _screenWidth = -1;
            
            UpdateWidget();
            UpdateAllData();
        }

        private void ToggleLabels(bool isCharacter)
        {
                Bar1Label = "HP:";
                Bar2Label = "MP:";
        }

        private void UpdateWidget()
        {
            var screenHeight = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiHeight);
            var screenWidth = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiWidth);
            var screenScale = GetPlayerDeviceProperty(Player, PlayerDevicePropertyType.GuiScale);

            if (_screenHeight != screenHeight ||
                _screenWidth != screenWidth ||
                _screenScale != screenScale)
            {
                const float WidgetWidth = 200f;
                const float WidgetHeight = 75f;
                const float XOffset = 255f;
                const float YOffset = 165f;

                var scale = screenScale / 100f;
                var x = screenWidth - XOffset * scale;
                var y = screenHeight - YOffset * scale;

                Geometry = new GuiRectangle(
                    x,
                    y,
                    WidgetWidth,
                    WidgetHeight);

                x = 60f * scale;
                RelativeValuePosition = new GuiRectangle(x, 2f, 110f, 50f);

                _screenHeight = screenHeight;
                _screenWidth = screenWidth;
                _screenScale = screenScale;
            }
        }

        private void UpdateHP()
        {
            var currentHP = GetCurrentHitPoints(Player);
            var maxHP = GetMaxHitPoints(Player);

            Bar1Value = $"{currentHP} / {maxHP}";
            Bar1Progress = maxHP <= 0 ? 0 : (float)currentHP / (float)maxHP > 1.0f ? 1.0f : (float)currentHP / (float)maxHP;
        }


        private void UpdateMP()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            var currentSTM = dbPlayer.Magick;
            var maxSTM = Stat.GetMaxMagick(Player, dbPlayer);

            Bar2Value = $"{currentSTM} / {maxSTM}";
            Bar2Progress = maxSTM <= 0 ? 0 : (float)currentSTM / (float)maxSTM > 1.0f ? 1.0f : (float)currentSTM / (float)maxSTM;
        }

        private void UpdateSingleData(PlayerStatusRefreshEvent.StatType type)
        {
                ToggleLabels(true);
                Bar1Color = _hpColor;
                Bar2Color = _mpColor;

                if (type == PlayerStatusRefreshEvent.StatType.HP)
                {
                    UpdateHP();
                }
                else if (type == PlayerStatusRefreshEvent.StatType.MP)
                {
                    UpdateMP();
                }
        }

        private void UpdateAllData()
        {
                ToggleLabels(true);
                Bar1Color = _hpColor;
                Bar2Color = _mpColor;
                UpdateHP();
                UpdateMP();
        }

        public void Refresh(PlayerStatusRefreshEvent payload)
        {
            UpdateWidget();
            UpdateSingleData(payload.Type);
        }
    }
}
