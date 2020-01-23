using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChipViewApp
{
    /// <summary>
    /// Interaction logic for TimingDiagram_Float_UserControl.xaml
    /// </summary>
    public partial class TimingDiagram_Float_UserControl : UserControl
    {
        public TimingDiagram_Float_UserControl()
        {
            InitializeComponent();
        }

        public TimingDiagram_Float_UserControl(Timing_Parameters timingParam, Brush path_brush, int slotNum)
        {
            InitializeComponent();

            timingParam.SLOT_DEFAULT_BRUSH = path_brush;
            timingParam.PRECONDITION_BRUSH = path_brush;
            timingParam.LED_BRUSH = path_brush;
            timingParam.MOD_BRUSH = path_brush;
            timingParam.INTEG_BRUSH = path_brush;
            Append_SlotName(slotNum);
        }
        public TimingDiagram_Float_UserControl(Brush path_brush, int SlotNum)
        {
            InitializeComponent();

            path_precondition.Stroke = path_brush;
            path_Connect_Float.Stroke = path_brush;
            path_integratorSequence.Stroke = path_brush;
            path_LEDPulses.Stroke = path_brush;
            Append_SlotName(SlotNum);
        }

        private void Append_SlotName(int slotNum)
        {
            try
            {
                var txtBlockLst2 = timingDiagram_perSlot.Children.OfType<System.Windows.Controls.TextBlock>();

                foreach (var textblock in txtBlockLst2)
                {
                    var tb_text = textblock.Text;
                    if (tb_text.Contains("+"))
                    {
                        var tb_text_arr = tb_text.Split('+');
                        var tb_text1 = tb_text_arr[0].Substring(0,tb_text_arr[0].Length-2) + (SLOT_TYPE)slotNum;
                        var tb_text2 = tb_text_arr[1].Substring(0,tb_text_arr[1].Length-1) + (SLOT_TYPE)slotNum;

                        textblock.Text = tb_text1 + " + " + tb_text2;

                    }
                    else if (tb_text.Contains("_X"))
                    {
                        var tb_text1 = tb_text.Substring(0,tb_text.Length-1);
                        textblock.Text = tb_text1 + (SLOT_TYPE)slotNum;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Append_SlotName API = " + ex);
            }
        }
    }
}
