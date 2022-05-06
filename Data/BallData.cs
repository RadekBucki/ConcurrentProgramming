using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data;

public class BallData : IBallData, INotifyPropertyChanged
{
    private int _radius;
    private int _weight;
    private int _xSpeed;
    private int _ySpeed;
    public override event PropertyChangedEventHandler? PropertyChanged;

    public BallData(int radius, int weight, int xSpeed, int ySpeed)
    {
        XSpeed = xSpeed;
        YSpeed = ySpeed;
        Radius = radius;
        Weight = weight;
    }

    public override int Weight
    {
        get => _weight;
        set => _weight = value;
    }

    public override int Radius
    {
        get => _radius;
        set => _radius = value;
    }

    public override int XSpeed
    {
        get => _xSpeed;
        set
        {
            _xSpeed = value; 
            RaisePropertyChanged();
        }
    }

    public override int YSpeed
    {
        get => _ySpeed;
        set
        {
            _ySpeed = value;
            RaisePropertyChanged();
        }
    }

    public override void ChangeXSense()
    {
        XSpeed *= -1;
    }

    public override void ChangeYSense()
    {
        YSpeed *= -1;
    }
    
    private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}