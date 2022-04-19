using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTests
{
    [TestClass]
    public class BallsRepositoryTest
    {
        [TestMethod]
        public void AddBallToRepositoryTest()
        {
            BallsRepository ballsRepository = new();
            Assert.AreEqual(0, ballsRepository.GetBalls().Length);
            ballsRepository.Add(new Ball(1, 2, 1));
            Assert.AreEqual(1, ballsRepository.GetBalls().Length);
        }
    }
}