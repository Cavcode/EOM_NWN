﻿using System.Collections.Generic;
using EOM.Game.Server.Service.PropertyService;

namespace EOM.Game.Server.Feature.PropertyLayoutDefinition
{
    public class MedicalCenterLayoutDefinition: IPropertyLayoutListDefinition
    {
        private readonly PropertyLayoutBuilder _builder = new();

        public Dictionary<PropertyLayoutType, PropertyLayout> Build()
        {
            MedicalCenter();

            return _builder.Build();
        }

        private void MedicalCenter()
        {
            _builder.Create(PropertyLayoutType.MedicalCenterStyle1)
                .PropertyType(PropertyType.MedicalCenter)
                .Name("Medical Center")
                .StructureLimit(80)
                .ItemStorageLimit(0)
                .BuildingLimit(0)
                .InitialPrice(0)
                .PricePerDay(0)
                .AreaInstance("medical_center");
        }
    }
}
