using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Data;

namespace Logic
{
    internal class Ball : IBall, INotifyPropertyChanged
    {
        public override event PropertyChangedEventHandler? PropertyChanged;

        public override int XPosition { get; set; }

        public override int YPosition { get; set; }

        public override int Radius { get; set; }

        public override void UpdateBall(Object s, PropertyChangedEventArgs e)
        {
            IBallDataChangedEventArgs args = (IBallDataChangedEventArgs) e;
            XPosition = args.X;
            YPosition = args.Y;
            RaisePropertyChanged();
        }

        public Ball(int xPosition, int yPosition, int radius)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Radius = radius;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new BallChangedEventArgs(propertyName, XPosition, YPosition));
        }
    }
}