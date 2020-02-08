using ChipViewApp.Model;
using ChipViewApp.Utils;
using ChipViewApp.ViewModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    public partial class ChipDesignView
    {
        #region INPUTMUX HANDLER
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
        #endregion

        private void init_PairConfig_Params(string pairXX, string pair_type)
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
    }

}
