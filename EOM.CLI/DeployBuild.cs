﻿using System;
using System.IO;

namespace EOM.CLI
{
    internal class DeployBuild
    {
        private const string DebugServerPath = "../debugserver/";
        private const string DotnetPath = DebugServerPath + "dotnet";
        private const string HakPath = DebugServerPath + "hak";
        private const string ModulesPath = DebugServerPath + "modules";
        private const string TlkPath = DebugServerPath + "tlk";

        private readonly HakBuilder _hakBuilder = new();

        public void Process()
        {
            CreateDebugServerDirectory();
            CopyBinaries();
            BuildHaks();
            BuildModule();
        }

        private void CreateDebugServerDirectory()
        {
            Directory.CreateDirectory(DebugServerPath);
            Directory.CreateDirectory(DotnetPath);
            Directory.CreateDirectory(HakPath);
            Directory.CreateDirectory(ModulesPath);
            Directory.CreateDirectory(TlkPath);

            var source = new DirectoryInfo("../EOM.Game.Server/Docker");
            var target = new DirectoryInfo(DebugServerPath);

            CopyAll(source, target, "EOM.env");
        }

        private void CopyBinaries()
        {
            var binPath = "../EOM.Game.Server/bin/Debug/net8.0/";

            var source = new DirectoryInfo(binPath);
            var target = new DirectoryInfo(DotnetPath);

            CopyAll(source, target, string.Empty);
        }

        private void BuildHaks()
        {
            _hakBuilder.Process();
        }

        private void BuildModule()
        {
            var modulePath = "../Module/eom.mod";
            File.Copy(modulePath, ModulesPath + "/eom", true);
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
