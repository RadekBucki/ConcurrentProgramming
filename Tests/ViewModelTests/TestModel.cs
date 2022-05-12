using System;
using System.Collections.ObjectModel;
using Presentation.Model;

namespace ViewModelTests
{
    internal class TestModel : ModelAbstractApi
    {
        private readonly int _boardWidth;
        private readonly int _boardHeight;
        private readonly int _ballRadius;
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
    }
}