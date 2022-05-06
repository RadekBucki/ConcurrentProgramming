using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class BallDataTest
    {
        private IBallData _ball = IBallData.CreateBallData(1, 2, 1, 10);

        [TestMethod]
        public void CreateBallTest()
        {
            Assert.AreEqual(1, _ball.XSpeed);
            Assert.AreEqual(10, _ball.YSpeed);
            Assert.AreEqual(1, _ball.Radius);
            Assert.AreEqual(2, _ball.Weight);
        }

        [TestMethod]
        public void SetBallSpeedTest()
        {
            _ball.XSpeed = 2;
            _ball.YSpeed = 3;
            Assert.AreEqual(2, _ball.XSpeed);
            Assert.AreEqual(3, _ball.YSpeed);
        }

        [TestMethod]
        public void SetRadiusTest()
        {
            _ball.Radius = 2;
            Assert.AreEqual(2, _ball.Radius);
        }


        [TestMethod]
        public void SetWeightTest()
        {
            _ball.Weight = 20;
            Assert.AreEqual(20, _ball.Weight);
        }

        [TestMethod]
        public void ChangeSenseTest()
        {
            _ball.XSpeed = 2;
            _ball.YSpeed = 3;
            Assert.AreEqual(2, _ball.XSpeed);
            Assert.AreEqual(3, _ball.YSpeed);
            _ball.ChangeXSense();
            _ball.ChangeYSense();
            Assert.AreEqual(-2, _ball.XSpeed);
            Assert.AreEqual(-3, _ball.YSpeed);
        }
    }
}