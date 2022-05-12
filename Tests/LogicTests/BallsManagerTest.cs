using System;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class BallsManagerTest
    {
        private readonly LogicAbstractApi _ballsManager = LogicAbstractApi.CreateApi(
            100, 100, new TestData(100, 100)
        );

        [TestInitialize]
        public void Init()
        {
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Count);
        }

        [TestMethod]
        public void CreateBallTest()
        {
            _ballsManager.CreateBall(10, 20, 1, 2);
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Count);
        }

        [DataTestMethod]
        [DataRow(-1, 1, 1, 1)]
        [DataRow(1, -1, 1, 1)]
        [DataRow(1, 101, 1, 1)]
        [DataRow(101, 1, 1, 1)]
        [DataRow(101, 1, -101, 1)]
        [DataRow(101, 1, 101, 1)]
        [DataRow(101, 1, 1, 101)]
        [DataRow(101, 1, 1, -101)]
        public void CreateBallNegativeTest(int x, int y, int xSpeed, int ySpeed)
        {
            Assert.ThrowsException<ArgumentException>(() => _ballsManager.CreateBall(x, y, xSpeed, ySpeed));
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Count);
        }

        [TestMethod]
        public void CreateBallInRandomPlaceTest()
        {
            _ballsManager.CreateBallInRandomPlace();
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Count);
        }

        [TestMethod]
        public void RemoveAllBallsTest()
        {
            _ballsManager.CreateBallInRandomPlace();
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Count);
            _ballsManager.RemoveAllBalls();
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Count);
        }
    }
}