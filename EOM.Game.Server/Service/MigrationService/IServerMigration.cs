namespace EOM.Game.Server.Service.MigrationService
{
    public interface IServerMigration
    {
        int Version { get; }
        void Migrate();
    }
}
