using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private int _xPosition;
        private int _yPosition;
        private int _radius;
        private int _xSpeed;
        private int _ySpeed;
        public override event PropertyChangedEventHandler? PropertyChanged;
        public override int XPosition
        {
            get => _xPosition;
            set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }
        
        public override int YPosition
        {
            get => _yPosition;
            set
            {
                _yPosition = value;
                RaisePropertyChanged();
            }
        }
        
        public override int Radius
        {
            get => _radius;
            set
            {
                _radius = value;
                RaisePropertyChanged();
            }
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
            _xSpeed *= -1;
        }

        public override void ChangeYSense()
        {
            _ySpeed *= -1;
        }

        public override void Move()
        {
            XPosition += XSpeed;
            YPosition += YSpeed;
        }

        public Ball(int xPosition, int yPosition, int radius, int xSpeed = 0, int ySpeed = 0)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}