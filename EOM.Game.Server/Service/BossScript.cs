using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.VisualEffect;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Feature.AIDefinition;
using EOM.Game.Server.Service.AIService;
using static NWN.Native.API.CVirtualMachineScript.JmpData;

namespace EOM.Game.Server.Service
{
    public static class BossScript
    {
        [NWNEventHandler("crea_hb_aft")]
        public static void CreatureSpawn()
        {
            Console.Write(GetObjectType(OBJECT_SELF));
            Console.Write(GetName(OBJECT_SELF));
            DelayCommand(2.0f, () =>
            {
                ClearAllActions();
            }); 

        }
    }
}
