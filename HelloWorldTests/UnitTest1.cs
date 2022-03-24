using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HelloWorldTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void HelloWorldTest()
        {
            Assert.AreEqual("Hello world", HelloWorld.Class1.GetHelloWorld());
        }

        [TestMethod]
        public void AddMethodTest()
        {
            Assert.IsNotNull(HelloWorld.Class1.Add(1, 2));
            Assert.AreEqual(3,HelloWorld.Class1.Add(1, 2));
        }
    }
}