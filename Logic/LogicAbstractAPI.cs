using System;
using System.Collections.Generic;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        protected DataAbstractApi DataLayer;
        public static LogicAbstractApi CreateApi(int boardWidth, int boardHeight, DataAbstractApi? dataAbstractApi = null)
        {
            return new BallsManager(boardWidth, boardHeight, dataAbstractApi ?? DataAbstractApi.CreateApi());
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