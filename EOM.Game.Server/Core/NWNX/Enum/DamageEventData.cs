

namespace EOM.Game.Server.Core.NWNX.Enum
{
    public class DamageEventData
    {
        public uint Damager { get; set; }

        private int _bludgeoning;
        public int Bludgeoning
        {
            get => _bludgeoning;
            set
            {
                if (value < -1)
                    value = -1;

                _bludgeoning = value;
            }
        }

        private int _pierce;

        public int Pierce
        {
            get => _pierce;
            set
            {
                if (value < -1)
                    value = -1;

                _pierce = value;
            }
        }

        private int _slash;

        public int Slash
        {
            get => _slash;
            set
            {
                if (value < -1)
                    value = -1;

                _slash = value;
            }
        }

        private int _magical;
        public int Magical
        {
            get => _magical;
            set
            {
                if (value < -1)
                    value = -1;

                _magical = value;
            }
        }

        private int _acid;
        public int Acid
        {
            get => _acid;
            set
            {
                if (value < -1)
                    value = -1;

                _acid = value;
            }
        }

        private int _cold;
        public int Cold
        {
            get => _cold;
            set
            {
                if (value < -1)
                    value = -1;

                _cold = value;
            }
        }

        private int _divine;
        public int Divine
        {
            get => _divine;
            set
            {
                if (value < -1)
                    value = -1;

                _divine = value;
            }
        }

        private int _electrical;
        public int Electrical
        {
            get => _electrical;
            set
            {
                if (value < -1)
                    value = -1;

                _electrical = value;
            }
        }

        private int _fire;
        public int Fire
        {
            get => _fire;
            set
            {
                if (value < -1)
                    value = -1;

                _fire = value;
            }
        }

        private int _negative;
        public int Negative
        {
            get => _negative;
            set
            {
                if (value < -1)
                    value = -1;

                _negative = value;
            }
        }

        private int _positive;
        public int Positive
        {
            get => _positive;
            set
            {
                if (value < -1)
                    value = -1;

                _positive = value;
            }
        }

        private int _sonic;
        public int Sonic
        {
            get => _sonic;
            set
            {
                if (value < -1)
                    value = -1;

                _sonic = value;
            }
        }

        private int _base;
        public int Base
        {
            get => _base;
            set
            {
                if (value < -1)
                    value = -1;

                _base = value;
            }
        }

        private int _custom1;
        public int Custom1
        {
            get => _custom1;
            set
            {
                if (value < -1)
                    value = -1;

                _custom1 = value;
            }
        }

        private int _custom2;
        public int Custom2
        {
            get => _custom2;
            set
            {
                if (value < -1)
                    value = -1;

                _custom2 = value;
            }
        }

        private int _custom3;
        public int Custom3
        {
            get => _custom3;
            set
            {
                if (value < -1)
                    value = -1;

                _custom3 = value;
            }
        }

        private int _custom4;
        public int Custom4
        {
            get => _custom4;
            set
            {
                if (value < -1)
                    value = -1;

                _custom4 = value;
            }
        }

        private int _custom5;
        public int Custom5
        {
            get => _custom5;
            set
            {
                if (value < -1)
                    value = -1;

                _custom5 = value;
            }
        }

        private int _custom6;
        public int Custom6
        {
            get => _custom6;
            set
            {
                if (value < -1)
                    value = -1;

                _custom6 = value;
            }
        }

        private int _custom7;
        public int Custom7
        {
            get => _custom7;
            set
            {
                if (value < -1)
                    value = -1;

                _custom7 = value;
            }
        }

        private int _custom8;
        public int Custom8
        {
            get => _custom8;
            set
            {
                if (value < -1)
                    value = -1;

                _custom8 = value;
            }
        }

        private int _custom9;
        public int Custom9
        {
            get => _custom9;
            set
            {
                if (value < -1)
                    value = -1;

                _custom9 = value;
            }
        }

        private int _custom10;
        public int Custom10
        {
            get => _custom10;
            set
            {
                if (value < -1)
                    value = -1;

                _custom10 = value;
            }
        }

        private int _custom11;
        public int Custom11
        {
            get => _custom11;
            set
            {
                if (value < -1)
                    value = -1;

                _custom11 = value;
            }
        }


        private int _custom12;
        public int Custom12
        {
            get => _custom12;
            set
            {
                if (value < -1)
                    value = -1;

                _custom12 = value;
            }
        }

        private int _custom13;
        public int Custom13
        {
            get => _custom13;
            set
            {
                if (value < -1)
                    value = -1;

                _custom13 = value;
            }
        }

        private int _custom14;
        public int Custom14
        {
            get => _custom14;
            set
            {
                if (value < -1)
                    value = -1;

                _custom14 = value;
            }
        }

        private int _custom15;
        public int Custom15
        {
            get => _custom15;
            set
            {
                if (value < -1)
                    value = -1;

                _custom15 = value;
            }
        }

        private int _custom16;
        public int Custom16
        {
            get => _custom16;
            set
            {
                if (value < -1)
                    value = -1;

                _custom16 = value;
            }
        }

        private int _custom17;
        public int Custom17
        {
            get => _custom17;
            set
            {
                if (value < -1)
                    value = -1;

                _custom17 = value;
            }
        }

        private int _custom18;
        public int Custom18
        {
            get => _custom18;
            set
            {
                if (value < -1)
                    value = -1;

                _custom18 = value;
            }
        }

        private int _custom19;
        public int Custom19
        {
            get => _custom19;
            set
            {
                if (value < -1)
                    value = -1;

                _custom19 = value;
            }
        }

        private static int CalculateAdjustment(int original, float percent)
        {
            if (original <= -1) return -1;

            var output = (int)(original + original * percent);
            if (original <= 0)
                output = 0;

            return output;
        }

        /// <summary>
        /// Adjusts all damage amounts by a specified percentage.
        /// E.G: 0.5 will increase all values by 50%.
        /// </summary>
        /// <param name="percent"></param>
        public void AdjustAllByPercent(float percent)
        {
            Bludgeoning = CalculateAdjustment(Bludgeoning, percent);
            Pierce = CalculateAdjustment(Pierce, percent);
            Slash = CalculateAdjustment(Slash, percent);
            Magical = CalculateAdjustment(Magical, percent);
            Acid = CalculateAdjustment(Acid, percent);
            Cold = CalculateAdjustment(Cold, percent);
            Divine = CalculateAdjustment(Divine, percent);
            Electrical = CalculateAdjustment(Electrical, percent);
            Fire = CalculateAdjustment(Fire, percent);
            Negative = CalculateAdjustment(Negative, percent);
            Positive = CalculateAdjustment(Positive, percent);
            Sonic = CalculateAdjustment(Sonic, percent);
            Base = CalculateAdjustment(Base, percent);
            Custom1 = CalculateAdjustment(Custom1, percent);
            Custom2 = CalculateAdjustment(Custom2, percent);
            Custom3 = CalculateAdjustment(Custom3, percent);
            Custom4 = CalculateAdjustment(Custom4, percent);
            Custom5 = CalculateAdjustment(Custom5, percent);
            Custom6 = CalculateAdjustment(Custom6, percent);
            Custom7 = CalculateAdjustment(Custom7, percent);
            Custom8 = CalculateAdjustment(Custom8, percent);
            Custom9 = CalculateAdjustment(Custom9, percent);
            Custom10 = CalculateAdjustment(Custom10, percent);
            Custom11 = CalculateAdjustment(Custom11, percent);
            Custom12 = CalculateAdjustment(Custom12, percent);
            Custom13 = CalculateAdjustment(Custom13, percent);
            Custom14 = CalculateAdjustment(Custom14, percent);
            Custom15 = CalculateAdjustment(Custom15, percent);
            Custom16 = CalculateAdjustment(Custom16, percent);
            Custom17 = CalculateAdjustment(Custom17, percent);
            Custom18 = CalculateAdjustment(Custom18, percent);
            Custom19 = CalculateAdjustment(Custom19, percent);
        }

        public int Total => (Bludgeoning < 0 ? 0 : Bludgeoning) +
                            (Pierce < 0 ? 0 : Pierce) +
                            (Slash < 0 ? 0 : Slash) +
                            (Magical < 0 ? 0 : Magical) +
                            (Acid < 0 ? 0 : Acid) +
                            (Cold < 0 ? 0 : Cold) +
                            (Divine < 0 ? 0 : Divine) +
                            (Electrical < 0 ? 0 : Electrical) +
                            (Fire < 0 ? 0 : Fire) +
                            (Negative < 0 ? 0 : Negative) +
                            (Positive < 0 ? 0 : Positive) +
                            (Sonic < 0 ? 0 : Sonic) +
                            (Base < 0 ? 0 : Base) +
                            (Custom1 < 0 ? 0 : Custom1) +
                            (Custom2 < 0 ? 0 : Custom2) +
                            (Custom3 < 0 ? 0 : Custom3) +
                            (Custom4 < 0 ? 0 : Custom4) +
                            (Custom5 < 0 ? 0 : Custom5) +
                            (Custom6 < 0 ? 0 : Custom6) +
                            (Custom7 < 0 ? 0 : Custom7) +
                            (Custom8 < 0 ? 0 : Custom8) +
                            (Custom9 < 0 ? 0 : Custom9) +
                            (Custom10 < 0 ? 0 : Custom10) +
                            (Custom11 < 0 ? 0 : Custom11) +
                            (Custom12 < 0 ? 0 : Custom12) +
                            (Custom13 < 0 ? 0 : Custom13) +
                            (Custom14 < 0 ? 0 : Custom14) +
                            (Custom15 < 0 ? 0 : Custom15) +
                            (Custom16 < 0 ? 0 : Custom16) +
                            (Custom17 < 0 ? 0 : Custom17) +
                            (Custom18 < 0 ? 0 : Custom18) +
                            (Custom19 < 0 ? 0 : Custom19);
    }
}