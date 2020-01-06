using System;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChipViewApp
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
            Append_SlotName(slotNum);
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

    public class DynamicTimingDiagram
    {
        private const string DEFAULT_PATHDATA = "M1,100 L 100,100";
        private const int MAX_TIMING_LIMIT = 20;

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
                var led_length = GetTimingTotalLength(TIMING_TYPE.LED,timingParam);
                var integ_length = GetTimingTotalLength(TIMING_TYPE.INTEG_SEQUENCE,timingParam);

                var mod_length = GetTimingTotalLength(TIMING_TYPE.MODULATED_STIMULUS,timingParam);

                if ((led_length.timingCheckpoint >= integ_length.timingCheckpoint) && (led_length.timingCheckpoint >= mod_length.timingCheckpoint))
                {
                    var ref_timecoord = new TimeCoord(TIMING_TYPE.LED);
                    timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));

                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, ref_timecoord));

                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam, ref_timecoord));

                    timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));

                }
                else if ((mod_length.timingCheckpoint >= integ_length.timingCheckpoint) && (mod_length.timingCheckpoint >= led_length.timingCheckpoint))
                {
                    var ref_timecoord = new TimeCoord(TIMING_TYPE.MODULATED_STIMULUS);
                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, ref_timecoord));

                    timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));

                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam, ref_timecoord));

                    timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));
                }
                else
                {
                    var ref_timecoord = new TimeCoord(TIMING_TYPE.INTEG_SEQUENCE);
                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam, ref_timecoord));

                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, ref_timecoord));

                    timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));

                    timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetPathData API = " + ex);
            }
            return timingParam;
        }

        private string GenerateTimingData(TIMING_TYPE eTimingType, Timing_Parameters timingParam, TimeCoord timeCoord)
        {
            string timingData = string.Empty;
            int automatic_period = 250;
            int initial_width = 15;

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

            var default_Y = 100.0; var peak_height = 50.0;
            var x1 = 0.0; var X2 = 0.0; var X3 = 0.0; var X4 = 0.0; var X5 = 0.0; var x6 = 0.0; var X7 = 0.0;
            var Y1 = 0.0; var Y2 = 0.0;
            var extra_width = 100.0;


            try
            {
                if (eTimingType == TIMING_TYPE.PRE_CONDITION)
                {
                    #region precondition
                    x1 = (TimeCoord.START_POINT + initial_width);
                    X2 = (x1 + (pre_width * TimeCoord.Per_ms));
                    X3 = (X2 + (timeCoord.TotalTimingLength - X2));

                    Y1 = default_Y - peak_height;
                    timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += x1 + "," + default_Y + " ";
                    timingData += x1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y;

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

                    // Console.WriteLine("Pre Condition Timing data = " + timingData);
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.LED)
                {
                    #region LED
                    var end_width_diff = 0.0;

                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + (led_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (led_width * TimeCoord.Per_ms);


                    if (timeCoord.timingCheckpoint == 0)
                    {
                        X3 = X2 + automatic_period; //automatically calculated period

                        timeCoord.ref_offset = led_offset;

                        timeCoord.timingCheckpoint = X3;
                    }
                    else
                    {
                        integSeq_LedOffset_diff = led_offset - timeCoord.ref_offset;

                        X3 = (timeCoord.timingCheckpoint + (integSeq_LedOffset_diff * TimeCoord.Per_ms));
                    }



                    X4 = X3 + (led_width * TimeCoord.Per_ms);

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X5 = X4 + extra_width;
                        timeCoord.TotalTimingLength = X5;

                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + extra_width;
                    }
                    else
                    {
                        X5 = timeCoord.TotalTimingLength;
                    }

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

                    timingParam.LEDOFFSET_LEFT_MARGIN = initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.LED_OFFSET_WIDTH = (x1 - (timingParam.LEDOFFSET_LEFT_MARGIN - 70));

                    timingParam.LEDWIDTH_LEFT_MARGIN = X3 + 70;

                    timingParam.LED_WIDTH_VAL = (X4 - X3);

                    if (led_width == 0)
                    {
                        timingParam.IS_LEDPATH_ENABLE = "Hidden";
                        timingParam.IS_LEDPATH_OFFSET_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;

                        timingParam.LED_BRUSH = Brushes.Gray;

                    }
                    else
                    {

                        timingParam.IS_LEDPATH_ENABLE = "Visible";
                        if (led_offset != 0)
                        {
                            timingParam.IS_LEDPATH_OFFSET_ENABLE = "Visible";
                        }
                        else
                        {
                            timingParam.IS_LEDPATH_OFFSET_ENABLE = "Hidden";
                        }
                        timingParam.LED_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }




                    timingParam.LED_DATA = Geometry.Parse(timingData);

                    //  Console.WriteLine("LED Timing data = " + timingData);
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.MODULATED_STIMULUS)
                {
                    #region MOD
                    var end_width_diff = 0.0;
                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + (mod_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (mod_width * TimeCoord.Per_ms);



                    if (timeCoord.timingCheckpoint == 0)
                    {
                        X3 = X2 + automatic_period; //automatically calculated period

                        timeCoord.ref_offset = mod_offset;

                        timeCoord.timingCheckpoint = X3;
                    }
                    else
                    {
                        integSeq_ModOffset_diff = mod_offset - timeCoord.ref_offset;
                        X3 = (timeCoord.timingCheckpoint + (integSeq_ModOffset_diff * TimeCoord.Per_ms));
                    }


                    X4 = X3 + (mod_width * TimeCoord.Per_ms);

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X5 = X4 + extra_width;
                        timeCoord.TotalTimingLength = X5;

                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + extra_width;
                    }
                    else
                    {
                        X5 = timeCoord.TotalTimingLength;
                    }

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

                    timingParam.MODOFFSET_LEFT_MARGIN = initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.MODOFFSET_WIDTH = (x1 - (timingParam.MODOFFSET_LEFT_MARGIN - 70));

                    timingParam.MODWIDTH_LEFT_MARGIN = X3 + 70;

                    timingParam.MODULATED_WIDTH = (X4 - X3);


                    if (mod_width == 0)
                    {
                        timingParam.IS_MODPATH_ENABLE = "Hidden";
                        timingParam.IS_MODPATH_OFFSET_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;
                        timingParam.MOD_BRUSH = Brushes.Gray;
                    }
                    else
                    {
                        timingParam.IS_MODPATH_ENABLE = "Visible";
                        if (mod_offset != 0)
                        {
                            timingParam.IS_MODPATH_OFFSET_ENABLE = "Visible";
                        }
                        else
                        {
                            timingParam.IS_MODPATH_OFFSET_ENABLE = "Hidden";
                        }
                        timingParam.MOD_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(timingData);

                    //   Console.WriteLine("MOD Timing data = " + timingData);
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    #region INTEG

                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + (integ_offset * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    Y2 = default_Y + peak_height;
                    X2 = x1 + (integ_width * TimeCoord.Per_ms);
                    X3 = X2 + (integ_width * TimeCoord.Per_ms);

                    if (timeCoord.timingCheckpoint == 0)
                    {
                        X4 = X3 + timeCoord.automatic_period;

                        timeCoord.ref_offset = integ_offset;

                        timeCoord.timingCheckpoint = X4;
                    }
                    else
                    {
                        integSeq_LedOffset_diff = integ_offset - timeCoord.ref_offset;
                        X4 = (timeCoord.timingCheckpoint + (integSeq_LedOffset_diff * TimeCoord.Per_ms));
                    }

                    X5 = X4 + (integ_width * TimeCoord.Per_ms);
                    x6 = X5 + (integ_width * TimeCoord.Per_ms);

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X7 = x6 + extra_width;
                        timeCoord.TotalTimingLength = X7;

                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + extra_width;
                    }
                    else
                    {
                        X7 = timeCoord.TotalTimingLength;
                    }

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

                    timingParam.INTEGOFFSET_LEFT_MARGIN = initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    timingParam.INTEGOFFSET_WIDTH = (x1 - (timingParam.INTEGOFFSET_LEFT_MARGIN - 70));

                    timingParam.INTEGWIDTH_LEFT_MARGIN = X4 + 70;
                    timingParam.INTEGRATED_WIDTH = X5 - X4;

                    if (integ_width == 0)
                    {
                        timingParam.IS_INTEGPATH_ENABLE = "Hidden";
                        timingParam.IS_INTEGPATH_OFFSET_ENABLE = "Hidden";
                        timingData = DEFAULT_PATHDATA;
                        timingParam.INTEG_BRUSH = Brushes.Gray;
                    }
                    else
                    {
                        timingParam.IS_INTEGPATH_ENABLE = "Visible";
                        if (integ_offset != 0)
                        {
                            timingParam.IS_INTEGPATH_OFFSET_ENABLE = "Visible";
                        }
                        else
                        {
                            timingParam.IS_INTEGPATH_OFFSET_ENABLE = "Hidden";
                        }
                        timingParam.INTEG_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(timingData);

                    // Console.WriteLine("INTEG Sequence timing data = " + timingData);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return timingData;
        }

        private TimeCoord GetTimingTotalLength(TIMING_TYPE eTimingType, Timing_Parameters timingParam, bool reinit_endwidth = false)
        {
            TimeCoord timeCoord = new TimeCoord(eTimingType);

            try
            {
                string timingData = string.Empty;

                int automatic_period = 250;
                int initial_width = 15;


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

                var default_Y = 100.0; var peak_height = 50.0;

                var x1 = 0.0; var X2 = 0.0; var X3 = 0.0; var X4 = 0.0; var X5 = 0.0; var x6 = 0.0; var X7 = 0.0;

                var Y1 = 0.0; var Y2 = 0.0;

                var extra_width = 100.0;



                if (eTimingType == TIMING_TYPE.LED)
                {
                    #region LED
                    var end_width_diff = 0.0;
                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + ((led_offset * 1) * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (led_width * TimeCoord.Per_ms);

                    //X3 = (integ_checkpoint + (integSeq_LedOffset_diff * TimeCoord.Per_ms));

                    // X3 = X2 + ((led_offset + led_width) * 10);      // automatic_period; //automatically calculated period

                    X3 = X2 + automatic_period;

                    timeCoord.timingCheckpoint = X3;

                    X4 = X3 + (led_width * TimeCoord.Per_ms);

                    X5 = X4 + extra_width;

                    timeCoord.TotalTimingLength = X5;

                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.MODULATED_STIMULUS)
                {
                    #region MOD
                    var end_width_diff = 0.0;
                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + ((mod_offset * 1) * TimeCoord.Per_ms);
                    Y1 = default_Y - peak_height;
                    X2 = x1 + (mod_width * TimeCoord.Per_ms);

                    // X3 = (integ_checkpoint + (integSeq_ModOffset_diff * TimeCoord.Per_ms));
                    // X3 = X2 + ((mod_offset + mod_width) * 10);  // automatic_period; //automatically calculated period
                    X3 = X2 + automatic_period;
                    timeCoord.timingCheckpoint = X3;

                    X4 = X3 + (mod_width * TimeCoord.Per_ms);

                    X5 = X4 + extra_width;

                    timeCoord.TotalTimingLength = X5;

                    //if (X4 > END_WIDTH)
                    //{
                    //    end_width_diff = X4 - END_WIDTH;
                    //    X5 = X4 - end_width_diff;

                    //    X5 = X4 + 100;
                    //    END_WIDTH = X5;

                    //    // var timingCoord_local = new TimeCoord(TIMING_TYPE.MODULATED_STIMULUS);

                    //    GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, true);
                    //}
                    //else
                    //{
                    //    end_width_diff = END_WIDTH - X4;
                    //    X5 = X4 + end_width_diff;
                    //}

                    //X5 = X4 + end_width_diff;

                    //timingData = "M " + TimeCoord.START_POINT + "," + default_Y + " ";
                    //timingData += "L ";
                    //timingData += x1 + "," + default_Y + " ";
                    //timingData += (x1 + 1) + "," + Y1 + " ";
                    //timingData += X2 + "," + Y1 + " ";
                    //timingData += (X2 + 1) + "," + default_Y + " ";
                    //timingData += X3 + "," + default_Y + " ";
                    //timingData += (X3 + 1) + "," + Y1 + " ";
                    //timingData += X4 + "," + Y1 + " ";
                    //timingData += (X4 + 1) + "," + default_Y + " ";
                    //timingData += X5 + "," + default_Y;

                    ////timingParam.INTEGOFFSET_LEFT_MARGIN = x1 + 70;
                    //timingParam.MODOFFSET_LEFT_MARGIN = initial_width + (pre_width * TimeCoord.Per_ms) + 70;

                    //timingParam.MODOFFSET_WIDTH = (x1 - (timingParam.MODOFFSET_LEFT_MARGIN - 70));

                    //timingParam.MODWIDTH_LEFT_MARGIN = X3 + 70;

                    //timingParam.MODULATED_WIDTH = (X4 - X3);

                    //if (mod_width == 0)
                    //{
                    //    timingParam.IS_MODPATH_ENABLE = "Hidden";
                    //    timingData = DEFAULT_PATHDATA;
                    //    timingParam.MOD_BRUSH = Brushes.Gray;
                    //}
                    //else
                    //{
                    //    timingParam.IS_MODPATH_ENABLE = "Visible";
                    //    timingParam.MOD_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    //}

                    //timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(timingData);

                    //Console.WriteLine("MOD Timing data = " + timingData);
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    #region INTEG

                    x1 = initial_width + (pre_width * TimeCoord.Per_ms) + ((integ_offset * 1) * TimeCoord.Per_ms);
                    //Y1 = default_Y - peak_height;
                    //Y2 = default_Y + peak_height;
                    X2 = x1 + (integ_width * TimeCoord.Per_ms);
                    X3 = X2 + (integ_width * TimeCoord.Per_ms);
                    //X4 = X3 + automatic_period;

                    // X4 = X3 + ((integ_offset + integ_width) * 10);
                    X4 = X3 + automatic_period;
                    integSeq_LedOffset_diff = led_offset - integ_offset;
                    integSeq_ModOffset_diff = mod_offset - integ_offset;

                    //integ_checkpoint = X4;
                    timeCoord.timingCheckpoint = X4;

                    X5 = X4 + (integ_width * TimeCoord.Per_ms);
                    x6 = X5 + (integ_width * TimeCoord.Per_ms);

                    X7 = x6 + extra_width;

                    timeCoord.TotalTimingLength = X7;

                    //Console.WriteLine("INTEG Sequence timing data = " + timingData);
                    #endregion

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GetTimingTotalength API = " + ex);
            }

            return timeCoord;
        }

        public class TimeCoord
        {
            public const int Per_ms = 10;  //6;
            public const int START_POINT = 1;
            public int initial_width { get; set; }
            public double TotalTimingLength { get; set; }
            public double automatic_period { get; set; }
            public double timingCheckpoint { get; set; }

            public double ref_offset { get; set; }

            public TIMING_TYPE eTimingType { get; set; }

            public TimeCoord(TIMING_TYPE e_timingType)
            {
                initial_width = 15;

                automatic_period = 250;

                eTimingType = e_timingType;

                timingCheckpoint = 0;

                TotalTimingLength = 0;

                ref_offset = 0;

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
