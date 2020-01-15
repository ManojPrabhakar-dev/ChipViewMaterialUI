using System;
using System.Windows;
using System.Windows.Controls;

namespace ChipViewApp
{
    public enum SELECTED_LED
    {
        LED1,
        LED2,
        LED3,
        LED4
    }

    /// <summary>
    /// Interaction logic for LEDSetting_UserControl.xaml
    /// </summary>
    public partial class LEDSetting_UserControl : UserControl
    {
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
        DependencyProperty.Register("SelectedLED", typeof(SELECTED_LED), typeof(LEDSetting_UserControl), new PropertyMetadata(OnSelectedLEDchanged));
        private static void OnSelectedLEDchanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            LEDSetting_UserControl UserControl1Control = d as LEDSetting_UserControl;
            UserControl1Control.OnSelectedLEDchanged(e);
        }

        public void OnSelectedLEDchanged(DependencyPropertyChangedEventArgs e)
        {
            //  cb_topLeft.IsChecked = (bool)e.NewValue;
            var e_selectedLED = (SELECTED_LED)e.NewValue;

            //if (e_selectedLED == SELECTED_LED.LED1)
            //{
            //    SelectedLED = SELECTED_LED.LED1;
            //    rb_led1.IsChecked = true;
            //}
            //else if (e_selectedLED == SELECTED_LED.LED2)
            //{
            //    SelectedLED = SELECTED_LED.LED2;
            //    rb_led2.IsChecked = true;
            //}
            //else if (e_selectedLED == SELECTED_LED.LED3)
            //{
            //    rb_led3.IsChecked = true;
            //}
            //else if (e_selectedLED == SELECTED_LED.LED4)
            //{
            //    rb_led4.IsChecked = true;
            //}

        }
        #endregion

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

        private void LedCurrent_X_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
