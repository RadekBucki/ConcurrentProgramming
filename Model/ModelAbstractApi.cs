using System.Collections.ObjectModel;
using Logic;

namespace Presentation.Model
{
    public abstract class ModelAbstractApi
    {
        public static ModelAbstractApi CreateApi(LogicAbstractApi? logicLayer = null)
        {
            return new MainModel(logicLayer ?? LogicAbstractApi.CreateApi(580, 580));
        }

        public abstract ObservableCollection<ICircle> GetCircles();

        public abstract void CreateBallInRandomPlace();

        public abstract void CreateNBallsInRandomPlaces(int numOfBalls);

        public abstract void ClearCircles();
    }
}