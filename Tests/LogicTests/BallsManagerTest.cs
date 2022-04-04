using System;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.LogicTests
{
    [TestClass]
    public class BallsManagerTest
    {
        BallsManager _ballsManager = new(100, 100);

        [TestInitialize]
        public void Init()
        {
            _ballsManager = new(100, 100);
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Length);
        }

        [TestMethod]
        public void CreateBallTest()
        {
            _ballsManager.CreateBall(1, 2, 1, 2);
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Length);
        }

        [DataTestMethod]
        [DataRow(-1, 1)]
        [DataRow(1, -1)]
        [DataRow(1, 101)]
        [DataRow(101, 1)]
        public void CreateBallNegativeTest(int x, int y)
        {
            Assert.ThrowsException<ArgumentException>(() => _ballsManager.CreateBall(x, y, 1, 2));
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Length);
        }

        [TestMethod]
        public void CreateBallInRandomPlaceTest()
        {
            _ballsManager.CreateBallInRandomPlace();
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Length);
        }

        [TestMethod]
        public void RemoveAllBallsTest()
        {
            _ballsManager.CreateBallInRandomPlace();
            Assert.AreEqual(1, _ballsManager.GetAllBalls().Length);
            _ballsManager.RemoveAllBalls();
            Assert.AreEqual(0, _ballsManager.GetAllBalls().Length);
        }
    }
}