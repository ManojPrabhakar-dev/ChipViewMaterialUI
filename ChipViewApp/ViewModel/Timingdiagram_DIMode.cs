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
    class Timingdiagram_DIMode:IDynamicTimingDiagram
    {        
        #region DI_MODE
        public TimingParametersModel GetPathData(TimingParametersModel timingParam)
        {
            try
            {
                var ref_timecoord = new TimingCoordinates(TIMING_TYPE.LED);
                timingParam.LED_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.LED, timingParam, ref_timecoord));
                timingParam.PRECONDITION_DATA = Geometry.Parse(GenerateTimingData(TIMING_TYPE.PRE_CONDITION, timingParam, ref_timecoord));
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
            var str_led_width = timingParam.LED_WIDTH;            
            var str_min_period = timingParam.MIN_PERIOD;
            var str_dark1_offset = timingParam.DARK1_OFFSET;
            var str_lit_offset = timingParam.LIT_OFFSET;
            var str_dark2_offset = timingParam.DARK2_OFFSET;

            int pre_width = Convert.ToInt32(str_pre_width.Substring(0, str_pre_width.Length - 3));
            int led_offset = Convert.ToInt32(str_led_offset.Substring(0, str_led_offset.Length - 3));              
            int led_width = Convert.ToInt32(str_led_width.Substring(0, str_led_width.Length - 3));           
            int min_period = Convert.ToInt32(str_min_period.Substring(0, str_min_period.Length - 3)); 
            int dark1_offset = Convert.ToInt32(str_dark1_offset.Substring(0, str_dark1_offset.Length - 3));
            int lit_offset = Convert.ToInt32(str_lit_offset.Substring(0, str_lit_offset.Length - 3)); 
            int dark2_offset = Convert.ToInt32(str_dark2_offset.Substring(0, str_dark2_offset.Length - 3)); 

            if ((led_width > 100) || (led_offset > 100))
            {
                TimingCoordinates.Per_ms = 4;
            }
            else if ((led_width > 60) || (led_offset > 60))
            {
                TimingCoordinates.Per_ms = 5;
            }
            else if ((led_width > 30) || (led_offset > 30))
            {
                TimingCoordinates.Per_ms = 7;
            }
            else
            {
                TimingCoordinates.Per_ms = 10;
            }

            var default_Y = 100.0; var peak_height = 50.0;
            var X1 = 0.0; var X2 = 0.0; var X3 = 0.0; var X4 = 0.0; var X5 = 0.0; 
            var Y1 = 0.0;            

            try
            {
                if (eTimingType == TIMING_TYPE.PRE_CONDITION)
                {
                    #region precondition
                    X1 = (TimingCoordinates.START_POINT + TimingCoordinates.INITIAL_WIDTH);
                    X2 = (X1 + (pre_width * TimingCoordinates.Per_ms));
                    X3 = (X2 + (timeCoord.TotalTimingLength - X2));

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
                   
                    if (timeCoord.TotalTimingLength == 0)
                    {
                        X5 = X4 + TimingCoordinates.EXTRA_ENDWIDTH;
                        timeCoord.TotalTimingLength = X5;
                        timingParam.PATH_WIDTH = timeCoord.TotalTimingLength;
                        timingParam.USERCONTROL_WIDTH = timeCoord.TotalTimingLength + TimingCoordinates.EXTRA_ENDWIDTH;
                    }
                    else
                    {
                        X5 = timeCoord.TotalTimingLength;
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

                    timingData += X5 + "," + default_Y;

                    timingParam.LEDOFFSET_LEFT_MARGIN = TimingCoordinates.INITIAL_WIDTH + (pre_width * TimingCoordinates.Per_ms) + TimingCoordinates.CANVAS_LEFT;
                    timingParam.LED_OFFSET_WIDTH = (X1 - (timingParam.LEDOFFSET_LEFT_MARGIN - TimingCoordinates.CANVAS_LEFT));
                    timingParam.LEDWIDTH_LEFT_MARGIN = X3 + TimingCoordinates.CANVAS_LEFT;
                    timingParam.MINPERIOD_LEFT_MARGIN = timingParam.LEDOFFSET_LEFT_MARGIN + timingParam.LED_OFFSET_WIDTH;
                    timingParam.MIN_PERIOD_WIDTH_INFO = (X3 - X1);

                    //ADC Convert offset settings
                    timingParam.DIG_INT_OFFSET_LEFT_MARGIN = timingParam.LEDOFFSET_LEFT_MARGIN;
                    timingParam.DARK1_OFFSET_WIDTH_INFO = (dark1_offset * TimingCoordinates.Per_ms);
                    timingParam.LIT_OFFSET_WIDTH_INFO = (lit_offset * TimingCoordinates.Per_ms);
                    timingParam.DARK2_OFFSET_WIDTH_INFO = (dark2_offset * TimingCoordinates.Per_ms);
                                      
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

                    if (dark1_offset == 0)
                    {
                        timingParam.IS_DARK1_OFFSET_ENABLE = "Hidden";
                    }
                    else
                    {
                        timingParam.IS_DARK1_OFFSET_ENABLE = "Visible";
                    }

                    if (lit_offset == 0)
                    {
                        timingParam.IS_LIT_OFFSET_ENABLE = "Hidden";
                    }
                    else
                    {
                        timingParam.IS_LIT_OFFSET_ENABLE = "Visible";
                    }

                    if (dark2_offset == 0)
                    {
                        timingParam.IS_DARK2_OFFSET_ENABLE = "Hidden";
                    }
                    else
                    {
                        timingParam.IS_DARK2_OFFSET_ENABLE = "Visible";
                    }

                    if ((dark1_offset == 0) && (lit_offset == 0) && (dark2_offset == 0))
                    {
                        timingParam.MOD_BRUSH = Brushes.Gray;  // MOD_BRUSH is used for ADC since MOD is not available in DI mode
                    }
                    else
                    {
                        timingParam.MOD_BRUSH = timingParam.SLOT_DEFAULT_BRUSH;
                    }

                    timingParam.LED_DATA = Geometry.Parse(timingData);

                    //  Console.WriteLine("LED Timing data = " + timingData);
                    #endregion
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in GenerateTimingData API = " + ex);
            }
            return timingData;
        }
        #endregion
    }
}
