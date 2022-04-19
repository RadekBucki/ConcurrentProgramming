using System;
using Data;

namespace Logic;

public abstract class LogicAbstractAPI
{
    public abstract Ball CreateBall(int x, int y, int xSpeed, int ySpeed);

    public abstract Ball CreateBallInRandomPlace();

    public abstract Ball[] GetAllBalls();

    public abstract void RemoveAllBalls();

    public abstract void StartBalls();

    public abstract void StopBalls();

    public abstract void MoveBallsAccordingToSpeed(Object? stateInfo);
}