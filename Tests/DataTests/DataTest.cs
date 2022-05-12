using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class DataTest
    {
        private DataAbstractApi _dataLayer = DataAbstractApi.CreateApi(100, 100);

        [TestMethod]
        public void CreateBallTest()
        {
            _dataLayer.CreateBallData(10, 20, 10, 10);
            Assert.AreEqual(1, _dataLayer.GetAllBalls().Count);
        }

        [TestMethod]
        public void RemoveBallsTests()
        {
            _dataLayer.CreateBallData(10, 20, 10, 10);
            _dataLayer.CreateBallData(20, 20, 10, 10);
            Assert.AreEqual(2, _dataLayer.GetAllBalls().Count);
            _dataLayer.RemoveAllBalls();
            Assert.AreEqual(0, _dataLayer.GetAllBalls().Count);
        }
    }
}