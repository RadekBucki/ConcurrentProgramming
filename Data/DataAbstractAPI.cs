namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi? CreateApi()
        {
            return new BallsRepository();
        }

        public abstract void Connect();
    }
}