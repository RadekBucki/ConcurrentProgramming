using Presentation.Model;
using Presentation.ViewModel.MVVMCore;
using Data;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _buttonEnabled = true;
        private string _numOfBalls;
        private Timer? _refreshTimer;

        public MainViewModel()
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _numOfBalls = "";
            MainModel = new MainModel();
        }

        public RelayCommand StartCommand { get; }

        public RelayCommand StopCommand { get; }


        public bool ButtonEnabled
        {
            get => _buttonEnabled;
            set
            {
                _buttonEnabled = value;
                RaisePropertyChanged();
            }
        }

        public string NumOfBalls
        {
            get => _numOfBalls;
            set
            {
                _numOfBalls = value;
                RaisePropertyChanged();
            }
        }

        public MainModel MainModel { get; }

        public Ball[]? Balls
        {
            get => MainModel.GetBallsArray();
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
                RaisePropertyChanged("Balls");
                MainModel.StartBallsMovement();
                _refreshTimer = new Timer(RefreshBalls, null, 0, 8);
                DoChangeButtonEnabled();
            }
            catch (Exception)
            {
                NumOfBalls = "";
                RaisePropertyChanged();
            }
        }

        private void StopBalls()
        {
            MainModel.ClearBalls();
            RaisePropertyChanged("Balls");
            MainModel.StopBallsMovement();
            _refreshTimer?.Dispose();
            DoChangeButtonEnabled();
        }

        private void RefreshBalls(Object? stateInfo)
        {
            RaisePropertyChanged("Balls");
        }

        private void DoChangeButtonEnabled()
        {
            ButtonEnabled = !ButtonEnabled;
            StartCommand.RaiseCanExecuteChanged();
            StopCommand.RaiseCanExecuteChanged();
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