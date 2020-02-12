using ChipViewApp.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Xceed.Wpf.Toolkit;

namespace ChipViewApp
{
    public partial class ChipDesignView
    {
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

                if ((nUpDwn.Value >= 10000) && (nNameKey == "SamplingRate"))
                {
                    nUpDwn.Value = 10000;
                }

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

                    UpdateLedParam_ActiveSlots();

                    uc_ledSetting.DataContext = ledsettingParam.lst_ledsettingParams[nSlotSel];
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

        private void LEDSelectX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBox comboBox = (ComboBox)sender;
                string nNameKey = "";
                int nParamIdx = 0;
                string nSettingsKey = "LEDSettings";

                if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED1)
                {
                    nNameKey = "LEDSelect1";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDSELECT1 = comboBox.SelectedIndex;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED2)
                {
                    nNameKey = "LEDSelect2";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDSELECT2 = comboBox.SelectedIndex;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED3)
                {
                    nNameKey = "LEDSelect3";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDSELECT3 = comboBox.SelectedIndex;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED4)
                {
                    nNameKey = "LEDSelect4";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDSELECT4 = comboBox.SelectedIndex;
                }


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

                string nNameKey = ""; // radNumUpDown.Name;
                int nParamIdx = 0;
                string nSettingsKey = "LEDSettings";// ((Grid)(radNumUpDown.Parent)).Name;

                if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED1)
                {
                    nNameKey = "LEDCurrent1";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDCURRENT1 = (double)radNumUpDown.Value;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED2)
                {
                    nNameKey = "LEDCurrent2";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDCURRENT2 = (double)radNumUpDown.Value;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED3)
                {
                    nNameKey = "LEDCurrent3";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDCURRENT3 = (double)radNumUpDown.Value;
                }
                else if (ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX == SELECTED_LED.LED4)
                {
                    nNameKey = "LEDCurrent4";
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LEDCURRENT4 = (double)radNumUpDown.Value;
                }

                ledsettingParam.lst_ledsettingParams[nSlotSel].LBL_LEDCURRENT_X_mA = ledsettingParam.lst_ledsettingParams[nSlotSel].Convert_Ledx_Current_in_mA((double)radNumUpDown.Value) + " mA";

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

        private void IntegerUpDown_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i = 0;

            bool result = int.TryParse(e.Text.ToString(), out i);

            if (!result && (e.Text[0] < '0' || e.Text[0] > '9'))
            {
                e.Handled = true;
            }
        }

        private void IntegerUpDown_ADC_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int i = 0;

            bool result = int.TryParse(e.Text.ToString(), out i);

            if (!result && (e.Text[0] < '0' || e.Text[0] > '9') && (e.Text[0] < 'A' || e.Text[0] > 'F') && (e.Text[0] < 'a' || e.Text[0] > 'f'))
            {
                e.Handled = true;
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
                    nFillGrad = (LinearGradientBrush)LayoutWin.Resources["ProgressBrush"];
                }
                else
                {
                    nStrokeSel = StrokeDis;
                    nFillGrad = (LinearGradientBrush)LayoutWin.Resources["disablegradient"];
                }
                if (chkbox.Name.Contains("Chop"))
                {
                    if (chkbox.Name.Contains("Ch1"))
                    {
                        ChopCh1.IsEnabled = (bool)chkbox.IsChecked;
                        ChopCh1.Fill = nFillGrad;
                        ChopCh1.Stroke = nStrokeSel;
                    }
                    else if (chkbox.Name.Contains("Ch2"))
                    {
                        ChopCh2.IsEnabled = (bool)chkbox.IsChecked;
                        ChopCh2.Fill = nFillGrad;
                        ChopCh2.Stroke = nStrokeSel;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ChkboxCtrl_Checked API = " + ex);
            }
        }

        #endregion

        #region SLOTSEL COMBOBOX
        private void SlotSelComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var comboBox = sender as ComboBox;
            uc_ledSetting.SelectedLED = SELECTED_LED.LED1;
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

                UpdateTimingDiagram_Mode();

                UpdateControlValues(true);

                UpdateLEDSettingParams();
            }
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
                    EnDisBPF(true);
                    UpdateChannel2_Status(true);
                }
                else
                {
                    EnDisCHCtrls(2, false);
                    EnDisCh2.Header = "Enable CH2";
                    EnDisCH2MuxCtrls(false);
                    EnDisBPF(false);
                    UpdateChannel2_Status(false);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in MenuItem_Click API = " + ex);
            }
        }

        private void Chop_MenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem chopMenu = (MenuItem)sender;
            try
            {
                if (chopMenu.Header.ToString() == "Enable ChopMode")
                {
                    chopMenu.Header = "Disable ChopMode";
                    EnDischopMode_Status(true);
                    Update_ChopModeEnDis_UI("ON");
                }
                else
                {
                    chopMenu.Header = "Enable ChopMode";
                    EnDischopMode_Status(false);
                    Update_ChopModeEnDis_UI("OFF");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Chop_MenuItem_Click API = " + ex);
            }
        }

        private void EnDischopMode_Status(bool isEnabled)
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

        #endregion

        private Expander nExp = null;
        private void AdpdControls_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Type nSenderType = sender.GetType();
                Grid nSettingsGrid;

                string nNameKey = (string)((FrameworkElement)sender).Tag;
                Point relativePoint = e.GetPosition(layer1);

                if (nNameKey.Contains("BPF") || nNameKey.Contains("Chop"))
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
                        if (ChipView.Width < 1000)
                        {
                            ChipView.Width = ChipView.Width + 100;
                        }                       
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
    }
}
