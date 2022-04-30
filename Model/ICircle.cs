using System.ComponentModel;

namespace Presentation.Model
{
    public abstract class ICircle
    {
        public static ICircle CreateCircle(int xPosition, int yPosition, int radius)
        {
            return new Circle(xPosition, yPosition, radius);
        }

        public abstract event PropertyChangedEventHandler? PropertyChanged;
        public abstract int X { get; set; }

        public abstract int Y { get; set; }

        public abstract int Radius { get; set; }

        public abstract void UpdateCircle(Object s, PropertyChangedEventArgs e);
    }
}