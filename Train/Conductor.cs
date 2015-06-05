using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Train
{
    public class Conductor
    {
        public static TimeSpan GetNextDepartureTime(TimeSpan desiredDepatureTime, Platform platformStart, Platform platformEnd)
        {
            return new TimeSpan(7, 3, 0);
        }

        public static TrainLine GetNextTrainLine(TimeSpan desiredDepartureTime, Platform platformStart, Platform platformEnd)
        {
            return TrainLine.Blue;
        }
    }
}
