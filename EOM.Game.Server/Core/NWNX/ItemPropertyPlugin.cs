using System;
using EOM.Game.Server.Core.NWNX.Enum;

namespace EOM.Game.Server.Core.NWNX
{

    public class ItempropPlugin
    {
        /// @addtogroup itemproperty ItemProperty
        /// Utility functions to manipulate the builtin itemproperty type.
        /// @{
        /// @file nwnx_itemprop.nss
        public const string NWNX_ItemProperty = "NWNX_ItemProperty";

        ///&lt; @private
        /// An unpacked itemproperty.
        /// Convert native itemproperty type to unpacked structure.
        /// <param name="ip">The itemproperty to convert.</param>
        /// <returns>A constructed NWNX_IPUnpacked.</returns>
        public static ItemPropertyUnpacked UnpackIP(System.IntPtr ip)
        {
            NWNXPushItemProperty(ip);
            NWNXCall(NWNX_ItemProperty, "UnpackIP");
            ItemPropertyUnpacked n = default;
            n.Id = NWNXPopString();
            n.Property = NWNXPopInt();
            n.SubType = NWNXPopInt();
            n.CostTable = NWNXPopInt();
            n.CostTableValue = NWNXPopInt();
            n.Param1 = NWNXPopInt();
            n.Param1Value = NWNXPopInt();
            n.UsesPerDay = NWNXPopInt();
            n.ChanceToAppear = NWNXPopInt();
            n.IsUseable = Convert.ToBoolean(NWNXPopInt());
            n.SpellId = NWNXPopInt();
            n.Creator = NWNXPopObject();
            n.Tag = NWNXPopString();
            return n;
        }

        /// Convert unpacked itemproperty structure to native type.
        /// <param name="ip">The NWNX_IPUnpacked structure to convert.</param>
        /// <returns>The itemproperty.</returns>
        public static System.IntPtr PackIP(ItemPropertyUnpacked n)
        {
            NWNXPushString(n.Tag);
            NWNXPushObject(n.Creator);
            NWNXPushInt(n.SpellId);
            NWNXPushInt(Convert.ToInt32(n.IsUseable));
            NWNXPushInt(n.ChanceToAppear);
            NWNXPushInt(n.UsesPerDay);
            NWNXPushInt(n.Param1Value);
            NWNXPushInt(n.Param1);
            NWNXPushInt(n.CostTableValue);
            NWNXPushInt(n.CostTable);
            NWNXPushInt(n.SubType);
            NWNXPushInt(n.Property);
            NWNXCall(NWNX_ItemProperty, "PackIP");
            return NWNXPopItemProperty();
        }

        /// Gets the active item property at the index
        /// <param name="oItem">- the item with the property</param>
        /// <param name="nIndex">- the index such as returned by some Item Events</param>
        /// <returns>A constructed NWNX_IPUnpacked, except for creator, and spell id.</returns>
        public static ItemPropertyUnpacked GetActiveProperty(uint oItem, int nIndex)
        {
            NWNXPushInt(nIndex);
            NWNXPushObject(oItem);
            NWNXCall(NWNX_ItemProperty, "GetActiveProperty");
            ItemPropertyUnpacked n = default;
            n.Property = NWNXPopInt();
            n.SubType = NWNXPopInt();
            n.CostTable = NWNXPopInt();
            n.CostTableValue = NWNXPopInt();
            n.Param1 = NWNXPopInt();
            n.Param1Value = NWNXPopInt();
            n.UsesPerDay = NWNXPopInt();
            n.ChanceToAppear = NWNXPopInt();
            n.IsUseable = Convert.ToBoolean(NWNXPopInt());
            n.Tag = NWNXPopString();
            return n;
        }

        // @}
    }

}