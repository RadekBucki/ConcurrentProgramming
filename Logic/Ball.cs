using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        private int _xPosition;
        private int _yPosition;
        private int _weight;
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
            set => _xSpeed = value;
        }

        public override int YSpeed
        {
            get => _ySpeed;
            set => _ySpeed = value;
        }

        public override void UpdateBall(Object s, PropertyChangedEventArgs e)
        {
            IBallData ball = (IBallData) s;
            GetType().GetProperty(e.PropertyName!)!.SetValue(
                this, ball.GetType().GetProperty(e.PropertyName!)!.GetValue(ball)
            );
        }

        public Ball(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Weight = weight;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}