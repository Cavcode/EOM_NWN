using System;
using System.IO;

namespace EOM.CLI
{
    internal class DeployBuild
    {
        private const string DebugServerPath = "I:/Github/EOM_NWN/EOM.Game.Server";
        private string DotnetPath = DebugServerPath + "dotnet";
        private string HakPath = DebugServerPath + "hak";
        private string ModulesPath = DebugServerPath + "modules";
        private string TlkPath = DebugServerPath + "tlk";

        private readonly HakBuilder _hakBuilder = new();

        public void Process()
        {
            CreateDebugServerDirectory();
            CopyBinaries();
        }

        private void CreateDebugServerDirectory()
        {
            Directory.CreateDirectory(DebugServerPath);
            Directory.CreateDirectory(DotnetPath);
            Directory.CreateDirectory(HakPath);
            Directory.CreateDirectory(ModulesPath);
            Directory.CreateDirectory(TlkPath);

            var source = new DirectoryInfo("I:/Github/EOM_NWN/EOM.Game.Server/Docker");
            var target = new DirectoryInfo(DebugServerPath);

            CopyAll(source, target, "EOM.env");
        }

        private void CopyBinaries()
        {
            var binPath = "I:/Github/EOM_NWN/EOM.Game.Server/bin/Debug/net7.0/";

            var source = new DirectoryInfo(binPath);
            var target = new DirectoryInfo(DotnetPath);

            CopyAll(source, target, string.Empty);
        }


        private static void CopyAll(DirectoryInfo source, DirectoryInfo target, string excludeFile)
        {
            Directory.CreateDirectory(target.FullName);
            foreach (var fi in source.GetFiles())
            {
                var targetPath = Path.Combine(target.FullName, fi.Name);

                if (File.Exists(targetPath) && fi.Name == excludeFile)
                    continue;

                fi.CopyTo(targetPath, true);
            }
            foreach (var diSourceSubDir in source.GetDirectories())
            {
                var nextTargetSubDir =
                    target.CreateSubdirectory(diSourceSubDir.Name);
                CopyAll(diSourceSubDir, nextTargetSubDir, excludeFile);
            }
        }
    }
}
