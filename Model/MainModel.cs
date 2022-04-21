using System.Collections.ObjectModel;
using Data;
using Logic;

namespace Presentation.Model;

internal class MainModel : ModelAbstractAPI
{
    private readonly LogicAbstractAPI _logicLayer;
    private ObservableCollection<ICircle> Circles = new();

    public MainModel() : this(LogicAbstractAPI.CreateApi(580, 580))
    {
    }

    public MainModel(LogicAbstractAPI logicLayer)
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