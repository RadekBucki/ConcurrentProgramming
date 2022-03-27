using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.DataTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HelloWorldTest()
        {
            Assert.AreEqual("Hello world", Data.HelloWorld.GetHelloWorld());
        }
    }
}