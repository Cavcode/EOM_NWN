using System.Collections.Generic;
using EOM.Game.Server.Core.NWScript.Enum;

namespace EOM.Game.Server.Service.AbilityService
{
    public interface IAbilityListDefinition
    {
        public Dictionary<FeatType, AbilityDetail> BuildAbilities();
    }
}
