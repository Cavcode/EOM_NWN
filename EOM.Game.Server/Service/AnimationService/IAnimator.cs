﻿using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;

namespace EOM.Game.Server.Service.AnimationService
{
    public interface IAnimator
    {
        VisualEffect Vfx { get; set; }

        AnimationEvent Event { get; set; }

        DurationType Duration { get; set; }

        public float Scale { get; set; }

        public void SetLocalVariables(uint oObject);
    }
}
