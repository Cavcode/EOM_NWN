using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using EOM.Game.Server.Core;
using EOM.Game.Server.Core.NWScript.Enum;
using EOM.Game.Server.Entity;
using EOM.Game.Server.Service.LanguageService;
using EOM.Game.Server.Service.StatusEffectService;
using JobType = EOM.Game.Server.Service.SkillService.SkillType;

namespace EOM.Game.Server.Service
{
    public static class Language
    {
        private static Dictionary<JobType, ITranslator> _translators = new Dictionary<JobType, ITranslator>();
        private static readonly TranslatorGeneric _genericTranslator = new TranslatorGeneric();

        /// <summary>
        /// When the module loads, create translators for every language and store them into cache.
        /// </summary>
        [NWNEventHandler("mod_load")]
        public static void LoadTranslators()
        {
            _translators = new Dictionary<JobType, ITranslator>
            {
                { JobType.Bothese, new TranslatorBothese() },
                { JobType.Catharese, new TranslatorCatharese() },
                { JobType.Cheunh, new TranslatorCheunh() },
                { JobType.Dosh, new TranslatorDosh() },
                { JobType.Droidspeak, new TranslatorDroidspeak() },
                { JobType.Huttese, new TranslatorHuttese() },
                { JobType.Mandoa,  new TranslatorMandoa() },
                { JobType.Shyriiwook, new TranslatorShyriiwook() },
                { JobType.Twileki, new TranslatorTwileki() },
                { JobType.Zabraki, new TranslatorZabraki() },
                { JobType.Togruti, new TranslatorTogruti() },
                { JobType.Rodese, new TranslatorRodese() },
                { JobType.Mirialan, new TranslatorMirialan() },
                { JobType.MonCalamarian, new TranslatorMonCalamarian() },
                { JobType.Ugnaught, new TranslatorUgnaught() },
                { JobType.KelDor, new TranslatorKelDor() },
                { JobType.Nautila, new TranslatorNautila() }
            };
        }

        public static string TranslateSnippetForListener(uint speaker, uint listener, JobType language, string snippet)
        {
            var translator = _translators.ContainsKey(language) ? _translators[language] : _genericTranslator;
            var languageSkill = Skill.GetSkillDetails(language);

            if (GetIsPC(speaker))
            {
                var playerId = GetObjectUUID(speaker);
                var dbSpeaker = DB.Get<Player>(playerId);
                // Get the rank and max rank for the speaker, and garble their English text based on it.
                var speakerSkillRank = dbSpeaker == null ? 
                    languageSkill.MaxRank : 
                    dbSpeaker.Skills[language].Rank;

                if (speakerSkillRank != languageSkill.MaxRank)
                {
                    var garbledChance = 100 - (int)((speakerSkillRank / (float)languageSkill.MaxRank) * 100);

                    var split = snippet.Split(' ');
                    for (var i = 0; i < split.Length; ++i)
                    {
                        if (Random.Next(100) <= garbledChance)
                        {
                            split[i] = new string(split[i].ToCharArray().OrderBy(s => (Random.Next(2) % 2) == 0).ToArray());
                        }
                    }

                    snippet = split.Aggregate((a, b) => a + " " + b);
                }
            }

            if (!GetIsPC(listener) || GetIsDM(listener))
            {
                // Short circuit for a DM or NPC - they will always understand the text.
                return snippet;
            }

            // Let's grab the max rank for the listener skill, and then we roll for a successful translate based on that.
            var listenerId = GetObjectUUID(listener);
            var dbListener = DB.Get<Player>(listenerId);
            var rank = dbListener == null ? 
                languageSkill.MaxRank : 
                dbListener.Skills[language].Rank;
            var maxRank = languageSkill.MaxRank;

            // Check for the Comprehend Speech concentration ability.
            var grantSenseXP = false;
            var statusEffectBonus = 0;
            if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech1))
                statusEffectBonus = 5;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech2))
                statusEffectBonus = 10;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech3))
                statusEffectBonus = 15;
            else if (StatusEffect.HasStatusEffect(listener, StatusEffectType.ComprehendSpeech4))
                statusEffectBonus = 20;

            if (statusEffectBonus > 0)
            {
                rank += statusEffectBonus;
                grantSenseXP = true;
            }

            // Ensure we don't go over the maximum.
            if (rank > maxRank)
                rank = maxRank;

            if (rank == maxRank || speaker == listener)
            {
                // Guaranteed success - return original.
                return snippet;
            }

            var textAsForeignLanguage = translator.Translate(snippet);

            if (rank != 0)
            {
                var englishChance = (int)((rank / (float)maxRank) * 100);

                var originalSplit = snippet.Split(' ');
                var foreignSplit = textAsForeignLanguage.Split(' ');

                var endResult = new StringBuilder();

                // WARNING: We're making the assumption that originalSplit.Length == foreignSplit.Length.
                // If this assumption changes, the below logic needs to change too.
                for (var i = 0; i < originalSplit.Length; ++i)
                {
                    if (Random.Next(100) <= englishChance)
                    {
                        endResult.Append(originalSplit[i]);
                    }
                    else
                    {
                        endResult.Append(foreignSplit[i]);
                    }

                    endResult.Append(" ");
                }

                textAsForeignLanguage = endResult.ToString();
            }

            var now = DateTime.Now.Ticks;
            var lastSkillUpLow = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW");
            var lastSkillUpHigh = GetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH");
            long lastSkillUp = lastSkillUpHigh;
            lastSkillUp = (lastSkillUp << 32) | (uint)lastSkillUpLow;
            var differenceInSeconds = (now - lastSkillUp) / 10000000;

            if (differenceInSeconds / 60 >= 2)
            {
                // Reward exp towards the language - we scale this with character count, maxing at 50 exp for 150 characters.
                // A bonus is given if listener's Social modifier is greater than zero.
                var amount = Math.Max(10, Math.Min(150, snippet.Length) / 3);
                var socialModifier = GetAbilityModifier(AbilityType.Social, listener);
                if (socialModifier > 0)
                {
                    amount += socialModifier * 10;
                }

                Skill.GiveSkillXP(listener, language, amount, false, false);

                // Grant Force XP if player is concentrating Comprehend Speech.
                if (grantSenseXP)
                    Skill.GiveSkillXP(listener, JobType.Force, amount * 10, false, false);

                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_LOW", (int)(now & 0xFFFFFFFF));
                SetLocalInt(listener, "LAST_LANGUAGE_SKILL_INCREASE_HIGH", (int)((now >> 32) & 0xFFFFFFFF));
            }

            return textAsForeignLanguage;
        }

        public static (byte, byte, byte) GetColor(JobType language)
        {
            byte r = 0;
            byte g = 0;
            byte b = 0;

            switch (language)
            {
                case JobType.Basic: r = 255; g = 255; b = 255; break;
                case JobType.Bothese: r = 132; g = 56; b = 18; break;
                case JobType.Catharese: r = 235; g = 235; b = 199; break;
                case JobType.Cheunh: r = 82; g = 143; b = 174; break;
                case JobType.Dosh: r = 166; g = 181; b = 73; break;
                case JobType.Droidspeak: r = 192; g = 192; b = 192; break;
                case JobType.Huttese: r = 162; g = 74; b = 10; break;
                case JobType.KelDor: r = 162; g = 162; b = 0; break;
                case JobType.Mandoa: r = 255; g = 215; b = 0; break;
                case JobType.Rodese: r = 82; g = 255; b = 82; break;
                case JobType.Shyriiwook: r = 149; g = 125; b = 86; break;
                case JobType.Togruti: r = 82; g = 82; b = 255; break;
                case JobType.Twileki: r = 65; g = 105; b = 225; break;
                case JobType.Zabraki: r = 255; g = 102; b = 102; break;
                case JobType.Mirialan: r = 77; g = 230; b = 215; break;
                case JobType.MonCalamarian: r = 128; g = 128; b = 192; break;
                case JobType.Ugnaught: r = 255; g = 193; b = 233; break;
                case JobType.Nautila: r = 76; g = 230; b = 104; break;
            }

            return (r, g, b);
        }

        public static string GetName(JobType language)
        {
            switch (language)
            {
                case JobType.Bothese: return "Bothese";
                case JobType.Catharese: return "Catharese";
                case JobType.Cheunh: return "Cheunh";
                case JobType.Dosh: return "Dosh";
                case JobType.Droidspeak: return "Droidspeak";
                case JobType.Huttese: return "Huttese";
                case JobType.KelDor: return "KelDor";
                case JobType.Mandoa: return "Mandoa";
                case JobType.Rodese: return "Rodese";
                case JobType.Shyriiwook: return "Shyriiwook";
                case JobType.Togruti: return "Togruti";
                case JobType.Twileki: return "Twi'leki";
                case JobType.Zabraki: return "Zabraki";
                case JobType.Mirialan: return "Mirialan";
                case JobType.MonCalamarian: return "Mon Calamarian";
                case JobType.Ugnaught: return "Ugnaught";
                case JobType.Nautila: return "Nautila";
            }

            return "Basic";
        }

        public static JobType GetActiveLanguage(uint obj)
        {
            var ret = GetLocalInt(obj, "ACTIVE_LANGUAGE");

            if (ret == 0)
            {
                return JobType.Basic;
            }

            return (JobType)ret;
        }

        public static void SetActiveLanguage(uint obj, JobType language)
        {
            if (language == JobType.Basic)
            {
                DeleteLocalInt(obj, "ACTIVE_LANGUAGE");
            }
            else
            {
                SetLocalInt(obj, "ACTIVE_LANGUAGE", (int)language);
            }
        }

        private static IEnumerable<LanguageCommand> _languages;

        public static IEnumerable<LanguageCommand> Languages
        {
            get
            {
                if (_languages == null)
                {
                    var languages = new List<LanguageCommand>
                    {
                        new LanguageCommand("Basic", JobType.Basic, new [] { "basic" }),
                        new LanguageCommand("Bothese", JobType.Bothese, new[] {"bothese"}),
                        new LanguageCommand("Catharese", JobType.Catharese, new []{"catharese"}),
                        new LanguageCommand("Cheunh", JobType.Cheunh, new []{"cheunh"}),
                        new LanguageCommand("Dosh", JobType.Dosh, new []{"dosh"}),
                        new LanguageCommand("Droidspeak", JobType.Droidspeak, new []{"droidspeak"}),
                        new LanguageCommand("Huttese", JobType.Huttese, new []{"huttese"}),
                        new LanguageCommand("KelDor", JobType.KelDor, new []{"keldor"}),
                        new LanguageCommand("Mando'a", JobType.Mandoa, new []{"mandoa"}),
                        new LanguageCommand("Mirialan", JobType.Mirialan, new []{"mirialan"}),
                        new LanguageCommand("Mon Calamarian", JobType.MonCalamarian, new []{"moncalamarian", "moncal"}),
                        new LanguageCommand("Nautila", JobType.Nautila, new []{ "nautilan" }),
                        new LanguageCommand("Rodese", JobType.Rodese, new []{"rodese", "rodian"}),
                        new LanguageCommand("Shyriiwook", JobType.Shyriiwook, new []{"shyriiwook", "wookieespeak"}),
                        new LanguageCommand("Togruti", JobType.Togruti, new []{"togruti"}),
                        new LanguageCommand("Twi'leki", JobType.Twileki, new []{"twileki", "ryl"}),
                        new LanguageCommand("Ugnaught", JobType.Ugnaught, new []{"ugnaught"}),
                        new LanguageCommand("Zabraki", JobType.Zabraki, new []{"zabraki", "zabrak"}),
                    };

                    _languages = languages;
                }

                return _languages;
            }
        }
    }
}
