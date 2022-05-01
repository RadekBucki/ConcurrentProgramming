using System.Threading;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Model;

namespace ModelTests
{
    [TestClass]
    public class MainModelTest
    {
        private ModelAbstractApi? _model;
        private LogicAbstractApi _testLogic = new TestLogic(10, 10);

        [TestInitialize]
        public void TestInitialize()
        {
            _model = ModelAbstractApi.CreateApi(_testLogic);
        }

        [TestMethod]
        public void CreateCircleTest()
        {            
            Assert.IsNotNull(_model);
            _model.CreateBallInRandomPlace();
            Assert.AreEqual(1, _model.GetCircles().Count);
        }

        [TestMethod]
        public void CreateNCircleTest()
        {
            Assert.IsNotNull(_model);
            _model.CreateNBallsInRandomPlaces(3);
            Assert.AreEqual(3, _model.GetCircles().Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            Assert.IsNotNull(_model);
            _model.CreateNBallsInRandomPlaces(3);
            Assert.AreEqual(3, _model.GetCircles().Count);
            _model.ClearCircles();
            Assert.AreEqual(0, _model.GetCircles().Count);
        }

        [TestMethod]
        public void StartStopTest()
        {
            Assert.IsNotNull(_model);
            _model.CreateBallInRandomPlace();
            int x = _testLogic.GetAllBalls()[0].XPosition;
            int y = _testLogic.GetAllBalls()[0].YPosition;
            if (_testLogic.GetAllBalls()[0].XSpeed == 0 || _testLogic.GetAllBalls()[0].YSpeed == 0)
            {
                StartStopTest();
                return;
            }

            _model.StartBallsMovement();
            Thread.Sleep(100);
            _model.StopBallsMovement();
            Assert.AreNotEqual(x, _testLogic.GetAllBalls()[0].XPosition);
            Assert.AreNotEqual(y, _testLogic.GetAllBalls()[0].YPosition);
        }
    }
}