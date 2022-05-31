﻿namespace Data
{
    internal class Board : DataAbstractApi
    {
        private List<IBallData> _ballsData = new();
        private Logger.Logger _logger;

        public Board(int boardWidth, int boardHeight)
        {
            BoardWidth = boardWidth;
            BoardHeight = boardHeight;
            _logger = new Logger.Logger();
        }

        public override IBallData CreateBallData(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0,
            int ySpeed = 0)
        {
            IBallData ballData = IBallData.CreateBallData(xPosition, yPosition, radius, weight, xSpeed, ySpeed);
            _logger.LogCreate(ballData);
            ballData.LoggerPropertyChanged += _logger.LogChange;
            _ballsData.Add(ballData);
            ballData.StartBall();
            return ballData;
        }

        public override List<IBallData> GetAllBalls()
        {
            return _ballsData;
        }

        public override void RemoveAllBalls()
        {
            _ballsData.ForEach(ball => ball.Stop());
            _ballsData.Clear();
            _logger.EndLogging();
        }
        public override int BoardWidth { get; }
        public override int BoardHeight { get; }
    }
}