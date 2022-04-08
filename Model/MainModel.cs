using Data;
using Logic;

namespace Presentation.Model;

public class MainModel
{
    private readonly BallsManager _ballsManager = new(580, 580);
    
    public Ball[] GetBallsArray()
    {
        return _ballsManager.GetAllBalls();
    }

    public Ball CreateBallInRandomPlace()
    {
        return _ballsManager.CreateBallInRandomPlace();
    }

    public void CreateNBallsInRandomPlaces(int numOfBalls)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            _ballsManager.CreateBallInRandomPlace();
        }
    }

    public void ClearBalls()
    {
        _ballsManager.RemoveAllBalls();
    }
}