using ChipViewApp.Utils;
using System.ComponentModel;
using System.Windows.Media;

namespace ChipViewApp.Model
{
    public class TimingParametersModel : INotifyPropertyChanged
    {
        private int nSlotSel;
        public int SLOTNUM
        {
            get { return nSlotSel; }
            set { nSlotSel = value; }
        }

        private OPERATING_MODE operatingMode;
        public OPERATING_MODE OPERATING_MODE
        {
            get { return operatingMode; }
            set { operatingMode = value; }
        }

        private double num_int;
        public double NUM_INT
        {
            get { return num_int; }

            set
            {
                num_int = value;
            }
        }

        private SAMPLE_TYPE sample_type;
        public SAMPLE_TYPE SAMPLE_TYPE
        {
            get { return sample_type; }

            set
            {
                sample_type = value;
            }
        }

        #region Timing_Register_Values
        private string pre_width;
        public string PRE_WIDTH
        {
            get { return pre_width; }

            set
            {
                pre_width = value;
                OnPropertyChanged(nameof(PRE_WIDTH));
            }
        }

        private string led_width;
        public string LED_WIDTH
        {
            get { return led_width; }

            set
            {
                led_width = value;
                OnPropertyChanged(nameof(LED_WIDTH));
            }
        }

        private string led_offset;
        public string LED_OFFSET
        {
            get { return led_offset; }

            set
            {
                led_offset = value;
                OnPropertyChanged(nameof(LED_OFFSET));
            }
        }

        private string integ_offset;
        public string INTEG_OFFSET
        {
            get { return integ_offset; }

            set
            {
                integ_offset = value;
                OnPropertyChanged(nameof(INTEG_OFFSET));
            }
        }

        private string integ_width;
        public string INTEG_WIDTH
        {
            get { return integ_width; }

            set
            {
                integ_width = value;
                OnPropertyChanged(nameof(INTEG_WIDTH));
            }
        }

        private string mod_offset;
        public string MOD_OFFSET
        {
            get { return mod_offset; }

            set
            {
                mod_offset = value;
                OnPropertyChanged(nameof(MOD_OFFSET));
            }
        }

        private string mod_width;
        public string MOD_WIDTH
        {
            get { return mod_width; }

            set
            {
                mod_width = value;
                OnPropertyChanged(nameof(MOD_WIDTH));
            }
        }

        private string min_period;
        public string MIN_PERIOD
        {
            get { return min_period; }

            set
            {
                min_period = value;
                OnPropertyChanged(nameof(MIN_PERIOD));
            }
        }

        private string dark1_offset;
        public string DARK1_OFFSET
        {
            get { return dark1_offset; }

            set
            {
                dark1_offset = value;
                OnPropertyChanged(nameof(DARK1_OFFSET));
            }
        }

        private string lit_offset;
        public string LIT_OFFSET
        {
            get { return lit_offset; }

            set
            {
                lit_offset = value;
                OnPropertyChanged(nameof(LIT_OFFSET));
            }
        }

        private string dark2_offset;
        public string DARK2_OFFSET
        {
            get { return dark2_offset; }

            set
            {
                dark2_offset = value;
                OnPropertyChanged(nameof(DARK2_OFFSET));
            }
        }

        #endregion

        #region Timing_Path_Data
        private Geometry precondition_Data;
        public Geometry PRECONDITION_DATA
        {
            get { return precondition_Data; }

            set
            {
                precondition_Data = value;
                OnPropertyChanged(nameof(PRECONDITION_DATA));
            }
        }

        private Geometry led_Data;
        public Geometry LED_DATA
        {
            get { return led_Data; }

            set
            {
                led_Data = value;
                OnPropertyChanged(nameof(LED_DATA));
            }
        }

        private Geometry integSequence_Data;
        public Geometry INTEGSEQUENCE_DATA
        {
            get { return integSequence_Data; }

            set
            {
                integSequence_Data = value;
                OnPropertyChanged(nameof(INTEGSEQUENCE_DATA));
            }
        }

        private Geometry modulateStimulus_Data;
        public Geometry MODULATESTIMULUS_DATA
        {
            get { return modulateStimulus_Data; }

            set
            {
                modulateStimulus_Data = value;
                OnPropertyChanged(nameof(MODULATESTIMULUS_DATA));
            }
        }

        #endregion

        #region Timing_Diagram_Info

        #region Param_Leftmargin_Info
        private double precondition_leftMargin;
        public double PRECONDITION_LEFT_MARGIN
        {
            get { return precondition_leftMargin; }

            set
            {
                precondition_leftMargin = value;
                OnPropertyChanged(nameof(PRECONDITION_LEFT_MARGIN));
            }
        }

        private double ledOffset_leftMargin;
        public double LEDOFFSET_LEFT_MARGIN
        {
            get { return ledOffset_leftMargin; }

            set
            {
                ledOffset_leftMargin = value;
                OnPropertyChanged(nameof(LEDOFFSET_LEFT_MARGIN));
            }
        }

        private double ledwidth_leftMargin;
        public double LEDWIDTH_LEFT_MARGIN
        {
            get { return ledwidth_leftMargin; }

            set
            {
                ledwidth_leftMargin = value;
                OnPropertyChanged(nameof(LEDWIDTH_LEFT_MARGIN));
            }
        }

        private double integOffset_leftMargin;
        public double INTEGOFFSET_LEFT_MARGIN
        {
            get { return integOffset_leftMargin; }

            set
            {
                integOffset_leftMargin = value;
                OnPropertyChanged(nameof(INTEGOFFSET_LEFT_MARGIN));
            }
        }

        private double integwidth_leftMargin;
        public double INTEGWIDTH_LEFT_MARGIN
        {
            get { return integwidth_leftMargin; }

            set
            {
                integwidth_leftMargin = value;
                OnPropertyChanged(nameof(INTEGWIDTH_LEFT_MARGIN));
            }
        }

        private double modOffset_leftMargin;
        public double MODOFFSET_LEFT_MARGIN
        {
            get { return modOffset_leftMargin; }

            set
            {
                modOffset_leftMargin = value;
                OnPropertyChanged(nameof(MODOFFSET_LEFT_MARGIN));
            }
        }

        private double modwidth_leftMargin;
        public double MODWIDTH_LEFT_MARGIN
        {
            get { return modwidth_leftMargin; }

            set
            {
                modwidth_leftMargin = value;
                OnPropertyChanged(nameof(MODWIDTH_LEFT_MARGIN));
            }
        }

        private double minperiod_leftMargin;
        public double MINPERIOD_LEFT_MARGIN
        {
            get { return minperiod_leftMargin; }

            set
            {
                minperiod_leftMargin = value;
                OnPropertyChanged(nameof(MINPERIOD_LEFT_MARGIN));
            }
        }

        private double dig_int_Offset_leftMargin;
        public double DIG_INT_OFFSET_LEFT_MARGIN
        {
            get { return dig_int_Offset_leftMargin; }

            set
            {
                dig_int_Offset_leftMargin = value;
                OnPropertyChanged(nameof(DIG_INT_OFFSET_LEFT_MARGIN));
            }
        }

        private double di_dark1_arrow_leftMargin;
        public double DI_DARK1_ARROW_LEFT_MARGIN
        {
            get { return di_dark1_arrow_leftMargin; }

            set
            {
                di_dark1_arrow_leftMargin = value;
                OnPropertyChanged(nameof(DI_DARK1_ARROW_LEFT_MARGIN));
            }
        }

        private double di_lit_arrow_leftMargin;
        public double DI_LIT_ARROW_LEFT_MARGIN
        {
            get { return di_lit_arrow_leftMargin; }

            set
            {
                di_lit_arrow_leftMargin = value;
                OnPropertyChanged(nameof(DI_LIT_ARROW_LEFT_MARGIN));
            }
        }

        private double di_dark2_arrow_leftMargin;
        public double DI_DARK2_ARROW_LEFT_MARGIN
        {
            get { return di_dark2_arrow_leftMargin; }

            set
            {
                di_dark2_arrow_leftMargin = value;
                OnPropertyChanged(nameof(DI_DARK2_ARROW_LEFT_MARGIN));
            }
        }

        #endregion Param_Leftmargin_Info

        #region FLOAT_LEDPULSES_INFO
        private double ledpulse1_leftMargin;
        public double LEDPULSE1_LEFT_MARGIN
        {
            get { return ledpulse1_leftMargin; }

            set
            {
                ledpulse1_leftMargin = value;
                OnPropertyChanged(nameof(LEDPULSE1_LEFT_MARGIN));
            }
        }

        private double ledpulse2_leftMargin;
        public double LEDPULSE2_LEFT_MARGIN
        {
            get { return ledpulse2_leftMargin; }

            set
            {
                ledpulse2_leftMargin = value;
                OnPropertyChanged(nameof(LEDPULSE2_LEFT_MARGIN));
            }
        }

        private double ledpulse3_leftMargin;
        public double LEDPULSE3_LEFT_MARGIN
        {
            get { return ledpulse3_leftMargin; }

            set
            {
                ledpulse3_leftMargin = value;
                OnPropertyChanged(nameof(LEDPULSE3_LEFT_MARGIN));
            }
        }

        private double ledpulse4_leftMargin;
        public double LEDPULSE4_LEFT_MARGIN
        {
            get { return ledpulse4_leftMargin; }

            set
            {
                ledpulse4_leftMargin = value;
                OnPropertyChanged(nameof(LEDPULSE4_LEFT_MARGIN));
            }
        }

        private string ledpulse1_status;
        public string LEDPULSE1_STATUS
        {
            get { return ledpulse1_status; }

            set
            {
                ledpulse1_status = value;
                OnPropertyChanged(nameof(LEDPULSE1_STATUS));
            }
        }

        private string ledpulse2_status;
        public string LEDPULSE2_STATUS
        {
            get { return ledpulse2_status; }

            set
            {
                ledpulse2_status = value;
                OnPropertyChanged(nameof(LEDPULSE2_STATUS));
            }
        }

        private string ledpulse3_status;
        public string LEDPULSE3_STATUS
        {
            get { return ledpulse3_status; }

            set
            {
                ledpulse3_status = value;
                OnPropertyChanged(nameof(LEDPULSE3_STATUS));
            }
        }

        private string ledpulse4_status;
        public string LEDPULSE4_STATUS
        {
            get { return ledpulse4_status; }

            set
            {
                ledpulse4_status = value;
                OnPropertyChanged(nameof(LEDPULSE4_STATUS));
            }
        }

        #endregion FLOAT_LEDPULSES_INFO

        #region Param_Width_Info
        private double precondition_width;
        public double PRECONDITION_WIDTH
        {
            get { return precondition_width; }

            set
            {
                precondition_width = value;
                OnPropertyChanged(nameof(PRECONDITION_WIDTH));
            }
        }

        private double ledoffset_width;
        public double LED_OFFSET_WIDTH
        {
            get { return ledoffset_width; }

            set
            {
                ledoffset_width = value;
                OnPropertyChanged(nameof(LED_OFFSET_WIDTH));
            }
        }

        private double led_width_val;
        public double LED_WIDTH_VAL
        {
            get { return led_width_val; }

            set
            {
                led_width_val = value;
                OnPropertyChanged(nameof(LED_WIDTH_VAL));
            }
        }

        private double integoffset_width;
        public double INTEGOFFSET_WIDTH
        {
            get { return integoffset_width; }

            set
            {
                integoffset_width = value;
                OnPropertyChanged(nameof(INTEGOFFSET_WIDTH));
            }
        }

        private double integrated_width;
        public double INTEGRATED_WIDTH
        {
            get { return integrated_width; }

            set
            {
                integrated_width = value;
                OnPropertyChanged(nameof(INTEGRATED_WIDTH));
            }
        }

        private double modoffset_width;
        public double MODOFFSET_WIDTH
        {
            get { return modoffset_width; }

            set
            {
                modoffset_width = value;
                OnPropertyChanged(nameof(MODOFFSET_WIDTH));
            }
        }

        private double modulated_width;
        public double MODULATED_WIDTH
        {
            get { return modulated_width; }

            set
            {
                modulated_width = value;
                OnPropertyChanged(nameof(MODULATED_WIDTH));
            }
        }

        private double min_period_width_info;
        public double MIN_PERIOD_WIDTH_INFO
        {
            get { return min_period_width_info; }

            set
            {
                min_period_width_info = value;
                OnPropertyChanged(nameof(MIN_PERIOD_WIDTH_INFO));
            }
        }

        private double dark1_offset_width_info;
        public double DARK1_OFFSET_WIDTH_INFO
        {
            get { return dark1_offset_width_info; }

            set
            {
                dark1_offset_width_info = value;
                OnPropertyChanged(nameof(DARK1_OFFSET_WIDTH_INFO));
            }
        }

        private double lit_offset_width_info;
        public double LIT_OFFSET_WIDTH_INFO
        {
            get { return lit_offset_width_info; }

            set
            {
                lit_offset_width_info = value;
                OnPropertyChanged(nameof(LIT_OFFSET_WIDTH_INFO));
            }
        }

        private double dark2_offset_width_info;
        public double DARK2_OFFSET_WIDTH_INFO
        {
            get { return dark2_offset_width_info; }

            set
            {
                dark2_offset_width_info = value;
                OnPropertyChanged(nameof(DARK2_OFFSET_WIDTH_INFO));
            }
        }

        private double arrow_width_info;
        public double ARROW_WIDTH_INFO
        {
            get { return arrow_width_info; }

            set
            {
                arrow_width_info = value;
                OnPropertyChanged(nameof(ARROW_WIDTH_INFO));
            }
        }

        private double dark_adc_width_info;
        public double DARK_ADC_WIDTH_INFO
        {
            get { return dark_adc_width_info; }

            set
            {
                dark_adc_width_info = value;
                OnPropertyChanged(nameof(DARK_ADC_WIDTH_INFO));
            }
        }

        private double lit_adc_width_info;
        public double LIT_ADC_WIDTH_INFO
        {
            get { return lit_adc_width_info; }

            set
            {
                lit_adc_width_info = value;
                OnPropertyChanged(nameof(LIT_ADC_WIDTH_INFO));
            }
        }


        private double di_dark_num_int;
        public double DI_DARK_NUM_INT
        {
            get { return di_dark_num_int; }

            set
            {
                di_dark_num_int = value;
                OnPropertyChanged(nameof(DI_DARK_NUM_INT));
            }
        }

        private double di_lit_num_int;
        public double DI_LIT_NUM_INT
        {
            get { return di_lit_num_int; }

            set
            {
                di_lit_num_int = value;
                OnPropertyChanged(nameof(DI_LIT_NUM_INT));
            }
        }

        #endregion Param_Width_info

        private double path_width;
        public double PATH_WIDTH
        {
            get { return path_width; }

            set
            {
                path_width = value;
                OnPropertyChanged(nameof(PATH_WIDTH));
            }
        }

        private double usercontrol_width;
        public double USERCONTROL_WIDTH
        {
            get { return usercontrol_width; }

            set
            {
                usercontrol_width = value;
                OnPropertyChanged(nameof(USERCONTROL_WIDTH));
            }
        }
        #endregion Timing_Diagram_Info

        #region Timing_Param_Info_Visibility

        private string is_preconditionPath_enable;
        public string IS_PRECONDITIONPATH_ENABLE
        {
            get { return is_preconditionPath_enable; }

            set
            {
                is_preconditionPath_enable = value;
                OnPropertyChanged(nameof(IS_PRECONDITIONPATH_ENABLE));
            }
        }

        private string is_ledpath_enable;
        public string IS_LEDPATH_ENABLE
        {
            get { return is_ledpath_enable; }

            set
            {
                is_ledpath_enable = value;
                OnPropertyChanged(nameof(IS_LEDPATH_ENABLE));
            }
        }

        private string is_ledpath_offset_enable;
        public string IS_LEDPATH_OFFSET_ENABLE
        {
            get { return is_ledpath_offset_enable; }

            set
            {
                is_ledpath_offset_enable = value;
                OnPropertyChanged(nameof(IS_LEDPATH_OFFSET_ENABLE));
            }
        }


        private string is_modpath_enable;
        public string IS_MODPATH_ENABLE
        {
            get { return is_modpath_enable; }

            set
            {
                is_modpath_enable = value;
                OnPropertyChanged(nameof(IS_MODPATH_ENABLE));
            }
        }

        private string is_modpath_offset_enable;
        public string IS_MODPATH_OFFSET_ENABLE
        {
            get { return is_modpath_offset_enable; }

            set
            {
                is_modpath_offset_enable = value;
                OnPropertyChanged(nameof(IS_MODPATH_OFFSET_ENABLE));
            }
        }

        private string is_integpath_enable;
        public string IS_INTEGPATH_ENABLE
        {
            get { return is_integpath_enable; }

            set
            {
                is_integpath_enable = value;
                OnPropertyChanged(nameof(IS_INTEGPATH_ENABLE));
            }
        }

        private string is_integpath_offset_enable;
        public string IS_INTEGPATH_OFFSET_ENABLE
        {
            get { return is_integpath_offset_enable; }

            set
            {
                is_integpath_offset_enable = value;
                OnPropertyChanged(nameof(IS_INTEGPATH_OFFSET_ENABLE));
            }
        }

        private string is_dark1_offset_enable;
        public string IS_DARK1_OFFSET_ENABLE
        {
            get { return is_dark1_offset_enable; }

            set
            {
                is_dark1_offset_enable = value;
                OnPropertyChanged(nameof(IS_DARK1_OFFSET_ENABLE));
            }
        }

        private string is_lit_offset_enable;
        public string IS_LIT_OFFSET_ENABLE
        {
            get { return is_lit_offset_enable; }

            set
            {
                is_lit_offset_enable = value;
                OnPropertyChanged(nameof(IS_LIT_OFFSET_ENABLE));
            }
        }

        private string is_dark2_offset_enable;
        public string IS_DARK2_OFFSET_ENABLE
        {
            get { return is_dark2_offset_enable; }

            set
            {
                is_dark2_offset_enable = value;
                OnPropertyChanged(nameof(IS_DARK2_OFFSET_ENABLE));
            }
        }

        private string is_dark2_arrow_enable;
        public string IS_DARK2_ARROW_ENABLE
        {
            get { return is_dark2_arrow_enable; }

            set
            {
                is_dark2_arrow_enable = value;
                OnPropertyChanged(nameof(IS_DARK2_ARROW_ENABLE));
            }
        }

        #endregion Timing_Param_Info_Visibility

        #region Timing_path_Brushes
        private Brush slot_default_brush;
        public Brush SLOT_DEFAULT_BRUSH
        {
            get { return slot_default_brush; }

            set
            {
                slot_default_brush = value;
                OnPropertyChanged(nameof(SLOT_DEFAULT_BRUSH));
            }
        }

        private Brush precondition_brush;
        public Brush PRECONDITION_BRUSH
        {
            get { return precondition_brush; }

            set
            {
                precondition_brush = value;
                OnPropertyChanged(nameof(PRECONDITION_BRUSH));
            }
        }

        private Brush led_brush;
        public Brush LED_BRUSH
        {
            get { return led_brush; }

            set
            {
                led_brush = value;
                OnPropertyChanged(nameof(LED_BRUSH));
            }
        }

        private Brush mod_brush;
        public Brush MOD_BRUSH
        {
            get { return mod_brush; }

            set
            {
                mod_brush = value;
                OnPropertyChanged(nameof(MOD_BRUSH));
            }
        }

        private Brush integ_brush;
        public Brush INTEG_BRUSH
        {
            get { return integ_brush; }

            set
            {
                integ_brush = value;
                OnPropertyChanged(nameof(INTEG_BRUSH));
            }
        }

        #endregion Timing_path_Brushes

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
