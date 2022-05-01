using System;
using System.Collections.Generic;
using System.Threading;
using Data;

namespace Logic
{
    internal class BallsManager : LogicAbstractApi
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private DataAbstractApi _dataLayer;
        private Timer? _movementTimer;
        private const int MaxBallSpeed = 5;
        private const int BoardToBallRatio = 50;
        private List<IBall> _balls = new();

        public BallsManager(int boardWidth, int boardHeight, DataAbstractApi dataLayer)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            _ballRadius = Math.Min(boardHeight, boardWidth) / BoardToBallRatio;
            _dataLayer = dataLayer;
        }

        public override IBall CreateBall(int x, int y, int xSpeed, int ySpeed)
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

            IBall ball = IBall.CreateBall(x, y, _ballRadius, xSpeed, ySpeed);
            _balls.Add(ball);
            return ball;
        }

        public override IBall CreateBallInRandomPlace()
        {
            Random r = new();

            return CreateBall(
                r.Next(_ballRadius, _boardWidth - _ballRadius), r.Next(_ballRadius, _boardHeight - _ballRadius),
                r.Next(-MaxBallSpeed, MaxBallSpeed),
                r.Next(-MaxBallSpeed, MaxBallSpeed)
            );
        }

        public override List<IBall> GetAllBalls()
        {
            return _balls;
        }

        public override void RemoveAllBalls()
        {
            _balls.Clear();
        }

        public override void StartBalls()
        {
            _movementTimer = new Timer(MoveBallsAccordingToSpeed, null, 0, 8);
        }

        public override void StopBalls()
        {
            _movementTimer?.Dispose();
        }

        public override void MoveBallsAccordingToSpeed(Object? stateInfo)
        {
            foreach (IBall ball in _balls.ToArray())
            {
                if (ball.XPosition + ball.XSpeed >= _boardWidth - _ballRadius ||
                    ball.XPosition + ball.XSpeed <= _ballRadius)
                {
                    ball.ChangeXSense();
                }

                if (ball.YPosition + ball.YSpeed >= _boardHeight - _ballRadius ||
                    ball.YPosition + ball.YSpeed <= _ballRadius)
                {
                    ball.ChangeYSense();
                }
                ball.Move();
            }
        }
    }
}