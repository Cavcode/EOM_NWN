using System.Collections.Generic;

namespace EOM.Game.Server.Service.PropertyService
{
    internal interface IPropertyLayoutListDefinition
    {
        public Dictionary<PropertyLayoutType, PropertyLayout> Build();
    }
}
