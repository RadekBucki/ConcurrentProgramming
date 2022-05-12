namespace Data
{
    public abstract class DataAbstractApi
    {
        public static DataAbstractApi CreateApi(int boardWidth, int boardHeight)
        {
            return new Board(boardWidth, boardHeight);
        }

        public abstract IBallData CreateBallData(int xPosition, int yPosition, int radius, int weight, int xSpeed = 0,
            int ySpeed = 0);

        public abstract List<IBallData> GetAllBalls();

        public abstract void RemoveAllBalls();
        public abstract int BoardWidth { get; }
        public abstract int BoardHeight { get; }
    }
}