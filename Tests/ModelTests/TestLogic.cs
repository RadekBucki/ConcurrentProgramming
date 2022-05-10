using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using Data;
using Logic;

namespace ModelTests
{
    internal class TestLogic : LogicAbstractApi
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private const int MaxBallSpeed = 5;
        private const int BoardToBallRatio = 50;
        private List<IBall> _balls = new();

        public TestLogic(int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            _ballRadius = Math.Min(boardHeight, boardWidth) / BoardToBallRatio;
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

            IBall ball = IBall.CreateBall(x, y, _ballRadius, _ballRadius * 10, xSpeed, ySpeed);
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

        public override void CheckCollision(Object s, PropertyChangedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}