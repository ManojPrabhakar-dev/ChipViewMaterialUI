using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ChipViewConfig
{
    public class Timing_Parameters : INotifyPropertyChanged
    {
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string chipDesignJsonPath = AppDomain.CurrentDomain.BaseDirectory + "Resources\\ChipDesign_V6.json";

        private Timing_Parameters timing_param = new Timing_Parameters();
        private List<Timing_Parameters> lst_timingParam = new List<Timing_Parameters>();
        private Dictionary<int, Brush> dict_slotBrushes = new Dictionary<int, Brush>();

        private DynamicTimingDiagram m_PathData_inst = new DynamicTimingDiagram();

        //List<aAdpdControlsstruct> AdpdCtrlItems  = new List<aAdpdControlsstruct>();

        TextBlock IN1Text;  // = new TextBlock();
        TextBlock IN2Text;  // = new TextBlock();
        TextBlock IN3Text;  // = new TextBlock();
        TextBlock IN4Text;  // = new TextBlock();
        TextBlock IN5Text;  // = new TextBlock();
        TextBlock IN6Text;  // = new TextBlock();
        TextBlock IN7Text;  // = new TextBlock();
        TextBlock IN8Text;  // = new TextBlock();
        TextBlock[] aInTextBlk; //= new TextBlock[8] {IN1Text, IN2Text,IN3Text,
                                //  IN4Text,IN5Text,IN6Text, IN7Text, IN8Text};
        System.Windows.Shapes.Path[] aInpLine = new System.Windows.Shapes.Path[8];
        double ChipGridAcWid;
        double nLeftDelta = 10;
        double nTopDelta = 10;
        double FontSizeLbl = 8;
        Brush StrokeEn = (Brush)new BrushConverter().ConvertFrom("#FF008066");
        Brush StrokeDis = Brushes.DarkGray;
        /* GLOBAL VARIABLES */
        int nSlotSel = 0;
        int[] aCh1Inp = new int[2];
        int[] aCh2Inp = new int[2];
        List<string> aSingleInp = new List<string>();
        List<string> aDiffInp = new List<string>();
        List<string> aDiffInpCh1 = new List<string>();
        List<string> aDiffInpCh2 = new List<string>();

        List<Border> lst_comboBorder = new List<Border>();

        public ObservableCollection<string> activeSlotLst { get; set; }

        double dValue = 0;
        int iValue = 0;
        string slotValue = string.Empty;

        bool is_InitCompleted = false;

        public const string IN1 = "IN1";
        public const string IN2 = "IN2";
        public const string IN3 = "IN3";
        public const string IN4 = "IN4";
        public const string IN5 = "IN5";
        public const string IN6 = "IN6";
        public const string IN7 = "IN7";
        public const string IN8 = "IN8";

        private INPUT_MUX_TYPE e_InputMux_Type_Ch1 = INPUT_MUX_TYPE.NONE;
        private INPUT_MUX_TYPE e_InputMux_Type_Ch2 = INPUT_MUX_TYPE.NONE;

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

        public MainWindow()
        {
            InitializeComponent();

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

            activeSlotLst = new ObservableCollection<string>();

            dict_slotBrushes.Clear();
            dict_slotBrushes.Add(1, Brushes.CadetBlue);
            dict_slotBrushes.Add(2, Brushes.LightSalmon);
            dict_slotBrushes.Add(3, Brushes.LightSkyBlue);
            dict_slotBrushes.Add(4, Brushes.MediumPurple);
            dict_slotBrushes.Add(5, Brushes.MediumSeaGreen);
            dict_slotBrushes.Add(6, Brushes.MediumVioletRed);
            dict_slotBrushes.Add(7, Brushes.MediumSlateBlue);
            dict_slotBrushes.Add(8, Brushes.LightSlateGray);
            dict_slotBrushes.Add(9, Brushes.MintCream);
            dict_slotBrushes.Add(10, Brushes.Indigo);
            dict_slotBrushes.Add(11, Brushes.IndianRed);
            dict_slotBrushes.Add(12, Brushes.DodgerBlue);

            lst_timingParam.Clear();
            for (int i = 0; i < 12; i++)
            {
                lst_timingParam.Add(new Timing_Parameters());
            }

            this.BringToFront();
            // this.DataContext = timing_param;

            ParseChipDesignJson();
            UpdateChipViewParameters();

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
               
        public delegate void ChipDesignViewEventHandler(object sender);  //, ChipViewEventArgs Args);
        public event ChipDesignViewEventHandler chipviewHandler;
        private void OnApplyConfigClicked(object sender, RoutedEventArgs e)
        {
            chipviewHandler?.Invoke(this);
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
                ChipGridAcWid = ChipView.Width;
                aInpLine[0] = IN1Line;
                aInpLine[1] = IN2Line;
                aInpLine[2] = IN3Line;
                aInpLine[3] = IN4Line;
                aInpLine[4] = IN5Line;
                aInpLine[5] = IN6Line;
                aInpLine[6] = IN7Line;
                aInpLine[7] = IN8Line;
                RbCh2Single.IsChecked = true;
                RbCh2Differential.IsChecked = false;
                RbCh1Single.IsChecked = true;
                RbCh1Differential.IsChecked = false;

                double nLeftval = InputMux.Data.Bounds.Left + (InputMux.Data.Bounds.Width / 3);
                double nInpHtRb = (IN2Line.Data.Bounds.Top - IN1Line.Data.Bounds.Top) / 3;
                double nTopval = IN1Line.Data.Bounds.Top + nInpHtRb;

                DisableInputLines(null);
                InputMux.ContextMenu = (ContextMenu)LayoutWin.Resources["InpMuxMenu"];
                SetSingleDiffInputLines();

                lst_comboBorder.Clear();

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
                    UpdateTimingDiagram_Params(nNameKey);
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

                //Adding timing Diagram to scroll view
                Add_TimingDiagram();

                for (int i = 0; i < 4; i++)
                {
                    AssignPathData_timingParam(i);
                }

                is_InitCompleted = true;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateChipView Parameters API = " + ex);
            }
        }



        private void Add_TimingDiagram()
        {
            try
            {
                //var brush = Brushes.Blue;
                //var timing_UserControl = new TimingDiagram_UserControl(brush);

                //sPanel_timingDiagram.Children.Add(timing_UserControl);

                sPanel_timingDiagram.Children.Clear();

                if (isContinuous == false)
                {
                    var timing_UserControl = new TimingDiagram_UserControl(dict_slotBrushes[nSlotSel + 1], nSlotSel + 1);
                    timing_UserControl.DataContext = lst_timingParam[nSlotSel];
                    sPanel_timingDiagram.Children.Add(timing_UserControl);
                }
                else
                {
                    for (int i = 1; i < 5; i++)
                    {
                        var timing_UserControl = new TimingDiagram_UserControl(dict_slotBrushes[i], i);
                        timing_UserControl.DataContext = lst_timingParam[i - 1];
                        sPanel_timingDiagram.Children.Add(timing_UserControl);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Add_TimingDiagram API = " + ex);
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

                        for (int i = 0; i < 5; i++)
                        {
                            AssignValue_timingParam(i, nNameKey, lst_slotValue[i]);
                        }

                        //if (nNameKey.Contains("LEDWidth"))
                        //{
                        //    timing_param.LED_WIDTH = dValue.ToString() + " µs";
                        //}
                        //else if (nNameKey.Contains("LEDOffset"))
                        //{
                        //    timing_param.LED_OFFSET = dValue.ToString() + " µs";
                        //}
                        //else if (nNameKey.Contains("AFEWidth"))
                        //{
                        //    timing_param.INTEG_WIDTH = dValue.ToString() + " µs";
                        //}
                        //else if (nNameKey.Contains("IntegratorOffset"))
                        //{
                        //    timing_param.INTEG_OFFSET = dValue.ToString() + " ns";
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateTimingDiagram_Params API = " + ex);
            }
        }

        private void AssignValue_timingParam(int slotSel, string nNameKey, double dValue)
        {
            try
            {
                if (nNameKey.Contains("LEDWidth"))
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
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignValue_timingParam API = " + ex);
            }
        }

        private void AssignPathData_timingParam(int slotSel)
        {
            try
            {
                var timingParam = m_PathData_inst.GetPathData(lst_timingParam[slotSel]);

                lst_timingParam[slotSel].PRECONDITION_DATA = timingParam.PRECONDITION_DATA;
                lst_timingParam[slotSel].LED_DATA = timingParam.LED_DATA;
                lst_timingParam[slotSel].INTEGSEQUENCE_DATA = timingParam.INTEGSEQUENCE_DATA;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignPathData_timingParam API = " + ex);
            }
        }

        private void update_ledOffset_check(string value)
        {
            timing_param.LED_WIDTH = value + " µs";
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

        private List<double> get_SelectedSlot_RegValue_lst(object value)
        {
            List<double> lst_val = new List<double>();
            double val = -1;
            try
            {
                JArray aArr = (JArray)value;
                List<string> aItems = aArr.ToObject<List<string>>();


                for (int i = 0; i <= 4; i++)
                {
                    double.TryParse(aItems[i].ToString(), out val);
                    lst_val.Add(val);
                }

                //if (aItems.Count > 1)
                //{
                //    double.TryParse(aItems[nSlotSel].ToString(), out val);
                //}
                //else
                //{
                //    double.TryParse(aItems[0].ToString(), out val);  //Global settings, no slot specific value
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in get_SelectedSlot_RegValue API = " + ex);
            }

            return lst_val;
        }

        private void UpdateComboBox_border(string nSettingsKey, string nNameKey)
        {
            //if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters != null)
            //{
            //    for (int nParamIdx = 0; nParamIdx < aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters.Count; nParamIdx++)
            //    {
            //        var control_name = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString();
            //        if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "ComboBox")
            //        {

            //        }
            //    }
            //}

            foreach (var border in lst_comboBorder)
            {
                var border_name = nNameKey + "_Border_";

                if (border.Name.Contains(border_name))
                {
                    border.BorderBrush = Brushes.Red;
                }
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
                        if ((control_name == "Channel2 Config") || (control_name == "InputMux Config"))
                        {
                            continue;
                        }

                        RowDefinition nParamRow = new RowDefinition();
                        nSettingsGrid.RowDefinitions.Add(nParamRow);
                        nParamRow.Height = GridLength.Auto;

                        TextBlock nParamName = new TextBlock();
                        nParamName.Text = aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"] + ":";
                        nParamName.FontSize = 12;
                        nParamName.FontWeight = FontWeights.Normal;
                        nParamName.TextAlignment = TextAlignment.Left;
                        nParamName.VerticalAlignment = VerticalAlignment.Top;
                        nParamName.Margin = new Thickness(5);
                        Grid.SetColumn(nParamName, 0);
                        Grid.SetRow(nParamName, (nParamIdx + rowidx));
                        nSettingsGrid.Children.Add(nParamName);

                        nParamRow = new RowDefinition();
                        nSettingsGrid.RowDefinitions.Add(nParamRow);
                        nParamRow.Height = GridLength.Auto;
                        if ((string)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Control"] == "ComboBox")
                        {
                            Border nborder = new Border();
                            nborder.Height = 22;
                            nborder.Width = 80;
                            nborder.HorizontalAlignment = HorizontalAlignment.Left;

                            ComboBox nEnCtrl = new ComboBox();
                            nEnCtrl.Name = nNameKey + "_Param_" + nParamIdx.ToString();
                            JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                            List<string> aItems = aArr.ToObject<List<string>>();
                            nEnCtrl.ItemsSource = aItems;
                            //nEnCtrl.Width = 80;
                            //nEnCtrl.Height = 20;
                            //nEnCtrl.HorizontalAlignment = HorizontalAlignment.Left;
                            nEnCtrl.FontSize = 11;
                            nEnCtrl.FontWeight = FontWeights.Normal;
                            nEnCtrl.HorizontalAlignment = HorizontalAlignment.Stretch;
                            nEnCtrl.VerticalAlignment = VerticalAlignment.Stretch;
                            nborder.BorderBrush = Brushes.Transparent;
                            nborder.BorderThickness = new Thickness(1);
                            nborder.Child = nEnCtrl;


                            Grid.SetColumn(nborder, 1);
                            Grid.SetRow(nborder, (nParamIdx + rowidx));

                            nborder.Name = nNameKey + "_Border_" + nParamIdx;
                            nborder.Margin = new Thickness(5);

                            nSettingsGrid.Children.Add(nborder);

                            lst_comboBorder.Add(nborder);

                            iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            if (IsDynamic_ChipSetting)
                            {
                                nEnCtrl.SelectedIndex = iValue;
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

                            //JArray aArr = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Listval"];
                            //List<string> aVals = aArr.ToObject<List<string>>();

                            //iValue = get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                            //if (aVals.Count >= 2)
                            //{
                            //    if (iValue == 0)
                            //    {
                            //        nEnCtrl.Content = aVals[0];
                            //    }
                            //    else
                            //    {
                            //        nEnCtrl.Content = aVals[1];
                            //    }
                            //}

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
                                nEnCtrl.FontSize = 12;
                                nEnCtrl.Width = 80;
                                nEnCtrl.FontWeight = FontWeights.Normal;

                                Grid.SetColumn(nEnCtrl, 1);
                                Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                                nEnCtrl.Margin = new Thickness(5);
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
                                nEnCtrl.FontSize = 12;
                                nEnCtrl.Width = 80;
                                nEnCtrl.FontWeight = FontWeights.Normal;

                                Grid.SetColumn(nEnCtrl, 1);
                                Grid.SetRow(nEnCtrl, (nParamIdx + rowidx));
                                nEnCtrl.Margin = new Thickness(5);
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
                            continue;
                        }

                        if (aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Name"].ToString() == "InputMux Config")
                        {

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
                                }
                                else
                                {
                                    nParamButton.Content = aVals[1];
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
                string nNameKey = comboBox.Name;
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

                string nNameKey = radNumUpDown.Name;
                int nParamIdx = 0;
                string nSettingsKey = ((Grid)(radNumUpDown.Parent)).Name;

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

                // int.TryParse(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"].ToString(), out iValue);

                iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"]);

                JArray aArr_value = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();
                //regVal_Lst[0] = iValue.ToString();
                if (aVals.Count >= 2)
                {
                    if (iValue == 0)
                    {
                        btn.Content = aVals[1];
                        regVal_Lst[0] = 1.ToString();
                        //aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = 1;
                    }
                    else
                    {
                        btn.Content = aVals[0];
                        // aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = 0;
                        regVal_Lst[0] = 0.ToString();
                    }
                }

                JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"];
                List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                IsVal_Changed_Lst[0] = "True";

                var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);

                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;

                // aRegAdpdCtrlItems[nSettingsKey][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = "True";

                if (btn.Name.Contains("InputConfig"))
                {
                    SetSingleDiffInputLines();
                    if (RbCh1Single.IsChecked == true)
                    {
                        RbInpCh_Checked(RbCh1Single, e);
                    }
                    else
                    {
                        RbInpCh_Checked(RbCh1Differential, e);
                    }


                    if (CmbCh2Inp1.IsEnabled)
                    {
                        if (RbCh2Single.IsChecked == true)
                        {
                            RbInpCh_Checked(RbCh2Single, e);
                        }
                        else
                        {
                            RbInpCh_Checked(RbCh2Differential, e);
                        }
                    }

                    //Temp Handling
                    if (CmbCh1Inp1.SelectedItem == null)
                    {
                        CmbCh1Inp1.SelectedIndex = 0;
                    }

                    if (CmbCh2Inp1.SelectedItem == null)
                    {
                        CmbCh2Inp1.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in BtnCtrl_Click API = " + ex);
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
                var border = (Border)((FrameworkElement)sender).Parent;
                string nGridName = ((Grid)(FrameworkElement)border.Parent).Name;
                //string nGridName = ((Grid)((FrameworkElement)sender).Parent).Name;

                if (is_InitCompleted)
                {
                    UpdateComboBox_border("GlobalSettings", nNameKey);
                }

                JArray aArr_value = (JArray)aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"];
                List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

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

                aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["Value"] = jarr_Val_mod;
                aRegAdpdCtrlItems[nGridName][nNameKey].Parameters[nParamIdx]["IsValueChanged"] = jarr_IsValChanged_mod;

                if (nNameKey.Contains("NumActiveSlots"))
                {
                    UpdateSlotSelectionList(nSendParamCtrl.SelectedIndex);
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
                    foreach (System.Windows.Shapes.Rectangle nRect in aRectList)
                    {
                        nNameKey = (string)nRect.Tag;
                        if (nNameKey != null)
                        {
                            nTextblk = new TextBlock();
                            nTextblk.Text = nNameKey.Substring(0, nNameKey.Length - 3);
                            nTextblk.FontSize = FontSizeLbl;
                            double fLeft = Canvas.GetLeft(nRect) + nLeftDelta;
                            double fTop = Canvas.GetTop(nRect) + nRect.Height - (nTopDelta / 2);
                            Canvas.SetLeft(nTextblk, fLeft);

                            Console.WriteLine("nameKey Text = " + nTextblk.Text);

                            if (nNameKey.Contains("BPF") || (nNameKey.Contains("Swap")))
                            {
                                fTop = Canvas.GetTop(nRect) + nTopDelta;
                                Canvas.SetTop(nTextblk, fTop);
                                nTextLabelColl.Add(nTextblk);
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
                Canvas.SetLeft(nTextblk, ADC.Data.Bounds.Left + 10);
                Canvas.SetTop(nTextblk, ADC.Data.Bounds.Top +
                                ((ADC.Data.Bounds.Bottom - ADC.Data.Bounds.Top) / 2 - 10));
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

        private void EnCommonCtrls()
        {
            IEnumerable<System.Windows.Shapes.Path> aCtrlList;
            aCtrlList = layer1.Children.OfType<System.Windows.Shapes.Path>();

            foreach (System.Windows.Shapes.Path nPath in aCtrlList)
            {
                if ((nPath.Name.Contains("Mux")) || (nPath.Name.Contains("ADC")) ||
                    (nPath.Name.Contains("Demux")))
                {
                    nPath.Fill = (LinearGradientBrush)LayoutWin.Resources["linearGradient8961"];
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
                nFillGrad = (LinearGradientBrush)LayoutWin.Resources["linearGradient8961"];
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

                UpdateControlValues(true);

                Add_TimingDiagram();
                //foreach (string nNameKey in aRegAdpdCtrlItems["SlotChipSettings"].Keys)
                //{
                //    Update_Resistor_Lbl(SlotChipSettings, nNameKey, "SlotChipSettings");
                //}
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

        private void InputSelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int inputMux_Config_Val = -1;
            string inputMux_ch1 = string.Empty;
            string inputMux_ch2 = string.Empty;

            ComboBox InpSel = (ComboBox)sender;

            if (InpSel.SelectedIndex != -1)
            {
                if (InpSel.Name == "CmbCh1Inp1")
                {
                    aCh1Inp[0] = InpSel.SelectedIndex;
                }
                //else if (InpSel.Name == "CmbCh1Inp2")
                //{
                //    aCh1Inp[1] = InpSel.SelectedIndex;
                //}
                else if (InpSel.Name == "CmbCh2Inp1")
                {
                    aCh2Inp[0] = InpSel.SelectedIndex;
                }
                //else
                //{
                //    aCh2Inp[1] = InpSel.SelectedIndex;
                //}

                if (e_InputMux_Type_Ch1 == INPUT_MUX_TYPE.SINGLE)
                {
                    if ((aSingleInp.Count > 0) && (aSingleInp.ElementAt(0) != "None"))
                    {
                        inputMux_ch1 = aSingleInp[aCh1Inp[0]];
                    }
                    else
                    {
                        inputMux_ch1 = "None";
                    }
                }
                else if (e_InputMux_Type_Ch1 == INPUT_MUX_TYPE.DIFFERENTIAL)
                {
                    if ((aDiffInp.Count > 0) && (aDiffInp.ElementAt(0) != "None"))
                    {
                        inputMux_ch1 = aDiffInp[aCh1Inp[0]];
                    }
                    else
                    {
                        inputMux_ch1 = "None";
                    }
                }

                if (e_InputMux_Type_Ch2 == INPUT_MUX_TYPE.SINGLE)
                {
                    if ((aSingleInp.Count > 0) && (aSingleInp.ElementAt(0) != "None"))
                    {
                        inputMux_ch2 = aSingleInp[aCh2Inp[0]];
                    }
                    else
                    {
                        inputMux_ch2 = "None";
                    }
                }
                else if (e_InputMux_Type_Ch2 == INPUT_MUX_TYPE.DIFFERENTIAL)
                {
                    if ((aDiffInp.Count > 0) && (aDiffInp.ElementAt(0) != "None"))
                    {
                        inputMux_ch2 = aDiffInp[aCh2Inp[0]];
                    }
                    else
                    {
                        inputMux_ch2 = "None";
                    }
                }

                if ((inputMux_ch1 == "None") && (inputMux_ch2 == "None"))
                {
                    return;
                }

                JArray aArr_value_ch2 = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["CH2_Enable"].Parameters[0]["Value"];
                List<string> regVal_ENCH2_Lst = aArr_value_ch2.ToObject<List<string>>();

                if (regVal_ENCH2_Lst[nSlotSel].ToString() == "0")
                {
                    inputMux_ch2 = "None";
                }

                if (inputMux_ch1 != inputMux_ch2)
                {
                    inputMux_Config_Val = 0;
                    JArray aArr_value = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["Value"];
                    List<string> regVal_Lst = aArr_value.ToObject<List<string>>();

                    //int.TryParse(regVal_Lst[nSlotSel].ToString(), out inputMux_Config_Val);

                    GetInputMux_Value(inputMux_ch1, inputMux_ch2, e_InputMux_Type_Ch1, e_InputMux_Type_Ch2, ref inputMux_Config_Val);

                    regVal_Lst[nSlotSel] = inputMux_Config_Val.ToString();

                    var jarr_Val_mod = JArray.FromObject(regVal_Lst);
                    aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["Value"] = jarr_Val_mod;


                    JArray aArr_IsValChanged = (JArray)aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["IsValueChanged"];
                    List<string> IsVal_Changed_Lst = aArr_IsValChanged.ToObject<List<string>>();
                    IsVal_Changed_Lst[nSlotSel] = "True";

                    var jarr_IsValChanged_mod = JArray.FromObject(IsVal_Changed_Lst);
                    aRegAdpdCtrlItems["SlotGlobalSettings"]["InputMux"].Parameters[0]["IsValueChanged"] = jarr_IsValChanged_mod;

                    Console.WriteLine("CH1 = " + inputMux_ch1 + " CH2 = " + inputMux_ch2 + " Value = " + inputMux_Config_Val);
                }
            }

        }

        private void InputSelComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            ComboBox InpSel = (ComboBox)sender;
            RadioButton[] aRbGrp = new RadioButton[2];
            aRbGrp[0] = RbCh1Single;
            aRbGrp[1] = RbCh2Single;

            if (InpSel.Name.Contains("Ch1"))
            {
                if ((bool)aRbGrp[0].IsChecked)
                {
                    InpSel.ItemsSource = aSingleInp;
                    e_InputMux_Type_Ch1 = INPUT_MUX_TYPE.SINGLE;
                    if (aSingleInp.Contains("None"))
                    {
                        Trigger_InputMux_SelectionChangedEvent(InpSel);

                        return;
                    }
                }
                else
                {
                    InpSel.ItemsSource = aDiffInp;
                    e_InputMux_Type_Ch1 = INPUT_MUX_TYPE.DIFFERENTIAL;
                    if (aDiffInp.Contains("None"))
                    {

                        Trigger_InputMux_SelectionChangedEvent(InpSel);
                        return;
                    }
                }
                if (InpSel.Items.Count > 0)
                {
                    // InpSel.SelectedIndex = 0;
                    Trigger_InputMux_SelectionChangedEvent(InpSel);
                }
            }
            else
            {
                if ((bool)aRbGrp[1].IsChecked)
                {
                    InpSel.ItemsSource = aSingleInp;
                    e_InputMux_Type_Ch2 = INPUT_MUX_TYPE.SINGLE;
                    if (aSingleInp.Contains("None"))
                    {
                        Trigger_InputMux_SelectionChangedEvent(InpSel);

                        return;
                    }
                }
                else
                {
                    InpSel.ItemsSource = aDiffInp;
                    e_InputMux_Type_Ch2 = INPUT_MUX_TYPE.DIFFERENTIAL;
                    if (aDiffInp.Contains("None"))
                    {

                        Trigger_InputMux_SelectionChangedEvent(InpSel);
                        return;
                    }
                }

                if (InpSel.Items.Count > 1)
                {
                    if (InpSel.SelectedIndex != 1)
                    {
                        InpSel.SelectedIndex = 1;
                    }
                    else
                    {
                        InputSelComboBox_SelectionChanged(InpSel, null);
                    }
                    //  InpSel.SelectedIndex = 1;
                }
                else if (InpSel.Items.Count == 1)
                {
                    if (InpSel.SelectedIndex != 1)
                    {
                        InpSel.SelectedIndex = 0;
                    }
                    else
                    {
                        InputSelComboBox_SelectionChanged(InpSel, null);
                    }
                }

            }
        }

        private void Trigger_InputMux_SelectionChangedEvent(object sender)
        {
            ComboBox InpSel = (ComboBox)sender;
            if (InpSel.SelectedIndex != 0)
            {
                InpSel.SelectedIndex = 0;
            }
            else
            {
                // InpSel.SelectedIndex = 0;
                InputSelComboBox_SelectionChanged(sender, null);
            }
            //InpSel.Items.Refresh();
        }

        private void EnDisCH2MuxCtrls(bool IsEnabled)
        {
            lblCh2Inp1.IsEnabled = IsEnabled;
            CmbCh2Inp1.IsEnabled = IsEnabled;
            RbCh2Differential.IsEnabled = IsEnabled;
            RbCh2Single.IsEnabled = IsEnabled;
        }

        private void RbInpCh_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rbInpCh = (RadioButton)sender;
            if (rbInpCh.Name.Contains("Ch1"))
            {
                if ((string)rbInpCh.Content == "Single")
                {
                    RbCh1Single.IsChecked = rbInpCh.IsChecked;
                    RbCh1Differential.IsChecked = !rbInpCh.IsChecked;
                    e_InputMux_Type_Ch1 = INPUT_MUX_TYPE.SINGLE;
                }
                else
                {
                    RbCh1Single.IsChecked = !rbInpCh.IsChecked;
                    RbCh1Differential.IsChecked = rbInpCh.IsChecked;
                    e_InputMux_Type_Ch1 = INPUT_MUX_TYPE.DIFFERENTIAL;
                }
                InputSelComboBox_Loaded(CmbCh1Inp1, e);
            }
            else
            {
                if ((string)rbInpCh.Content == "Single")
                {
                    RbCh2Single.IsChecked = rbInpCh.IsChecked;
                    RbCh2Differential.IsChecked = !rbInpCh.IsChecked;
                    e_InputMux_Type_Ch2 = INPUT_MUX_TYPE.SINGLE;
                }
                else
                {
                    RbCh2Single.IsChecked = !rbInpCh.IsChecked;
                    RbCh2Differential.IsChecked = rbInpCh.IsChecked;
                    e_InputMux_Type_Ch2 = INPUT_MUX_TYPE.DIFFERENTIAL;
                }
                InputSelComboBox_Loaded(CmbCh2Inp1, e);
            }

            CmbCh1Inp1.Items.Refresh();
            CmbCh2Inp1.Items.Refresh();
        }

        private void RadExpChipSettings_Collapsed(object sender, RoutedEventArgs e)
        {
            ChipView.Width = ChipGridAcWid;
            RadExpChipSettings.Visibility = Visibility.Hidden;
        }

        private void GetInputMux_Value(string inputCh1, string inputCh2, INPUT_MUX_TYPE e_Input_Mux_Type_ch1, INPUT_MUX_TYPE e_Input_Mux_Type_ch2, ref int inputMux_Val_ChX)
        {
            int inputMux_Val_Combined = -1;
            int inputMux_Val_Ch1 = -1;
            int inputMux_Val_Ch2 = -1;
            int startbit = -1;
            int endbit = -1;
            PAIR_CONFIG e_PairInfo = PAIR_CONFIG.NONE;

            //Channel1
            string IN_1_1 = string.Empty;
            string IN_1_2 = string.Empty;

            //Channel2
            string IN_2_1 = string.Empty;
            string IN_2_2 = string.Empty;

            try
            {
                Get_PairInfo_BitRange(inputCh1, ref startbit, ref endbit, ref e_PairInfo, ref IN_1_1, ref IN_1_2);
                var e_PairInfo_Ch1 = e_PairInfo;
                var startbit_ch1 = startbit;
                var endbit_ch1 = endbit;

                Get_PairInfo_BitRange(inputCh2, ref startbit, ref endbit, ref e_PairInfo, ref IN_2_1, ref IN_2_2);
                var e_PairInfo_Ch2 = e_PairInfo;
                var startbit_ch2 = startbit;
                var endbit_ch2 = endbit;

                if (e_Input_Mux_Type_ch1 == INPUT_MUX_TYPE.SINGLE)
                {
                    if (e_PairInfo_Ch1 == e_PairInfo_Ch2)  //Combined
                    {
                        if ((inputCh1 != "None") && (inputCh2 != "None"))
                        {
                            if (inputCh1.Contains(IN_1_1) && inputCh2.Contains(IN_1_2))
                            {
                                inputMux_Val_Combined = 5;
                            }

                            if (inputCh1.Contains(IN_1_2) && inputCh2.Contains(IN_1_1))
                            {
                                inputMux_Val_Combined = 6;
                            }
                        }
                    }
                    else
                    {
                        if (inputCh1 != "None")
                        {
                            //Pair 1                 
                            if (inputCh1.Contains(IN_1_1))
                            {
                                inputMux_Val_Ch1 = 1;
                            }
                            else if (inputCh1.Contains(IN_1_2))
                            {
                                inputMux_Val_Ch1 = 3;
                            }
                        }
                    }
                }

                if (e_Input_Mux_Type_ch2 == INPUT_MUX_TYPE.SINGLE)
                {
                    if (e_PairInfo_Ch1 == e_PairInfo_Ch2) //may not needed..this will be updated in ch1 combined block
                    {
                        if ((inputCh1 != "None") && (inputCh2 != "None"))
                        {
                            if (inputCh1.Contains(IN_2_1) && inputCh2.Contains(IN_2_2))
                            {
                                inputMux_Val_Combined = 5;
                            }
                            if (inputCh1.Contains(IN_2_2) && inputCh2.Contains(IN_2_1))
                            {
                                inputMux_Val_Combined = 6;
                            }
                        }
                    }
                    else
                    {
                        if (inputCh2 != "None")
                        {
                            if (inputCh2.Contains(IN_2_1))
                            {
                                inputMux_Val_Ch2 = 2;
                            }
                            else if (inputCh2.Contains(IN_2_2))
                            {
                                inputMux_Val_Ch2 = 4;
                            }
                        }
                    }
                }


                if (e_Input_Mux_Type_ch1 == INPUT_MUX_TYPE.DIFFERENTIAL)
                {
                    if (inputCh1 != "None")
                    {
                        if (inputCh1.Contains(IN_1_1) && inputCh1.Contains(IN_1_2))
                        {
                            inputMux_Val_Ch1 = 7;
                        }
                    }
                }
                if (e_Input_Mux_Type_ch2 == INPUT_MUX_TYPE.DIFFERENTIAL)
                {
                    if (inputCh2 != "None")
                    {
                        if (inputCh2.Contains(IN_2_1) && inputCh2.Contains(IN_2_2))
                        {
                            inputMux_Val_Ch2 = 8;
                        }
                    }
                }

                if (inputMux_Val_Combined != -1)
                {
                    inputMux_Val_ChX = (ushort)setBitValues(inputMux_Val_ChX, inputMux_Val_Combined, startbit_ch1, endbit_ch1);
                }
                else
                {
                    if (inputMux_Val_Ch1 != -1)
                    {
                        inputMux_Val_ChX = (ushort)setBitValues(inputMux_Val_ChX, inputMux_Val_Ch1, startbit_ch1, endbit_ch1);
                    }

                    if (inputMux_Val_Ch2 != -1)
                    {
                        inputMux_Val_ChX = (ushort)setBitValues(inputMux_Val_ChX, inputMux_Val_Ch2, startbit_ch2, endbit_ch2);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in HetInputMux_Value API = " + ex);
            }

        }

        private void Get_PairInfo_BitRange(string inputChX, ref int startbit, ref int endbit, ref PAIR_CONFIG e_PairConfig, ref string IN_X_1, ref string IN_X_2)
        {
            try
            {
                if (inputChX.Contains("1") || inputChX.Contains("2"))
                {
                    startbit = 0;
                    endbit = 3;
                    e_PairConfig = PAIR_CONFIG.PAIR12;
                    IN_X_1 = "1";
                    IN_X_2 = "2";
                }
                else if (inputChX.Contains("3") || inputChX.Contains("4"))
                {
                    startbit = 4;
                    endbit = 7;
                    e_PairConfig = PAIR_CONFIG.PAIR34;
                    IN_X_1 = "3";
                    IN_X_2 = "4";
                }
                else if (inputChX.Contains("5") || inputChX.Contains("6"))
                {
                    startbit = 8;
                    endbit = 11;
                    e_PairConfig = PAIR_CONFIG.PAIR56;
                    IN_X_1 = "5";
                    IN_X_2 = "6";
                }
                else if (inputChX.Contains("7") || inputChX.Contains("8"))
                {
                    startbit = 12;
                    endbit = 15;
                    e_PairConfig = PAIR_CONFIG.PAIR78;
                    IN_X_1 = "7";
                    IN_X_2 = "8";
                }
                else
                {
                    e_PairConfig = PAIR_CONFIG.NONE;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Get_PairInfo_BitRange API = " + ex);
            }
        }

        public int setBitValues(int OriginalRegVal, int Bitfieldval, int startBit, int endBit)
        {

            ushort mask = 0xFFFF;
            var OriginalRegValtemp = (ushort)OriginalRegVal;
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
            var XTemp = (ushort)x;
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

                InputSelComboBox_SelectionChanged(CmbCh1Inp1, null);

                InputSelComboBox_SelectionChanged(CmbCh2Inp1, null);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in MenuItem_Click API = " + ex);
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

        private void AdpdControls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Type nSenderType = sender.GetType();
                Grid nSettingsGrid;
                Expander nExp;
                string nNameKey = (string)((FrameworkElement)sender).Tag;
                Point relativePoint = e.GetPosition(layer1);

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
                    nFillGrad = (LinearGradientBrush)LayoutWin.Resources["linearGradient8961"];
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

        private void change_ledOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown radNumUpDown = (IntegerUpDown)sender;

            update_ledOffset_check(radNumUpDown.Value.ToString());

            //foreach (string nNameKey in aRegAdpdCtrlItems["TimingSettings"].Keys)
            //{
            //   // UpdateTimingDiagram_Params(nNameKey);
            //}
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
