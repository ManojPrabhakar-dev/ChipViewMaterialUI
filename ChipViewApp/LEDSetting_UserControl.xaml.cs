using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChipViewApp
{
    /// <summary>
    /// Interaction logic for LEDSetting_UserControl.xaml
    /// </summary>
    public partial class LEDSetting_UserControl : UserControl
    {
        public LEDSetting_UserControl()
        {
            InitializeComponent();
        }

        private void rb_ledsetting1_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                var rb_ledSettingX = sender as RadioButton;

                var rb_name = rb_ledSettingX.Name;

                if (rb_name == "rb_led1")
                {

                    rb_led1.Foreground = Brushes.WhiteSmoke;

                    rb_led2.Foreground = Brushes.Black;
                }
                else if (rb_name == "rb_led2")
                {
                    rb_led2.Foreground = Brushes.WhiteSmoke;

                    rb_led1.Foreground = Brushes.Black;
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
