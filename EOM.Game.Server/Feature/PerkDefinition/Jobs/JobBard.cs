using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;

namespace EOM.Game.Server.Feature.PerkDefinition.Jobs
{
    public class GeneralPerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();

        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            BardTier1();

            return _builder.Build();
        }

        private void BardTier1()
        {
            //void ToggleDash(uint player)
            //{
            //    if (Ability.IsAbilityToggled(player, AbilityToggleType.Dash))
            //    {
            //        Ability.ToggleAbility(player, AbilityToggleType.Dash, false);
            //    }
            //}

            _builder.Create(PerkCategoryType.JobBard, PerkType.SeraphHymn)
                .Name("Seraph Hymn")

                .AddPerkLevel()
                .Description("A musical prayer to the heavens pleading for protection. Restores MP * Social to all allies.")
                .Price(2)
                .GrantsFeat(FeatType.SeraphHymn);

        }
    }
}
