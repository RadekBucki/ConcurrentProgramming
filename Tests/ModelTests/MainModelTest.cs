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
    }
}