using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Presentation.Model;

namespace ViewModelTests
{
    internal class TestModel : ModelAbstractApi
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
        private Timer? _movementTimer;
        private const int MaxBallSpeed = 5;
        private const int BoardToBallRatio = 50;
        private ObservableCollection<ICircle> _circles = new();

        public TestModel(int boardWidth, int boardHeight)
        {
            _boardWidth = boardWidth;
            _boardHeight = boardHeight;
            _ballRadius = Math.Min(boardHeight, boardWidth) / BoardToBallRatio;
        }

        public override ObservableCollection<ICircle> GetCircles()
        {
            return _circles;
        }

        public override void CreateBallInRandomPlace()
        {
            Random r = new();

            _circles.Add(
                new TestCircle(
                    r.Next(_ballRadius, _boardWidth - _ballRadius), r.Next(_ballRadius, _boardHeight - _ballRadius),
                    _ballRadius
                )
            );
        }

        public override void CreateNBallsInRandomPlaces(int numOfBalls)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                CreateBallInRandomPlace();
            }
        }

        public override void ClearCircles()
        {
            _circles.Clear();
        }

        public override void StartBallsMovement()
        {
            _movementTimer = new Timer(MoveBallsAccordingToSpeed, null, 0, 8);
        }

        public override void StopBallsMovement()
        {
            _movementTimer?.Dispose();
        }

        private void MoveBallsAccordingToSpeed(object? state)
        {
            foreach (ICircle circle in _circles.ToArray())
            {
                if (circle.X + MaxBallSpeed >= _boardWidth - _ballRadius ||
                    circle.X + MaxBallSpeed <= _ballRadius)
                {
                    circle.X *= -1;
                }

                if (circle.Y + MaxBallSpeed >= _boardHeight - _ballRadius ||
                    circle.Y + MaxBallSpeed <= _ballRadius)
                {
                    circle.Y *= -1;
                }

                circle.X += MaxBallSpeed;
                circle.Y += MaxBallSpeed;
            }
        }
    }
}