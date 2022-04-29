using System.Collections.ObjectModel;
using Logic;

namespace Presentation.Model
{

    public abstract class ModelAbstractApi
    {
        protected LogicAbstractApi LogicLayer;
        public static ModelAbstractApi CreateApi(LogicAbstractApi? logicLayer = null)
        {
            return new MainModel(logicLayer ?? LogicAbstractApi.CreateApi(580, 580));
        }

        public abstract ObservableCollection<ICircle> GetCircles();

        public abstract void CreateBallInRandomPlace();

        public abstract void CreateNBallsInRandomPlaces(int numOfBalls);

        public abstract void ClearBalls();

        public abstract void StartBallsMovement();

        public abstract void StopBallsMovement();
    }
}