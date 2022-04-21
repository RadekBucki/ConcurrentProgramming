using Presentation.Model;
using Presentation.ViewModel.MVVMCore;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ModelAbstractAPI _modelLayer;
        private bool _buttonEnabled = true;
        private string _numOfBalls = "";
        private Timer? _refreshTimer;

        public MainViewModel() : this(ModelAbstractAPI.CreateApi())
        {
        }
        
        public MainViewModel(ModelAbstractAPI modelLayer)
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _modelLayer = modelLayer;
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

        public Object[]? Balls
        {
            get => _modelLayer.GetBallsArray();
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

                _modelLayer.CreateNBallsInRandomPlaces(ballsNum);
                RaisePropertyChanged("Balls");
                _modelLayer.StartBallsMovement();
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
            _modelLayer.ClearBalls();
            RaisePropertyChanged("Balls");
            _modelLayer.StopBallsMovement();
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