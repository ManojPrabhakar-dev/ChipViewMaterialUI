using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChipViewConfig
{
    /// <summary>
    /// Interaction logic for TimingDiagram_UserControl.xaml
    /// </summary>
    public partial class TimingDiagram_UserControl : UserControl
    {
        //private Brush path_brush;// = Brushes.Black;
        //public Brush PATH_BRUSH
        //{
        //    get { return path_brush; }

        //    set
        //    {
        //        path_brush = value;
        //    }
        //}

        DynamicTimingDiagram dynamicTiming_Inst = new DynamicTimingDiagram();

        public TimingDiagram_UserControl()
        {
            InitializeComponent();

            // Is_Continuous = (bool)cb_isContinuous.IsChecked;

        }
        public TimingDiagram_UserControl(Brush path_brush, int slotNum)
        {
            InitializeComponent();

            path_precondition.Stroke = path_brush;
            path_LED.Stroke = path_brush;
            path_integratorSequence.Stroke = path_brush;
            Append_SlotName(slotNum);



            //path_integratorSequence.Data = Geometry.Parse(dynamicTiming_Inst.GetTimingData(TIMING_TYPE.INTEG_SEQUENCE));

            //path_LED.Data = Geometry.Parse(dynamicTiming_Inst.GetTimingData(TIMING_TYPE.LED));

            //path_precondition.Data = Geometry.Parse(dynamicTiming_Inst.GetTimingData(TIMING_TYPE.PRE_CONDITION));


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
                        var tb_text1 = tb_text_arr[0].Substring(0, tb_text_arr[0].Length - 2) + (SLOT_TYPE)slotNum;
                        var tb_text2 = tb_text_arr[1].Substring(0, tb_text_arr[1].Length - 1) + (SLOT_TYPE)slotNum;

                        textblock.Text = tb_text1 + " + " + tb_text2;

                    }
                    else if (tb_text.Contains("_X"))
                    {
                        var tb_text1 = tb_text.Substring(0, tb_text.Length - 1);
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

    public class DynamicTimingDiagram
    {
        TimeCoord m_preCoord { get; set; }
        TimeCoord m_LedCoord { get; set; }

        static double integ_checkpoint = 0.0;
        static double END_WIDTH = 0.0;
        static double integSeq_Offset_diff = 0.0;
        public DynamicTimingDiagram()
        {

        }

        public Timing_Parameters GetPathData(Timing_Parameters timingParam)
        {
            try
            {
                timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam));

                timingParam.LED_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.LED, timingParam));

                timingParam.PRECONDITION_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.PRE_CONDITION, timingParam));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetPathData API = " + ex);
            }
            return timingParam;
        }

        private string GetTimingData(TIMING_TYPE e_timingType, Timing_Parameters timingParam)
        {
            try
            {
                if (e_timingType == TIMING_TYPE.PRE_CONDITION)
                {
                    var timingCoord = new TimeCoord(TIMING_TYPE.PRE_CONDITION);

                    return GenerateTimingData(timingCoord, timingParam);
                }
                else if (e_timingType == TIMING_TYPE.LED)
                {
                    var timingCoord = new TimeCoord(TIMING_TYPE.LED);

                    return GenerateTimingData(timingCoord, timingParam);
                }
                else if (e_timingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    var timingCoord = new TimeCoord(TIMING_TYPE.INTEG_SEQUENCE);

                    return GenerateTimingData(timingCoord, timingParam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return string.Empty;
        }

        private string GenerateTimingData(TimeCoord timingCoord, Timing_Parameters timingParam)
        {
            string timingData = string.Empty;

            int automatic_period = 150;

            int pre_width = 8; //8

            var str_led_offset = timingParam.LED_OFFSET;
            var str_integ_offset = timingParam.INTEG_OFFSET;
            var str_led_width = timingParam.LED_WIDTH;
            var str_integ_width = timingParam.INTEG_WIDTH;

            int led_offset = Convert.ToInt32(str_led_offset.Substring(0, str_led_offset.Length - 3));  //16
            double integ_offset = (Convert.ToDouble(str_integ_offset.Substring(0, str_integ_offset.Length - 3)) / 1000.0); // Divide by 1000 to convert nanosecond to microsecond
            int led_width = Convert.ToInt32(str_led_width.Substring(0, str_led_width.Length - 3));//2
            int integ_width = Convert.ToInt32(str_integ_width.Substring(0, str_integ_width.Length - 3)); //3

            var default_Y = 100.0;
            var peak_height = 50.0;

            var x1 = 0.0;
            var X2 = 0.0;
            var X3 = 0.0;
            var X4 = 0.0;
            var X5 = 0.0;
            var x6 = 0.0;
            var X7 = 0.0;

            var Y1 = 0.0;
            var Y2 = 0.0;


            try
            {
                if (timingCoord.eTimingType == TIMING_TYPE.PRE_CONDITION)
                {
                    x1 = (TimeCoord.START_POINT + timingCoord.initial_width);
                    X2 = (x1 + (pre_width * TimeCoord.Per_ms));
                    X3 = (X2 + (END_WIDTH - X2));

                    Y1 = default_Y - peak_height;
                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += x1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y;

                    m_preCoord = timingCoord;

                    timingParam.PRECONDITION_LEFT_MARGIN = x1 + 70;
                    timingParam.PRECONDITION_WIDTH = (X2 - x1);

                    Console.WriteLine("Pre Condition Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.LED)
                {
                    var end_width_diff = 0.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (led_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (led_width * TimeCoord.Per_ms);

                    X3 = (integ_checkpoint + (integSeq_Offset_diff * TimeCoord.Per_ms));
                    // X3 = X2 + automatic_period; //automatically calculated period

                    X4 = X3 + (led_width * TimeCoord.Per_ms);
                    if (X4 > END_WIDTH)
                    {
                        end_width_diff = X4 - END_WIDTH;
                        X5 = X4 - end_width_diff;
                    }
                    else
                    {
                        end_width_diff = END_WIDTH - X4;
                        X5 = X4 + end_width_diff;
                    }

                    //X5 = X4 + end_width_diff;

                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += (x1 + 1) + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += (X2 + 1) + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y + " ";
                    timingData += (X3 + 1) + "," + Y1 + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += (X4 + 1) + "," + default_Y + " ";
                    timingData += X5 + "," + default_Y;

                    timingParam.INTEGOFFSET_LEFT_MARGIN = x1 + 70;

                    Console.WriteLine("LED Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    var end_width = 100.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (integ_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    Y2 = default_Y + peak_height;
                    X2 = x1 + (integ_width * TimeCoord.Per_ms);
                    X3 = X2 + (integ_width * TimeCoord.Per_ms);
                    X4 = X3 + automatic_period;

                    integSeq_Offset_diff = led_offset - integ_offset;

                    integ_checkpoint = X4;

                    X5 = X4 + (integ_width * TimeCoord.Per_ms);
                    x6 = X5 + (integ_width * TimeCoord.Per_ms);

                    X7 = x6 + end_width;
                    END_WIDTH = X7;
                    timingParam.PATH_WIDTH = END_WIDTH;
                    timingParam.USERCONTROL_WIDTH = END_WIDTH + 100;

                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += x1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + Y2 + " ";
                    timingData += X3 + "," + Y2 + " ";
                    timingData += X3 + "," + default_Y + " ";
                    timingData += X4 + "," + default_Y + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += X5 + "," + Y1 + " ";
                    timingData += X5 + "," + Y2 + " ";
                    timingData += x6 + "," + Y2 + " ";
                    timingData += x6 + "," + default_Y + " ";
                    timingData += X7 + "," + default_Y + " ";

                    Console.WriteLine("INTEG Sequence timing data = " + timingData);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return timingData;
        }

        private string GenerateTimingData_local(TimeCoord timingCoord, Timing_Parameters timingParam)
        {
            string timingData = string.Empty;

            int automatic_period = 150;

            int pre_width = 8;//8
            int led_offset = 16; //16
            double integ_offset = (16625 / 1000.0); // Divide by 1000 to convert nanosecond to microsecond
            int led_width = 2;//2
            int integ_width = 3; //3

            var default_Y = 100.0;
            var peak_height = 50.0;

            var x1 = 0.0;
            var X2 = 0.0;
            var X3 = 0.0;
            var X4 = 0.0;
            var X5 = 0.0;
            var x6 = 0.0;
            var X7 = 0.0;

            var Y1 = 0.0;
            var Y2 = 0.0;


            try
            {
                if (timingCoord.eTimingType == TIMING_TYPE.PRE_CONDITION)
                {
                    x1 = (TimeCoord.START_POINT + timingCoord.initial_width);
                    X2 = (x1 + (pre_width * TimeCoord.Per_ms));
                    X3 = (X2 + (END_WIDTH - X2));

                    Y1 = default_Y - peak_height;
                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += x1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y;

                    m_preCoord = timingCoord;
                    Console.WriteLine("Pre Condition Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.LED)
                {
                    var end_width_diff = 0.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (led_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (led_width * TimeCoord.Per_ms);

                    X3 = (integ_checkpoint + (integSeq_Offset_diff * TimeCoord.Per_ms));
                    // X3 = X2 + automatic_period; //automatically calculated period

                    X4 = X3 + (led_width * TimeCoord.Per_ms);
                    if (X4 > END_WIDTH)
                    {
                        end_width_diff = X4 - END_WIDTH;
                        X5 = X4 - end_width_diff;
                    }
                    else
                    {
                        end_width_diff = END_WIDTH - X4;
                        X5 = X4 + end_width_diff;
                    }

                    //X5 = X4 + end_width_diff;

                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += (x1 + 1) + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += (X2 + 1) + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y + " ";
                    timingData += (X3 + 1) + "," + Y1 + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += (X4 + 1) + "," + default_Y + " ";
                    timingData += X5 + "," + default_Y;

                    Console.WriteLine("LED Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    var end_width = 100.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (integ_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    Y2 = default_Y + peak_height;
                    X2 = x1 + (integ_width * TimeCoord.Per_ms);
                    X3 = X2 + (integ_width * TimeCoord.Per_ms);
                    X4 = X3 + automatic_period;

                    integSeq_Offset_diff = led_offset - integ_offset;

                    integ_checkpoint = X4;

                    X5 = X4 + (integ_width * TimeCoord.Per_ms);
                    x6 = X5 + (integ_width * TimeCoord.Per_ms);

                    X7 = x6 + end_width;
                    END_WIDTH = X7;

                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += x1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + Y2 + " ";
                    timingData += X3 + "," + Y2 + " ";
                    timingData += X3 + "," + default_Y + " ";
                    timingData += X4 + "," + default_Y + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += X5 + "," + Y1 + " ";
                    timingData += X5 + "," + Y2 + " ";
                    timingData += x6 + "," + Y2 + " ";
                    timingData += x6 + "," + default_Y + " ";
                    timingData += X7 + "," + default_Y + " ";

                    Console.WriteLine("INTEG Sequence timing data = " + timingData);

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return timingData;
        }
        public class TimeCoord
        {
            public const int Per_ms = 10;  //6;
            public const int START_POINT = 1;
            public int initial_width { get; set; }


            public TIMING_TYPE eTimingType { get; set; }

            public TimeCoord(TIMING_TYPE e_timingType)
            {
                initial_width = 15;

                eTimingType = e_timingType;

                //if (e_timingType == TIMING_TYPE.PRE_CONDITION)
                //{
                //    eTimingType = e_timingType;
                //}
                //else if (e_timingType == TIMING_TYPE.LED)
                //{
                //    eTimingType = e_timingType;
                //}
                //else if (e_timingType == TIMING_TYPE.INTEG_SEQUENCE)
                //{
                //    eTimingType = e_timingType;
                //}
            }
        }

    }

    public enum TIMING_TYPE
    {
        PRE_CONDITION,
        LED,
        MOD,
        INTEG_SEQUENCE,
        NONE
    }

    enum SLOT_TYPE
    {
        A = 1,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L
    }
}
