namespace Data
{
    public class BallsRepository
    {
        private List<Ball> _balls = new();

        public void Add(Ball ball)
        {
            _balls.Add(ball);
        }
        
        public void Remove(Ball ball)
        {
            _balls.Remove(ball);
        }

        public Ball[] GetBalls()
        {
            return _balls.ToArray();
        }

        public void Clear()
        {
            _balls.Clear();
        }
    }
}