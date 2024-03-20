using EOM.Game.Server.Entity;
using EOM.Game.Server.Enumeration;

namespace EOM.Game.Server.Service.PerkService
{
    public class PerkRequirementCharacterType: IPerkRequirement
    {
        private readonly CharacterType _requiredCharacterType;

        public PerkRequirementCharacterType(CharacterType type)
        {
            _requiredCharacterType = type;
        }

        public string CheckRequirements(uint player)
        {
            var playerId = GetObjectUUID(player);
            var dbPlayer = DB.Get<Player>(playerId);

            if (dbPlayer.CharacterType != _requiredCharacterType)
            {
                return $"Only {_requiredCharacterType} character types may access this perk.";
            }

            return string.Empty;
        }

        public string RequirementText => $"Character Type: {Perk.GetCharacterType(_requiredCharacterType).Name}";
    }
}
