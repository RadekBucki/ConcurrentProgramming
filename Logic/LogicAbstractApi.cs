using System;
using System.Collections.Generic;
using System.ComponentModel;
using Data;

namespace Logic
{
    public abstract class LogicAbstractApi
    {
        public static LogicAbstractApi CreateApi(int boardWidth, int boardHeight, DataAbstractApi? dataAbstractApi = null)
        {
            return new BallsManager(boardWidth, boardHeight, dataAbstractApi ?? DataAbstractApi.CreateApi());
        }

        public abstract IBall CreateBall(int x, int y, int xSpeed, int ySpeed);

        public abstract IBall CreateBallInRandomPlace();

        public abstract List<IBall> GetAllBalls();

        public abstract void RemoveAllBalls();

        public abstract void CheckCollision(Object s, PropertyChangedEventArgs e);
    }
}