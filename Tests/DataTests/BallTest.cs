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
            Assert.AreEqual(1, ball.XPosition);
            Assert.AreEqual(2, ball.YPosition);
            Assert.AreEqual(0, ball.XSpeed);
            Assert.AreEqual(0, ball.YSpeed);
        }
        [TestMethod]
        public void SetBallCoordinatesTest()
        {
            ball.XPosition = 2;
            ball.YPosition = 3;
            Assert.AreEqual(2, ball.XPosition);
            Assert.AreEqual(3, ball.YPosition);
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