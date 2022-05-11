using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
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
        private const int MaxBallSpeed = 5;
        private const int BoardToBallRatio = 50;
        private const int BallWeight = 100;
        private List<IBall> _balls = new();
        private List<IBallData> _ballsInCollision = new();

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
            WallReflection(ball);
            BallReflection(ball);
            try
            {
                _ballsInCollision.Remove(ball);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void WallReflection(IBallData ball)
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
        }

        private void BallReflection(IBallData ball1)
        {
            foreach (IBallData ball2 in _dataLayer.GetAllBalls().ToArray())
            {
                if (ball1.Equals(ball2))
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
                     ) <= _ballRadius * 2.0) &&
                    !_ballsInCollision.Contains(ball2) &&
                    !_ballsInCollision.Contains(ball1) &&
                    Monitor.TryEnter(ball1, new TimeSpan(0, 0, 0, 0, 10))
                   )
                {
                    _ballsInCollision.Add(ball1);
                    _ballsInCollision.Add(ball2);

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

                    ball1.Move();
                    ball2.Move();

                    Monitor.Exit(ball1);
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