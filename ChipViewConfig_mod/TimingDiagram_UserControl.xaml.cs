using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChipViewConfig
{
    /// <summary>
    /// Interaction logic for TimingDiagram_UserControl.xaml
    /// </summary>
    public partial class TimingDiagram_UserControl : UserControl
    {
        private Timing_Parameters timing_param = new Timing_Parameters();

        public TimingDiagram_UserControl()
        {
            InitializeComponent();
        }

        public TimingDiagram_UserControl(Timing_Parameters timingParam, Brush path_brush, int slotNum)
        {
            InitializeComponent();

            timingParam.SLOT_DEFAULT_BRUSH = path_brush;
            timingParam.PRECONDITION_BRUSH = path_brush;
            timingParam.LED_BRUSH = path_brush;
            timingParam.MOD_BRUSH = path_brush;
            timingParam.INTEG_BRUSH = path_brush;

            //path_precondition.Stroke = path_brush;            
            //path_integratorSequence.Stroke = path_brush;
            //timingParam.MOD_BRUSH = path_brush;

            Append_SlotName(slotNum);

            // Is_Continuous = (bool)cb_isContinuous.IsChecked;

        }
        public TimingDiagram_UserControl(Brush path_brush, int SlotNum)
        {
            InitializeComponent();

            path_precondition.Stroke = path_brush;
            path_LED.Stroke = path_brush;
            path_integratorSequence.Stroke = path_brush;
            path_MODULATE.Stroke = path_brush;
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
        private const string DEFAULT_PATHDATA = "M1,100 L 100,100";
        private const int MAX_TIMING_LIMIT = 20;
        TimeCoord m_preCoord { get; set; }
        TimeCoord m_LedCoord { get; set; }

        static double integ_checkpoint = 0.0;
        static double END_WIDTH = 0.0;
        static double integSeq_LedOffset_diff = 0.0;
        static double integSeq_ModOffset_diff = 0.0;
        public DynamicTimingDiagram()
        {

        }

        public Timing_Parameters GetPathData(Timing_Parameters timingParam)
        {
            try
            {
                timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam));

                timingParam.LED_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.LED, timingParam));

                timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GetTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam));


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
                else if (e_timingType == TIMING_TYPE.MODULATED_STIMULUS)
                {
                    var timingCoord = new TimeCoord(TIMING_TYPE.MODULATED_STIMULUS);

                    return GenerateTimingData(timingCoord, timingParam);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return string.Empty;
        }

        private string GenerateTimingData(TimeCoord timingCoord, Timing_Parameters timingParam, bool reinit_endwidth = false)
        {
            string timingData = string.Empty;

            int automatic_period = 250;


            var str_pre_width = timingParam.PRE_WIDTH;
            var str_led_offset = timingParam.LED_OFFSET;
            var str_integ_offset = timingParam.INTEG_OFFSET;
            var str_led_width = timingParam.LED_WIDTH;
            var str_integ_width = timingParam.INTEG_WIDTH;
            var str_mod_offset = timingParam.MOD_OFFSET;
            var str_mod_width = timingParam.MOD_WIDTH;

            int pre_width = Convert.ToInt32(str_pre_width.Substring(0,str_pre_width.Length-3));
            int led_offset = Convert.ToInt32(str_led_offset.Substring(0,str_led_offset.Length-3));  //16
            double integ_offset = (Convert.ToDouble(str_integ_offset.Substring(0,str_integ_offset.Length-3))/ 1000.0); // Divide by 1000 to convert nanosecond to microsecond
            int led_width = Convert.ToInt32(str_led_width.Substring(0,str_led_width.Length-3));//2
            int integ_width = Convert.ToInt32(str_integ_width.Substring(0,str_integ_width.Length-3)); //3
            int mod_offset = Convert.ToInt32(str_mod_offset.Substring(0,str_mod_offset.Length-3));  //16
            int mod_width = Convert.ToInt32(str_mod_width.Substring(0,str_mod_width.Length-3)); //3

            led_offset = led_offset > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : led_offset;
            integ_offset = integ_offset > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : integ_offset;
            mod_offset = mod_offset > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : mod_offset;

            led_width = led_width > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : led_width;
            integ_width = integ_width > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : integ_width;
            mod_width = mod_width > MAX_TIMING_LIMIT ? MAX_TIMING_LIMIT : mod_width;

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

                    if (pre_width == 0)
                    {
                        timingParam.IS_PRECONDITIONPATH_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;

                        timingParam.PRECONDITION_BRUSH = Brushes.Gray;
                    }
                    else
                    {
                        timingParam.IS_PRECONDITIONPATH_ENABLE = "Visible";
                        timingParam.PRECONDITION_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.PRECONDITION_DATA = Geometry.Parse(timingData);

                    Console.WriteLine("Pre Condition Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.LED)
                {
                    var end_width_diff = 0.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (led_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (led_width * TimeCoord.Per_ms);

                    X3 = (integ_checkpoint + (integSeq_LedOffset_diff * TimeCoord.Per_ms));
                    // X3 = X2 + automatic_period; //automatically calculated period

                    X4 = X3 + (led_width * TimeCoord.Per_ms);
                    if (X4 > END_WIDTH)
                    {
                        end_width_diff = X4 - END_WIDTH;
                        X5 = X4 - end_width_diff;

                        X5 = X4 + 100;
                        END_WIDTH = X5;

                        var timingCoord_local = new TimeCoord(TIMING_TYPE.INTEG_SEQUENCE);

                        GenerateTimingData(timingCoord_local, timingParam, true);
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

                    //timingParam.INTEGOFFSET_LEFT_MARGIN = x1 + 70;
                    timingParam.LEDOFFSET_LEFT_MARGIN = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.LED_OFFSET_WIDTH = (x1 - (timingParam.LEDOFFSET_LEFT_MARGIN - 70));

                    timingParam.LEDWIDTH_LEFT_MARGIN = X3 + 70;

                    timingParam.LED_WIDTH_VAL = (X4 - X3);

                    if (led_width == 0)
                    {
                        timingParam.IS_LEDPATH_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;

                        timingParam.LED_BRUSH = Brushes.Gray;

                    }
                    else
                    {
                        timingParam.IS_LEDPATH_ENABLE = "Visible";
                        timingParam.LED_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }


                    timingParam.LED_DATA = Geometry.Parse(timingData);

                    Console.WriteLine("LED Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.MODULATED_STIMULUS)
                {
                    var end_width_diff = 0.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (mod_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (mod_width * TimeCoord.Per_ms);

                    X3 = (integ_checkpoint + (integSeq_ModOffset_diff * TimeCoord.Per_ms));
                    // X3 = X2 + automatic_period; //automatically calculated period

                    X4 = X3 + (mod_width * TimeCoord.Per_ms);
                    if (X4 > END_WIDTH)
                    {
                        end_width_diff = X4 - END_WIDTH;
                        X5 = X4 - end_width_diff;

                        X5 = X4 + 100;
                        END_WIDTH = X5;

                        var timingCoord_local = new TimeCoord(TIMING_TYPE.MODULATED_STIMULUS);

                        GenerateTimingData(timingCoord_local, timingParam, true);
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

                    //timingParam.INTEGOFFSET_LEFT_MARGIN = x1 + 70;
                    timingParam.MODOFFSET_LEFT_MARGIN = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.MODOFFSET_WIDTH = (x1 - (timingParam.MODOFFSET_LEFT_MARGIN - 70));

                    timingParam.MODWIDTH_LEFT_MARGIN = X3 + 70;

                    timingParam.MODULATED_WIDTH = (X4 - X3);

                    if (mod_width == 0)
                    {
                        timingParam.IS_MODPATH_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;
                        timingParam.MOD_BRUSH = Brushes.Gray;
                    }
                    else
                    {
                        timingParam.IS_MODPATH_ENABLE = "Visible";
                        timingParam.MOD_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(timingData);

                    Console.WriteLine("MOD Timing data = " + timingData);
                }
                else if (timingCoord.eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    var extra_width = 100.0;
                    x1 = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + (integ_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    Y2 = default_Y + peak_height;
                    X2 = x1 + (integ_width * TimeCoord.Per_ms);
                    X3 = X2 + (integ_width * TimeCoord.Per_ms);
                    X4 = X3 + automatic_period;

                    integSeq_LedOffset_diff = led_offset - integ_offset;

                    integSeq_ModOffset_diff = mod_offset - integ_offset;

                    integ_checkpoint = X4;

                    X5 = X4 + (integ_width * TimeCoord.Per_ms);
                    x6 = X5 + (integ_width * TimeCoord.Per_ms);

                    X7 = x6 + extra_width;

                    if (!reinit_endwidth)
                    {
                        END_WIDTH = X7;
                    }
                    else
                    {
                        X7 = END_WIDTH;
                    }

                    timingParam.PATH_WIDTH = END_WIDTH;
                    timingParam.USERCONTROL_WIDTH = END_WIDTH + extra_width;

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

                    timingParam.INTEGOFFSET_LEFT_MARGIN = timingCoord.initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.INTEGOFFSET_WIDTH = (x1 - (timingParam.INTEGOFFSET_LEFT_MARGIN - 70));

                    timingParam.INTEGWIDTH_LEFT_MARGIN = integ_checkpoint + 70;
                    timingParam.INTEGRATED_WIDTH = X5 - integ_checkpoint;

                    if (integ_width == 0)
                    {
                        timingParam.IS_INTEGPATH_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;
                        timingParam.INTEG_BRUSH = Brushes.Gray;
                    }
                    else
                    {
                        timingParam.IS_INTEGPATH_ENABLE = "Visible";
                        timingParam.INTEG_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(timingData);

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
            public const int Per_ms = 20;  //6;
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
        MODULATED_STIMULUS,
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
