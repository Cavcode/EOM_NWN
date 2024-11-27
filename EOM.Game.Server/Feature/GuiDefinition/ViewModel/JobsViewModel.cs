using System;
using System.Collections.Generic;
using System.Linq;
using EOM.Game.Server.Core.NWNX;
using EOM.Game.Server.Core.NWNX.Enum;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Core.NWScript.Enum.Associate;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Extension;
using EOM.Game.Server.Feature.GuiDefinition.RefreshEvent;
using EOM.Game.Server.Service;
using EOM.Game.Server.Service.CurrencyService;
using EOM.Game.Server.Service.GuiService;
using EOM.Game.Server.Service.GuiService.Component;
using EOM.Game.Server.Service.JobService;
using EOM.Game.Server.Service.LogService;
using EOM.Game.Server.Service.PerkService;
using EOM.Game.Server.Service.StatusEffectService;
using Skill = EOM.Game.Server.Service.Skill;

namespace EOM.Game.Server.Feature.GuiDefinition.ViewModel
{
    public class JobsViewModel : GuiViewModelBase<JobsViewModel, GuiPayloadBase>
    {
        public const string JobWarriorElement = "JOB_WARRIOR_PARTIAL_VIEW";
        public const string JobMonkElement = "JOB_MONK_PARTIAL_VIEW";
        public const string JobGunbreakerElement = "JOB_GUNBREAKER_PARTIAL_VIEW";
        public const string JobWhiteMageElement = "JOB_WHITE_MAGE_PARTIAL_VIEW";
        public const string JobBlackMageElement = "JOB_BLACK_MAGE_PARTIAL_VIEW";

        public const string JobWarriorPartial = "JOB_WARRIOR_PARTIAL";
        public const string JobMonkPartial = "JOB_MONK_PARTIAL";
        public const string JobGunbreakerPartial = "JOB_GUNBREAKER_PARTIAL";
        public const string JobBlackMagePartial = "JOB_BLACK_MAGE_PARTIAL";
        public const string JobWhiteMagePartial = "JOB_WHITE_MAGE_PARTIAL";
        public GuiBindingList<string> JobOptions
        {
            get => Get<GuiBindingList<string>>();
            set => Set(value);
        }

        public JobsViewModel()
        {
            JobOptions = new GuiBindingList<string>();
        }
        public string SelectedJob
        {
            get => Get<string>();
            set => Set(value);
        }

        protected override void Initialize(GuiPayloadBase initialPayload)
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);

            Console.WriteLine("initializing");
            IsWarriorSelected = false;
            JobPoints = dbPlayer.UnallocatedJP;
            SelectedJob = String.Empty;
            JobIndex = String.Empty;
            LoadJobs();

            WatchOnClient(model => model.SelectedJob);
        }
        public bool IsWarriorSelected
        {
            get => Get<bool>();
            set => Set(value);
        }
        public int JobPoints
        {
            get => Get<int>();
            set => Set(value);
        }
        public bool IsMonkSelected
        {
            get => Get<bool>();
            set => Set(value);
        }

        public bool IsGunbreakerSelected
        {
            get => Get<bool>();
            set => Set(value);
        }
        public bool IsWhiteMageSelected
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string SelectedJobRank
        {
            get => Get<string>();
            set => Set(value);
        }
        public string SelectedPerkName
        {
            get => Get<string>();
            set => Set(value);
        }
        public string SelectedPerkDetails
        {
            get => Get<string>();
            set => Set(value);
        }
        public bool IsBlackMageSelected
        {
            get => Get<bool>();
            set => Set(value);
        }
        public GuiBindingList<bool> JobSelected
        {
            get => Get<GuiBindingList<bool>>();
            set => Set(value);
        }
        public string JobIndex
        {
            get => Get<string>();
            set => Set(value);
        }
        private void LoadJobs()
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);

            var perkButtonColors = new GuiBindingList<GuiColor>();
            var perkButtonIcons = new GuiBindingList<string>();
            var perkButtonTexts = new GuiBindingList<string>();
            var jobList = new GuiBindingList<string>();
            var jobs = Job.GetAllJobs();

            ChangePartialView(JobWarriorPartial, JobWarriorPartial);

            Console.WriteLine("firing in load jobs");

            foreach (var (jobType, attribute) in jobs)
            {
                Console.WriteLine(attribute.Name);
                jobList.Add(attribute.Name);
            }

            JobOptions = jobList;
        }

        public Action OnSelectJob() => () =>
        {
            var playerId = GetObjectUUID(Player);
            var dbPlayer = DB.Get<Player>(playerId);
            Console.WriteLine("Before index OnSelectJob");
            var index = NuiGetEventArrayIndex();
            Console.WriteLine("After index OnSelectJob");


            switch (index)
            {
                case 0:
                    SelectedJob = "Warrior";
                    SelectedJobRank = dbPlayer.WarriorRank.ToString();
                    Console.WriteLine(SelectedJobRank);
                    break;
                case 1:
                    SelectedJob = "Monk";
                    SelectedJobRank = dbPlayer.MonkRank.ToString();
                    break;
                case 2:
                    SelectedJob = "White Mage";
                    SelectedJobRank = dbPlayer.WhiteMageRank.ToString();
                    break;
                case 3:
                    SelectedJob = "Black Mage";
                    SelectedJobRank = dbPlayer.BlackMageRank.ToString();
                    break;
                case 4:
                    SelectedJob = "Gunbreaker";
                    SelectedJobRank = dbPlayer.GunbreakerRank.ToString();
                    break;
            }


            //JobSelected[SelectedJobIndex] = false;

            //SelectedJobIndex = index;
            //JobSelected[index] = true;


        };
    }
}
