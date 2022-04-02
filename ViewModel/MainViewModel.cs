using Presentation.Model;
using Presentation.ViewModel.MVVMCore;
using System.Collections.ObjectModel;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private DataLayer _dataLayer;
        private ObservableCollection<Ball>? _balls;
        private bool _buttonEnabled = true;
        private string _numOfBalls;
        public MainViewModel()
        {
            StartCommand = new RelayCommand(StartBalls, CanDoDisableButton);
            StopCommand = new RelayCommand(StopBalls, CanDoEnableButton);
            _numOfBalls = "";
            DataLayer = new DataLayer();
            Balls = new ObservableCollection<Ball>(DataLayer.Ball);
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
            set
            { 
                _buttonEnabled = value; 
                OnPropertyChanged();
            }
        }
        
        public string NumOfBalls
        {
            get => _numOfBalls;
            set { _numOfBalls = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Ball>? Balls
        {
            get => _balls;
            set
            {
                _balls = value;
                OnPropertyChanged();
            }
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
                for (int i = 0; i < ballsNum; i++)
                {
                    Balls?.Add(new Ball());
                }
                DoChangeButtonEnabled();
            }
            catch (Exception ex)
            {
                NumOfBalls = "";
                OnPropertyChanged();
            }
        }

        private void StopBalls()
        {
            Balls?.Clear();
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

        public DataLayer DataLayer
        {
            get => _dataLayer;
            set
            {
                _dataLayer = value;
            }
        }


    }
}
