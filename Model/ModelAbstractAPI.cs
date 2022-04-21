using System.Collections.ObjectModel;

namespace Presentation.Model
{

    public abstract class ModelAbstractAPI
    {
        public static ModelAbstractAPI CreateApi()
        {
            return new MainModel();
        }

        public abstract ObservableCollection<ICircle> GetCircles();

        public abstract void CreateBallInRandomPlace();

        public abstract void CreateNBallsInRandomPlaces(int numOfBalls);

        public abstract void ClearBalls();

        public abstract void StartBallsMovement();

        public abstract void StopBallsMovement();
    }
}