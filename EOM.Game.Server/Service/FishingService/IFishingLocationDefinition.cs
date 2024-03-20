using System.Collections.Generic;

namespace EOM.Game.Server.Service.FishingService
{
    public interface IFishingLocationDefinition
    {
        public Dictionary<FishingLocationType, FishingLocationDetail> Build();
    }
}
