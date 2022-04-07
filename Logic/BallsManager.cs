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
            if (
                x < 0 || x > _boardWidth ||
                y < 0 || y > _boardHeight ||
                xSpeed > 100 || xSpeed < -100 ||
                ySpeed > 100 || ySpeed < -100
            )
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
            return CreateBall(
                r.Next(0, _boardWidth), r.Next(0, _boardHeight),
                r.Next(-100, 100), r.Next(-100, 100)
            );
        }

        public Ball[] GetAllBalls()
        {
            return _ballsRepository.GetBalls();
        }

        public void RemoveAllBalls()
        {
            _ballsRepository.Clear();
        }
    }
}