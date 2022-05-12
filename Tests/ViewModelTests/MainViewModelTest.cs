using Microsoft.VisualStudio.TestTools.UnitTesting;
using Presentation.ViewModel;

namespace ViewModelTests
{
    [TestClass]
    public class MainViewModelTest
    {
        private TestModel _testModel = new(10, 10);
        private MainViewModel? _mainViewModel;
        
        [TestInitialize]
        public void TestInitialize()
        {
            _mainViewModel = new(_testModel);
        }
        
        [TestMethod]
        public void StartStopTest()
        {
            Assert.IsNotNull(_mainViewModel);
            Assert.AreEqual(0, _mainViewModel.Circles.Count);
            Assert.AreEqual(true, _mainViewModel.ButtonEnabled);
            Assert.AreEqual(true, _mainViewModel.ButtonEnabled);
            _mainViewModel.NumOfBalls = "5";
            _mainViewModel.StartCommand.Execute(null);
            Assert.AreEqual(false, _mainViewModel.ButtonEnabled);
            Assert.AreEqual(5, _mainViewModel.Circles.Count);
        }
        
        [TestMethod]
        [DataRow("-5")]
        [DataRow("abc")]
        public void StartStopNegativeTest(string numOfBalls)
        {
            Assert.IsNotNull(_mainViewModel);
            _mainViewModel.NumOfBalls = numOfBalls;
            _mainViewModel.StartCommand.Execute(null);
            Assert.AreEqual("", _mainViewModel.NumOfBalls);
        }
    }
}