using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using Data;

namespace Logic
{
    internal class BallsManager : LogicAbstractApi
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private DataAbstractApi _dataLayer;
        private const int MaxBallSpeed = 5;
        private const int BoardToBallRatio = 50;
        private const int BallWeight = 100;
        private List<IBall> _balls = new();
        private Dictionary<IBallData, IBallData> _ballsLastCollision = new();
        private readonly object _syncObject = new();

        public BallsManager(DataAbstractApi dataLayer)
        {
            _dataLayer = dataLayer;
            _boardWidth = _dataLayer.BoardWidth;
            _boardHeight = _dataLayer.BoardHeight;
            _ballRadius = Math.Min(_boardHeight, _boardWidth) / BoardToBallRatio;
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

            if (_balls.Any(
                    ball => Math.Abs(ball.XPosition - x) <= _ballRadius && Math.Abs(ball.YPosition - y) <= _ballRadius)
               )
            {
                throw new ArgumentException("Another ball is already here");
            }

            IBallData ballData = _dataLayer.CreateBallData(x, y, _ballRadius, BallWeight, xSpeed, ySpeed);
            IBall ball = IBall.CreateBall(ballData.XPosition, ballData.YPosition, ballData.Radius);
            ballData.PropertyChanged += ball.UpdateBall!;
            ballData.PropertyChanged += CheckCollision!;
            _balls.Add(ball);
            return ball;
        }

        public override IBall CreateBallInRandomPlace()
        {
            Random r = new();
            bool catched;
            do
            {
                catched = false;
                try
                {
                    return CreateBall(
                        r.Next(_ballRadius, _boardWidth - _ballRadius),
                        r.Next(_ballRadius, _boardHeight - _ballRadius),
                        r.Next(-MaxBallSpeed, MaxBallSpeed),
                        r.Next(-MaxBallSpeed, MaxBallSpeed)
                    );
                }
                catch (ArgumentException e)
                {
                    if (e.Message == "Another ball is already here")
                    {
                        catched = true;
                    }
                }
            } while (catched);

            throw new Exception();
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

        private void CheckCollision(object s, PropertyChangedEventArgs e)
        {
            IBallDataChangedEventArgs args = (IBallDataChangedEventArgs) e;
            IBallData ball = (IBallData) s;

            BallReflection(ball, args.X, args.Y);
            WallReflection(ball, args.X, args.Y);
        }

        private void WallReflection(IBallData ball, int x, int y)
        {
            if (x + ball.XSpeed > _boardWidth - _ballRadius ||
                x + ball.XSpeed < _ballRadius)
            {
                _ballsLastCollision.Remove(ball);
                ball.XSpeed *= -1;
            }

            if (y + ball.YSpeed > _boardHeight - _ballRadius ||
                y + ball.YSpeed < _ballRadius)
            {
                _ballsLastCollision.Remove(ball);
                ball.YSpeed *= -1;
            }
        }

        private void BallReflection(IBallData ball1, int x, int y)
        {
            lock (_syncObject)
            {
                foreach (IBallData ball2 in _dataLayer.GetAllBalls().ToArray())
                {
                    IBallData lastBall1, lastBall2;
                    if ((_ballsLastCollision.TryGetValue(ball1, out lastBall1!) &&
                         _ballsLastCollision.TryGetValue(ball2, out lastBall2!) &&
                         lastBall1 == ball2 && lastBall2 == ball1) || ball1.Equals(ball2))
                    {
                        continue;
                    }

                    int ball1XPosition = x;
                    int ball1YPosition = y;
                    int ball2XPosition;
                    int ball2YPosition;

                    lock (ball2)
                    {
                        ball2XPosition = ball2.XPosition;
                        ball2YPosition = ball2.YPosition;
                    }

                    if ( //condition of circles external contact: (r_1 + r_2) <= |AB|
                        (Math.Abs(Math.Sqrt(
                             (ball1XPosition - ball2XPosition) * (ball1XPosition - ball2XPosition) +
                             (ball1YPosition - ball2YPosition) * (ball1YPosition - ball2YPosition)
                         )) <= _ballRadius * 2.0 ||
                         Math.Sqrt(
                             (ball1XPosition + ball1.XSpeed - ball2XPosition + ball2.XSpeed) *
                             (ball1XPosition + ball1.XSpeed - ball2XPosition + ball2.XSpeed) +
                             (ball1YPosition + ball1.YSpeed - ball2YPosition + ball2.YSpeed) *
                             (ball1YPosition + ball1.YSpeed - ball2YPosition + ball2.YSpeed)
                         ) < _ballRadius * 2.0)
                       )
                    {
                        int ball1NewYSpeed = ball2.YSpeed;
                        int ball2NewYSpeed = ball1.YSpeed;
                        int ball1NewXSpeed = ball2.XSpeed;
                        int ball2NewXSpeed = ball1.XSpeed;

                        // Change sense
                        if (ball1NewXSpeed * ball2NewXSpeed > 0)
                        {
                            switch (ball2NewXSpeed)
                            {
                                case > 0 when ball1XPosition > ball2XPosition:
                                case < 0 when ball1XPosition < ball2XPosition:
                                    ball2NewXSpeed *= -1;
                                    break;
                                case < 0 when ball1XPosition < ball2XPosition:
                                case > 0 when ball1XPosition > ball2XPosition:
                                    ball1NewXSpeed *= -1;
                                    break;
                            }
                        }

                        if (ball1NewYSpeed * ball2NewYSpeed > 0)
                        {
                            switch (ball2NewYSpeed)
                            {
                                case > 0 when ball1YPosition > ball2YPosition:
                                case < 0 when ball1YPosition < ball2YPosition:
                                    ball2NewYSpeed *= -1;
                                    break;
                                case < 0 when ball1YPosition < ball2YPosition:
                                case > 0 when ball1YPosition > ball2YPosition:
                                    ball1NewYSpeed *= -1;
                                    break;
                            }
                        }

                        ball1.XSpeed = ball1NewXSpeed;
                        ball1.YSpeed = ball1NewYSpeed;
                        ball2.XSpeed = ball2NewXSpeed;
                        ball2.YSpeed = ball2NewYSpeed;

                        _ballsLastCollision.Remove(ball1);
                        _ballsLastCollision.Remove(ball2);
                        _ballsLastCollision.Add(ball1, ball2);
                        _ballsLastCollision.Add(ball2, ball1);
                    }
                }
            }
        }
    }
}