using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Data
{
    internal class BallData : IBallData, INotifyPropertyChanged
    {
        private int _xPosition;
        private int _yPosition;
        private int _xSpeed;
        private int _ySpeed;
        private bool _moving = true;
        public override event PropertyChangedEventHandler? PropertyChanged;

        public BallData(int xPosition, int yPosition, int radius, int weight, int xSpeed, int ySpeed)
        {
            _xPosition = xPosition;
            _yPosition = yPosition;
            XSpeed = xSpeed;
            YSpeed = ySpeed;
            Radius = radius;
            Weight = weight;
            Thread ballThread = new(StartMovement)
            {
                IsBackground = true
            };
            ballThread.Start();
        }

        public override int XPosition
        {
            get => _xPosition;
            internal set
            {
                _xPosition = value;
                RaisePropertyChanged();
            }
        }

        public override int YPosition
        {
            get => _yPosition;
            internal set
            {
                _yPosition = value;
                RaisePropertyChanged();
            }
        }

        public override int Weight { get; }

        public override int Radius { get; }

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

        private void StartMovement()
        {
            Stopwatch stopwatch = new();
            while (_moving)
            {
                stopwatch.Start();
                XPosition += XSpeed;
                YPosition += YSpeed;
                stopwatch.Stop();

                if ((int) stopwatch.ElapsedMilliseconds < 8)
                {
                    Thread.Sleep(8 - (int) stopwatch.ElapsedMilliseconds);
                }

                stopwatch.Reset();
            }
        }

        internal override void Stop()
        {
            _moving = false;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}