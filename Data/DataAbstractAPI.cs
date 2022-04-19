namespace Data;

public abstract class DataAbstractAPI
{
    public abstract void Add(Ball ball);

    public abstract void Remove(Ball ball);

    public abstract Ball[] GetBalls();

    public abstract void Clear();
}