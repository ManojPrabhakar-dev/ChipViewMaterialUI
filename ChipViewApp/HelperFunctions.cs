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
                                    double fLeft1 = Canvas.GetLeft(nRect) + nLeftDelta + 5;
                                    double fTop1 = Canvas.GetTop(nRect) + nRect.Height - ((nTopDelta + 68) / 2);
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
                    nPath.Fill = (LinearGradientBrush)App.Current.Resources["ProgressBrush"];  //App.Current.Resources["AppName"]
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
                nFillGrad = (LinearGradientBrush)App.Current.Resources["ProgressBrush"];
                nStrokeSel = StrokeEn;
            }
            else
            {
                nStrokeSel = StrokeDis;
                nFillGrad = (LinearGradientBrush)App.Current.Resources["disablegradient"];
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

        private void EnDisBPF(bool isCh2Enabled = true)
        {
            try
            {
                Brush nStrokeSel;
                LinearGradientBrush nFillGrad;

                if ((OperatingMode == OPERATING_MODE.FLOAT) || (OperatingMode == OPERATING_MODE.DIG_INT))
                {
                    nStrokeSel = StrokeDis;
                    nFillGrad = (LinearGradientBrush)App.Current.Resources["disablegradient"];
                }
                else
                {
                    nFillGrad = (LinearGradientBrush)App.Current.Resources["ProgressBrush"];
                    nStrokeSel = StrokeEn;
                }


                BPFCh1.Stroke = nStrokeSel;
                BPFCh1.Fill = nFillGrad;

                if (isCh2Enabled)
                {
                    BPFCh2.Stroke = nStrokeSel;
                    BPFCh2.Fill = nFillGrad;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in EnDisBPF Api = " + ex);
            }
        }

        #endregion

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

        private void LayoutWin_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            SendJsonObjectString("Disconnect");
            e.Cancel = true;
            this.Hide();
        }

        private void RadExpChipSettings_Collapsed(object sender, RoutedEventArgs e)
        {
            ChipView.Width = ChipGridAcWid;
            ChipView.Height = ChipGridAcHeight;
            RadExpChipSettings.Visibility = Visibility.Hidden;
        }
    }
}
