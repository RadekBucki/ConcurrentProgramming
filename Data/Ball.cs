namespace Data
{
    public class Ball
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int XSpeed { get; set; }
        public int YSpeed { get; set; }
        
        public Ball(int x, int y, int xSpeed = 0, int ySpeed = 0)
        {
            X = x;
            Y = y;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
        }
    }
}