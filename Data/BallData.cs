using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Data.Logger;

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
        internal override event PropertyChangedEventHandler? LoggerPropertyChanged;

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
                OnLoggerPropertyChanged(_xPosition, value);
                _xPosition = value;
            }
        }

        public override int YPosition
        {
            get => _yPosition;
            internal set
            {
                OnLoggerPropertyChanged(_yPosition, value);
                _yPosition = value;
            }
        }

        public override int Weight { get; }

        public override int Radius { get; }

        public override int XSpeed
        {
            get => _xSpeed;
            set
            {
                OnLoggerPropertyChanged(_xSpeed, value);
                _xSpeed = value;
            }
        }

        public override int YSpeed
        {
            get => _ySpeed;
            set
            {
                OnLoggerPropertyChanged(_ySpeed, value);
                _ySpeed = value;
            }
        }

        private void StartMovement()
        {
            Stopwatch stopwatch = new();
            while (_moving)
            {
                stopwatch.Start();
                lock (this)
                {
                    XPosition += XSpeed;
                    YPosition += YSpeed;
                }
                RaisePropertyChanged("Position");
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

        private void OnLoggerPropertyChanged(
            object oldValue, object newValue,
            [CallerMemberName] string? propertyName = null
        )
        {
            Thread thread = new(
                () => LoggerPropertyChanged?.Invoke(
                    this,
                    new LoggerPropertyChangedEventArgs(propertyName, oldValue, newValue)
                )
            );
            thread.Start();
        }
    }
}