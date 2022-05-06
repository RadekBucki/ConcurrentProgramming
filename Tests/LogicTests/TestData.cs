using System;
using System.Collections.Generic;
using Data;

namespace LogicTests
{
    internal class TestData : DataAbstractApi
    {
        private List<IBallData> _ballsData = new();

        public override IBallData CreateBallData(int radius, int weight, int xSpeed = 0, int ySpeed = 0)
        {
            IBallData ballData = IBallData.CreateBallData(radius, weight, xSpeed, ySpeed);
            _ballsData.Add(ballData);
            return ballData;
        }
        
        public override List<IBallData> GetAllBalls()
        {
            return _ballsData;
        }

        public override void RemoveAllBalls()
        {
            _ballsData.Clear();
        }
    }
}