using ChipViewApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipViewApp.Model
{ 
    public class LEDParametersModel : INotifyPropertyChanged
    {
        private List<LEDParametersModel> _lst_ledsettingParams;
        public List<LEDParametersModel> lst_ledsettingParams
        {
            get
            {
                return _lst_ledsettingParams;
            }

            set
            {
                _lst_ledsettingParams = value;
            }
        }

        public LEDParametersModel()
        {

        }

        private int nSlotSel;
        public int NSlotSelCopy
        {
            get { return nSlotSel; }
            set { nSlotSel = value; }
        }

        private SELECTED_LED ledSelectedIndex;

        public SELECTED_LED LED_SELECTED_INDEX
        {
            get
            {
                return ledSelectedIndex;
            }
            set
            {
                ledSelectedIndex = value;

                if (ledSelectedIndex == SELECTED_LED.LED1)
                {
                    LEDCURRENT_X = LEDCURRENT1;
                    LEDSELECT_X = LEDSELECT1;
                    LBL_LEDCURRENTX = "LED_CURRENT1";
                    LBL_LEDDRIVESIDEX = "LED_DRIVESIDE1";
                    LBL_LEDX_A = "LED 1A";
                    LBL_LEDX_B = "LED 1B";
                }
                else if (ledSelectedIndex == SELECTED_LED.LED2)
                {
                    LEDCURRENT_X = LEDCURRENT2;
                    LEDSELECT_X = LEDSELECT2;
                    LBL_LEDCURRENTX = "LED_CURRENT2";
                    LBL_LEDDRIVESIDEX = "LED_DRIVESIDE2";
                    LBL_LEDX_A = "LED 2A";
                    LBL_LEDX_B = "LED 2B";
                }
                else if (ledSelectedIndex == SELECTED_LED.LED3)
                {
                    LEDCURRENT_X = LEDCURRENT3;
                    LEDSELECT_X = LEDSELECT3;
                    LBL_LEDCURRENTX = "LED_CURRENT3";
                    LBL_LEDDRIVESIDEX = "LED_DRIVESIDE3";
                    LBL_LEDX_A = "LED 3A";
                    LBL_LEDX_B = "LED 3B";
                }
                else if (ledSelectedIndex == SELECTED_LED.LED4)
                {
                    LEDCURRENT_X = LEDCURRENT4;
                    LEDSELECT_X = LEDSELECT4;
                    LBL_LEDCURRENTX = "LED_CURRENT4";
                    LBL_LEDDRIVESIDEX = "LED_DRIVESIDE4";
                    LBL_LEDX_A = "LED 4A";
                    LBL_LEDX_B = "LED 4B";
                }

                OnPropertyChanged(nameof(LED_SELECTED_INDEX));
            }
        }

        #region LED_CURRENT

        private string lbl_ledcurrentX;
        public string LBL_LEDCURRENTX
        {
            get { return lbl_ledcurrentX; }

            set
            {
                lbl_ledcurrentX = value;
                OnPropertyChanged(nameof(LBL_LEDCURRENTX));
            }
        }

        private double ledcurrent1;
        public double LEDCURRENT1
        {
            get { return ledcurrent1; }

            set
            {
                ledcurrent1 = value;
                OnPropertyChanged(nameof(LEDCURRENT1));
            }
        }

        private double ledcurrent2;
        public double LEDCURRENT2
        {
            get { return ledcurrent2; }

            set
            {
                ledcurrent2 = value;
                OnPropertyChanged(nameof(LEDCURRENT2));
            }
        }

        private double ledcurrent3;
        public double LEDCURRENT3
        {
            get { return ledcurrent3; }

            set
            {
                ledcurrent3 = value;
                OnPropertyChanged(nameof(LEDCURRENT3));
            }
        }

        private double ledcurrent4;
        public double LEDCURRENT4
        {
            get { return ledcurrent4; }

            set
            {
                ledcurrent4 = value;
                OnPropertyChanged(nameof(LEDCURRENT4));
            }
        }

        private double ledcurrentx;
        public double LEDCURRENT_X
        {
            get { return ledcurrentx; }

            set
            {
                ledcurrentx = value;
                OnPropertyChanged(nameof(LEDCURRENT_X));


            }
        }

        #endregion LED_CURRENT

        #region LED_SELECT

        private string lbl_leddrivesideX;
        public string LBL_LEDDRIVESIDEX
        {
            get { return lbl_leddrivesideX; }

            set
            {
                lbl_leddrivesideX = value;
                OnPropertyChanged(nameof(LBL_LEDDRIVESIDEX));
            }
        }


        private int ledselect1;
        public int LEDSELECT1
        {
            get { return ledselect1; }

            set
            {
                ledselect1 = value;
                OnPropertyChanged(nameof(LEDSELECT1));
            }
        }

        private int ledselect2;
        public int LEDSELECT2
        {
            get { return ledselect2; }

            set
            {
                ledselect2 = value;
                OnPropertyChanged(nameof(LEDSELECT2));
            }
        }

        private int ledselect3;
        public int LEDSELECT3
        {
            get { return ledselect3; }

            set
            {
                ledselect3 = value;
                OnPropertyChanged(nameof(LEDSELECT3));
            }
        }

        private int ledselect4;
        public int LEDSELECT4
        {
            get { return ledselect4; }

            set
            {
                ledselect4 = value;
                OnPropertyChanged(nameof(LEDSELECT4));
            }
        }

        private int ledselectx;
        public int LEDSELECT_X
        {
            get { return ledselectx; }

            set
            {
                ledselectx = value;
                OnPropertyChanged(nameof(LEDSELECT_X));
            }
        }

        private string lbl_ledxA;
        public string LBL_LEDX_A
        {
            get { return lbl_ledxA; }

            set
            {
                lbl_ledxA = value;
                OnPropertyChanged(nameof(LBL_LEDX_A));
            }
        }

        private string lbl_ledxB;
        public string LBL_LEDX_B
        {
            get { return lbl_ledxB; }

            set
            {
                lbl_ledxB = value;
                OnPropertyChanged(nameof(LBL_LEDX_B));
            }
        }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
