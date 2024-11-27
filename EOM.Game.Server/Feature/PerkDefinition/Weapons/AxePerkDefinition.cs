using System.Collections.Generic;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.AbilityService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.SkillService;
using Random = EOM.Game.Server.Service.Random;

namespace EOM.Game.Server.Feature.PerkDefinition.Weapons
{
    public class AxePerkDefinition : IPerkListDefinition
    {
        private readonly PerkBuilder _builder = new();
        public Dictionary<PerkType, PerkDetail> BuildPerks()
        {
            PowerfulGrip();

            return _builder.Build();
        }

        private void PowerfulGrip()
        {
            _builder.Create(PerkCategoryType.WeaponAxe, PerkType.PowerfulGrip)
                .Name("Powerful Grip")

                .AddPerkLevel()
                .Description(
                    "Grants the character a better grip on the axe, giving the wielder a large bonus to Damage.")
                .Price(3)
                .RequirementSkill(SkillType.Axes, 15)
                .GrantsFeat(FeatType.PowerfulGrip);

        }

    
    }
}
