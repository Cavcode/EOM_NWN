﻿using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.SkillService;

namespace EOM.Game.Server.Feature.GuiDefinition.Payload
{
    public class RecipesPayload: GuiPayloadBase
    {
        public SkillType Skill { get; set; }

        public RecipesPayload(SkillType skill)
        {
            Skill = skill;
        }
    }
}
