using System;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTests;

public class DataTest
{
    [TestClass]
    public class BallsRepositoryTest
    {
        [TestMethod]
        public void CreateBallNegativeTest()
        {
            DataAbstractAPI dataLayer = DataAbstractAPI.CreateApi();
            Assert.ThrowsException<NotImplementedException>(() => dataLayer.Connect());
        }
    }
}