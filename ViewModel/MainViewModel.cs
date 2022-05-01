using System.Collections.ObjectModel;
using Presentation.Model;
using Presentation.ViewModel.MVVMCore;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private readonly ModelAbstractApi _modelLayer;
        private bool _buttonEnabled = true;
        private string _numOfBalls = "";

        public MainViewModel() : this(ModelAbstractApi.CreateApi())
        {
        }

        public MainViewModel(ModelAbstractApi modelLayer)
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

        public ObservableCollection<ICircle> Circles => _modelLayer.GetCircles();

        private void StartBalls()
        {
            try
            {
                int ballsNum = int.Parse(NumOfBalls);
                if (ballsNum <= 0)
                {
                    throw new ArgumentException("Not an positive integer");
                }

                _modelLayer.CreateNBallsInRandomPlaces(ballsNum);
                RaisePropertyChanged(nameof(Circles));
                _modelLayer.StartBallsMovement();
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
            _modelLayer.ClearCircles();
            RaisePropertyChanged(nameof(Circles));
            _modelLayer.StopBallsMovement();
            DoChangeButtonEnabled();
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