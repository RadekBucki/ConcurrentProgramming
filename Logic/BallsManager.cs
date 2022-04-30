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
        private const int BallWeight = 100;
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

            IBall ball = IBall.CreateBall(x, y, _ballRadius, BallWeight, xSpeed, ySpeed);
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
                WallReflection(ball);
                BallReflection(ball);
            }
        }

        private void WallReflection(IBall ball)
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

        private void BallReflection(IBall ball1)
        {
            foreach (IBall ball2 in _balls.ToArray())
            {
                if (ball1.Equals(ball2))
                {
                    continue;
                }

                if ( //condition of circles external contact: (r_1 + r_2) <= |AB|
                    (Math.Abs(Math.Sqrt(
                         (ball1.XPosition - ball2.XPosition) * (ball1.XPosition - ball2.XPosition) +
                         (ball1.YPosition - ball2.YPosition) * (ball1.YPosition - ball2.YPosition)
                     ) - _ballRadius * 2.0) < 0.1 ||
                     Math.Sqrt(
                         (ball1.XPosition + ball1.XSpeed - ball2.XPosition + ball2.XSpeed) *
                         (ball1.XPosition + ball1.XSpeed - ball2.XPosition + ball2.XSpeed) +
                         (ball1.YPosition + ball1.YSpeed - ball2.YPosition + ball2.YSpeed) *
                         (ball1.YPosition + ball1.YSpeed - ball2.YPosition + ball2.YSpeed)
                     ) <= _ballRadius * 2.0) &&
                    !ball1.InCollisionWithBall.Contains(ball2) &&
                    !ball2.InCollisionWithBall.Contains(ball1)
                   )
                {
                    ball1.InCollisionWithBall.Add(ball2);
                    ball2.InCollisionWithBall.Add(ball1);

                    int ball1StartXSpeed = ball1.XSpeed;
                    int ball1StartYSpeed = ball1.YSpeed;
                    int ball2StartXSpeed = ball2.XSpeed;
                    int ball2StartYSpeed = ball2.YSpeed;

                    ball1.YSpeed = ball2StartYSpeed;
                    ball2.YSpeed = ball1StartYSpeed;
                    ball1.XSpeed = ball2StartXSpeed;
                    ball2.XSpeed = ball1StartXSpeed;

                    if (ball1StartXSpeed * ball2StartXSpeed > 0)
                    {
                        if (
                            (ball1.XSpeed > 0 && ball1.XPosition > ball2.XPosition) ||
                            (ball1.XSpeed < 0 && ball1.XPosition < ball2.XPosition)
                        )
                        {
                            ball2.ChangeXSense();
                        }
                        else if (
                            (ball1.XSpeed < 0 && ball1.XPosition < ball2.XPosition) ||
                            (ball1.XSpeed > 0 && ball1.XPosition > ball2.XPosition)
                        )
                        {
                            ball1.ChangeXSense();
                        }
                    }

                    if (ball1StartYSpeed * ball2StartYSpeed > 0)
                    {
                        if (
                            (ball1.YSpeed > 0 && ball1.YPosition > ball2.YPosition) ||
                            (ball1.YSpeed < 0 && ball1.YPosition < ball2.YPosition)
                        )
                        {
                            ball2.ChangeYSense();
                        }
                        else if (
                            (ball1.YSpeed < 0 && ball1.YPosition < ball2.YPosition) ||
                            (ball1.YSpeed > 0 && ball1.YPosition > ball2.YPosition)
                        )
                        {
                            ball1.ChangeYSense();
                        }
                    }

                    ball1.Move();
                    ball2.Move();

                    ball1.InCollisionWithBall.Remove(ball2);
                    ball2.InCollisionWithBall.Remove(ball1);
                }
            }
        }
    }
}