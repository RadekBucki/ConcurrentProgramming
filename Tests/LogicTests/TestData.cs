using System.Collections.Generic;
using Data;

namespace LogicTests
{
    internal class TestData : DataAbstractApi
    {
        private List<IBallData> _ballsData = new();

        public TestData(int boardWidth, int boardHeight)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
        }

        public override IBallData CreateBallData(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0,
            int ySpeed = 0)
        {
            IBallData ballData = IBallData.CreateBallData(xPosition, yPosition, radius, weight, xSpeed, ySpeed);
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

        public override int BoardWidth { get; }
        public override int BoardHeight { get; }
    }
}