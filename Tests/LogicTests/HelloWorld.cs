using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.LogicTests
{
    [TestClass]
    public class HelloWorld
    {
        [TestMethod]
        public void HelloWorldTest()
        {
            Assert.AreEqual("Hello world", Logic.HelloWorld.GetHelloWorld());
        }

        [TestMethod]
        public void AddMethodTest()
        {
            Assert.IsNotNull(Logic.HelloWorld.Add(1, 2));
            Assert.AreEqual(3, Logic.HelloWorld.Add(1, 2));
        }
    }
}