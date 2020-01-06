using System.Windows;
using System.Windows.Controls;

namespace ChipViewConfig
{
    /// <summary>
    /// Interaction logic for TimingParam_Info_UserControl.xaml
    /// </summary>
    public partial class TimingParam_Info_UserControl : UserControl
    {
        public TimingParam_Info_UserControl()
        {
            InitializeComponent();
        }

        public string TimingValue
        {
            get
            {
                return (string)GetValue(TimingValueProperty);
            }
            set
            {
                SetValue(TimingValueProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for TimingValue.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimingValueProperty =
            DependencyProperty.Register("TimingValue", typeof(string), typeof(TimingParam_Info_UserControl), new PropertyMetadata(OnTimingValue_TextChanged));

        private static void OnTimingValue_TextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            TimingParam_Info_UserControl UserControl1Control = d as TimingParam_Info_UserControl;
            UserControl1Control.OnTimingValue_TextChanged(e);
        }

        private void OnTimingValue_TextChanged(DependencyPropertyChangedEventArgs e)
        {
            tb_Width_value.Text = e.NewValue.ToString();
        }


        //---- TimingWidthName



        public string TimingWidthName
        {
            get { return (string)GetValue(TimingWidthNameProperty); }
            set { SetValue(TimingWidthNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TimingWidthName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TimingWidthNameProperty =
            DependencyProperty.Register("TimingWidthName", typeof(string), typeof(TimingParam_Info_UserControl), new PropertyMetadata(OnTimingWidthName_TextChanged));

        private static void OnTimingWidthName_TextChanged(DependencyObject d,
         DependencyPropertyChangedEventArgs e)
        {
            TimingParam_Info_UserControl UserControl1Control = d as TimingParam_Info_UserControl;
            UserControl1Control.OnTimingWidthName_TextChanged(e);
        }

        private void OnTimingWidthName_TextChanged(DependencyPropertyChangedEventArgs e)
        {
            tb_widthName.Text = e.NewValue.ToString();
        }
    }
}
