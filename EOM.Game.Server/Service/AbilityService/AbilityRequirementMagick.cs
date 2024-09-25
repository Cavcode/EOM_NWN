namespace EOM.Game.Server.Service.AbilityService
{
    /// <summary>
    /// Adds a Magick requirement to activate a perk.
    /// </summary>
    public class AbilityRequirementMagick : IAbilityActivationRequirement
    {
        public int RequiredMP { get; }

        public AbilityRequirementMagick(int requiredMP)
        {
            RequiredMP = requiredMP;
        }

        public string CheckRequirements(uint player)
        {
            // DMs are assumed to be able to activate.
            if (GetIsDM(player)) return string.Empty;

            var Magick = Stat.GetCurrentMagick(player);

            if (Magick >= RequiredMP) return string.Empty;
            return $"Not enough Magick. (Required: {RequiredMP})";
        }

        public void AfterActivationAction(uint player)
        {
            if (GetIsDM(player)) return;

            Stat.ReduceMagick(player, RequiredMP);
        }
    }
}