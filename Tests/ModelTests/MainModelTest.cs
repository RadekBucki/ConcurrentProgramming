using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.Model;

namespace ModelTests
{
    [TestClass]
    public class MainModelTest
    {
        private ModelAbstractApi _model = ModelAbstractApi.CreateApi(new TestLogic(10, 10));

        [TestMethod]
        public void CreateCircleTest()
        {
            _model.CreateBallInRandomPlace();
            Assert.AreEqual(1, _model.GetCircles().Count);
        }

        [TestMethod]
        public void CreateNCircleTest()
        {
            _model.CreateNBallsInRandomPlaces(3);
            Assert.AreEqual(3, _model.GetCircles().Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            _model.CreateNBallsInRandomPlaces(3);
            Assert.AreEqual(3, _model.GetCircles().Count);
            _model.ClearCircles();
            Assert.AreEqual(0, _model.GetCircles().Count);
        }

        [TestMethod]
        public void StartStopTest()
        {
            _model.StartBallsMovement();
            _model.StopBallsMovement();
        }
    }
}