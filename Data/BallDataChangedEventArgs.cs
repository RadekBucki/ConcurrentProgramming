using System.ComponentModel;

namespace Data;

internal class BallDataChangedEventArgs : PropertyChangedEventArgs, IBallDataChangedEventArgs
{
    public int X { get; set; }
    public int Y { get; set; }

    public BallDataChangedEventArgs(string? propertyName, int x, int y) : base(propertyName)
    {
        X = x;
        Y = y;
    }
}