using System;
using System.Linq;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.DBService;
using EOM.Game.Server.Service.MigrationService;

namespace EOM.Game.Server.Feature.MigrationDefinition.ServerMigration
{
    public class _2_LegacyServerMigration: LegacyMigrationBase, IServerMigration
    {
        public int Version => 2;
        public void Migrate()
        {
            MigratePersistentStorageItems();
        }

        private void MigratePersistentStorageItems()
        {
            var query = new DBQuery<InventoryItem>();
            var itemCount = (int)DB.SearchCount(query);
            var items = DB.Search(query.AddPaging(itemCount, 0)).ToList();
            var tempStorage = GetObjectByTag("MIGRATION_STORAGE");

            foreach (var item in items)
            {
                if (item.StorageId != "BANK_CZ220" && item.StorageId != "BANK_VELES" && item.StorageId != "BANK_MONCALA")
                    continue;

                if (item.IconResref != "unknown_item")
                    continue;

                var deserialized = ObjectPlugin.Deserialize(item.Data);
                if (!GetIsObjectValid(deserialized))
                    continue;

                ObjectPlugin.AcquireItem(tempStorage, deserialized);

                WipeItemProperties(deserialized);
                Item.MarkLegacyItem(deserialized);
                WipeDescription(deserialized);
                WipeVariables(deserialized);
                CleanItemName(deserialized);

                item.Data = ObjectPlugin.Serialize(deserialized);
                DB.Set(item);

                DestroyObject(deserialized);
            }
        }
    }
}
