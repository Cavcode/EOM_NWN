using EOM.Game.Server.Service.GuiService;

namespace EOM.Game.Server.Feature.GuiDefinition.Payload
{
    public class ChangePortraitPayload: GuiPayloadBase
    {
        public uint Target { get; set; }

        public ChangePortraitPayload(uint target)
        {
            Target = target;
        }
    }
}
