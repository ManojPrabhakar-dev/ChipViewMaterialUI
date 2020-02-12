using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChipViewApp
{
    /// <summary>
    /// Interaction logic for ADC_Sampling_Region.xaml
    /// </summary>
    public partial class ADC_Sampling_Region_UserControl : UserControl
    {
        public ADC_Sampling_Region_UserControl()
        {
            InitializeComponent();
        }

        public Brush ArrowColor
        {
            get { return (Brush)GetValue(ArrowColorProperty); }
            set { SetValue(ArrowColorProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArrowColor.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrowColorProperty =
        DependencyProperty.Register("ArrowColor", typeof(Brush), typeof(ADC_Sampling_Region_UserControl), new PropertyMetadata(Brushes.Black));


        public double ArrowWidth
        {
            get { return (double)GetValue(ArrowWidthProperty); }
            set { SetValue(ArrowWidthProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ArrowWidth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ArrowWidthProperty =
        DependencyProperty.Register("ArrowWidth", typeof(double), typeof(ADC_Sampling_Region_UserControl), new PropertyMetadata(1.0));


        public double Num_Int_ADC
        {
            get { return (double)GetValue(Num_Int_ADCProperty); }
            set
            {
                SetValue(Num_Int_ADCProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Num_Int_ADC.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Num_Int_ADCProperty =
        DependencyProperty.Register("Num_Int_ADC", typeof(double), typeof(ADC_Sampling_Region_UserControl), new PropertyMetadata(OnNumIntADC_Changed));

        private static void OnNumIntADC_Changed(DependencyObject d,
        DependencyPropertyChangedEventArgs e)
        {
            ADC_Sampling_Region_UserControl UserControl1Control = d as ADC_Sampling_Region_UserControl;
            UserControl1Control.OnNumIntADC_Changed(e);
        }

        private void OnNumIntADC_Changed(DependencyPropertyChangedEventArgs e)
        {
            GenerateADC_Arrows();
        }

        private void GenerateADC_Arrows()
        {
            try
            {
                sPanel_ArrowContainer.Children.Clear();

                for (int i = 1; i <= Num_Int_ADC; i++)
                {
                    var uc_arrow = new ADC_Arrow();
                    uc_arrow.lineArrow_up.Stroke = ArrowColor;
                    uc_arrow.Width = ArrowWidth;

                    sPanel_ArrowContainer.Children.Add(uc_arrow);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
