using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChipViewApp.Utils
{
    public class TimingCoordinates
    {
        public static int Per_ms = 10;  //6;
        public const int START_POINT = 1;
        public const int CANVAS_LEFT = 75;
        public const int EXTRA_ENDWIDTH = 100;
        public const int INITIAL_WIDTH = 15;
        public const string DEFAULT_PATHDATA = "M1,100 L 100,100";
        public double automatic_period { get; set; }
        public double TotalTimingLength { get; set; }        
        public double timingCheckpoint { get; set; }
        public double ref_offset { get; set; }
        public TIMING_TYPE eTimingType { get; set; }

        public TimingCoordinates(TIMING_TYPE e_timingType)
        {       
            automatic_period = 250;
            eTimingType = e_timingType;
            timingCheckpoint = 0;
            TotalTimingLength = 0;
            ref_offset = 0;
        }
    }
}
