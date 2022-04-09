using System.Drawing;

namespace Data
{
    public class Ball
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int Radius { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }

        public Ball(int xPosition, int yPosition, int radius, int xSpeed = 0, int ySpeed = 0)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
        }
    }
}