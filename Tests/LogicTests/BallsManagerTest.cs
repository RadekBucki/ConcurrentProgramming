using System;
using System.Threading;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LogicTests
{
    [TestClass]
    public class BallsManagerTest
    {
        private readonly LogicAbstractApi _ballsManager = LogicAbstractApi.CreateApi(
            100, 100, new TestData()
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

        [DataTestMethod]
        [DataRow(50, 50, 2, 2)]
        [DataRow(40, 40, 1, 0)]
        [DataRow(30, 30, 0, 1)]
        [DataRow(40, 40, -1, -1)]
        public void MoveBalls(int x, int y, int xSpeed, int ySpeed)
        {
            _ballsManager.CreateBall(x, y, xSpeed, ySpeed);
            _ballsManager.MoveBallsAccordingToSpeed(null);
            Assert.AreEqual(x + xSpeed, _ballsManager.GetAllBalls()[0].XPosition);
            Assert.AreEqual(y + ySpeed, _ballsManager.GetAllBalls()[0].YPosition);
        }

        [TestMethod]
        public void MoveBallsWall()
        {
            _ballsManager.CreateBall(97, 97, 2, 2);
            _ballsManager.MoveBallsAccordingToSpeed(null);
            Assert.AreEqual(95, _ballsManager.GetAllBalls()[0].XPosition);
            Assert.AreEqual(95, _ballsManager.GetAllBalls()[0].YPosition);
        }

        [TestMethod]
        public void MovementTimerTest()
        {
            _ballsManager.CreateBall(50, 50, 2, 2);
            _ballsManager.StartBalls();
            Thread.Sleep(100);
            _ballsManager.StopBalls();
            int newPosX = _ballsManager.GetAllBalls()[0].XPosition;
            int newPosY = _ballsManager.GetAllBalls()[0].YPosition;
            Assert.AreNotEqual(50, _ballsManager.GetAllBalls()[0].XPosition);
            Assert.AreNotEqual(50, _ballsManager.GetAllBalls()[0].YPosition);
            Thread.Sleep(100);
            Assert.AreEqual(newPosX, _ballsManager.GetAllBalls()[0].XPosition);
            Assert.AreEqual(newPosY, _ballsManager.GetAllBalls()[0].YPosition);
        }
    }
}