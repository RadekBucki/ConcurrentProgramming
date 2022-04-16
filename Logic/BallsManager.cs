using System;
using System.Threading;
using System.Threading.Tasks;
using Data;

namespace Logic
{
    public class BallsManager
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private readonly BallsRepository _ballsRepository = new();
        private Timer? _movementTimer;

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
                r.Next(-5, 5),
                r.Next(-5, 5)
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

        public void StartBalls()
        {
            _movementTimer = new Timer(MoveBallsAccordingToSpeed, null, 0, 8);
        }

        public void StopBalls()
        {
            _movementTimer?.Dispose();
        }

        public void MoveBallsAccordingToSpeed(Object? stateInfo)
        {
            foreach (var ball in _ballsRepository.GetBalls())
            {
                if (ball.XPosition + ball.XSpeed >= _boardWidth - _ballRadius ||
                    ball.XPosition + ball.XSpeed <= _ballRadius)
                {
                    ball.XSpeed *= -1;
                }

                if (ball.YPosition + ball.YSpeed >= _boardHeight - _ballRadius ||
                    ball.YPosition + ball.YSpeed <= _ballRadius)
                {
                    ball.YSpeed *= -1;
                }

                ball.XPosition += ball.XSpeed;
                ball.YPosition += ball.YSpeed;
            }
        }
    }
}