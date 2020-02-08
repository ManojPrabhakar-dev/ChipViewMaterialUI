using ChipViewApp.Model;
using ChipViewApp.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    /// <summary>
    /// Interaction logic for ChipDesignView.xaml
    /// </summary>
    public partial class ChipDesignView
    {
        private string chipDesignJsonPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\ChipDesign_V6.json"; //New
        private const double MAX_TIMING_LIMIT = 30.0;

        public const string IN1 = "IN1";
        public const string IN2 = "IN2";
        public const string IN3 = "IN3";
        public const string IN4 = "IN4";
        public const string IN5 = "IN5";
        public const string IN6 = "IN6";
        public const string IN7 = "IN7";
        public const string IN8 = "IN8";
        public const string LEDPULSE_MASKED = "MASKED";
        public const string LEDPULSE_FLASHED = "FLASHED";
        private Brush StrokeEn = Brushes.DarkCyan; // (Brush)new BrushConverter().ConvertFrom("#FF008066");
        private Brush StrokeDis = Brushes.DarkGray;

        private List<TimingParametersModel> lst_timingParam = new List<TimingParametersModel>();
        private Dictionary<int, Brush> dict_slotBrushes = new Dictionary<int, Brush>();
        private LEDParametersModel ledsettingParam = new LEDParametersModel();
        private SlotConfigParams m_slotConfigParams = new SlotConfigParams();

        private List<string> aSingleInp = new List<string>();
        private List<string> aDiffInp = new List<string>();
        private List<string> aDiffInpCh1 = new List<string>();
        private List<string> aDiffInpCh2 = new List<string>();

        public ObservableCollection<string> activeSlotLst { get; set; }

        private double dValue = 0;
        private int iValue = 0;
        private string slotValue = string.Empty;

        public ushort CHIP400x = 0xc0; public ushort CHIP410x = 0xc2;
        private TextBlock IN1Text;
        private TextBlock IN2Text;
        private TextBlock IN3Text;
        private TextBlock IN4Text;
        private TextBlock IN5Text;
        private TextBlock IN6Text;
        private TextBlock IN7Text;
        private TextBlock IN8Text;
        private TextBlock[] aInTextBlk;
        private System.Windows.Shapes.Path[] aInpLine = new System.Windows.Shapes.Path[8];
        private double ChipGridAcWid;
        private double ChipGridAcHeight;
        private double nLeftDelta = 6;
        private double nTopDelta = 12;
        private double FontSizeLbl = 10;

        /* GLOBAL VARIABLES */

        private OPERATING_MODE operatingMode;
        public OPERATING_MODE OperatingMode
        {
            get => operatingMode;
            set => operatingMode = value;
        }

        private int nSlotSel = 0;
        public int NSlotSel
        {
            get => nSlotSel;
            set
            {
                nSlotSel = value;

                ledsettingParam.NSlotSelCopy = nSlotSel;
            }
        }

        private static int nActiveSlots = 0;
        private int[] aCh1Inp = new int[2];
        private int[] aCh2Inp = new int[2];

        //private static ClientNPipe clientPipe { get; set; }
        private int[] CHOPMODE_ENABLE = new int[] { 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55, 0x55 };

        public ChipDesignView()
        {
            InitializeComponent();

            //  Kill_ChipViewProcess();

            activeSlotLst = new ObservableCollection<string>();
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
                lst_timingParam.Add(new TimingParametersModel());
            }

            ledsettingParam.lst_ledsettingParams = new List<LEDParametersModel>();
            ledsettingParam.lst_ledsettingParams.Clear();
            for (int i = 0; i < 12; i++)
            {
                ledsettingParam.lst_ledsettingParams.Add(new LEDParametersModel());
            }

            //TODO : Need to Remove After integration
            ParseChipDesignJson();
            UpdateChipViewParameters();
            TopControls.DataContext = m_slotConfigParams;

        }

        //TODO : Need to Remove After integration
        private void ParseChipDesignJson()
        {
            try
            {
                using (StreamReader r = new StreamReader(chipDesignJsonPath))
                {
                    string json = r.ReadToEnd();
                    adpdControlJObject = JsonConvert.DeserializeObject<NAryDictionary<string, string, aAdpdCtrlRegParams>>(json);
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
            get => chipID;

            set => chipID = value;
        }

        private NAryDictionary<string, string, aAdpdCtrlRegParams> aRegAdpdCtrlItems = new NAryDictionary<string, string, aAdpdCtrlRegParams>();
        public NAryDictionary<string, string, aAdpdCtrlRegParams> adpdControlJObject
        {
            get => aRegAdpdCtrlItems;
            set => aRegAdpdCtrlItems = value;
        }

        public bool is_ChipViewApp_Initialized = false;
        private bool IsChipView_Initialized
        {
            get => is_ChipViewApp_Initialized;
            set => is_ChipViewApp_Initialized = value;
        }

        public delegate void ChipDesignViewEventHandler(object sender);  //, ChipViewEventArgs Args);
        public event ChipDesignViewEventHandler chipviewHandler;

        private void OnApplyConfigClicked(object sender, RoutedEventArgs e)
        {
            var GlobalSetting_EntryError = IsSettingFieldEmpty(GlobalSettings);
            var TimingSetting_EntryError = IsSettingFieldEmpty(TimingSettings);
            var LedSetting_EntryError = false;

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

        #region CHIPVIEW_CLIENT
        //private ClientNPipe CreateClient()
        //{
        //    ClientNPipe clientPipe = null;
        //    try
        //    {
        //        clientPipe = new ClientNPipe(".", "NPipeComm", p => p.StartStringReaderAsync());
        //        clientPipe.DataReceived += (sndr, args) =>
        //            Dispatcher.BeginInvoke((Action)(() => DataReceivedFromServer(args.String)));
        //        clientPipe.Connect();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.MessageBox.Show("Exception in CreateClient API = " + ex);
        //    }

        //    return clientPipe;
        //}

        private void SendJsonObjectString(string dictObj_str)
        {
            //try
            //{
            //    clientPipe.WriteString(dictObj_str);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine("Exception in ClientSend API = " + ex);

            //    clientPipe = CreateClient();
            //    if (clientPipe != null)
            //    {
            //        clientPipe.WriteString(dictObj_str);
            //    }
            //    else
            //    {
            //        //Kill_ChipViewProcess();
            //        //this.Close();
            //    }
            //}
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
                        UpdateTimingDiagram_Mode();
                        UpdateLEDSettingParams();
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

        public void UpdateChipViewParameters()
        {
            try
            {
                //Create TextBlock
                IN1Text = new TextBlock(); IN2Text = new TextBlock(); IN3Text = new TextBlock(); IN4Text = new TextBlock();
                IN5Text = new TextBlock(); IN6Text = new TextBlock(); IN7Text = new TextBlock(); IN8Text = new TextBlock();
                aInTextBlk = new TextBlock[8] { IN1Text, IN2Text, IN3Text, IN4Text, IN5Text, IN6Text, IN7Text, IN8Text };
                ChipGridAcWid = ChipView.Width;
                ChipGridAcHeight = ChipView.Height;
                aInpLine[0] = IN1Line; aInpLine[1] = IN2Line; aInpLine[2] = IN3Line; aInpLine[3] = IN4Line;
                aInpLine[4] = IN5Line; aInpLine[5] = IN6Line; aInpLine[6] = IN7Line; aInpLine[7] = IN8Line;

                double nLeftval = InputMux.Data.Bounds.Left + (InputMux.Data.Bounds.Width / 3);
                double nInpHtRb = (IN2Line.Data.Bounds.Top - IN1Line.Data.Bounds.Top) / 3;
                double nTopval = IN1Line.Data.Bounds.Top + nInpHtRb;

                DisableInputLines(null);
                InputMux.ContextMenu = (ContextMenu)LayoutWin.Resources["InpMuxMenu"];
                SetSingleDiffInputLines();
                SwapCh1.ContextMenu = (ContextMenu)LayoutWin.Resources["SwapMenu"];
                SwapCh2.ContextMenu = (ContextMenu)LayoutWin.Resources["SwapMenu"];

                GetOperatingMode();

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
                    catch (Exception)
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
                EnDisBPF(false);
                UpdateTimingParam_ActiveSlots();
                Add_TimingDiagram();

                for (int i = 0; i < nActiveSlots; i++)
                {
                    AssignPathData_timingParam(i);
                }

                UpdateLedParam_ActiveSlots();
                uc_ledSetting.DataContext = ledsettingParam.lst_ledsettingParams[nSlotSel];
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception in UpdateChipView Parameters API = " + ex);
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
                    UpdateLEDSettings(uc_ledSetting, nNameKey, "LEDSettings");
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

        private void AddControltoSettings(Grid nSettingsGrid, int rowidx, string nNameKey, string nSettingsKey, bool IsDynamic_ChipSetting = false)
        {
            try
            {
                if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
                {
                    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
                    {
                        var control_name = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString();
                        if ((control_name == "Channel2 Config") || (control_name == "InputMux Config") || (control_name == "Precondition Width (us)") || (control_name == "ChopMode Config") || (control_name == "Operating Mode") || (control_name == "LEDPulse Status"))
                        {
                            continue;
                        }

                        RowDefinition nParamRow = new RowDefinition();
                        nSettingsGrid.RowDefinitions.Add(nParamRow);
                        nParamRow.Height = GridLength.Auto;

                        TextBlock nParamName = new TextBlock();
                        nParamName.Name = "lbl_" + nNameKey;
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
                                nEnCtrl.PreviewTextInput += IntegerUpDown_ADC_PreviewTextInput;

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
                                EnDisBPF(false);
                                MenuItem channel2Menu = InputMux.ContextMenu.Items[0] as MenuItem;
                                channel2Menu.Header = "Enable CH2";
                            }
                            else
                            {
                                EnDisCHCtrls(2, true);
                                EnDisCH2MuxCtrls(true);
                                EnDisBPF(true);
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

                        if ((aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "Precondition Width (us)") ||
                            (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "LEDPulse Status")) //these two parameters dont have OnNumericUpDownvalueChange event,
                                                                                                                                       //so need to update the timing diagram
                        {
                            UpdateTimingDiagram_Params(nNameKey);

                            AssignPathData_timingParam(nSlotSel);
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "InputMux Config")
                        {
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

    }

}

