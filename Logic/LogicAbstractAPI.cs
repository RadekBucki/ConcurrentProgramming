using System;
using System.Collections.Generic;

namespace Logic
{
    public abstract class LogicAbstractAPI
    {
        public static LogicAbstractAPI CreateApi(int boardWidth, int boardHeight)
        {
            return new BallsManager(boardWidth, boardHeight);
        }

        public abstract IBall CreateBall(int x, int y, int xSpeed, int ySpeed);

        public abstract IBall CreateBallInRandomPlace();

        public abstract List<IBall> GetAllBalls();

        public abstract void RemoveAllBalls();

        public abstract void StartBalls();

        public abstract void StopBalls();

        public abstract void MoveBallsAccordingToSpeed(Object? stateInfo);
    }
}