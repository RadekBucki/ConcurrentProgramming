namespace Presentation.Model
{
    public class DataLayer
    {
        private List<Ball> _balls = new();
        public IEnumerable<Ball> Ball
        {
            set => _balls = (List<Ball>)value;
            get => _balls;
        }
    }
}