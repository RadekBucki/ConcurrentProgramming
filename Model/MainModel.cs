using Data;
using Logic;

namespace Presentation.Model;

public class MainModel
{
    private readonly BallsManager _ballsManager;

    public MainModel(int boardWidth, int boardHeight)
    {
        _ballsManager = new(boardWidth, boardHeight);
    }

    public Ball[] GetBallsArray()
    {
        return _ballsManager.GetAllBalls();
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