using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Train
{
    public enum TrainIndex
    {
        SWork = 0,
        SHome = 1,
        GreenHome = 2,
        GreenWork = 3,
        BlueHome = 4,
        BlueWork = 5
    }

    public class Conductor
    {
        const string jsonFileLocation = @"C:\Users\mabank\Home\Sandox\Learning\Train\Data\Trains.json";
        private static dynamic _trainData = null;

        public Conductor()
        {
            _trainData = JObject.Parse(File.ReadAllText(jsonFileLocation));
        }

        public static TimeSpan GetNextDepartureTime(TimeSpan desiredDepatureTime, Platform platformStart, Platform platformEnd)
        {
            return new TimeSpan(7, 3, 0);
        }

        public static TrainLine GetNextTrainLine(TimeSpan desiredDepartureTime, Platform platformStart, Platform platformEnd)
        {
            return TrainLine.Blue;
        }

        public static TimeSpan GetArrivalTime(TimeSpan desiredDepartureTime, Platform platformStart, Platform platformEnd)
        {
           return new TimeSpan(7, 25, 0);
        }

        private static TimeSpan GetLineArrivalTime(TimeSpan desiredDepartureTime, Platform platformStart, Platform platformEnd)
        {
            var nextDepartureTime = GetNextDepartureTime(desiredDepartureTime, platformStart, platformEnd);

            return new TimeSpan(7, 25, 0);
        }


        public static string ReadInJson()
        {

            return _trainData.trains[(int)TrainIndex.SWork].line.ToString();
        }

        private JArray GetTrainTimes(TrainIndex trainIndex)
        {
            JArray timeArray = (JArray) _trainData.trains[(int) trainIndex].times;
            return timeArray;
        }


    }
}
