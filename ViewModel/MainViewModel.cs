using Presentation.Model;
using Presentation.ViewModel.MVVMCore;
using Data;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private Ball[]? _balls;
        private bool _buttonEnabled = true;
        private string _numOfBalls;
        public MainViewModel()
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _numOfBalls = "";
            MainModel = new MainModel();
            Balls = MainModel.GetBallsArray();
        }

        public RelayCommand StartCommand
        {
            get;
        }

        public RelayCommand StopCommand
        {
            get;
        }


        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set { _buttonEnabled = value; OnPropertyChanged(); }
        }
        
        public string NumOfBalls
        {
            get => _numOfBalls;
            set { _numOfBalls = value; OnPropertyChanged(); }
        }
        
        public MainModel MainModel
        {
            get;
        }

        public Ball[]? Balls
        {
            get => _balls;
            set { _balls = value; OnPropertyChanged(); }
        }

        private void StartBalls()
        {
            try
            {
                int ballsNum = int.Parse(NumOfBalls);
                if (ballsNum < 0)
                {
                    throw new ArgumentException("Not an positive integer");
                }
                MainModel.CreateNBallsInRandomPlaces(ballsNum);
                Balls = MainModel.GetBallsArray();
                DoChangeButtonEnabled();
            }
            catch (Exception)
            {
                NumOfBalls = "";
                OnPropertyChanged();
            }
        }

        private void StopBalls()
        {
            Balls = Array.Empty<Ball>();
            MainModel.ClearBalls();
            DoChangeButtonEnabled();
        }

        private void DoChangeButtonEnabled()
        {
            ButtonEnabled = !ButtonEnabled;
            StartCommand.OnCanExecuteChanged();
            StopCommand.OnCanExecuteChanged();
        }

        private bool CanDoDisableButton()
        {
            return ButtonEnabled;
        }

        private bool CanDoEnableButton()
        {
            return !ButtonEnabled;
        }
    }
}
