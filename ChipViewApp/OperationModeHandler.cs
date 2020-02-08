using ChipViewApp.Utils;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Xceed.Wpf.Toolkit;

namespace ChipViewApp
{
    public partial class ChipDesignView
    {
        private void GetOperatingMode()
        {
            try
            {
                iValue = (int)get_SelectedSlot_RegValue(aRegAdpdCtrlItems["SlotGlobalSettings"]["OperatingMode"].Parameters[0]["Value"]);

                OperatingMode = (OPERATING_MODE)iValue;

                SetSlotConfig_OperatingMode(OperatingMode);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetOpeartingMode API = " + ex);
            }
        }

        private void SetSlotConfig_OperatingMode(OPERATING_MODE e_operatingMode)
        {
            try
            {
                if (e_operatingMode == OPERATING_MODE.NORMAL)
                {
                    m_slotConfigParams.OPERATING_MODE_NAME = "NORMAL MODE";
                }
                else if (e_operatingMode == OPERATING_MODE.FLOAT)
                {
                    m_slotConfigParams.OPERATING_MODE_NAME = "FLOAT MODE";
                }
                else if (e_operatingMode == OPERATING_MODE.DIG_INT)
                {
                    m_slotConfigParams.OPERATING_MODE_NAME = "DIGITAL INTEGRATION MODE";
                }
                else
                {
                    m_slotConfigParams.OPERATING_MODE_NAME = "";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in SetSlotConfig_OperatingMode API = " + ex);
            }
        }

        private void EnDisModeCtrls(OPERATING_MODE operationMode)
        {
            List<string> lst_numericTimingCtrl = new List<string>() { "MinPeriod_Param_0", "LitOffset_Param_0", "Dark1Offset_Param_0", "Dark2Offset_Param_0" };
            List<string> lst_NonDITimingCtrl = new List<string>() { "AFEWidth_Param_0", "IntegratorOffset_Param_0", "MODWidth_Param_0", "MODOffset_Param_0" };

            List<string> lst_TextblockTimingCtrl = new List<string>() { "lbl_MinPeriod", "lbl_LitOffset", "lbl_Dark1Offset", "lbl_Dark2Offset" };
            List<string> lst_Tb_NonDI_TimingCtrl = new List<string>() { "lbl_AFEWidth", "lbl_IntegratorOffset", "lbl_MODWidth", "lbl_MODOffset" };

            try
            {
                //DecimalUpDown nParamNumUpDown;
                //TextBlock nParamTextBlock;

                foreach (var numCtrl in lst_numericTimingCtrl)
                {
                    if ((operationMode == OPERATING_MODE.NORMAL) || (operationMode == OPERATING_MODE.UNKNOWN))
                    {
                        EnDis_NumericCtrl(numCtrl, Visibility.Collapsed);
                        foreach (var ctrl in lst_NonDITimingCtrl)
                        {
                            EnDis_NumericCtrl(ctrl, Visibility.Visible);
                        }
                    }
                    else if (operationMode == OPERATING_MODE.FLOAT)
                    {
                        if (numCtrl != "MinPeriod_Param_0")
                        {
                            EnDis_NumericCtrl(numCtrl, Visibility.Collapsed);
                        }
                        else
                        {
                            EnDis_NumericCtrl(numCtrl, Visibility.Visible);
                            foreach (var ctrl in lst_NonDITimingCtrl)
                            {
                                EnDis_NumericCtrl(ctrl, Visibility.Visible);
                            }
                        }
                    }
                    else if (operationMode == OPERATING_MODE.DIG_INT)
                    {
                        EnDis_NumericCtrl(numCtrl, Visibility.Visible);
                        foreach (var ctrl in lst_NonDITimingCtrl)
                        {
                            EnDis_NumericCtrl(ctrl, Visibility.Collapsed);
                        }
                    }
                }

                foreach (var CtrlName in lst_TextblockTimingCtrl)
                {
                    if ((operationMode == OPERATING_MODE.NORMAL) || (operationMode == OPERATING_MODE.UNKNOWN))
                    {
                        EnDis_TextBlockCtrl(CtrlName, Visibility.Collapsed);
                        foreach (var ctrl in lst_Tb_NonDI_TimingCtrl)
                        {
                            EnDis_TextBlockCtrl(ctrl, Visibility.Visible);
                        }
                    }
                    else if (operationMode == OPERATING_MODE.FLOAT)
                    {
                        if (CtrlName != "lbl_MinPeriod")
                        {
                            EnDis_TextBlockCtrl(CtrlName, Visibility.Collapsed);
                        }
                        else
                        {
                            EnDis_TextBlockCtrl(CtrlName, Visibility.Visible);
                            foreach (var ctrl in lst_Tb_NonDI_TimingCtrl)
                            {
                                EnDis_TextBlockCtrl(ctrl, Visibility.Visible);
                            }
                        }
                    }
                    else if (operationMode == OPERATING_MODE.DIG_INT)
                    {
                        EnDis_TextBlockCtrl(CtrlName, Visibility.Visible);
                        foreach (var ctrl in lst_Tb_NonDI_TimingCtrl)
                        {
                            EnDis_TextBlockCtrl(ctrl, Visibility.Collapsed);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in EnDisMinPeriod API = " + ex);
            }
        }

        private void EnDis_NumericCtrl(string CtrlName, Visibility visibleType)
        {
            DecimalUpDown nParamNumUpDown;

            nParamNumUpDown = FindChild<DecimalUpDown>(TimingSettings, CtrlName);

            if (nParamNumUpDown != null)
            {
                nParamNumUpDown.Visibility = visibleType;
            }
        }

        private void EnDis_TextBlockCtrl(string CtrlName, Visibility visibleType)
        {
            TextBlock nParamTextBlock;

            nParamTextBlock = FindChild<TextBlock>(TimingSettings, CtrlName);

            if (nParamTextBlock != null)
            {
                nParamTextBlock.Visibility = visibleType;
            }
        }

    }
}
