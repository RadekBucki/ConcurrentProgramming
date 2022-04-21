namespace Data;

public abstract class DataAbstractAPI
{
    public static DataAbstractAPI CreateApi()
    {
        return new BallsRepository();
    }
    public abstract void Add(Ball ball);

    public abstract void Remove(Ball ball);

    public abstract Ball[] GetBalls();

    public abstract void Clear();
}