using System;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DataTests
{
    [TestClass]
    public class DataTest
    {
        [TestMethod]
        public void CreateBallNegativeTest()
        {
            DataAbstractApi? dataLayer = DataAbstractApi.CreateApi();
            Assert.ThrowsException<NotImplementedException>(() => dataLayer.Connect());
        }
    }
}