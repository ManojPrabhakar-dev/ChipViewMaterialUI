using ChipViewApp.Model;
using ChipViewApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ChipViewApp.ViewModel
{
    class TimingDiagram_FloatMode:IDynamicTimingDiagram
    {        
        #region FLOAT_MODE
        public TimingParametersModel GetPathData(TimingParametersModel timingParam)
        {
            try
            {
                var led_length = GetTimingTotalLength(TIMING_TYPE.LED, timingParam);
                var integ_length = GetTimingTotalLength(TIMING_TYPE.INTEG_SEQUENCE, timingParam);
                var mod_length = GetTimingTotalLength(TIMING_TYPE.MODULATED_STIMULUS, timingParam);

                if ((led_length.TotalTimingLength >= integ_length.TotalTimingLength) && (led_length.TotalTimingLength >= mod_length.TotalTimingLength))
                {
                    var ref_timecoord = new TimingCoordinates(TIMING_TYPE.LED);
                    timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));
                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, ref_timecoord));
                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam, ref_timecoord));
                    timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));

                }
                else if ((mod_length.TotalTimingLength >= integ_length.TotalTimingLength) && (mod_length.TotalTimingLength >= led_length.TotalTimingLength))
                {
                    var ref_timecoord = new TimingCoordinates(TIMING_TYPE.MODULATED_STIMULUS);
                    timingParam.MODULATESTIMULUS_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.MODULATED_STIMULUS, timingParam, ref_timecoord));
                    timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));
                    timingParam.INTEGSEQUENCE_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.INTEG_SEQUENCE, timingParam, ref_timecoord));
                    timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));
                }
                else
                {
                    var ref_timecoord = new TimingCoordinates(TIMING_TYPE.INTEG_SEQUENCE);
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

        private string GenerateTimingData(TIMING_TYPE eTimingType, TimingParametersModel timingParam, TimingCoordinates timeCoord)
        {
            string timingData = string.Empty;           
            var str_pre_width = timingParam.PRE_WIDTH;
            var str_led_offset = timingParam.LED_OFFSET;
            var str_integ_offset = timingParam.INTEG_OFFSET;
            var str_led_width = timingParam.LED_WIDTH;
            var str_integ_width = timingParam.INTEG_WIDTH;
            var str_mod_offset = timingParam.MOD_OFFSET;
            var str_mod_width = timingParam.MOD_WIDTH;
            var str_min_period = timingParam.MIN_PERIOD;

            int pre_width = Convert.ToInt32(str_pre_width.Substring(0, str_pre_width.Length - 3));
            int led_offset = Convert.ToInt32(str_led_offset.Substring(0, str_led_offset.Length - 3));  //16
            double integ_offset = (Convert.ToDouble(str_integ_offset.Substring(0, str_integ_offset.Length - 3)) / 1000.0); // Divide by 1000 to convert nanosecond to microsecond
            int led_width = Convert.ToInt32(str_led_width.Substring(0, str_led_width.Length - 3));//2
            int integ_width = Convert.ToInt32(str_integ_width.Substring(0, str_integ_width.Length - 3)); //3
            int mod_offset = Convert.ToInt32(str_mod_offset.Substring(0, str_mod_offset.Length - 3));  //16
            int mod_width = Convert.ToInt32(str_mod_width.Substring(0, str_mod_width.Length - 3)); //3
            int min_period = Convert.ToInt32(str_min_period.Substring(0, str_min_period.Length - 3)); //3

            if (min_period > 70)
            {
                TimingCoordinates.Per_ms = 7;
            }
            else if (min_period > 50)
            {
                TimingCoordinates.Per_ms = 8;
            }
            else if (min_period > 30)
            {
                TimingCoordinates.Per_ms = 9;
            }
            else
            {
                TimingCoordinates.Per_ms = 10;
            }

            var default_Y = 100.0; var peak_height = 50.0;
            var X1 = 0.0; var X2 = 0.0; var X3 = 0.0; var X4 = 0.0; var X5 = 0.0; var X6 = 0.0; var X7 = 0.0;
            var X8 = 0.0; var X9 = 0.0; var X10 = 0.0; var X11 = 0.0; var X12 = 0.0; var X13 = 0.0; var X14 = 0.0;
            var Y1 = 0.0; var Y2 = 0.0;            
            var uc_extra_width = 90;

            try
            {
                if (eTimingType == TIMING_TYPE.PRE_CONDITION)
                {
                    #region precondition
                    X1 = TimingCoordinates.INITIAL_WIDTH;
                    X2 = (X1 + (pre_width * TimingCoordinates.Per_ms));
                    X3 = timeCoord.TotalTimingLength;

                    Y1 = default_Y - peak_height;
                    timingData = "M " + TimingCoordinates.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += X1 + "," + default_Y + " ";
                    timingData += X1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";
                    timingData += X3 + "," + default_Y;

                    timingParam.PRECONDITION_LEFT_MARGIN = X1 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.PRECONDITION_WIDTH = (X2 - X1);

                    if (pre_width == 0)
                    {
                        timingParam.IS_PRECONDITIONPATH_ENABLE = "Hidden";
                        timingData = TimingCoordinates.DEFAULT_PATHDATA;
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
                    var led_width_inscale = (led_width * TimingCoordinates.Per_ms);
                    var led_time_diff = (min_period - led_width) * TimingCoordinates.Per_ms;

                    X1 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + (led_offset * TimingCoordinates.Per_ms);
                    Y1 = default_Y - peak_height;

                    X2 = X1 + led_width_inscale;
                    X3 = X2 + led_time_diff;

                    X4 = X3 + led_width_inscale;
                    X5 = X4 + led_time_diff;

                    X6 = X5 + led_width_inscale;
                    X7 = X6 + led_time_diff;

                    X8 = X7 + led_width_inscale;
                    X9 = X8 + led_time_diff;

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X10 = X9 + TimingCoordinates.EXTRA_ENDWIDTH;
                        timeCoord.TotalTimingLength = X10;
                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + uc_extra_width;
                    }
                    else
                    {
                        X10 = timeCoord.TotalTimingLength;
                    }

                    timingData = "M " + TimingCoordinates.START_POINT + "," + default_Y + " ";
                    timingData += "L ";

                    timingData += X1 + "," + default_Y + " ";
                    timingData += X1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";

                    timingData += X3 + "," + default_Y + " ";
                    timingData += X3 + "," + Y1 + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += X4 + "," + default_Y + " ";

                    timingData += X5 + "," + default_Y + " ";
                    timingData += X5 + "," + Y1 + " ";
                    timingData += X6 + "," + Y1 + " ";
                    timingData += X6 + "," + default_Y + " ";

                    timingData += X7 + "," + default_Y + " ";
                    timingData += X7 + "," + Y1 + " ";
                    timingData += X8 + "," + Y1 + " ";
                    timingData += X8 + "," + default_Y + " ";

                    timingData += X9 + "," + default_Y + " ";
                    timingData += X10 + "," + default_Y;


                    timingParam.LEDOFFSET_LEFT_MARGIN = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + TimingCoordinates.CANVAS_LEFT;
                    timingParam.LED_OFFSET_WIDTH = (X1 - (timingParam.LEDOFFSET_LEFT_MARGIN - TimingCoordinates.CANVAS_LEFT));
                    timingParam.LEDWIDTH_LEFT_MARGIN = X3 + TimingCoordinates.CANVAS_LEFT;

                    //LEDPULSE 1 to 4 Leftmargin set to show MASKED/FLASHED LED Status
                    timingParam.LEDPULSE1_LEFT_MARGIN = X1 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.LEDPULSE2_LEFT_MARGIN = X3 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.LEDPULSE3_LEFT_MARGIN = X5 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.LEDPULSE4_LEFT_MARGIN = X7 + TimingCoordinates.CANVAS_LEFT;

                    timingParam.LED_WIDTH_VAL = (X4 - X3);

                    if (led_width == 0)
                    {
                        timingParam.IS_LEDPATH_ENABLE = "Hidden";
                        timingParam.IS_LEDPATH_OFFSET_ENABLE = "Hidden";
                        timingData = TimingCoordinates.DEFAULT_PATHDATA;
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
                    var mod_width_inscale = (mod_width * TimingCoordinates.Per_ms);
                    var mod_time_diff = (min_period - mod_width) * TimingCoordinates.Per_ms;
                    var X0 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms);

                    X1 = X0 + (mod_offset * TimingCoordinates.Per_ms);
                    Y1 = default_Y - peak_height;

                    X2 = X1 + mod_width_inscale;
                    X3 = X2 + mod_time_diff;

                    X4 = X3 + mod_width_inscale;
                    X5 = X4 + mod_time_diff;

                    X6 = X5 + mod_width_inscale;
                    X7 = X6 + mod_time_diff;

                    X8 = X7 + mod_width_inscale;
                    X9 = X8 + mod_time_diff;

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X10 = X9 + TimingCoordinates.EXTRA_ENDWIDTH;
                        timeCoord.TotalTimingLength = X10;
                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + uc_extra_width;
                    }
                    else
                    {
                        X10 = timeCoord.TotalTimingLength;
                    }

                    timingData = "M " + TimingCoordinates.START_POINT + "," + Y1 + " ";
                    timingData += "L ";

                    timingData += X0 + "," + Y1 + " ";
                    timingData += X0 + "," + default_Y + " ";

                    timingData += X1 + "," + default_Y + " ";
                    timingData += X1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + default_Y + " ";

                    timingData += X3 + "," + default_Y + " ";
                    timingData += X3 + "," + Y1 + " ";
                    timingData += X4 + "," + Y1 + " ";
                    timingData += X4 + "," + default_Y + " ";

                    timingData += X5 + "," + default_Y + " ";
                    timingData += X5 + "," + Y1 + " ";
                    timingData += X6 + "," + Y1 + " ";
                    timingData += X6 + "," + default_Y + " ";

                    timingData += X7 + "," + default_Y + " ";
                    timingData += X7 + "," + Y1 + " ";
                    timingData += X8 + "," + Y1 + " ";
                    timingData += X8 + "," + default_Y + " ";

                    timingData += X9 + "," + default_Y + " ";
                    timingData += X10 + "," + default_Y;

                    timingParam.MODOFFSET_LEFT_MARGIN = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + TimingCoordinates.CANVAS_LEFT;
                    timingParam.MODOFFSET_WIDTH = (X1 - (timingParam.MODOFFSET_LEFT_MARGIN - TimingCoordinates.CANVAS_LEFT));
                    timingParam.MINPERIOD_LEFT_MARGIN = timingParam.MODOFFSET_LEFT_MARGIN + timingParam.MODOFFSET_WIDTH;
                    timingParam.MIN_PERIOD_WIDTH_INFO = (X3 - X1);
                    timingParam.MODWIDTH_LEFT_MARGIN = X3 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.MODULATED_WIDTH = (X4 - X3);

                    if (mod_width == 0)
                    {
                        timingParam.IS_MODPATH_ENABLE = "Hidden";
                        timingParam.IS_MODPATH_OFFSET_ENABLE = "Hidden";
                        timingData = TimingCoordinates.DEFAULT_PATHDATA;
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
                    var integ_width_inscale = integ_width * TimingCoordinates.Per_ms;
                    var integ_time_diff = (min_period - (2 * integ_width)) * TimingCoordinates.Per_ms;

                    X1 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + (integ_offset * TimingCoordinates.Per_ms);
                    Y1 = default_Y - peak_height;
                    Y2 = default_Y + peak_height;

                    X2 = X1 + integ_width_inscale;
                    X3 = X2 + integ_width_inscale;
                    X4 = X3 + integ_time_diff;

                    X5 = X4 + integ_width_inscale;
                    X6 = X5 + integ_width_inscale;
                    X7 = X6 + integ_time_diff;

                    X8 = X7 + integ_width_inscale;
                    X9 = X8 + integ_width_inscale;
                    X10 = X9 + integ_time_diff;

                    X11 = X10 + integ_width_inscale;
                    X12 = X11 + integ_width_inscale;
                    X13 = X12 + integ_time_diff;

                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X14 = X13 + TimingCoordinates.EXTRA_ENDWIDTH;
                        timeCoord.TotalTimingLength = X14;
                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + uc_extra_width;
                    }
                    else
                    {
                        X14 = timeCoord.TotalTimingLength;
                    }

                    timingData = "M " + TimingCoordinates.START_POINT + "," + default_Y + " ";
                    timingData += "L ";
                    timingData += X1 + "," + default_Y + " ";

                    timingData += X1 + "," + Y1 + " ";
                    timingData += X2 + "," + Y1 + " ";
                    timingData += X2 + "," + Y2 + " ";
                    timingData += X3 + "," + Y2 + " ";
                    timingData += X3 + "," + default_Y + " ";
                    timingData += X4 + "," + default_Y + " ";

                    timingData += X4 + "," + Y1 + " ";
                    timingData += X5 + "," + Y1 + " ";
                    timingData += X5 + "," + Y2 + " ";
                    timingData += X6 + "," + Y2 + " ";
                    timingData += X6 + "," + default_Y + " ";
                    timingData += X7 + "," + default_Y + " ";

                    timingData += X7 + "," + Y1 + " ";
                    timingData += X8 + "," + Y1 + " ";
                    timingData += X8 + "," + Y2 + " ";
                    timingData += X9 + "," + Y2 + " ";
                    timingData += X9 + "," + default_Y + " ";
                    timingData += X10 + "," + default_Y + " ";

                    timingData += X10 + "," + Y1 + " ";
                    timingData += X11 + "," + Y1 + " ";
                    timingData += X11 + "," + Y2 + " ";
                    timingData += X12 + "," + Y2 + " ";
                    timingData += X12 + "," + default_Y + " ";
                    timingData += X13 + "," + default_Y + " ";

                    timingData += X14 + "," + default_Y + " ";

                    timingParam.INTEGOFFSET_LEFT_MARGIN = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + TimingCoordinates.CANVAS_LEFT;
                    timingParam.INTEGOFFSET_WIDTH = (X1 - (timingParam.INTEGOFFSET_LEFT_MARGIN - TimingCoordinates.CANVAS_LEFT));
                    timingParam.INTEGWIDTH_LEFT_MARGIN = X4 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.INTEGRATED_WIDTH = X5 - X4;

                    if (integ_width == 0)
                    {
                        timingParam.IS_INTEGPATH_ENABLE = "Hidden";
                        timingParam.IS_INTEGPATH_OFFSET_ENABLE = "Hidden";
                        timingData = TimingCoordinates.DEFAULT_PATHDATA;
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

        private TimingCoordinates GetTimingTotalLength(TIMING_TYPE eTimingType, TimingParametersModel timingParam, bool reinit_endwidth = false)
        {
            TimingCoordinates timeCoord = new TimingCoordinates(eTimingType);

            try
            {
                string timingData = string.Empty;              
                var str_pre_width = timingParam.PRE_WIDTH;
                var str_led_offset = timingParam.LED_OFFSET;
                var str_integ_offset = timingParam.INTEG_OFFSET;               
                var str_mod_offset = timingParam.MOD_OFFSET;                
                var str_min_period = timingParam.MIN_PERIOD;

                int pre_width = Convert.ToInt32(str_pre_width.Substring(0, str_pre_width.Length - 3));
                int led_offset = Convert.ToInt32(str_led_offset.Substring(0, str_led_offset.Length - 3));  //16
                double integ_offset = (Convert.ToDouble(str_integ_offset.Substring(0, str_integ_offset.Length - 3)) / 1000.0); // Divide by 1000 to convert nanosecond to microsecond               
                int mod_offset = Convert.ToInt32(str_mod_offset.Substring(0, str_mod_offset.Length - 3));  //16               
                int min_period = Convert.ToInt32(str_min_period.Substring(0, str_min_period.Length - 3)); //3
                var X1 = 0.0; var X2 = 0.0; var X3 = 0.0; 
                
                if (eTimingType == TIMING_TYPE.LED)
                {
                    #region LED   
                    X1 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + ((led_offset * 1) * TimingCoordinates.Per_ms);
                    X2 = X1 + ((min_period * TimingCoordinates.Per_ms) * 4);  // Multiplying by 4 because of 4 LED pulses
                    X3 = X2 + TimingCoordinates.EXTRA_ENDWIDTH;
                    timeCoord.TotalTimingLength = X3;
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.MODULATED_STIMULUS)
                {
                    #region MOD                    
                    X1 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + ((mod_offset * 1) * TimingCoordinates.Per_ms);
                    X2 = X1 + ((min_period * TimingCoordinates.Per_ms) * 4);  // Multiplying by 4 because of 4 LED pulses
                    X3 = X2 + TimingCoordinates.EXTRA_ENDWIDTH;
                    timeCoord.TotalTimingLength = X3;
                    #endregion
                }
                else if (eTimingType == TIMING_TYPE.INTEG_SEQUENCE)
                {
                    #region INTEG
                    X1 = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + ((integ_offset * 1) * TimingCoordinates.Per_ms);
                    X2 = X1 + ((min_period * TimingCoordinates.Per_ms) * 4);  // Multiplying by 4 because of 4 LED pulses
                    X3 = X2 + TimingCoordinates.EXTRA_ENDWIDTH;
                    timeCoord.TotalTimingLength = X3;
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

        #endregion
    }
}
