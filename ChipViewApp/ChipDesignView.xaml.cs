using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NPipeCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ChipViewApp
{

    public class Timing_Parameters : INotifyPropertyChanged
    {
        private int nSlotSel;
        public int SLOTNUM
        {
            get { return nSlotSel; }
            set { nSlotSel = value; }
        }

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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    /// <summary>
    /// Interaction logic for ChipDesignView.xaml
    /// </summary>
    public partial class ChipDesignView
    {
        private string chipDesignJsonPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\ChipDesign_V6.json"; //New
        private const double MAX_TIMING_LIMIT = 30.0;
        private Timing_Parameters timing_param = new Timing_Parameters();
        private List<Timing_Parameters> lst_timingParam = new List<Timing_Parameters>();
        private Dictionary<int,Brush> dict_slotBrushes = new Dictionary<int, Brush>();

        private DynamicTimingDiagram m_PathData_inst = new DynamicTimingDiagram();

        TextBlock IN1Text;
        TextBlock IN2Text;
        TextBlock IN3Text;
        TextBlock IN4Text;
        TextBlock IN5Text;
        TextBlock IN6Text;
        TextBlock IN7Text;
        TextBlock IN8Text;
        TextBlock[] aInTextBlk;

        System.Windows.Shapes.Path[] aInpLine = new System.Windows.Shapes.Path[8];
        double ChipGridAcWid;
        double ChipGridAcHeight;
        double nLeftDelta = 6;
        double nTopDelta = 12;
        double FontSizeLbl = 10;
        Brush StrokeEn = Brushes.DarkCyan; // (Brush)new BrushConverter().ConvertFrom("#FF008066");
        Brush StrokeDis = Brushes.DarkGray;
        /* GLOBAL VARIABLES */
        int nSlotSel = 0;
        static int nActiveSlots = 0;
        int[] aCh1Inp = new int[2];
        int[] aCh2Inp = new int[2];
        List<string> aSingleInp = new List<string>();
        List<string> aDiffInp = new List<string>();
        List<string> aDiffInpCh1 = new List<string>();
        List<string> aDiffInpCh2 = new List<string>();
        public ObservableCollection<string> activeSlotLst { get; set; }

        double dValue = 0;
        int iValue = 0;
        string slotValue = string.Empty;

        public const string IN1 = "IN1";
        public const string IN2 = "IN2";
        public const string IN3 = "IN3";
        public const string IN4 = "IN4";
        public const string IN5 = "IN5";
        public const string IN6 = "IN6";
        public const string IN7 = "IN7";
        public const string IN8 = "IN8";

        public ushort CHIP400x = 0xc0;
        public ushort CHIP410x = 0xc2;

        private static ClientNPipe clientPipe { get; set; }

        private int[] CHOPMODE_ENABLE = new int[] {0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55};

        public ChipDesignView()
        {
            InitializeComponent();

            //Kill_ChipViewProcess();

            activeSlotLst = new ObservableCollection<string>();

            // clientPipe = CreateClient();  //TODO 

            dict_slotBrushes.Clear();
            dict_slotBrushes.Add(1, Brushes.CadetBlue);
            dict_slotBrushes.Add(2, Brushes.DarkSalmon);
            dict_slotBrushes.Add(3, Brushes.Plum);
            dict_slotBrushes.Add(4, Brushes.MediumPurple);
            dict_slotBrushes.Add(5, Brushes.MediumSeaGreen);
            dict_slotBrushes.Add(6, Brushes.MediumVioletRed);
            dict_slotBrushes.Add(7, Brushes.MediumSlateBlue);
            dict_slotBrushes.Add(8, Brushes.YellowGreen);
            dict_slotBrushes.Add(9, Brushes.Brown);
            dict_slotBrushes.Add(10, Brushes.Indigo);
            dict_slotBrushes.Add(11, Brushes.IndianRed);
            dict_slotBrushes.Add(12, Brushes.DodgerBlue);

            lst_timingParam.Clear();
            for (int i = 0; i < 12; i++)
            {
                lst_timingParam.Add(new Timing_Parameters());
            }

            //TODO : Need to Remove After integration
            ParseChipDesignJson();
            UpdateChipViewParameters();

        }

        //TODO : Need to Remove After integration
        private void ParseChipDesignJson()
        {
            try
            {
                using (StreamReader r = new StreamReader(chipDesignJsonPath))
                {
                    string json = r.ReadToEnd();
                    this.adpdControlJObject = JsonConvert.DeserializeObject<NAryDictionary<string, string, aAdpdCtrlRegParams>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ParseChipDesignjson API = " + ex);
            }
        }

        private int chipID = 0xC0;  // Set to Default to ADPD4000
        public int CHIP_ID
        {
            get
            {
                return chipID;
            }

            set
            {
                chipID = value;
            }
        }

        NAryDictionary<string, string, aAdpdCtrlRegParams> aRegAdpdCtrlItems = new NAryDictionary<string, string, aAdpdCtrlRegParams>();
        public NAryDictionary<string, string, aAdpdCtrlRegParams> adpdControlJObject
        {
            get
            {
                return aRegAdpdCtrlItems;
            }
            set
            {
                aRegAdpdCtrlItems = value;
            }
        }

        public bool is_ChipViewApp_Initialized = false;

        private bool IsChipView_Initialized
        {
            get
            {
                return is_ChipViewApp_Initialized;
            }
            set
            {
                is_ChipViewApp_Initialized = value;
            }
        }


        public delegate void ChipDesignViewEventHandler(object sender);  //, ChipViewEventArgs Args);
        public event ChipDesignViewEventHandler chipviewHandler;

        private void OnApplyConfigClicked(object sender, RoutedEventArgs e)
        {
            var GlobalSetting_EntryError = IsSettingFieldEmpty(GlobalSettings);

            var TimingSetting_EntryError = IsSettingFieldEmpty(TimingSettings);

            var LedSetting_EntryError = IsSettingFieldEmpty(LEDSettings);

            if ((GlobalSetting_EntryError == false) && (TimingSetting_EntryError == false) && (LedSetting_EntryError == false))
            {
                Mouse.OverrideCursor = Cursors.Wait;

                Apply_InputMuxValue();

                var serialized_dictRegMap = JsonConvert.SerializeObject(adpdControlJObject);
                SendJsonObjectString(serialized_dictRegMap);

                Thread.Sleep(1000);
                Mouse.OverrideCursor = null;
            }
            else
            {
                System.Windows.MessageBox.Show("One or more field value is empty. Please modify setting to proper value to apply changes to the device.");
            }
        }

        private bool IsSettingFieldEmpty(Grid settingsGrid)
        {
            try
            {
                var decimalUpDown = settingsGrid.Children.OfType<DecimalUpDown>();

                foreach (var numeric in decimalUpDown)
                {
                    if (numeric.Value == null)
                    {
                        return true;
                    }
                }

                var integUpDown = settingsGrid.Children.OfType<IntegerUpDown>();

                foreach (var numeric in integUpDown)
                {
                    if (numeric.Value == null)
                    {
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in IsSettingFieldEmpty API = " + ex);
            }

            return false;
        }

        private void Apply_InputMuxValue()
        {
            try
            {
                int inputmux_val = 0;

                var pair12_val = getInputMuxValue(uc_InputMux.m_inputConfig_inst.m_pair12);
                var pair34_val = getInputMuxValue(uc_InputMux.m_inputConfig_inst.m_pair34);
                var pair56_val = getInputMuxValue(uc_InputMux.m_inputConfig_inst.m_pair56);
                var pair78_val = getInputMuxValue(uc_InputMux.m_inputConfig_inst.m_pair78);

                inputmux_val = (ushort)setBitValues(inputmux_val, pair12_val, 0, 3);
                inputmux_val = (ushort)setBitValues(inputmux_val, pair34_val, 4, 7);
                inputmux_val = (ushort)setBitValues(inputmux_val, pair56_val, 8, 11);
                inputmux_val = (ushort)setBitValues(inputmux_val, pair78_val, 12, 15);

                JArray aArr_value = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                regVal_Lst[nSlotSel] = inputmux_val.ToString();


                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["Value"] = jarr_Val_mod;

                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                IsVal_Changed_Lst[nSlotSel] = "True";

                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["IsValueChanged"] = jarr_IsValChanged_mod;

                Console.WriteLine(" " + pair12_val + " " + pair34_val + " " + pair56_val + " " + pair78_val);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Apply_InputMuxValue API = " + ex);
            }
        }
        private int getInputMuxValue(PairXX pairxx)
        {
            INPUT_PAIR e_InputPair = INPUT_PAIR.INPUT_PAIR_DISABLED;
            try
            {
                if ((pairxx.TOP_LEFT == true) && (pairxx.TOP_RIGHT == false) && (pairxx.BOTTOM_RIGHT == false))
                {
                    e_InputPair = INPUT_PAIR.INx1_CH1__INx2_DISCONNECTED;
                }
                else if ((pairxx.BOTTOM_LEFT == true) && (pairxx.TOP_RIGHT == false) && (pairxx.BOTTOM_RIGHT == false))
                {
                    e_InputPair = INPUT_PAIR.INx1_CH2__INx2_DISCONNECTED;
                }
                else if ((pairxx.TOP_RIGHT == true) && (pairxx.TOP_LEFT == false) && (pairxx.BOTTOM_LEFT == false))
                {
                    e_InputPair = INPUT_PAIR.INx1_DISCONNECTED__INx2_CH1;
                }
                else if ((pairxx.BOTTOM_RIGHT == true) && (pairxx.TOP_LEFT == false) && (pairxx.BOTTOM_LEFT == false))
                {
                    e_InputPair = INPUT_PAIR.INx1_DISCONNECTED__INx2_CH2;
                }
                else if ((pairxx.TOP_LEFT == true) && (pairxx.TOP_RIGHT == false) && (pairxx.BOTTOM_RIGHT == true))
                {
                    e_InputPair = INPUT_PAIR.INx1_CH1__INx2_CH2;
                }
                else if ((pairxx.TOP_RIGHT == true) && (pairxx.TOP_LEFT == false) && (pairxx.BOTTOM_LEFT == true))
                {
                    e_InputPair = INPUT_PAIR.INx1_CH2__INx2_CH1;
                }
                else if ((pairxx.TOP_LEFT == true) && (pairxx.TOP_RIGHT == true))
                {
                    e_InputPair = INPUT_PAIR.INx1__INx2_CH1;
                }
                else if ((pairxx.BOTTOM_LEFT == true) && (pairxx.BOTTOM_RIGHT == true))
                {
                    e_InputPair = INPUT_PAIR.INx1__INx2_CH2;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in getInputMuxvalue API = " + ex);
            }

            return (int)e_InputPair;
        }
        private void Update_InputMuxSelection_UI(int inputVal_X)
        {
            try
            {
                var pairxx_val = getBitValues(inputVal_X, 0, 3);
                SetInputMuxSelection_Params(pairxx_val, uc_InputMux.m_inputConfig_inst.m_pair12);

                pairxx_val = getBitValues(inputVal_X, 4, 7);
                SetInputMuxSelection_Params(pairxx_val, uc_InputMux.m_inputConfig_inst.m_pair34);

                pairxx_val = getBitValues(inputVal_X, 8, 11);
                SetInputMuxSelection_Params(pairxx_val, uc_InputMux.m_inputConfig_inst.m_pair56);

                pairxx_val = getBitValues(inputVal_X, 12, 15);
                SetInputMuxSelection_Params(pairxx_val, uc_InputMux.m_inputConfig_inst.m_pair78);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Update_InputMuxSelection_UI API = " + ex);
            }
        }
        private void SetInputMuxSelection_Params(int pairxx_val, PairXX pairxx)
        {
            try
            {
                switch (pairxx_val)
                {
                    case (int)INPUT_PAIR.INPUT_PAIR_DISABLED:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1_CH1__INx2_DISCONNECTED:
                        pairxx.TOP_LEFT = true;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1_CH2__INx2_DISCONNECTED:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = true;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1_DISCONNECTED__INx2_CH1:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = true;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1_DISCONNECTED__INx2_CH2:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = true;
                        break;
                    case (int)INPUT_PAIR.INx1_CH1__INx2_CH2:
                        pairxx.TOP_LEFT = true;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = true;
                        break;
                    case (int)INPUT_PAIR.INx1_CH2__INx2_CH1:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = true;
                        pairxx.BOTTOM_LEFT = true;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1__INx2_CH1:
                        pairxx.TOP_LEFT = true;
                        pairxx.TOP_RIGHT = true;
                        pairxx.BOTTOM_LEFT = false;
                        pairxx.BOTTOM_RIGHT = false;
                        break;
                    case (int)INPUT_PAIR.INx1__INx2_CH2:
                        pairxx.TOP_LEFT = false;
                        pairxx.TOP_RIGHT = false;
                        pairxx.BOTTOM_LEFT = true;
                        pairxx.BOTTOM_RIGHT = true;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SetInputMuxSelection_UI API = " + ex);
            }
        }


        #region CHIPVIEW_CLIENT
        private ClientNPipe CreateClient()
        {
            ClientNPipe clientPipe = null;
            try
            {
                clientPipe = new ClientNPipe(".", "NPipeComm", p => p.StartStringReaderAsync());

                clientPipe.DataReceived += (sndr, args) =>
                    Dispatcher.BeginInvoke((Action)(() => DataReceivedFromServer(args.String)));

                clientPipe.Connect();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception in CreateClient API = " + ex);
            }

            return clientPipe;
        }

        private void SendJsonObjectString(string dictObj_str)
        {
            try
            {
                clientPipe.WriteString(dictObj_str);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ClientSend API = " + ex);

                clientPipe = CreateClient();
                if (clientPipe != null)
                {
                    clientPipe.WriteString(dictObj_str);
                }
                else
                {
                    //Kill_ChipViewProcess();
                    //this.Close();
                }
            }
        }

        private void Kill_ChipViewProcess()
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName("ChipViewApp"))
                {
                    proc.Kill();
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        private void DataReceivedFromServer(string DataReceived)
        {
            string sampleRate = string.Empty;
            string AFEWidth = string.Empty;

            try
            {
                if (DataReceived.Contains("CHIP_ID_"))
                {
                    var str_chipid_arr = DataReceived.Split('_');
                    var chipId_str = str_chipid_arr[2];
                    int.TryParse(chipId_str, out chipID);
                }
                else
                {

                    adpdControlJObject = JsonConvert.DeserializeObject<NAryDictionary<string, string, aAdpdCtrlRegParams>>(DataReceived);

                    if (IsChipView_Initialized == false)
                    {
                        UpdateChipViewParameters();
                        IsChipView_Initialized = true;
                    }
                    else
                    {
                        UpdateControlValues();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DataReceivedFromServer API = " + ex);
            }
        }
        #endregion

        private bool isContinuous;
        public bool Is_Continuous
        {
            get
            {
                return isContinuous;
            }
            set
            {
                isContinuous = value;
            }
        }

        private void cb_isContinuous_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            var cb_cont = (CheckBox)sender;

            if (cb_cont.IsChecked == true)
            {
                isContinuous = true;
            }
            else
            {
                isContinuous = false;
            }

            Add_TimingDiagram();
        }

        public void UpdateChipViewParameters()
        {
            try
            {
                //Create TextBlock
                IN1Text = new TextBlock();
                IN2Text = new TextBlock();
                IN3Text = new TextBlock();
                IN4Text = new TextBlock();
                IN5Text = new TextBlock();
                IN6Text = new TextBlock();
                IN7Text = new TextBlock();
                IN8Text = new TextBlock();
                aInTextBlk = new TextBlock[8] {IN1Text, IN2Text,IN3Text,
                                        IN4Text,IN5Text,IN6Text, IN7Text, IN8Text};

                ChipGridAcWid = ChipView.Width;
                ChipGridAcHeight = ChipView.Height;

                aInpLine[0] = IN1Line;
                aInpLine[1] = IN2Line;
                aInpLine[2] = IN3Line;
                aInpLine[3] = IN4Line;
                aInpLine[4] = IN5Line;
                aInpLine[5] = IN6Line;
                aInpLine[6] = IN7Line;
                aInpLine[7] = IN8Line;

                double nLeftval = InputMux.Data.Bounds.Left + (InputMux.Data.Bounds.Width / 3);
                double nInpHtRb = (IN2Line.Data.Bounds.Top - IN1Line.Data.Bounds.Top) / 3;
                double nTopval = IN1Line.Data.Bounds.Top + nInpHtRb;

                DisableInputLines(null);
                InputMux.ContextMenu = (ContextMenu)LayoutWin.Resources["InpMuxMenu"];
                SetSingleDiffInputLines();

                SwapCh1.ContextMenu = (ContextMenu)LayoutWin.Resources["SwapMenu"];

                SwapCh2.ContextMenu = (ContextMenu)LayoutWin.Resources["SwapMenu"];

                #region ADDING CONTROLS AND SETTING VALUES

                int nRowidx = 0;
                foreach (string nNameKey in aRegAdpdCtrlItems["GlobalSettings"].Keys)
                {
                    AddControltoSettings(GlobalSettings, nRowidx, nNameKey, "GlobalSettings");
                    nRowidx += 1;
                }

                nRowidx = 0;
                foreach (string nNameKey in aRegAdpdCtrlItems["SlotGlobalSettings"].Keys)
                {
                    AddControltoSettings(SlotGlobalSettings, nRowidx, nNameKey, "SlotGlobalSettings");
                    nRowidx += 1;
                }

                nRowidx = 0;
                foreach (string nNameKey in aRegAdpdCtrlItems["TimingSettings"].Keys)
                {
                    AddControltoSettings(TimingSettings, nRowidx, nNameKey, "TimingSettings");
                    nRowidx += 1;
                }
                #endregion

                #region ADDING INPUTLINE TEXT

                for (int idx = 0; idx < 8; idx++)
                {
                    try
                    {
                        nLeftval = aInpLine[idx].Data.Bounds.Left;
                        nTopval = aInpLine[idx].Data.Bounds.Top + nTopDelta;
                        string nInpText = aInpLine[idx].Name.ToString().Substring(0, 3);
                        aInTextBlk[idx].Name = nInpText + "Text";
                        aInTextBlk[idx].FontSize = 9;
                        aInTextBlk[idx].FontWeight = FontWeights.Bold;
                        aInTextBlk[idx].Foreground = Brushes.LightGray;
                        aInTextBlk[idx].Text = nInpText;
                        Canvas.SetTop(aInTextBlk[idx], nTopval);
                        Canvas.SetLeft(aInTextBlk[idx], nLeftval);
                        layer1.Children.Add(aInTextBlk[idx]);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("problem in adding Textblock for InputLine");
                    }
                }

                #endregion

                #region ASSIGNING EVENT HANDLERS FOR CONTROLS
                foreach (string sRegCtrlKey in aRegAdpdCtrlItems["SlotChipSettings"].Keys)
                {
                    foreach (FrameworkElement comp in layer1.Children)
                    {
                        if (comp.Name.Contains(sRegCtrlKey))
                        {
                            comp.MouseLeftButtonDown += AdpdControls_MouseLeftButtonDown;
                            comp.Tag = sRegCtrlKey;
                        }
                    }

                    foreach (FrameworkElement comp in Channel1Ctrls.Children)
                    {
                        if (comp.Name.Contains(sRegCtrlKey))
                        {
                            comp.MouseLeftButtonDown += AdpdControls_MouseLeftButtonDown;
                            comp.Tag = sRegCtrlKey;
                        }
                    }

                    foreach (FrameworkElement comp in Channel2Ctrls.Children)
                    {
                        if (comp.Name.Contains(sRegCtrlKey))
                        {
                            comp.MouseLeftButtonDown += AdpdControls_MouseLeftButtonDown;
                            comp.Tag = sRegCtrlKey;
                        }
                    }
                }
                #endregion

                AddTextLabels();
                UpdateControlValues();
                EnCommonCtrls();
                EnDisCHCtrls(1, true);
                UpdateTimingParam_ActiveSlots();
                Add_TimingDiagram();

                for (int i = 0; i < nActiveSlots; i++)
                {
                    AssignPathData_timingParam(i);
                }

            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception in UpdateChipView Parameters API = " + ex);
            }
        }

        private void UpdateTimingParam_ActiveSlots()
        {
            foreach (string nNameKey in aRegAdpdCtrlItems["TimingSettings"].Keys)
            {
                UpdateTimingDiagram_Params(nNameKey);
            }
        }

        private void Add_TimingDiagram()
        {
            try
            {

                sPanel_timingDiagram.Children.Clear();

                if (isContinuous == false)
                {
                    var timing_UserControl = new TimingDiagram_UserControl(lst_timingParam[nSlotSel],dict_slotBrushes[nSlotSel+1],nSlotSel+1);
                    lst_timingParam[nSlotSel].SLOTNUM = nSlotSel;
                    timing_UserControl.DataContext = lst_timingParam[nSlotSel];
                    sPanel_timingDiagram.Children.Add(timing_UserControl);
                }
                else
                {
                    for (int i = 1; i <= nActiveSlots; i++)
                    {
                        var timing_UserControl = new TimingDiagram_UserControl(lst_timingParam[nSlotSel],dict_slotBrushes[i],i);
                        lst_timingParam[nSlotSel].SLOTNUM = nSlotSel;
                        timing_UserControl.DataContext = lst_timingParam[i - 1];
                        sPanel_timingDiagram.Children.Add(timing_UserControl);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception in Add_TimingDiagram API = " + ex);
            }
        }

        private void AssignPathData_timingParam(int slotSel)
        {
            try
            {
                var timingParam = m_PathData_inst.GetPathData2(lst_timingParam[slotSel]);

                lst_timingParam[slotSel].PRECONDITION_DATA = timingParam.PRECONDITION_DATA;
                lst_timingParam[slotSel].LED_DATA = timingParam.LED_DATA;
                lst_timingParam[slotSel].MODULATESTIMULUS_DATA = timingParam.MODULATESTIMULUS_DATA;  // Geometry.Parse("M1,100 L 100,100");
                lst_timingParam[slotSel].INTEGSEQUENCE_DATA = timingParam.INTEGSEQUENCE_DATA;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignPathData_timingParam API = " + ex);
            }
        }

        private void UpdateTimingDiagram_Params(string nNameKey)
        {
            var nSettingsKey = "TimingSettings";
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        dValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                        var lst_slotValue = get_SelectedSlot_RegValue_lst(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                        for (int i = 0; i < nActiveSlots; i++)
                        {
                            AssignValue_timingParam(i, nNameKey, lst_slotValue[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateTimingDiagram_Params API = " + ex);
            }
        }

        private List<double> get_SelectedSlot_RegValue_lst(object value)
        {
            List<double> lst_val = new List<double>();
            double val = -1;
            try
            {
                JArray aArr = (JArray)value;
                List<string> aItems = aArr.ToObject<List<string>>();

                for (int i = 0; i < nActiveSlots; i++)
                {
                    double.TryParse(aItems[i].ToString(), out val);
                    lst_val.Add(val);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in get_SelectedSlot_RegValue API = " + ex);
            }

            return lst_val;
        }

        private void AssignValue_timingParam(int slotSel, string nNameKey, double dValue)
        {
            try
            {
                if (nNameKey.Contains("PreconditionWidth"))
                {
                    lst_timingParam[slotSel].PRE_WIDTH = (dValue * 2).ToString() + " µs";
                }
                else if (nNameKey.Contains("LEDWidth"))
                {
                    lst_timingParam[slotSel].LED_WIDTH = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("LEDOffset"))
                {
                    lst_timingParam[slotSel].LED_OFFSET = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("AFEWidth"))
                {
                    lst_timingParam[slotSel].INTEG_WIDTH = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("IntegratorOffset"))
                {
                    lst_timingParam[slotSel].INTEG_OFFSET = dValue.ToString() + " ns";
                }
                else if (nNameKey.Contains("MODWidth"))
                {
                    lst_timingParam[slotSel].MOD_WIDTH = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("MODOffset"))
                {
                    lst_timingParam[slotSel].MOD_OFFSET = dValue.ToString() + " µs";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignValue_timingParam API = " + ex);
            }
        }

        public void UpdateControlValues(bool is_SoltSelCalled = false, bool OnInitCalled = false)
        {
            try
            {
                foreach (string nNameKey in aRegAdpdCtrlItems["GlobalSettings"].Keys)
                {
                    UpdateConfigSettings(GlobalSettings, nNameKey, "GlobalSettings", is_SoltSelCalled);
                }

                foreach (string nNameKey in aRegAdpdCtrlItems["SlotGlobalSettings"].Keys)
                {
                    UpdateConfigSettings(SlotGlobalSettings, nNameKey, "SlotGlobalSettings");
                }

                foreach (string nNameKey in aRegAdpdCtrlItems["TimingSettings"].Keys)
                {
                    UpdateConfigSettings(TimingSettings, nNameKey, "TimingSettings");
                }

                foreach (string nNameKey in aRegAdpdCtrlItems["LEDSettings"].Keys)
                {
                    UpdateLEDSettings(LEDSettings, nNameKey, "LEDSettings");
                }

                foreach (string nNameKey in aRegAdpdCtrlItems["SlotChipSettings"].Keys)
                {
                    Update_Resistor_Lbl(SlotChipSettings, nNameKey, "SlotChipSettings");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateControlValues API = " + ex);
            }
        }

        private double get_SelectedSlot_RegValue(object value)
        {
            double val = -1;
            try
            {
                JArray aArr = (JArray)value;
                List<string> aItems = aArr.ToObject<List<string>>();

                if (aItems.Count > 1)
                {
                    double.TryParse(aItems[nSlotSel].ToString(), out val);
                }
                else
                {
                    double.TryParse(aItems[0].ToString(), out val);  //Global settings, no slot specific value
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in get_SelectedSlot_RegValue API = " + ex);
            }

            return val;
        }

        private void AddControltoSettings(Grid nSettingsGrid, int rowidx, string nNameKey, string nSettingsKey, bool IsDynamic_ChipSetting = false)
        {
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        var control_name = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString();
                        if ((control_name == "Channel2 Config") || (control_name == "InputMux Config") || (control_name == "Precondition Width (us)") || (control_name == "ChopMode Config"))
                        {
                            continue;
                        }

                        RowDefinition nParamRow = new RowDefinition();
                        nSettingsGrid.RowDefinitions.Add(nParamRow);
                        nParamRow.Height = GridLength.Auto;

                        TextBlock nParamName = new TextBlock();
                        nParamName.Text = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"] + ":";
                        nParamName.FontSize = 14;
                        nParamName.FontWeight = FontWeights.Normal;
                        nParamName.TextAlignment = TextAlignment.Left;
                        nParamName.VerticalAlignment = VerticalAlignment.Top;
                        nParamName.Margin = new Thickness(20, 9, 5, 15);
                        Grid.SetColumn(nParamName, 0);
                        Grid.SetRow(nParamName, (nParamIdx + rowidx));
                        nSettingsGrid.Children.Add(nParamName);

                        nParamRow = new RowDefinition();
                        nSettingsGrid.RowDefinitions.Add(nParamRow);
                        nParamRow.Height = GridLength.Auto;
                        if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "ComboBox")
                        {
                            ComboBox nEnCtrl = new ComboBox();
                            nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                            JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                            List<string> aItems = aArr.ToObject<List<string>>();
                            nEnCtrl.ItemsSource = aItems;

                            nEnCtrl.Width = 90;
                            nEnCtrl.Height = 30;

                            nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                            nEnCtrl.FontSize = 14;
                            nEnCtrl.FontWeight = FontWeights.Normal;
                            nEnCtrl.HorizontalContentAlignment = HorizontalAlignment.Center;
                            nEnCtrl.Margin = new Thickness(5, 0, 5, 5);
                            Grid.SetColumn(nEnCtrl, 1);
                            Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));


                            nSettingsGrid.Children.Add(nEnCtrl);

                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            if (CHIP_ID == CHIP400x)
                            {
                                if (nNameKey == "RintCh2")
                                {
                                    iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey]["RintCh1"].Parameters[nParamIdx]["Value"]);
                                }
                            }

                            if (IsDynamic_ChipSetting)
                            {
                                nEnCtrl.SelectedIndex = iValue;
                            }

                            if (nNameKey.Contains("NumActiveSlots"))
                            {
                                nActiveSlots = iValue + 1;
                            }

                            nEnCtrl.SelectionChanged += NComboBoxCtrl_SelectionChanged;
                        }
                        else if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "CheckBox")
                        {
                            CheckBox nEnCtrl = new CheckBox();
                            nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                            nEnCtrl.Checked += ChkboxCtrl_Checked;
                            nEnCtrl.Unchecked += ChkboxCtrl_Checked;
                            nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                            nEnCtrl.FontSize = 12;
                            nEnCtrl.Width = 80;
                            nEnCtrl.Height = 20;
                            nEnCtrl.FontWeight = FontWeights.Normal;
                            Grid.SetColumn(nEnCtrl, 1);
                            Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                            nEnCtrl.Margin = new Thickness(5);
                            nSettingsGrid.Children.Add(nEnCtrl);
                        }
                        else if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "Button")
                        {
                            Button nEnCtrl = new Button();
                            nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                            nEnCtrl.Click += BtnCtrl_Click;

                            nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                            nEnCtrl.FontSize = 12;
                            nEnCtrl.Width = 90;
                            nEnCtrl.Height = 25;
                            nEnCtrl.FontWeight = FontWeights.Normal;
                            Grid.SetColumn(nEnCtrl, 1);
                            Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                            nEnCtrl.Margin = new Thickness(5, 0, 5, 5);
                            nSettingsGrid.Children.Add(nEnCtrl);
                        }
                        else if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "NumericUpDown")
                        {
                            if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString().Contains("ADC"))
                            {
                                IntegerUpDown nEnCtrl = new IntegerUpDown();
                                nEnCtrl.ParsingNumberStyle = NumberStyles.HexNumber;
                                nEnCtrl.FormatString = "X";

                                nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                                JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                                List<int> nVals = aArr.ToObject<List<int>>();
                                nEnCtrl.Minimum = nVals[0];
                                nEnCtrl.Maximum = nVals[1];
                                nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                                nEnCtrl.Width = 90;
                                nEnCtrl.Height = 25;
                                nEnCtrl.FontSize = 16;
                                nEnCtrl.FontWeight = FontWeights.Normal;
                                nEnCtrl.PreviewTextInput += IntegerUpDown_PreviewTextInput;

                                Grid.SetColumn(nEnCtrl, 1);
                                Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                                nEnCtrl.Margin = new Thickness(5, 0, 5, 5);
                                nSettingsGrid.Children.Add(nEnCtrl);

                                if (IsDynamic_ChipSetting)
                                {
                                    dValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);  //handle for double
                                    nEnCtrl.Value = (int)dValue;
                                }
                                nEnCtrl.ValueChanged += NumUpDwnCtrl_ADC_ValueChanged;

                            }
                            else
                            {
                                DecimalUpDown nEnCtrl = new DecimalUpDown();

                                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "Integ Offset (ns)")
                                {
                                    nEnCtrl.FormatString = "F2";
                                    nEnCtrl.Increment = (decimal)31.25;
                                }
                                else if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "Sampling Rate")
                                {
                                    nEnCtrl.FormatString = "F2";
                                }
                                else
                                {
                                    nEnCtrl.FormatString = "F0";
                                }


                                nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                                JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                                List<int> nVals = aArr.ToObject<List<int>>();
                                nEnCtrl.Minimum = nVals[0];
                                nEnCtrl.Maximum = nVals[1];
                                nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                                nEnCtrl.FontSize = 16;
                                nEnCtrl.Width = 90;
                                nEnCtrl.Height = 25;
                                nEnCtrl.FontWeight = FontWeights.Normal;
                                nEnCtrl.PreviewTextInput += IntegerUpDown_PreviewTextInput;

                                Grid.SetColumn(nEnCtrl, 1);
                                Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                                nEnCtrl.Margin = new Thickness(5, 0, 5, 5);
                                nSettingsGrid.Children.Add(nEnCtrl);

                                if (IsDynamic_ChipSetting)
                                {
                                    dValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);  //handle for double
                                    nEnCtrl.Value = (decimal)dValue;
                                }
                                nEnCtrl.ValueChanged += RadNumUpDwnCtrl_ValueChanged;
                            }
                        }
                        else if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "TextBox")
                        {
                            TextBox nEnCtrl = new TextBox();
                            nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                            nEnCtrl.VerticalAlignment = VerticalAlignment.Top;
                            nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                            nEnCtrl.FontSize = 12;
                            nEnCtrl.Width = 80;
                            nEnCtrl.Height = 20;
                            nEnCtrl.FontWeight = FontWeights.Normal;
                            nEnCtrl.Text = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"].ToString();
                            nEnCtrl.TextChanged += NtextboxCtrl_TextChanged;
                            Grid.SetColumn(nEnCtrl, 1);
                            Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                            nEnCtrl.Margin = new Thickness(5);
                            nSettingsGrid.Children.Add(nEnCtrl);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AddControltoSettings API = " + ex);
            }
        }

        private void UpdateLEDSettings(Grid nLEDSettingsGrid, string nNameKey, string nSettingsKey)
        {
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "ComboBox")
                        {
                            ComboBox nParamComboBox;

                            nParamComboBox = FindChild<ComboBox>(nLEDSettingsGrid, nNameKey);

                            if (nParamComboBox != null)
                            {
                                iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                                if (nParamComboBox.Items.Count >= iValue)
                                {
                                    nParamComboBox.SelectedIndex = iValue;
                                }

                                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                                IsVal_Changed_Lst[nSlotSel] = "False";

                                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                            }
                        }
                        else if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "NumericUpDown")
                        {
                            IntegerUpDown nParamNumUpDown;

                            nParamNumUpDown = FindChild<IntegerUpDown>(nLEDSettingsGrid, nNameKey);

                            if (nParamNumUpDown != null)
                            {
                                dValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);
                                nParamNumUpDown.Value = (int)dValue;

                                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                                IsVal_Changed_Lst[nSlotSel] = "False";

                                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateLEDSettings API = " + ex);
            }
        }

        private void UpdateConfigSettings(Grid nConfigSettingsGrid, string nNameKey, string nSettingsKey, bool skip_IsValueChangesSet = false, bool OnInitCalled = false)
        {
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "Channel2 Config")
                        {
                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            if (iValue == 0)
                            {
                                EnDisCHCtrls(2, false);
                                EnDisCH2MuxCtrls(false);
                                MenuItem channel2Menu = InputMux.ContextMenu.Items[0] as MenuItem;
                                channel2Menu.Header = "Enable CH2";
                            }
                            else
                            {
                                EnDisCHCtrls(2, true);
                                EnDisCH2MuxCtrls(true);
                                MenuItem channel2Menu = InputMux.ContextMenu.Items[0] as MenuItem;
                                channel2Menu.Header = "Disable CH2";
                            }

                            if (!skip_IsValueChangesSet)
                            {
                                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                                if (IsVal_Changed_Lst.Count > nSlotSel)
                                {
                                    IsVal_Changed_Lst[nSlotSel] = "False";
                                }
                                else if (IsVal_Changed_Lst.Count != 0)
                                {
                                    IsVal_Changed_Lst[0] = "False";
                                }

                                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                            }
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "ChopMode Config")
                        {
                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            if (iValue == 0)
                            {
                                MenuItem SwapMenu = SwapCh1.ContextMenu.Items[0] as MenuItem;
                                SwapMenu.Header = "Enable ChopMode";
                                Update_ChopModeEnDis_UI("OFF");
                            }
                            else
                            {
                                CHOPMODE_ENABLE[nSlotSel] = iValue;
                                MenuItem SwapMenu = SwapCh1.ContextMenu.Items[0] as MenuItem;
                                SwapMenu.Header = "Disable ChopMode";
                                Update_ChopModeEnDis_UI("ON");
                            }
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "Precondition Width (us)")
                        {
                            UpdateTimingDiagram_Params(nNameKey);

                            AssignPathData_timingParam(nSlotSel);
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "InputMux Config")
                        {
                            JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey]["InputMux"].Parameters[0]["Value"];
                            List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey]["InputMux"].Parameters[0]["Value"]);
                            Update_InputMuxSelection_UI(iValue);
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "ComboBox")
                        {
                            ComboBox nParamComboBox;
                            var ctrlName = nNameKey + "_Param_" + nParamIdx;
                            nParamComboBox = FindChild<ComboBox>(nConfigSettingsGrid, ctrlName);

                            if (nParamComboBox != null)
                            {
                                iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                                if (nParamComboBox.Items.Count >= iValue)
                                {
                                    nParamComboBox.SelectedIndex = iValue;
                                }

                                if (!skip_IsValueChangesSet)
                                {
                                    JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                                    List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                                    if (IsVal_Changed_Lst.Count > nSlotSel)
                                    {
                                        IsVal_Changed_Lst[nSlotSel] = "False";
                                    }
                                    else if (IsVal_Changed_Lst.Count != 0)
                                    {
                                        IsVal_Changed_Lst[0] = "False";
                                    }

                                    var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                                    aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                                }
                            }
                        }
                        else if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "Button")
                        {
                            Button nParamButton;

                            var ctrlName = nNameKey + "_Param_" + nParamIdx;
                            nParamButton = FindChild<Button>(nConfigSettingsGrid, ctrlName);
                            JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                            List<string> aVals = aArr.ToObject<List<string>>();

                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            if (aVals.Count >= 2)
                            {
                                if (iValue == 0)
                                {
                                    nParamButton.Content = aVals[0];
                                    init_PairConfig_Params(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString(), aVals[0]);
                                }
                                else
                                {
                                    nParamButton.Content = aVals[1];
                                    init_PairConfig_Params(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString(), aVals[1]);
                                }
                            }

                        }
                        else if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "NumericUpDown")
                        {
                            DecimalUpDown nParamNumUpDown;
                            var ctrlName = nNameKey + "_Param_" + nParamIdx;
                            nParamNumUpDown = FindChild<DecimalUpDown>(nConfigSettingsGrid, ctrlName);

                            if (nParamNumUpDown != null)
                            {
                                dValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);
                                nParamNumUpDown.Value = (decimal)dValue;

                                if (!skip_IsValueChangesSet)
                                {
                                    JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                                    List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                                    if (IsVal_Changed_Lst.Count > nSlotSel)
                                    {
                                        IsVal_Changed_Lst[nSlotSel] = "False";
                                    }
                                    else if (IsVal_Changed_Lst.Count != 0)
                                    {
                                        IsVal_Changed_Lst[0] = "False";
                                    }

                                    var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                                    aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateConfigSettings API = " + ex);
            }
        }

        void init_PairConfig_Params(string pairXX, string pair_type)
        {
            try
            {
                if (pairXX.ToString().Contains("Pair12"))
                {
                    uc_InputMux.m_inputConfig_inst.PAIR12_CONFIG = pair_type;
                }
                else if (pairXX.ToString().Contains("Pair34"))
                {
                    uc_InputMux.m_inputConfig_inst.PAIR34_CONFIG = pair_type;
                }
                else if (pairXX.ToString().Contains("Pair56"))
                {
                    uc_InputMux.m_inputConfig_inst.PAIR56_CONFIG = pair_type;
                }
                else if (pairXX.ToString().Contains("Pair78"))
                {
                    uc_InputMux.m_inputConfig_inst.PAIR78_CONFIG = pair_type;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in init_PairConfig_Params API = " + ex);
            }
        }

        private void Update_Resistor_Lbl(Grid nConfigSettingsGrid, string nNameKey, string nSettingsKey)
        {
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx].Count > 0)
                        {
                            if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"].ToString() == "ComboBox")
                            {
                                var ctrlName = nNameKey + "Lbl";

                                int selectedIndex = -1;

                                selectedIndex = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems["SlotChipSettings"][nNameKey].Parameters[0]["Value"]);
                                JArray aArr = (JArray)aRegAdpdCtrlItems["SlotChipSettings"][nNameKey].Parameters[0]["Listval"];
                                List<string> aItems = aArr.ToObject<List<string>>();
                                if (aItems.Count > selectedIndex)
                                {
                                    if (nNameKey.Contains("Ch1"))
                                    {
                                        var txtBlockLst1 = Channel1Ctrls.Children.OfType<TextBlock>();

                                        foreach (var textblock in txtBlockLst1)
                                        {
                                            if (textblock.Name == ctrlName)
                                            {
                                                textblock.Text = aItems[selectedIndex];
                                            }
                                        }

                                    }
                                    else if (nNameKey.Contains("Ch2"))
                                    {
                                        var txtBlockLst2 = Channel2Ctrls.Children.OfType<TextBlock>();

                                        if (CHIP_ID == CHIP400x)
                                        {
                                            if (nNameKey.Contains("Rint"))
                                            {
                                                selectedIndex = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems["SlotChipSettings"]["RintCh1"].Parameters[0]["Value"]);  // For ADPD4000 ,Ch2 is not supported for integrator resistor.
                                                aArr = (JArray)aRegAdpdCtrlItems["SlotChipSettings"]["RintCh1"].Parameters[0]["Listval"];
                                                aItems = aArr.ToObject<List<string>>();
                                            }
                                        }

                                        foreach (var textblock in txtBlockLst2)
                                        {
                                            if (textblock.Name == ctrlName)
                                            {
                                                textblock.Text = aItems[selectedIndex];
                                            }
                                        }
                                    }
                                    else if (nNameKey.Contains("ChX"))
                                    {
                                        var txtBlockLst1 = Channel1Ctrls.Children.OfType<TextBlock>();

                                        foreach (var textblock in txtBlockLst1)
                                        {
                                            if (textblock.Name == ctrlName)
                                            {
                                                textblock.Text = aItems[selectedIndex];
                                            }
                                        }

                                        var txtBlockLst2 = Channel2Ctrls.Children.OfType<TextBlock>();

                                        foreach (var textblock in txtBlockLst2)
                                        {
                                            if (textblock.Name == ctrlName)
                                            {
                                                textblock.Text = aItems[selectedIndex];
                                            }
                                        }
                                    }
                                    else
                                    {
                                        var txtBlockLstLayer1 = layer1.Children.OfType<TextBlock>();

                                        foreach (var textblock in txtBlockLstLayer1)
                                        {
                                            if (textblock.Name == ctrlName)
                                            {
                                                textblock.Text = aItems[selectedIndex];
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Update_resistor_Lbl API = " + ex);
            }
        }

        private void LEDSelectX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                string nNameKey =  comboBox.Name;
                int nParamIdx = 0;
                string nSettingsKey = ((Grid)(comboBox.Parent)).Name;

                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count > 0)
                {
                    JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
                    List<string> regVal_Lst = aArr_value.ToObject<List<string>>();
                    regVal_Lst[nSlotSel] = comboBox.SelectedIndex.ToString();

                    JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                    List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                    IsVal_Changed_Lst[nSlotSel] = "True";

                    var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                    var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

                    aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                    aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("EXception in LEDSelectX_SelectionChanged API = " + ex);
            }
        }

        private void LEDCurrentX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            try
            {
                IntegerUpDown radNumUpDown = (IntegerUpDown)sender;

                string nNameKey =  radNumUpDown.Name;
                int nParamIdx = 0;
                string nSettingsKey = ((Grid)(radNumUpDown.Parent)).Name;

                if (aRegAdpdCtrlItems.Count != 0)
                {
                    if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count > 0)
                    {
                        JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
                        List<string> regVal_Lst = aArr_value.ToObject<List<string>>();
                        regVal_Lst[nSlotSel] = radNumUpDown.Value.ToString();

                        JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                        List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                        IsVal_Changed_Lst[nSlotSel] = "True";

                        var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                        var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

                        aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                        aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in LEDCurrentX_ValueChanged API = " + ex);
            }
        }

        #region CONTROL EVENT HANDLERS

        private void BtnCtrl_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button btn = (Button)sender;
                string nNameKey;
                int nParamIdx;
                string nSettingsKey = ((Grid)(btn.Parent)).Name;
                string[] aStrNames = new string[3];
                char delimiter = '_';
                aStrNames = btn.Name.Split(delimiter);
                nNameKey = aStrNames[0];
                nParamIdx = Convert.ToInt32(aStrNames[2]);

                JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                List<string> aVals = aArr.ToObject<List<string>>();

                iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (aVals.Count >= 2)
                {
                    if (iValue == 0)
                    {
                        btn.Content = aVals[1];
                        regVal_Lst[0] = 1.ToString();
                    }
                    else
                    {
                        btn.Content = aVals[0];
                        regVal_Lst[0] = 0.ToString();
                    }
                }

                reset_Pairxx(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString(), btn.Content.ToString());

                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                IsVal_Changed_Lst[0] = "True";

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in BtnCtrl_Click API = " + ex);
            }
        }

        private void reset_Pairxx(string pairxx, string pair_type)
        {
            if (pairxx.Contains("Pair12"))
            {
                uc_InputMux.m_inputConfig_inst.PAIR12_CONFIG = pair_type;
                SetInputMuxSelection_Params(0, uc_InputMux.m_inputConfig_inst.m_pair12);
            }
            else if (pairxx.Contains("Pair34"))
            {
                uc_InputMux.m_inputConfig_inst.PAIR34_CONFIG = pair_type;
                SetInputMuxSelection_Params(0, uc_InputMux.m_inputConfig_inst.m_pair34);
            }
            else if (pairxx.Contains("Pair56"))
            {
                uc_InputMux.m_inputConfig_inst.PAIR56_CONFIG = pair_type;
                SetInputMuxSelection_Params(0, uc_InputMux.m_inputConfig_inst.m_pair56);
            }
            else if (pairxx.Contains("Pair78"))
            {
                uc_InputMux.m_inputConfig_inst.PAIR78_CONFIG = pair_type;
                SetInputMuxSelection_Params(0, uc_InputMux.m_inputConfig_inst.m_pair78);
            }
        }


        private void NumUpDwnCtrl_ADC_ValueChanged(object sender, RoutedEventArgs e)
        {
            IntegerUpDown nUpDwn = (IntegerUpDown)sender;
            string nNameKey;
            int nParamIdx;
            string nSettingsKey = ((Grid)(nUpDwn.Parent)).Name;
            string[] aStrNames = new string[3];
            char delimiter = '_';
            aStrNames = nUpDwn.Name.Split(delimiter);
            nNameKey = aStrNames[0];
            nParamIdx = Convert.ToInt32(aStrNames[2]);

            JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
            List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

            if (regVal_Lst.Count > nSlotSel)
            {
                regVal_Lst[nSlotSel] = nUpDwn.Value.ToString();
            }
            else if (regVal_Lst.Count != 0)
            {
                regVal_Lst[0] = nUpDwn.Value.ToString();
            }

            JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
            List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();

            if (IsVal_Changed_Lst.Count > nSlotSel)
            {
                IsVal_Changed_Lst[nSlotSel] = "True";
            }
            else if (IsVal_Changed_Lst.Count != 0)
            {
                IsVal_Changed_Lst[0] = "True";
            }

            var jarr_Val_mod = JArray.FromObject(regVal_Lst);
            var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

            aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
            aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
        }
        private void RadNumUpDwnCtrl_ValueChanged(object sender, RoutedEventArgs e)
        {
            try
            {
                DecimalUpDown nUpDwn = (DecimalUpDown)sender;
                string nNameKey;
                int nParamIdx;
                string nSettingsKey = ((Grid)(nUpDwn.Parent)).Name;
                string[] aStrNames = new string[3];
                char delimiter = '_';
                aStrNames = nUpDwn.Name.Split(delimiter);
                nNameKey = aStrNames[0];
                nParamIdx = Convert.ToInt32(aStrNames[2]);

                JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (regVal_Lst.Count > nSlotSel)
                {
                    regVal_Lst[nSlotSel] = nUpDwn.Value.ToString();
                }
                else if (regVal_Lst.Count != 0)
                {
                    regVal_Lst[0] = nUpDwn.Value.ToString();
                }

                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();

                if (IsVal_Changed_Lst.Count > nSlotSel)
                {
                    IsVal_Changed_Lst[nSlotSel] = "True";
                }
                else if (IsVal_Changed_Lst.Count != 0)
                {
                    IsVal_Changed_Lst[0] = "True";
                }

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;

                UpdateTimingDiagram_Params(nNameKey);

                AssignPathData_timingParam(nSlotSel);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in RadNumUpDown_ValueChanged API = " + ex);
            }
        }

        private void NtextboxCtrl_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string nNameKey;
                int nParamIdx = 0;
                char delimiter = '_';
                string[] aParamName = new string[3];
                aParamName = ((FrameworkElement)sender).Name.Split(delimiter);
                nNameKey = aParamName[0];
                nParamIdx = System.Convert.ToInt32(aParamName[2]);
                TextBox nSendParamCtrl = (TextBox)sender;
                string ntextboxName = nNameKey + "Lbl";
                string nGridName = ((Grid)((FrameworkElement)sender).Parent).Name;

                aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"] = nSendParamCtrl.Text;
                aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = "True";

                if (nNameKey.Contains("Ch1"))
                {
                    TextBlock nParamTxt = FindChild<TextBlock>(Channel1Ctrls, ntextboxName);
                    nParamTxt.Text = nSendParamCtrl.Text;
                }
                else if (nNameKey.Contains("Ch2"))
                {
                    TextBlock nParamTxt = FindChild<TextBlock>(Channel2Ctrls, ntextboxName);
                    nParamTxt.Text = nSendParamCtrl.Text;
                }
                else
                {
                    TextBlock nParamTxt = FindChild<TextBlock>(layer1, ntextboxName);
                    nParamTxt.Text = nSendParamCtrl.Text;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in NTextboxCtrl_TextChanged API = " + ex);
            }
        }

        private void NComboBoxCtrl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string nNameKey;
                int nParamIdx = 0;
                char delimiter = '_';
                string[] aParamName = new string[3];
                aParamName = ((FrameworkElement)sender).Name.Split(delimiter);
                nNameKey = aParamName[0];
                nParamIdx = System.Convert.ToInt32(aParamName[2]);
                ComboBox nSendParamCtrl = (ComboBox)sender;
                string nCtrlName = nNameKey + "Lbl";
                string nGridName = ((Grid)((FrameworkElement)sender).Parent).Name;

                JArray aArr_value = (JArray)aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (CHIP_ID == CHIP400x)
                {
                    if (nNameKey.Contains("RintCh2"))
                    {
                        aArr_value = (JArray)aRegAdpdCtrlItems[nGridName]["RintCh1"].Parameters[nParamIdx]["Value"];  //For ADPD4000, no support for Ch2 Integrator resistor
                        regVal_Lst = aArr_value.ToObject<List<string>>();
                    }
                }

                if (regVal_Lst.Count > nSlotSel)
                {
                    regVal_Lst[nSlotSel] = nSendParamCtrl.SelectedIndex.ToString();
                }
                else if (regVal_Lst.Count != 0)
                {
                    regVal_Lst[0] = nSendParamCtrl.SelectedIndex.ToString();
                }

                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();

                if (CHIP_ID == CHIP400x)
                {
                    if (nNameKey.Contains("RintCh2"))
                    {
                        aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nGridName]["RintCh1"].Parameters[nParamIdx]["IsValueChanged"];
                        IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                    }
                }
                else
                {

                }

                if (IsVal_Changed_Lst.Count > nSlotSel)
                {
                    IsVal_Changed_Lst[nSlotSel] = "True";
                }
                else if (IsVal_Changed_Lst.Count != 0)
                {
                    IsVal_Changed_Lst[0] = "True";
                }

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);



                if (CHIP_ID == CHIP400x)
                {
                    if (nNameKey.Contains("RintCh2"))
                    {
                        aRegAdpdCtrlItems[nGridName]["RintCh1"].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                        aRegAdpdCtrlItems[nGridName]["RintCh1"].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                    }
                    else
                    {
                        aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                        aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                    }
                }
                else
                {
                    aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                    aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;
                }

                if (nNameKey.Contains("NumActiveSlots"))
                {
                    UpdateSlotSelectionList(nSendParamCtrl.SelectedIndex);
                    nActiveSlots = nSendParamCtrl.SelectedIndex + 1;
                    UpdateTimingParam_ActiveSlots();
                    Add_TimingDiagram();

                    for (int i = 0; i < nActiveSlots; i++)
                    {
                        AssignPathData_timingParam(i);
                    }
                }


                if (nNameKey.Contains("Ch1"))
                {
                    var txtBlockLst1 = Channel1Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                    foreach (var textblock in txtBlockLst1)
                    {
                        if (textblock.Name == nCtrlName)
                        {
                            textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                        }
                    }

                    if (CHIP_ID == CHIP400x)
                    {
                        if (nNameKey.Contains("Rint"))
                        {
                            nCtrlName = "RintCh2Lbl";
                            var txtBlockLst2 = Channel2Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                            foreach (var textblock in txtBlockLst2)
                            {
                                if (textblock.Name == nCtrlName)
                                {
                                    textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                                }
                            }

                        }
                    }
                }
                else if (nNameKey.Contains("Ch2"))
                {
                    var txtBlockLst2 = Channel2Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                    foreach (var textblock in txtBlockLst2)
                    {
                        if (textblock.Name == nCtrlName)
                        {
                            textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                        }
                    }

                    if (CHIP_ID == CHIP400x)
                    {
                        if (nNameKey.Contains("Rint"))
                        {
                            nCtrlName = "RintCh1Lbl";
                            var txtBlockLst1 = Channel1Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                            foreach (var textblock in txtBlockLst1)
                            {
                                if (textblock.Name == nCtrlName)
                                {
                                    textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                                }
                            }

                        }
                    }
                }
                else if (nNameKey.Contains("ChX"))
                {
                    var txtBlockLst1 = Channel1Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                    foreach (var textblock in txtBlockLst1)
                    {
                        if (textblock.Name == nCtrlName)
                        {
                            textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                        }
                    }

                    var txtBlockLst2 = Channel2Ctrls.Children.OfType<System.Windows.Controls.TextBlock>();

                    foreach (var textblock in txtBlockLst2)
                    {
                        if (textblock.Name == nCtrlName)
                        {
                            textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                        }
                    }
                }
                else
                {
                    var txtBlockLstLayer1 = layer1.Children.OfType<System.Windows.Controls.TextBlock>();

                    foreach (var textblock in txtBlockLstLayer1)
                    {
                        if (textblock.Name == nCtrlName)
                        {
                            textblock.Text = nSendParamCtrl.SelectedValue.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EXception in NComboBoxCtrl_SelectionChanged = " + ex);
            }
        }

        private void UpdateSlotSelectionList(int selectedIndex)
        {

            char ascii = 'A';

            try
            {
                activeSlotLst.Clear();

                for (int i = 1; i <= (selectedIndex + 1); i++)
                {
                    var slotItem = "Slot " + ascii++;
                    activeSlotLst.Add(slotItem);
                }

                SlotSel.ItemsSource = activeSlotLst;

                if (nSlotSel < activeSlotLst.Count)
                {
                    SlotSel.SelectedIndex = nSlotSel;
                }
                else
                {
                    SlotSel.SelectedIndex = 0;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateSlotSelectionList API = " + ex);
            }
        }

        #endregion

        #region CHIPVIEW INIT HELPER FUNCTIONS
        private void DisableInputLines(Canvas nInp)
        {
            try
            {
                if ((nInp == null) || (nInp.Name.Contains("1")))
                {
                    IN1Line.IsEnabled = false;
                    IN1Line.Stroke = Brushes.Gray;
                    IN1Text.Foreground = Brushes.LightGray;

                    IN2Line.IsEnabled = false;
                    IN2Line.Stroke = Brushes.Gray;
                    IN2Text.Foreground = Brushes.LightGray;
                }
                if ((nInp == null) || (nInp.Name.Contains("3")))
                {
                    IN3Line.IsEnabled = false;
                    IN3Line.Stroke = Brushes.Gray;
                    IN3Text.Foreground = Brushes.LightGray;

                    IN4Line.IsEnabled = false;
                    IN4Line.Stroke = Brushes.Gray;
                    IN4Text.Foreground = Brushes.LightGray;
                }
                if ((nInp == null) || (nInp.Name.Contains("5")))
                {
                    IN5Line.IsEnabled = false;
                    IN5Line.Stroke = Brushes.Gray;
                    IN5Text.Foreground = Brushes.LightGray;

                    IN6Line.IsEnabled = false;
                    IN6Line.Stroke = Brushes.Gray;
                    IN6Text.Foreground = Brushes.LightGray;
                }
                if ((nInp == null) || (nInp.Name.Contains("7")))
                {
                    IN7Line.IsEnabled = false;
                    IN7Line.Stroke = Brushes.Gray;
                    IN7Text.Foreground = Brushes.LightGray;

                    IN8Line.IsEnabled = false;
                    IN8Line.Stroke = Brushes.Gray;
                    IN8Text.Foreground = Brushes.LightGray;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in DisableInputLines API = " + ex);
            }
        }

        private void AddTextLabels()
        {
            try
            {
                UIElementCollection nChildrenColl;
                List<TextBlock> nTextLabelColl;
                List<TextBlock> nTextBoxColl;
                TextBlock nTextblk = new TextBlock();
                TextBlock nTextBoxParam = new TextBlock();
                ComboBox nComboBoxParam = new ComboBox();

                for (int nChIdx = 0; nChIdx < 2; nChIdx++)
                {
                    if (nChIdx == 0)
                    {
                        nChildrenColl = Channel1Ctrls.Children;
                    }
                    else
                    {
                        nChildrenColl = Channel2Ctrls.Children;
                    }

                    nTextLabelColl = new List<TextBlock>();
                    nTextBoxColl = new List<TextBlock>();
                    string nNameKey;

                    #region RECTANGLE - RESISTORS
                    IEnumerable<System.Windows.Shapes.Rectangle> aRectList;
                    aRectList = nChildrenColl.OfType<System.Windows.Shapes.Rectangle>();
                    double fLeft = 0;
                    double fTop = 0;
                    foreach (System.Windows.Shapes.Rectangle nRect in aRectList)
                    {
                        nNameKey = (string)nRect.Tag;
                        if (nNameKey != null)
                        {
                            nTextblk = new TextBlock();
                            nTextblk.Text = nNameKey.Substring(0, nNameKey.Length - 3);
                            nTextblk.FontSize = FontSizeLbl;
                            if (nNameKey.Contains("Rf"))
                            {
                                fLeft = Canvas.GetLeft(nRect) + (nLeftDelta + 4);
                                fTop = Canvas.GetTop(nRect) + nRect.Height - (nTopDelta / 2);
                                Canvas.SetLeft(nTextblk, fLeft);
                            }
                            else
                            {
                                fLeft = Canvas.GetLeft(nRect) + nLeftDelta;
                                fTop = Canvas.GetTop(nRect) + nRect.Height - (nTopDelta / 2);
                                Canvas.SetLeft(nTextblk, fLeft);
                            }

                            Console.WriteLine("nameKey Text = " + nTextblk.Text);

                            if (nNameKey.Contains("BPF") || (nNameKey.Contains("Swap")))
                            {
                                fTop = Canvas.GetTop(nRect) + nTopDelta;
                                Canvas.SetTop(nTextblk, fTop);
                                nTextLabelColl.Add(nTextblk);

                                if (nNameKey.Contains("Swap"))
                                {
                                    nTextblk = new TextBlock();
                                    nTextblk.Text = "ON";
                                    nTextblk.Name = nNameKey + "_chopmode";
                                    nTextblk.FontSize = 9;
                                    double fLeft1 = Canvas.GetLeft(nRect) + nLeftDelta+5;
                                    double fTop1 = Canvas.GetTop(nRect) + nRect.Height - ((nTopDelta+68) / 2);
                                    Canvas.SetLeft(nTextblk, fLeft1);
                                    fTop1 = Canvas.GetTop(nRect) + (nTopDelta + 68);
                                    Canvas.SetTop(nTextblk, fTop1);
                                    nTextLabelColl.Add(nTextblk);
                                }
                            }
                            else
                            {
                                Canvas.SetTop(nTextblk, fTop);
                                nTextLabelColl.Add(nTextblk);

                                #region TEXTBOX PARAM ADDITION
                                if ((aRegAdpdCtrlItems["SlotChipSettings"][nNameKey].Parameters.Count == 1)
                                    && ((string)aRegAdpdCtrlItems["SlotChipSettings"][nNameKey].Parameters[0]["Control"] == "ComboBox"))
                                {
                                    nTextBoxParam = new TextBlock();
                                    nTextBoxParam.FontSize = FontSizeLbl;
                                    nTextBoxParam.Name = nNameKey + "Lbl";
                                    Canvas.SetLeft(nTextBoxParam, fLeft);
                                    Canvas.SetTop(nTextBoxParam, Canvas.GetTop(nRect) - (nTopDelta / 2));
                                    nTextBoxColl.Add(nTextBoxParam);
                                }
                                #endregion
                            }
                        }
                    }

                    #endregion

                    foreach (TextBlock elem in nTextLabelColl)
                    {
                        nChildrenColl.Add(elem);
                    }

                    foreach (TextBlock elem in nTextBoxColl)
                    {
                        nChildrenColl.Add(elem);
                    }
                }

                nTextLabelColl = new List<TextBlock>();
                nTextBoxColl = new List<TextBlock>();

                nTextblk = new TextBlock();
                nTextblk.Text = "ADC";
                nTextblk.FontSize = FontSizeLbl;
                Canvas.SetLeft(nTextblk, ADC.Data.Bounds.Left + 21);
                Canvas.SetTop(nTextblk, ADC.Data.Bounds.Top +
                                ((ADC.Data.Bounds.Bottom - ADC.Data.Bounds.Top) / 2 - 7));
                nTextLabelColl.Add(nTextblk);

                #region TEXTBLOCK PARAM ADDITION
                if ((aRegAdpdCtrlItems["SlotChipSettings"]["ADC"].Parameters.Count == 1))
                {
                    nTextBoxParam = new TextBlock();
                    nTextBoxParam.FontSize = FontSizeLbl;
                    nTextBoxParam.Name = "ADCLbl";
                    nTextBoxParam.Text = aRegAdpdCtrlItems["SlotChipSettings"]["ADC"].Parameters[0]["Value"].ToString();
                    Canvas.SetLeft(nTextBoxParam, ADC.Data.Bounds.Left + nLeftDelta);
                    Canvas.SetTop(nTextBoxParam, (ADC.Data.Bounds.Top +
                        ((ADC.Data.Bounds.Bottom - ADC.Data.Bounds.Top) / 2)));
                    nTextBoxColl.Add(nTextBoxParam);
                }
                #endregion

                foreach (TextBlock elem in nTextLabelColl)
                {
                    layer1.Children.Add(elem);
                }

                foreach (TextBlock elem in nTextBoxColl)
                {
                    layer1.Children.Add(elem);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AddTextLabels API = " + ex);
            }
        }

        private void Update_ChopModeEnDis_UI(string EnableState)
        {
            try
            {
                UIElementCollection nChildrenColl;
                for (int nChIdx = 0; nChIdx < 2; nChIdx++)
                {
                    if (nChIdx == 0)
                    {
                        nChildrenColl = Channel1Ctrls.Children;
                    }
                    else
                    {
                        nChildrenColl = Channel2Ctrls.Children;
                    }

                    IEnumerable<TextBlock> aTextblockList;
                    aTextblockList = nChildrenColl.OfType<TextBlock>();
                    foreach (TextBlock nTextBlock in aTextblockList)
                    {
                        if (nTextBlock.Name.Contains("Swap"))
                        {
                            nTextBlock.Text = EnableState;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Update_ChopModeEnDis_UI API = " + ex);
            }
        }

        private void EnCommonCtrls()
        {
            IEnumerable<System.Windows.Shapes.Path> aCtrlList;
            aCtrlList = layer1.Children.OfType<System.Windows.Shapes.Path>();

            foreach (System.Windows.Shapes.Path nPath in aCtrlList)
            {
                if ((nPath.Name.Contains("Mux")) || (nPath.Name.Contains("ADC")) ||
                    (nPath.Name.Contains("Demux")))
                {
                    //nPath.Fill = (LinearGradientBrush)LayoutWin.Resources["linearGradient8961"];
                    //nPath.Stroke = (Brush)new BrushConverter().ConvertFrom("#FF008066");

                    nPath.Fill = (LinearGradientBrush)LayoutWin.Resources["ProgressBrush"];
                    nPath.Stroke = (Brush)new BrushConverter().ConvertFrom("#FF008066");
                }
            }
        }

        private void EnDisCHCtrls(int nChNum, bool EnFlag)
        {
            Brush nStrokeSel;
            LinearGradientBrush nFillGrad;
            UIElementCollection nChildrenColl;

            if (nChNum == 1)
            {
                nChildrenColl = Channel1Ctrls.Children;
            }
            else
            {
                nChildrenColl = Channel2Ctrls.Children;
            }

            if (EnFlag == true)
            {
                nFillGrad = (LinearGradientBrush)LayoutWin.Resources["ProgressBrush"];
                nStrokeSel = StrokeEn;
            }
            else
            {
                nStrokeSel = StrokeDis;
                nFillGrad = (LinearGradientBrush)LayoutWin.Resources["disablegradient"];
            }

            #region PATH CONTROLS
            IEnumerable<System.Windows.Shapes.Path> aCtrlList;
            aCtrlList = nChildrenColl.OfType<System.Windows.Shapes.Path>();
            foreach (System.Windows.Shapes.Path nPath in aCtrlList)
            {
                nPath.Stroke = nStrokeSel;
                nPath.IsEnabled = EnFlag;

                if (nPath.Name.Contains("Tia"))
                {
                    nPath.Fill = nFillGrad;
                }
            }
            #endregion

            #region RECTANGLE CONTROLS
            IEnumerable<System.Windows.Shapes.Rectangle> bCtrlList;
            bCtrlList = nChildrenColl.OfType<System.Windows.Shapes.Rectangle>();
            foreach (System.Windows.Shapes.Rectangle nPath in bCtrlList)
            {
                nPath.IsEnabled = EnFlag;
            }
            #endregion

            #region RECTANGLE - BPF, XOVER
            IEnumerable<System.Windows.Shapes.Rectangle> aRectList;
            aRectList = nChildrenColl.OfType<System.Windows.Shapes.Rectangle>();
            foreach (System.Windows.Shapes.Rectangle nRect in aRectList)
            {
                if (aRegAdpdCtrlItems["SlotChipSettings"].Keys.Contains(nRect.Name))
                {
                    nRect.Stroke = nStrokeSel;
                    nRect.Fill = nFillGrad;
                    nRect.IsEnabled = EnFlag;
                }
            }
            #endregion

        }

        #endregion

        #region SLOTSEL COMBOBOX
        private void SlotSelComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;

            comboBox.SelectedIndex = 0;
        }

        private void SlotSelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // ... Get the ComboBox.
            var comboBox = sender as ComboBox;

            // ... Set SelectedItem as Window Title.
            if (comboBox.SelectedIndex != -1)
            {
                nSlotSel = comboBox.SelectedIndex;
                Add_TimingDiagram();
                UpdateControlValues(true);
            }
        }
        #endregion

        #region MUXCONTROL EVENTS
        private void SetSingleDiffInputLines()
        {
            aSingleInp.Clear();
            aDiffInp.Clear();

            aSingleInp.Add("None");
            aDiffInp.Add("None");

            /* Set the Single and Differential Lines */
            for (int nInpIdx = 0; nInpIdx < aRegAdpdCtrlItems["GlobalSettings"]["InputConfig"].Parameters.Count; nInpIdx++)
            {
                JArray aArr_value = (JArray)aRegAdpdCtrlItems["GlobalSettings"]["InputConfig"].Parameters[nInpIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (regVal_Lst[0] == "0")
                {
                    if (aSingleInp.ElementAt(0) == "None")
                    {
                        aSingleInp.RemoveAt(0);
                    }
                    aSingleInp.Add("IN" + (2 * nInpIdx + 1).ToString());
                    aSingleInp.Add("IN" + (2 * nInpIdx + 2).ToString());
                }
                else
                {
                    if (aDiffInp.ElementAt(0) == "None")
                    {
                        aDiffInp.RemoveAt(0);
                    }
                    aDiffInp.Add("IN" + (2 * nInpIdx + 1).ToString() + "_" + (2 * nInpIdx + 2).ToString());
                }
            }
        }


        private void EnDisCH2MuxCtrls(bool IsEnabled)
        {
            //TODO: Disable/Enable Ch2 Mux controls
            try
            {
                uc_InputMux.m_inputConfig_inst.IS_CH2_ENABLED = IsEnabled;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in EnDisCH2MuxCtrls API = " + ex);
            }
        }

        private void RadExpChipSettings_Collapsed(object sender, RoutedEventArgs e)
        {
            ChipView.Width = ChipGridAcWid;
            ChipView.Height = ChipGridAcHeight;
            RadExpChipSettings.Visibility = Visibility.Hidden;
        }

        public int setBitValues(int OriginalRegVal, int Bitfieldval, int startBit, int endBit)
        {

            ushort mask = 0xFFFF;
            var OriginalRegValtemp = (ushort) OriginalRegVal;
            mask <<= 15 - endBit;
            mask >>= 15 - endBit;
            mask >>= startBit;
            mask <<= startBit;
            mask = (ushort)~mask;
            OriginalRegValtemp &= mask;
            OriginalRegValtemp |= (ushort)(Bitfieldval << startBit);
            return OriginalRegValtemp;
        }

        public int getBitValues(int x, int startBit, int endBit)
        {
            var XTemp = (ushort) x;
            XTemp <<= 15 - endBit;
            XTemp >>= 15 - endBit + startBit;
            return XTemp;
        }

        #endregion

        #region CHIPVIEW EVENTS
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem EnDisCh2 = (MenuItem)sender;

            try
            {
                if (EnDisCh2.Header.ToString() == "Enable CH2")
                {
                    EnDisCHCtrls(2, true);
                    EnDisCh2.Header = "Disable CH2";
                    EnDisCH2MuxCtrls(true);

                    UpdateChannel2_Status(true);
                }
                else
                {
                    EnDisCHCtrls(2, false);
                    EnDisCh2.Header = "Enable CH2";
                    EnDisCH2MuxCtrls(false);

                    UpdateChannel2_Status(false);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in MenuItem_Click API = " + ex);
            }
        }

        private void Swap_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem swapMenu = (MenuItem)sender;

            try
            {

                if (swapMenu.Header.ToString() == "Enable ChopMode")
                {
                    swapMenu.Header = "Disable ChopMode";
                    EnDischopMode_Status(true);
                    Update_ChopModeEnDis_UI("ON");
                }
                else
                {
                    swapMenu.Header = "Enable ChopMode";
                    EnDischopMode_Status(false);
                    Update_ChopModeEnDis_UI("OFF");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Swap_MenuItem_Click API = " + ex);
            }
        }

        void EnDischopMode_Status(bool isEnabled)
        {

            var chopModeDisable_Val = 0x00;

            try
            {
                JArray aArr_value = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["ChopMode_Enable"].Parameters[0]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (isEnabled)
                {
                    regVal_Lst[nSlotSel] = CHOPMODE_ENABLE[nSlotSel].ToString();
                }
                else
                {
                    regVal_Lst[nSlotSel] = chopModeDisable_Val.ToString();
                }

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["ChopMode_Enable"].Parameters[0]["Value"] = jarr_Val_mod;


                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["ChopMode_Enable"].Parameters[0]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                IsVal_Changed_Lst[nSlotSel] = "True";

                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["ChopMode_Enable"].Parameters[0]["IsValueChanged"] = jarr_IsValChanged_mod;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateChannel2_Status API = " + ex);
            }
        }

        private void UpdateChannel2_Status(bool isEnabled)
        {
            try
            {
                JArray aArr_value = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["CH2_Enable"].Parameters[0]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                if (isEnabled)
                {
                    regVal_Lst[nSlotSel] = 1.ToString();
                }
                else
                {
                    regVal_Lst[nSlotSel] = 0.ToString();
                }

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["CH2_Enable"].Parameters[0]["Value"] = jarr_Val_mod;


                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["CH2_Enable"].Parameters[0]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                IsVal_Changed_Lst[nSlotSel] = "True";

                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                aRegAdpdCtrlItems["SlotGlobalSettings"]["CH2_Enable"].Parameters[0]["IsValueChanged"] = jarr_IsValChanged_mod;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateChannel2_Status API = " + ex);
            }
        }

        Expander nExp = null;
        private void AdpdControls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Type nSenderType = sender.GetType();
                Grid nSettingsGrid;

                string nNameKey = (string)((FrameworkElement)sender).Tag;
                Point relativePoint = e.GetPosition(layer1);

                if (nNameKey.Contains("BPF") || nNameKey.Contains("Swap"))
                {
                    return;
                }

                nExp = RadExpChipSettings;
                nSettingsGrid = SlotChipSettings;
                nSettingsGrid.Children.Clear();
                nSettingsGrid.RowDefinitions.Clear();

                if (((FrameworkElement)sender).IsEnabled)
                {
                    RowDefinition nParamRow = new RowDefinition();
                    nSettingsGrid.RowDefinitions.Add(nParamRow);
                    nParamRow.Height = GridLength.Auto;

                    TextBlock nParamName = new TextBlock();
                    nParamName.Text = nNameKey;
                    nParamName.FontSize = 14;
                    nParamName.FontWeight = FontWeights.Normal;
                    nParamName.TextAlignment = TextAlignment.Center;
                    nParamName.VerticalAlignment = VerticalAlignment.Top;
                    nParamName.HorizontalAlignment = HorizontalAlignment.Right;
                    //nParamName.Margin = new Thickness(relativePoint.X - 50, 10, 0, 0);
                    Grid.SetRow(nParamName, 0);
                    nSettingsGrid.Children.Add(nParamName);

                    AddControltoSettings(nSettingsGrid, 1, nNameKey, "SlotChipSettings", true);

                    nExp.Margin = new Thickness(relativePoint.X, relativePoint.Y, 0, 0);
                    nExp.Visibility = Visibility.Visible;
                    nExp.IsExpanded = true;
                    RadExpChipSettings.IsExpanded = true;

                    FrameworkElementExt.BringToFront(nExp);
                    if (nNameKey.Contains("ADC"))
                    {
                        ChipView.Width = ChipView.Width + 100;
                    }

                    if ((nNameKey.Equals("RinChX") || nNameKey.Equals("RintCh2")) && (relativePoint.Y > 280))
                    {
                        ChipView.Height += 32;
                    }

                    if (nNameKey.Equals("RfCh2") && relativePoint.Y > 280)
                    {
                        ChipView.Height += 55;
                    }
                }
                else
                {
                    nExp.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AdpdControl_MouseLeftButtonDown API = " + ex);
            }
        }

        private void AddHandler()
        {
            AddHandler(Mouse.PreviewMouseDownOutsideCapturedElementEvent, new MouseButtonEventHandler(HandleClickOutsideOfControl), true);
        }
        private void HandleClickOutsideOfControl(object sender, MouseButtonEventArgs e)
        {
            if (nExp != null)
            {
                nExp.Visibility = Visibility.Hidden;
            }

            ReleaseMouseCapture();

            Console.WriteLine("Clicekd Outside..Minimizing Expander");
        }

        private void ChkboxCtrl_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                CheckBox chkbox = (CheckBox)sender;
                Brush nStrokeSel;
                LinearGradientBrush nFillGrad;

                if ((bool)chkbox.IsChecked)
                {
                    nStrokeSel = StrokeEn;
                    nFillGrad = (LinearGradientBrush)LayoutWin.Resources["ProgressBrush"];
                }
                else
                {
                    nStrokeSel = StrokeDis;
                    nFillGrad = (LinearGradientBrush)LayoutWin.Resources["disablegradient"];
                }
                if (chkbox.Name.Contains("Swap"))
                {
                    if (chkbox.Name.Contains("Ch1"))
                    {
                        SwapCh1.IsEnabled = (bool)chkbox.IsChecked;
                        SwapCh1.Fill = nFillGrad;
                        SwapCh1.Stroke = nStrokeSel;
                    }
                    else if (chkbox.Name.Contains("Ch2"))
                    {
                        SwapCh2.IsEnabled = (bool)chkbox.IsChecked;
                        SwapCh2.Fill = nFillGrad;
                        SwapCh2.Stroke = nStrokeSel;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ChkboxCtrl_Checked API = " + ex);
            }
        }

        #region SLOTCONFIGURATION SETTINGS
        #endregion

        #endregion

        #region OTHER HELPER FUNCTIONS
        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }

            return foundChild;
        }


        #endregion

        private void LayoutWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendJsonObjectString("Disconnect");
            e.Cancel = true;
            this.Hide();
        }

        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i = 0;

            bool result = int.TryParse(e.Text.ToString(), out i);

            if (!result && (e.Text[0] < '0' || e.Text[0] > '9'))
            {
                e.Handled = true;
            }
        }
    }


    public enum PAIR_CONFIG
    {
        PAIR12,
        PAIR34,
        PAIR56,
        PAIR78,
        NONE
    }

    enum INPUT_MUX_TYPE
    {
        SINGLE,
        DIFFERENTIAL,
        NONE
    }

    public static class FrameworkElementExt
    {
        public static void BringToFront(this FrameworkElement element)
        {
            if (element == null) return;

            Canvas parent = element.Parent as Canvas;
            if (parent == null) return;

            var maxZ = parent.Children.OfType<UIElement>()
              .Where(x => x != element)
              .Select(x => Canvas.GetZIndex(x))
              .Max();
            Canvas.SetZIndex(element, maxZ + 1);
        }
    }

    public class aAdpdCtrlRegParams
    {
        public List<Dictionary<string, object>> Parameters = new List<Dictionary<string, object>>();
    }

    public class NAryDictionary<TKey, TValue> :
        Dictionary<TKey, TValue>
    {
    }

    public class NAryDictionary<TKey1, TKey2, TValue> :
        Dictionary<TKey1, NAryDictionary<TKey2, TValue>>
    {
    }

}

