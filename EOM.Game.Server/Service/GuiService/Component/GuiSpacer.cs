using EOM.Game.Server.Core;
using EOM.Game.Server.Core.Beamdog;

namespace EOM.Game.Server.Service.GuiService.Component
{
    public class GuiSpacer<T> : GuiWidget<T, GuiSpacer<T>>
        where T: IGuiViewModel
    {
        /// <summary>
        /// Builds the Spacer element.
        /// </summary>
        /// <returns>Json representing the spacer element.</returns>
        public override Json BuildElement()
        {
            return Nui.Spacer();
        }
    }
}
