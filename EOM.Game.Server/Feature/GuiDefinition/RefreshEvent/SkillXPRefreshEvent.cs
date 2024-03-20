using System.Collections.Generic;
using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Feature.GuiDefinition.RefreshEvent
{
    public class SkillXPRefreshEvent : IGuiRefreshEvent
    {
        public List<SkillType> ModifiedSkills { get; set; }

        public SkillXPRefreshEvent(List<SkillType> skillTypes)
        {
            ModifiedSkills = skillTypes;
        }
    }
}
