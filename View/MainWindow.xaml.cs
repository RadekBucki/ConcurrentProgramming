using System.Windows;

namespace Presentation.View
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            Title = ViewModel.HelloWorld.GetHelloWorld();
        }

        private void HelloClick(object sender, RoutedEventArgs e)
        {
            HelloWorld.Content = ViewModel.HelloWorld.GetHelloWorld();
        }
    }
}