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
            Thread ballThread = new(StartMovement);
            ballThread.IsBackground = true;
            ballThread.Start();
        }

        public override int XPosition
        {
            get => _xPosition;
            internal set => _xPosition = value;
        }

        public override int YPosition
        {
            get => _yPosition;
            internal set => _yPosition = value;
        }

        public override int Weight { get; }

        public override int Radius { get; }

        public override int XSpeed { get; set; }

        public override int YSpeed { get; set; }

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
    }
}