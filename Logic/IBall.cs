using System;
using System.ComponentModel;

namespace Logic
{
    public abstract class IBall
    {
        public static IBall CreateBall(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0)
        {
            return new Ball(xPosition, yPosition, radius, weight, xSpeed, ySpeed);
        }

        public abstract event PropertyChangedEventHandler? PropertyChanged;
        public abstract int XPosition { get; set; }

        public abstract int YPosition { get; set; }

        public abstract int Weight { get; set; }
        
        public abstract int Radius { get; set; }

        public abstract int XSpeed { get; set; }

        public abstract int YSpeed { get; set; }

        public abstract void UpdateBall(Object s, PropertyChangedEventArgs e);
    }
}