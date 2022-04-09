using System;
using Data;

namespace Logic
{
    public class BallsManager
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private readonly BallsRepository _ballsRepository = new();

        public BallsManager(int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            _ballRadius = Math.Min(boardHeight, boardWidth) / 50;
        }

        public Ball CreateBall(int x, int y, int xSpeed, int ySpeed)
        {
            if (
                x < _ballRadius || x > _boardWidth - _ballRadius ||
                y < _ballRadius || y > _boardHeight - _ballRadius ||
                xSpeed > _boardHeight - _ballRadius || xSpeed < -1 * _boardHeight + _ballRadius ||
                ySpeed > _boardHeight - _ballRadius || ySpeed < -1 * _boardHeight + _ballRadius
            )
            {
                throw new ArgumentException("Coordinate out of board range.");
            }

            Ball ball = new(x, y, _ballRadius, xSpeed, ySpeed);
            _ballsRepository.Add(ball);
            return ball;
        }

        public Ball CreateBallInRandomPlace()
        {
            Random r = new();

            return CreateBall(
                r.Next(_ballRadius, _boardWidth - _ballRadius), r.Next(_ballRadius, _boardHeight - _ballRadius),
                r.Next(-1 * _boardHeight + _ballRadius, _boardHeight - _ballRadius),
                r.Next(-1 * _boardHeight + _ballRadius, _boardHeight - _ballRadius)
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