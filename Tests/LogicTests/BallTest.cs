using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class BallTest
    {
        private IBall _ball = IBall.CreateBall(1, 2, 1);

        [TestMethod]
        public void CreateBallTest()
        {
            Assert.AreEqual(1, _ball.XPosition);
            Assert.AreEqual(2, _ball.YPosition);
            Assert.AreEqual(1, _ball.Radius);
        }

        [TestMethod]
        public void SetBallCoordinatesTest()
        {
            _ball.XPosition = 2;
            _ball.YPosition = 3;
            Assert.AreEqual(2, _ball.XPosition);
            Assert.AreEqual(3, _ball.YPosition);
        }

        [TestMethod]
        public void SetRadiusTest()
        {
            _ball.Radius = 2;
            Assert.AreEqual(2, _ball.Radius);
        }
    }
}