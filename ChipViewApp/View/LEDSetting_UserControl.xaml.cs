using ChipViewApp.Utils;
using Microsoft.Expression.Shapes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace ChipViewApp
{
   
    /// <summary>
    /// Interaction logic for LEDSetting_UserControl.xaml
    /// </summary>
    public partial class LEDSetting_UserControl : UserControl
    {
        Brush StrokeEn = Brushes.DarkCyan; // (Brush)new BrushConverter().ConvertFrom("#FF008066");
        Brush StrokeDis = Brushes.DarkGray;

        public LEDSetting_UserControl()
        {
            InitializeComponent();
        }

        #region SELECTED_LED
        public SELECTED_LED SelectedLED
        {
            get
            {
                return (SELECTED_LED)GetValue(selectedLedProperty);
            }
            set
            {
                SetValue(selectedLedProperty, value);
                if (value == SELECTED_LED.LED1)
                {
                    rb_led1.IsChecked = true;
                }
                else if (value == SELECTED_LED.LED2)
                {
                    rb_led2.IsChecked = true;
                }
                else if (value == SELECTED_LED.LED3)
                {
                    rb_led3.IsChecked = true;
                }
                else if (value == SELECTED_LED.LED4)
                {
                    rb_led4.IsChecked = true;
                }
            }
        }

        // Using a DependencyProperty as the backing store for IsTopLeftChecked.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty selectedLedProperty =
        DependencyProperty.Register("SelectedLED", typeof(SELECTED_LED), typeof(LEDSetting_UserControl), new PropertyMetadata(SELECTED_LED.LED1));

        #endregion


        private void EnDis_LEDxCtrls(LED_DRIVESIDE ledSelect)
        {
            try
            {
                if (ledSelect == LED_DRIVESIDE.LEDA)
                {
                    ManageControlsVisibility(PATH_COMPONENT.PATH, LED_DRIVESIDE.LEDA, false);
                    ManageControlsVisibility(PATH_COMPONENT.PATH, LED_DRIVESIDE.LEDB, true);

                    ManageControlsVisibility(PATH_COMPONENT.REGULAR_POLYGON, LED_DRIVESIDE.LEDA, false);
                    ManageControlsVisibility(PATH_COMPONENT.REGULAR_POLYGON, LED_DRIVESIDE.LEDB, true);

                    ManageControlsVisibility(PATH_COMPONENT.ARC, LED_DRIVESIDE.LEDA, false);
                    ManageControlsVisibility(PATH_COMPONENT.ARC, LED_DRIVESIDE.LEDB, true);
                }
                else
                {
                    ManageControlsVisibility(PATH_COMPONENT.PATH, LED_DRIVESIDE.LEDB, false);
                    ManageControlsVisibility(PATH_COMPONENT.PATH, LED_DRIVESIDE.LEDA, true);

                    ManageControlsVisibility(PATH_COMPONENT.REGULAR_POLYGON, LED_DRIVESIDE.LEDB, false);
                    ManageControlsVisibility(PATH_COMPONENT.REGULAR_POLYGON, LED_DRIVESIDE.LEDA, true);

                    ManageControlsVisibility(PATH_COMPONENT.ARC, LED_DRIVESIDE.LEDB, false);
                    ManageControlsVisibility(PATH_COMPONENT.ARC, LED_DRIVESIDE.LEDA, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in EnDis_LEDxCtrls API = " + ex);
            }
        }

        private void ManageControlsVisibility(PATH_COMPONENT e_component, LED_DRIVESIDE e_ledDriveside, bool isEnable)
        {
            Brush nStrokeSel_EnDis;
            LinearGradientBrush nFillGrad_EnDis;
            UIElementCollection nChildrenColl_LEDx;

            try
            {
                if (e_ledDriveside == LED_DRIVESIDE.LEDA)
                {
                    nChildrenColl_LEDx = LEDA_Ctrls.Children;
                }
                else
                {
                    nChildrenColl_LEDx = LEDB_Ctrls.Children;
                }

                if (isEnable)
                {
                    nStrokeSel_EnDis = StrokeEn;
                    nFillGrad_EnDis = (LinearGradientBrush)App.Current.Resources["ProgressBrush"];
                }
                else
                {
                    nStrokeSel_EnDis = StrokeDis;
                    nFillGrad_EnDis = (LinearGradientBrush)App.Current.Resources["disablegradient"];
                }

                if (e_component == PATH_COMPONENT.PATH)
                {
                    IEnumerable<Path> aCtrlList;
                    aCtrlList = nChildrenColl_LEDx.OfType<Path>();
                    foreach (Path nPath in aCtrlList)
                    {
                        nPath.Stroke = nStrokeSel_EnDis;
                        nPath.IsEnabled = isEnable;
                    }
                }
                else if (e_component == PATH_COMPONENT.REGULAR_POLYGON)
                {
                    IEnumerable<RegularPolygon> ploygonCtrlList;
                    ploygonCtrlList = nChildrenColl_LEDx.OfType<RegularPolygon>();
                    foreach (RegularPolygon nPolygon in ploygonCtrlList)
                    {
                        nPolygon.Fill = nFillGrad_EnDis;
                        nPolygon.Stroke = StrokeDis;
                        nPolygon.IsEnabled = isEnable;
                    }
                }
                else if (e_component == PATH_COMPONENT.ARC)
                {
                    IEnumerable<Arc> aCtrlList;
                    aCtrlList = nChildrenColl_LEDx.OfType<Arc>();
                    foreach (var nArc in aCtrlList)
                    {
                        nArc.Stroke = nStrokeSel_EnDis;
                        nArc.IsEnabled = isEnable;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in ManageControlVisibility API = " + ex);
            }
        }

        private void rb_ledsettingX_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var rb_ledSettingX = sender as RadioButton;

                var rb_name = rb_ledSettingX.Name;

                if (rb_name == "rb_led1")
                {
                    //rb_led1.Foreground = Brushes.WhiteSmoke;
                    //rb_led2.Foreground = Brushes.Black;
                    SelectedLED = SELECTED_LED.LED1;

                }
                else if (rb_name == "rb_led2")
                {
                    //rb_led2.Foreground = Brushes.WhiteSmoke;
                    //rb_led1.Foreground = Brushes.Black;
                    SelectedLED = SELECTED_LED.LED2;
                }
                else if (rb_name == "rb_led3")
                {
                    //rb_led2.Foreground = Brushes.WhiteSmoke;
                    //rb_led1.Foreground = Brushes.Black;
                    SelectedLED = SELECTED_LED.LED3;
                }
                else if (rb_name == "rb_led4")
                {
                    //rb_led2.Foreground = Brushes.WhiteSmoke;
                    //rb_led1.Foreground = Brushes.Black;
                    SelectedLED = SELECTED_LED.LED4;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in rb_LedSettingsX Checked API = " + ex);
            }
        }


        public event RoutedPropertyChangedEventHandler<object> ledCurrentValueChanged;
        private void LedCurrent_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            ledCurrentValueChanged?.Invoke(sender, e);
        }


        public event SelectionChangedEventHandler ledDrivesideSelectionChanged;
        private void LedDriveside_X_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ledDrivesideSelectionChanged?.Invoke(sender, e);

            var combobox = sender as ComboBox;

            if (combobox.SelectedIndex == 0)
            {
                EnDis_LEDxCtrls(LED_DRIVESIDE.LEDB);
            }
            else
            {
                EnDis_LEDxCtrls(LED_DRIVESIDE.LEDA);
            }
        }
    }
}
