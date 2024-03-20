using EOM.Game.Server.Service.StatusEffectService;

namespace EOM.Game.Server.Service.AbilityService
{
    public class PlayerAuraDetail
    {
        public StatusEffectType Type { get; set; }
        public bool TargetsSelf { get; set; }
        public bool TargetsParty { get; set; }
        public bool TargetsNPCs { get; set; }

        public PlayerAuraDetail(StatusEffectType type, bool targetsSelf, bool targetsParty, bool targetsNPCs)
        {
            Type = type;
            TargetsSelf = targetsSelf;
            TargetsParty = targetsParty;
            TargetsNPCs = targetsNPCs;
        }
    }
}
