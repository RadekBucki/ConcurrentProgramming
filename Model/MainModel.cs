using Logic;

namespace Presentation.Model;

public class MainModel : ModelAbstractAPI
{
    private readonly LogicAbstractAPI _logicLayer;

    public MainModel() : this(LogicAbstractAPI.CreateApi(580, 580))
    {
    }

    public MainModel(LogicAbstractAPI logicLayer)
    {
        _logicLayer = logicLayer;
    }

    public override Object[] GetBallsArray()
    {
        return _logicLayer.GetAllBalls();
    }

    public override Object CreateBallInRandomPlace()
    {
        return _logicLayer.CreateBallInRandomPlace();
    }

    public override void CreateNBallsInRandomPlaces(int numOfBalls)
    {
        for (int i = 0; i < numOfBalls; i++)
        {
            _logicLayer.CreateBallInRandomPlace();
        }
    }

    public override void ClearBalls()
    {
        _logicLayer.RemoveAllBalls();
    }

    public override void StartBallsMovement()
    {
        _logicLayer.StartBalls();
    }

    public override void StopBallsMovement()
    {
        _logicLayer.StopBalls();
    }
}