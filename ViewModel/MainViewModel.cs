using Presentation.ViewModel.MVVMCore;

namespace Presentation.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            StartCommand = new RelayCommand(this.StartBalls, this.CanDoDisableButton);
            StopCommand = new RelayCommand(this.StopBalls, this.CanDoEnableButton);
            m_NumOfBalls = "";
        }

        public RelayCommand StartCommand
        {
            get;
        }

        public RelayCommand StopCommand
        {
            get;
        }

        private bool m_buttonEnabled = true;
        public bool ButtonEnabled
        {
            get { return m_buttonEnabled; }
            set
            { 
                m_buttonEnabled = value; 
                OnPropertyChanged();
            }
        }

        private string m_NumOfBalls;
        public string NumOfBalls
        {
            get { return m_NumOfBalls; }
            set { m_NumOfBalls = value; OnPropertyChanged(); }
        }
        private void StartBalls()
        {
            try
            {
                int ballsNum = Int32.Parse(NumOfBalls);
                if (ballsNum > 0)
                {
                    DoChangeButtonEnabled();
                }
                else
                {
                    throw new ArgumentException("Not an positive integer");
                }
            }
            catch (Exception ex)
            {
                NumOfBalls = "";
                OnPropertyChanged();
            }
        }

        private void StopBalls()
        {
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
