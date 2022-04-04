using System;
using Data;

namespace Logic
{
    public class BallsManager
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private BallsRepository _ballsRepository = new();

        public BallsManager(int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
        }
        
        public Ball CreateBall(int x, int y, int xSpeed, int ySpeed)
        {
            if (x < 0 || x > _boardWidth || y < 0 || y > _boardHeight)
            {
                throw new ArgumentException("Coordinate out of board range.");
            }
            Ball ball = new(x, y, xSpeed, ySpeed);
            _ballsRepository.Add(ball);
            return ball;
        }

        public Ball CreateBallInRandomPlace()
        {
            Random r = new();
            int x = r.Next(0, _boardWidth);
            int y = r.Next(0, _boardHeight);
            int xSpeed = r.Next(-100, 100);
            int ySpeed = r.Next(-100, 100);
            return CreateBall(x, y, xSpeed, ySpeed);
        }

        public Ball[] GetAllBalls()
        {
            return _ballsRepository.GetBalls();
        }

        public void RemoveAllBalls()
        {
            _ballsRepository = new BallsRepository();
        }
    }
}