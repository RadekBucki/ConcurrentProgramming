using System.ComponentModel;

namespace Logic;

internal class BallChangedEventArgs : PropertyChangedEventArgs, IBallChangedEventArgs
{
    public int X { get; set; }
    public int Y { get; set; }

    public BallChangedEventArgs(string? propertyName, int x, int y) : base(propertyName)
    {
        X = x;
        Y = y;
    }
}