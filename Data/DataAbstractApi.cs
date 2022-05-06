namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi()
        {
            return new BallsRepository();
        }

        public abstract IBallData CreateBallData(int radius, int weight, int xSpeed = 0, int ySpeed = 0);
        
        public abstract List<IBallData> GetAllBalls();

        public abstract void RemoveAllBalls();
    }
}