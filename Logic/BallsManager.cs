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
            IBallData ballData = _dataLayer.CreateBallData(_ballRadius, _ballRadius * 10, xSpeed, ySpeed);
            IBall ball = IBall.CreateBall(x, y, ballData.Radius, ballData.Weight, ballData.XSpeed, ballData.YSpeed);
            ballData.PropertyChanged += ball.UpdateBall!;
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
            _dataLayer.RemoveAllBalls();
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
            IBall[] balls = _balls.ToArray();
            IBallData[] dataBalls = _dataLayer.GetAllBalls().ToArray();
            for (int i = 0; i < balls.Length; i++)
            {
                if (balls[i].XPosition + balls[i].XSpeed >= _boardWidth - _ballRadius ||
                    balls[i].XPosition + balls[i].XSpeed <= _ballRadius)
                {
                    dataBalls[i].ChangeXSense();
                }

                if (balls[i].YPosition + balls[i].YSpeed >= _boardHeight - _ballRadius ||
                    balls[i].YPosition + balls[i].YSpeed <= _ballRadius)
                {
                    dataBalls[i].ChangeYSense();
                }
                balls[i].Move();
            }
        }
    }
}