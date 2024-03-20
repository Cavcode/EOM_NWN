using System.Collections.Generic;
using EOM.Game.Server.Enumeration;
using EOM.Game.Server.Feature.DialogDefinition;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.ChatCommandService;

namespace EOM.Game.Server.Feature.ChatCommandDefinition
{
    public class DiceChatCommand: IChatCommandListDefinition
    {
        public Dictionary<string, ChatCommandDetail> BuildChatCommands()
        {
            var builder = new ChatCommandBuilder();

            builder.Create("dice")
                .Description("Opens the dice bag menu.")
                .Permissions(AuthorizationLevel.All)
                .Action((user, target, location, args) =>
                {
                    Dialog.StartConversation(user, user, nameof(DiceDialog));
                });

            return builder.Build();
        }
    }
}
