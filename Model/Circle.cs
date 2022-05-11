using System.ComponentModel;
using System.Runtime.CompilerServices;
using Logic;

namespace Presentation.Model
{
    internal class Circle : ICircle, INotifyPropertyChanged
    {
        private int _x;
        private int _y;
        private int _radius;

        public override int X
        {
            get => _x;
            set
            {
                _x = value;
                RaisePropertyChanged();
            }
        }

        public override int Y
        {
            get => _y;
            set
            {
                _y = value;
                RaisePropertyChanged();
            }
        }

        public override int Radius
        {
            get => _radius;
            set => _radius = value;
        }

        public override event PropertyChangedEventHandler? PropertyChanged;

        public Circle(int x, int y, int radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public override void UpdateCircle(Object s, PropertyChangedEventArgs e)
        {
            IBall ball = (IBall) s;
            if (e.PropertyName == "XPosition")
            {
                X = ball.XPosition;
            }
            else if (e.PropertyName == "YPosition")
            {
                Y = ball.YPosition;
            }
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}