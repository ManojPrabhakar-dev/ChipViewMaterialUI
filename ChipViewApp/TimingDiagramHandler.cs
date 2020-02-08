using ChipViewApp.Utils;
using ChipViewApp.ViewModel;
using System;

namespace ChipViewApp
{
    public partial class ChipDesignView
    {
        #region TimingDiagramHandler
        private void UpdateTimingParam_ActiveSlots()
        {
            foreach (string nNameKey in aRegAdpdCtrlItems["TimingSettings"].Keys)
            {
                UpdateTimingDiagram_Params(nNameKey);
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
                else if (nNameKey.Contains("MinPeriod"))
                {
                    lst_timingParam[slotSel].MIN_PERIOD = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("LitOffset"))
                {
                    lst_timingParam[slotSel].LIT_OFFSET = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("Dark1Offset"))
                {
                    lst_timingParam[slotSel].DARK1_OFFSET = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("Dark2Offset"))
                {
                    lst_timingParam[slotSel].DARK2_OFFSET = dValue.ToString() + " µs";
                }
                else if (nNameKey.Contains("LEDPulseStatus"))
                {
                    var ledpulse1_stats = getBitValues((int)dValue, 0, 0);
                    var ledpulse2_stats = getBitValues((int)dValue, 1, 1);
                    var ledpulse3_stats = getBitValues((int)dValue, 2, 2);
                    var ledpulse4_stats = getBitValues((int)dValue, 3, 3);

                    if (ledpulse1_stats == 1)
                    {
                        lst_timingParam[slotSel].LEDPULSE1_STATUS = LEDPULSE_MASKED;
                    }
                    else
                    {
                        lst_timingParam[slotSel].LEDPULSE1_STATUS = LEDPULSE_FLASHED;
                    }

                    if (ledpulse2_stats == 1)
                    {
                        lst_timingParam[slotSel].LEDPULSE2_STATUS = LEDPULSE_MASKED;
                    }
                    else
                    {
                        lst_timingParam[slotSel].LEDPULSE2_STATUS = LEDPULSE_FLASHED;
                    }

                    if (ledpulse3_stats == 1)
                    {
                        lst_timingParam[slotSel].LEDPULSE3_STATUS = LEDPULSE_MASKED;
                    }
                    else
                    {
                        lst_timingParam[slotSel].LEDPULSE3_STATUS = LEDPULSE_FLASHED;
                    }

                    if (ledpulse4_stats == 1)
                    {
                        lst_timingParam[slotSel].LEDPULSE4_STATUS = LEDPULSE_MASKED;
                    }
                    else
                    {
                        lst_timingParam[slotSel].LEDPULSE4_STATUS = LEDPULSE_FLASHED;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignValue_timingParam API = " + ex);
            }
        }

        private void Add_TimingDiagram()
        {
            try
            {
                TimingDiagram_UserControl uc_timingdiagram_normal;
                TimingDiagram_Float_UserControl uc_timingdiagram_float;
                TimingDiagram_DI_UserControl uc_timingdiagram_digital;
                sPanel_timingDiagram.Children.Clear();

                if ((OperatingMode == OPERATING_MODE.NORMAL) || (OperatingMode == OPERATING_MODE.UNKNOWN))
                {
                    uc_timingdiagram_normal = new TimingDiagram_UserControl(lst_timingParam[nSlotSel], dict_slotBrushes[nSlotSel + 1], nSlotSel + 1);
                    lst_timingParam[nSlotSel].SLOTNUM = nSlotSel;
                    uc_timingdiagram_normal.DataContext = lst_timingParam[nSlotSel];
                    sPanel_timingDiagram.Children.Add(uc_timingdiagram_normal);
                }
                else if (OperatingMode == OPERATING_MODE.DIG_INT)
                {
                    uc_timingdiagram_digital = new TimingDiagram_DI_UserControl(lst_timingParam[nSlotSel], dict_slotBrushes[nSlotSel + 1], nSlotSel + 1);
                    lst_timingParam[nSlotSel].SLOTNUM = nSlotSel;
                    uc_timingdiagram_digital.DataContext = lst_timingParam[nSlotSel];
                    sPanel_timingDiagram.Children.Add(uc_timingdiagram_digital);
                }
                else
                {
                    uc_timingdiagram_float = new TimingDiagram_Float_UserControl(lst_timingParam[nSlotSel], dict_slotBrushes[nSlotSel + 1], nSlotSel + 1);
                    lst_timingParam[nSlotSel].SLOTNUM = nSlotSel;
                    uc_timingdiagram_float.DataContext = lst_timingParam[nSlotSel];
                    sPanel_timingDiagram.Children.Add(uc_timingdiagram_float);
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Exception in Add_TimingDiagram API = " + ex);
            }
        }

        private void AssignPathData_timingParam(int slotSel)
        {
            IDynamicTimingDiagram timingParameterInst;
            try
            {
                if (OperatingMode == OPERATING_MODE.FLOAT)
                {
                    timingParameterInst = new TimingDiagram_FloatMode();
                }
                else if (OperatingMode == OPERATING_MODE.DIG_INT)
                {
                    timingParameterInst = new Timingdiagram_DIMode();
                }
                else
                {
                    timingParameterInst = new TimingDiagram_NormalMode();
                }

                timingParameterInst.GetPathData(lst_timingParam[slotSel]);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in AssignPathData_timingParam API = " + ex);
            }
        }

        private void UpdateTimingDiagram_Mode()
        {
            try
            {
                GetOperatingMode();
                EnDisModeCtrls(OperatingMode);
                Add_TimingDiagram();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in UpdateTimingDiagram_Mode API = " + ex);
            }
        }

        #endregion
    }
}
