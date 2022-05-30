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
            IBallData ball = (IBallData) s;
            XPosition = ball.XPosition;
            YPosition = ball.YPosition;
            RaisePropertyChanged();
        }

        public Ball(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0, int ySpeed = 0)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            Radius = radius;
        }

        private void RaisePropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}