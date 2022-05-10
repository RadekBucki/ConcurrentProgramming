using System.Collections.ObjectModel;
using Logic;

namespace Presentation.Model
{
    internal class MainModel : ModelAbstractApi
    {
        private ObservableCollection<ICircle> Circles = new();
        private LogicAbstractApi _logicLayer;

        public MainModel(LogicAbstractApi logicLayer)
        {
            _logicLayer = logicLayer;
        }

        public override ObservableCollection<ICircle> GetCircles()
        {
            Circles.Clear();
            foreach (IBall ball in _logicLayer.GetAllBalls())
            {
                ICircle c = ICircle.CreateCircle(ball.XPosition, ball.YPosition, ball.Radius);
                Circles.Add(c);
                ball.PropertyChanged += c.UpdateCircle!;
            }

            return Circles;
        }

        public override void CreateBallInRandomPlace()
        {
            _logicLayer.CreateBallInRandomPlace();
        }

        public override void CreateNBallsInRandomPlaces(int numOfBalls)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                _logicLayer.CreateBallInRandomPlace();
            }
        }

        public override void ClearCircles()
        {
            _logicLayer.RemoveAllBalls();
        }
    }
}