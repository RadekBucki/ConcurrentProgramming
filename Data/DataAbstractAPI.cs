namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateApi()
        {
            return new BallsRepository();
        }

        public abstract void Connect();
    }
}