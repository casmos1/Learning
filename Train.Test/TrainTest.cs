using System;
using NUnit.Framework;
using Train;

namespace Train.Test
{
    public class TrainTest
    {
        [Test]
        public void conductor_should_tell_next_train_departure_time()
        {
            var desiredDepatureTime = new TimeSpan(7, 0, 0);
            var expectedDepartureTime = new TimeSpan(7, 3, 0);
            var actualDepartureTime = Conductor.GetNextDepartureTime(desiredDepatureTime, Platform.Home, Platform.CentralPointe);

            Assert.AreEqual(actualDepartureTime, expectedDepartureTime);
        }

        [Test]
        public void conductor_should_tell_next_departing_train()
        {
            var desiredDepartureTime = new TimeSpan(7, 10, 0);
            var expectedTrainLine = TrainLine.Blue;
            var nextTrainLine = Conductor.GetNextTrainLine(desiredDepartureTime, Platform.CentralPointe, Platform.GallivanPlaza);

            Assert.AreEqual(nextTrainLine, expectedTrainLine);
        }
    }
}
