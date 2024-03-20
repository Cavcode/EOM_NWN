using System;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.DBService;

namespace EOM.CLI
{
    internal class AdHocTool
    {
        public void Process()
        {
            Environment.SetEnvironmentVariable("NWNX_REDIS_HOST", "localhost");

            DB.Load();

            var query = new DBQuery<Player>()
                .AddFieldSearch(nameof(Player.Name), "Yasila", true);
            var entities = DB.Search(query);

            foreach (var entity in entities)
            {
                Console.WriteLine($"{entity.Name} = {entity.Id}");
            }

        }
    }
}
