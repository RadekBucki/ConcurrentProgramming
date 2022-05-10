using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class BallData : IBallData, INotifyPropertyChanged
    {
        private int _xPosition;
        private int _yPosition;
        private int _radius;
        private int _weight;
        private int _xSpeed;
        private int _ySpeed;
        public override event PropertyChangedEventHandler? PropertyChanged;

        public BallData(int xPosition, int yPosition, int radius, int weight, int xSpeed, int ySpeed)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Weight = weight;
            Thread ballThread = new Thread(StartMovement);
            ballThread.IsBackground = true;
            ballThread.Start();
        }

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

        public override void StartMovement()
        {
            while (true)
            {
                Move();
                Thread.Sleep(8);
            }
        }

        public override void Move()
        {
            XPosition += XSpeed;
            YPosition += YSpeed;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}