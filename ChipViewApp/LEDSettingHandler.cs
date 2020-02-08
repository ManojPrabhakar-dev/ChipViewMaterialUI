using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace ChipViewApp
{
    public partial class ChipDesignView
    {
        private void UpdateLedParam_ActiveSlots()
        {
            foreach (string nNameKey in aRegAdpdCtrlItems["LEDSettings"].Keys)
            {
                UpdateLedSetting_Params(nNameKey);
            }
        }

        private void UpdateLedSetting_Params(string nNameKey)
        {
            var nSettingsKey = "LEDSettings";
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
                            AssignValue_LedParam(i, nNameKey, lst_slotValue[i]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateTimingDiagram_Params API = " + ex);
            }
        }

        private void AssignValue_LedParam(int slotSel, string nNameKey, double dValue)
        {
            try
            {
                if (nNameKey.Contains("LEDCurrent1"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDCURRENT1 = dValue;
                }
                else if (nNameKey.Contains("LEDCurrent2"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDCURRENT2 = dValue;
                }
                else if (nNameKey.Contains("LEDCurrent3"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDCURRENT3 = dValue;
                }
                else if (nNameKey.Contains("LEDCurrent4"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDCURRENT4 = dValue;
                }

                if (nNameKey.Contains("LEDSelect1"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDSELECT1 = (int)dValue;
                }
                else if (nNameKey.Contains("LEDSelect2"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDSELECT2 = (int)dValue;
                }
                else if (nNameKey.Contains("LEDSelect3"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDSELECT3 = (int)dValue;
                }
                if (nNameKey.Contains("LEDSelect4"))
                {
                    ledsettingParam.lst_ledsettingParams[slotSel].LEDSELECT4 = (int)dValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignValue_timingParam API = " + ex);
            }
        }

        private void UpdateLEDSettings(UserControl nLEDSettingsUserControl, string nNameKey, string nSettingsKey)
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

                            nParamComboBox = FindChild<ComboBox>(nLEDSettingsUserControl, nNameKey);

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

                            nParamNumUpDown = FindChild<IntegerUpDown>(nLEDSettingsUserControl, nNameKey);

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

        private void UpdateLEDSettingParams()
        {
            try
            {
                UpdateLedParam_ActiveSlots();

                if (ledsettingParam != null)
                {
                    ledsettingParam.lst_ledsettingParams[nSlotSel].LED_SELECTED_INDEX = uc_ledSetting.SelectedLED;
                }

                uc_ledSetting.DataContext = ledsettingParam.lst_ledsettingParams[nSlotSel];
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateLEDSettingParams API = " + ex);
            }
        }

    }
}
