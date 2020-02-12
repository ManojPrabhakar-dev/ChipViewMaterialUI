using ChipViewApp.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;

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

                LBL_LEDCURRENT_X_mA = Convert_Ledx_Current_in_mA(LEDCURRENT_X) + " mA";

                OnPropertyChanged(nameof(LED_SELECTED_INDEX));
            }
        }

        public double Convert_Ledx_Current_in_mA(double LEDCurrentX)
        {
            double K1 = 1.0;
            double K2 = 2.0;

            // char slot = (char)65;
            ushort calculate_Led_Current = 0;
            calculate_Led_Current = Calculate_LED_Current();
            if (calculate_Led_Current > 255)
            {
                //DialogResult result = MetroMessageBox.Show(this, "Exceeding Total LED Safe Current Limits" + calculate_Led_Current.ToString(), "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                //m_driverclass.calibrationStatus("Exceeding Total LED Safe Current Limits", "", MESSAGE_PAUSE);

                Console.WriteLine("Exceeding Total LED Safe Current Limits");
            }

            //Set_bit_status(ref Slot_selection.Led1_Current, SlotTab.SelectedIndex);
            //btn_apply.Enabled = true;
            //Num_Led1_Current[SlotTab.SelectedIndex].ForeColor = Color.Red;

            /*
             * New Stuff to calculate the Led current
             * 
             */
            int led_coarse = (int)LEDCurrentX;

            double led_current = 0;

            if (led_coarse >= 16 && led_coarse <= 127)
            {
                led_current = (K2 * (led_coarse - 15)) + (K1 * 16);
            }
            else if (led_coarse < 16 && led_coarse > 0)
            {
                led_current = K1 * (led_coarse + 1);
            }
            else if (led_coarse == 0)
            {
                led_current = 0;
            }
            else
            {
                //Should not come here at all
            }

            // List_Led1Cuurent_lbl[SlotTab.SelectedIndex].Text = led_current.ToString();

            //if (!regControlList.Contains(ADPDCL_REG_LED_POW12_X + (char)(slot + SlotTab.SelectedIndex)))
            //    regControlList.Add(ADPDCL_REG_LED_POW12_X + (char)(slot + SlotTab.SelectedIndex));

            return led_current;
        }

        private ushort Calculate_LED_Current()
        {
            ushort Value = 0;

            try
            {
                Value = (ushort)(LEDCURRENT1 + LEDCURRENT2 + LEDCURRENT3 + LEDCURRENT4);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Calculate_LED_Current API = " + ex);
            }

            return Value;
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

        private string lbl_ledcurrentx_mA;
        public string LBL_LEDCURRENT_X_mA
        {
            get { return lbl_ledcurrentx_mA; }

            set
            {
                lbl_ledcurrentx_mA = value;
                OnPropertyChanged(nameof(LBL_LEDCURRENT_X_mA));


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
