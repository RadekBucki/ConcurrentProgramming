using Presentation.Model;
using Presentation.ViewModel.MVVMCore;
using Data;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _buttonEnabled = true;
        private string _numOfBalls;

        public MainViewModel()
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _numOfBalls = "";
            MainModel = new MainModel(1000, 800);
        }

        public RelayCommand StartCommand { get; }

        public RelayCommand StopCommand { get; }


        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set
            {
                _buttonEnabled = value;
                OnPropertyChanged();
            }
        }

        public string NumOfBalls
        {
            get => _numOfBalls;
            set
            {
                _numOfBalls = value;
                OnPropertyChanged();
            }
        }

        public MainModel MainModel { get; }

        public Ball[]? Balls
        {
            get => MainModel.GetBallsArray();
        }

        public int MainWindowHeight { get; set; }

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
                OnPropertyChanged("Balls");
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
            MainModel.ClearBalls();
            OnPropertyChanged("Balls");
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