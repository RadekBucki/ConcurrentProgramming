using Presentation.Model;
using Presentation.ViewModel.MVVMCore;
using Data;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private bool _buttonEnabled = true;
        private string _numOfBalls;
        private readonly CancellationTokenSource _tokenSource;

        public MainViewModel()
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _numOfBalls = "";
            MainModel = new MainModel();
            _tokenSource = new CancellationTokenSource();
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
                MainModel.StartBallsMovement();
                Task.Run(RefreshBalls, _tokenSource.Token);
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
            MainModel.StopBallsMovement();
            _tokenSource.Cancel();
            DoChangeButtonEnabled();
        }

        private void RefreshBalls()
        {
            while (true)
            {
                OnPropertyChanged("Balls");
            }
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