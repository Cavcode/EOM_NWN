using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Feature.GuiDefinition.Payload
{
    public class RPXPPayload: GuiPayloadBase
    {
        public string SkillName { get; set; }
        public int MaxRPXP { get; set; }
        public SkillType Skill { get; set; }
    }
}
