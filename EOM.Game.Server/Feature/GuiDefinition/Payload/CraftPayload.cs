using EOM.Game.Server.Service.CraftService;
using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.Payload
{
    public class CraftPayload: GuiPayloadBase
    {
        public RecipeType Recipe { get; set; }

        public CraftPayload(RecipeType recipe)
        {
            Recipe = recipe;
        }
    }
}
