using System.Collections.ObjectModel;
using Logic;

namespace Presentation.Model
{
    internal class MainModel : ModelAbstractApi
    {
        private ObservableCollection<ICircle> Circles = new();

        public MainModel(LogicAbstractApi logicLayer)
        {
            LogicLayer = logicLayer;
        }

        public override ObservableCollection<ICircle> GetCircles()
        {
            Circles.Clear();
            foreach (IBall ball in LogicLayer.GetAllBalls())
            {
                ICircle c = ICircle.CreateCircle(ball.XPosition, ball.YPosition, ball.Radius);
                Circles.Add(c);
                ball.PropertyChanged += c.UpdateCircle!;
            }

            return Circles;
        }

        public override void CreateBallInRandomPlace()
        {
            LogicLayer.CreateBallInRandomPlace();
        }

        public override void CreateNBallsInRandomPlaces(int numOfBalls)
        {
            for (int i = 0; i < numOfBalls; i++)
            {
                LogicLayer.CreateBallInRandomPlace();
            }
        }

        public override void ClearCircles()
        {
            LogicLayer.RemoveAllBalls();
        }

        public override void StartBallsMovement()
        {
            LogicLayer.StartBalls();
        }

        public override void StopBallsMovement()
        {
            LogicLayer.StopBalls();
        }
    }
}