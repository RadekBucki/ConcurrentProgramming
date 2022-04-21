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
            DataAbstractAPI ballsRepository = new BallsRepository();
            Assert.AreEqual(0, ballsRepository.GetBalls().Length);
            Ball b = new Ball(1, 2, 1);
            ballsRepository.Add(b);
            Assert.AreEqual(1, ballsRepository.GetBalls().Length);
            ballsRepository.Remove(b);
            Assert.AreEqual(0, ballsRepository.GetBalls().Length);
        }
    }
}