namespace Data
{
    public class BallsRepository : DataAbstractAPI
    {
        private List<Ball> _balls = new();

        public override void Add(Ball ball)
        {
            _balls.Add(ball);
        }
        
        public override void Remove(Ball ball)
        {
            _balls.Remove(ball);
        }

        public override Ball[] GetBalls()
        {
            return _balls.ToArray();
        }

        public override void Clear()
        {
            _balls.Clear();
        }
    }
}