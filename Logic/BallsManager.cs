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
        private Dictionary <IBallData, IBallData> _ballsLastCollision = new();
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
            IBall ball = IBall.CreateBall(ballData.XPosition, ballData.YPosition, ballData.Radius, ballData.Weight,
                ballData.XSpeed, ballData.YSpeed);
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

        public override void CheckCollision(Object s, PropertyChangedEventArgs e)
        {
            IBallData ball = (IBallData) s;

            if (e.PropertyName is not ("XPosition" or "YPosition")) return;
            
            BallReflection(ball);
            WallReflection(ball);
        }

        private void WallReflection(IBallData ball)
        {
            if (ball.XPosition + ball.XSpeed >= _boardWidth - _ballRadius ||
                ball.XPosition + ball.XSpeed <= _ballRadius)
            {
                _ballsLastCollision.Remove(ball);
                ball.ChangeXSense();
            }

            if (ball.YPosition + ball.YSpeed >= _boardHeight - _ballRadius ||
                ball.YPosition + ball.YSpeed <= _ballRadius)
            {
                _ballsLastCollision.Remove(ball);
                ball.ChangeYSense();
            }
        }

        private void BallReflection(IBallData ball1)
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
                    
                    if ( //condition of circles external contact: (r_1 + r_2) <= |AB|
                        (Math.Abs(Math.Sqrt(
                             (ball1.XPosition - ball2.XPosition) * (ball1.XPosition - ball2.XPosition) +
                             (ball1.YPosition - ball2.YPosition) * (ball1.YPosition - ball2.YPosition)
                         )) <= _ballRadius * 2.0 ||
                         Math.Sqrt(
                             (ball1.XPosition + ball1.XSpeed - ball2.XPosition + ball2.XSpeed) *
                             (ball1.XPosition + ball1.XSpeed - ball2.XPosition + ball2.XSpeed) +
                             (ball1.YPosition + ball1.YSpeed - ball2.YPosition + ball2.YSpeed) *
                             (ball1.YPosition + ball1.YSpeed - ball2.YPosition + ball2.YSpeed)
                         ) <= _ballRadius * 2.0)
                       )
                    {
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
                            ChangeXSenseToOpposite(ball1StartXSpeed, ball1, ball2);
                        }

                        if (ball1StartYSpeed * ball2StartYSpeed > 0)
                        {
                            ChangeYSenseToOpposite(ball1StartYSpeed, ball1, ball2);
                        }
                        _ballsLastCollision.Remove(ball1);
                        _ballsLastCollision.Remove(ball2);
                        _ballsLastCollision.Add(ball1, ball2);
                        _ballsLastCollision.Add(ball2, ball1);
                    }
                }
            }
        }

        private static void ChangeYSenseToOpposite(int ball1StartYSpeed, IBallData ball1, IBallData ball2)
        {
            switch (ball1StartYSpeed)
            {
                case > 0 when ball1.YPosition > ball2.YPosition:
                case < 0 when ball1.YPosition < ball2.YPosition:
                    ball2.ChangeYSense();
                    break;
                case < 0 when ball1.YPosition < ball2.YPosition:
                case > 0 when ball1.YPosition > ball2.YPosition:
                    ball1.ChangeYSense();
                    break;
            }
        }

        private static void ChangeXSenseToOpposite(int ball1StartXSpeed, IBallData ball1, IBallData ball2)
        {
            switch (ball1StartXSpeed)
            {
                case > 0 when ball1.XPosition > ball2.XPosition:
                case < 0 when ball1.XPosition < ball2.XPosition:
                    ball2.ChangeXSense();
                    break;
                case < 0 when ball1.XPosition < ball2.XPosition:
                case > 0 when ball1.XPosition > ball2.XPosition:
                    ball1.ChangeXSense();
                    break;
            }
        }
    }
}