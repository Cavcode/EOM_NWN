﻿using Microsoft.Extensions.CommandLineUtils;

namespace EOM.CLI
{
    internal class Program
    {
        private static readonly HakBuilder _hakBuilder = new();
        private static readonly PlaceableBuilder _placeableBuilder = new();
        private static readonly LanguageBuilder _languageBuilder = new();
        private static readonly ModulePacker _modulePacker = new();
        private static readonly StructureItemCreator _structureItemCreator = new();
        private static readonly EnhancementItemBuilder _enhancementItemBuilder = new();
        private static readonly RecipeCodeBuilder _recipeCodeBuilder = new();
        private static readonly AdHocTool _adHocTool = new();
        private static readonly DeployBuild _deployBuild = new();

        static void Main(string[] args)
        {
            var app = new CommandLineApplication();

            // Set up the options.
            var adHocToolOption = app.Option(
                "-$|-a |--adhoc",
                "Ad-hoc code testing.",
                CommandOptionType.NoValue);
            
            var beastBuilderOption = app.Option(
                "-$|-b |--beast",
                "Beast code generator.",
                CommandOptionType.NoValue);

            var placeableOption = app.Option(
                "-$|-c |--placeable",
                "Generates utp files in json format for all of the entries found in placeables.2da.",
                CommandOptionType.NoValue
            );

            var droidItemOption = app.Option(
                "-$|-d |--droid",
                "Generates uti files in json format for all of the entries found in droid_item_template.tsv.",
                CommandOptionType.NoValue
            );

            var enhancementOption = app.Option(
                "-$|-e |--enhancement",
                "Generates uti files in json format for all of the entries found in enhancement_list.csv.",
                CommandOptionType.NoValue
            );

            var hakBuilderOption = app.Option(
                "-$|-k |--hak",
                "Builds hakpak files based on the hakbuilder.json configuration file.",
                CommandOptionType.NoValue
            );

            var languageBuilderOption = app.Option(
                "-$|-l |--language",
                "Generates code for use with the language system.",
                CommandOptionType.NoValue
            );

            var deployOption = app.Option(
                "-$|-o |--outputDeploy",
                "Deploys DLLs in the bin folder to the NWN dotnet directory.",
                CommandOptionType.NoValue
            );

            var modulePackerOption = app.Option(
                "-$|-p |--pack",
                "Packs a module at the specified path. Target must be the path to a .mod file.",
                CommandOptionType.SingleValue
            );

            var recipeOption = app.Option(
                "-$|-r |--recipe",
                "Generates code file for all of the recipes in the recipes.tsv file.",
                CommandOptionType.NoValue);

            var structureOption = app.Option(
                "-$|-s |--structure",
                "Generates uti files in json format for all of the StructureType.cs enum values.",
                CommandOptionType.NoValue);

            var moduleUnpackOption = app.Option(
                "-$|-u |--unpack",
                "Unpacks a module within the running directory. Target must be the path to a .mod file.",
                CommandOptionType.SingleValue
            );

            app.HelpOption("-? | -h | --help");

            app.OnExecute(() =>
            {
                if (deployOption.HasValue())
                {
                    _deployBuild.Process();
                }
                return 0;
            });

            app.Execute(args);
        }
    }
}