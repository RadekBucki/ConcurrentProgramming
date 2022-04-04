using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTests
{
    [TestClass]
    public class BallTest
    {
        Ball ball = new(1, 2);
        [TestMethod]
        public void CreateBallTest()
        {
            Assert.AreEqual(1, ball.X);
            Assert.AreEqual(2, ball.Y);
            Assert.AreEqual(0, ball.XSpeed);
            Assert.AreEqual(0, ball.YSpeed);
        }
        [TestMethod]
        public void SetBallCoordinatesTest()
        {
            ball.X = 2;
            ball.Y = 3;
            Assert.AreEqual(2, ball.X);
            Assert.AreEqual(3, ball.Y);
        }
        [TestMethod]
        public void SetBallSpeedTest()
        {
            ball.XSpeed = 2;
            ball.YSpeed = 3;
            Assert.AreEqual(2, ball.XSpeed);
            Assert.AreEqual(3, ball.YSpeed);
        }
    }
}