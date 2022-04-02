using System.Windows.Input;

namespace Presentation.ViewModel.MVVMCore
{
    public class RelayCommand : ICommand
    {
        private readonly Action execute;
        private readonly Func<bool> canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }
        public RelayCommand(Action execute, Func<bool> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }

            this.execute = execute;
            this.canExecute = canExecute;
        }

        public bool CanExecute(object? parameter)
        {
            if (this.canExecute == null)
                return true;
            if (parameter == null)
                return this.canExecute();
            return this.canExecute();
        }

        public void Execute(object? parameter)
        {
            this.execute();
        }

        public event EventHandler? CanExecuteChanged;

        internal void OnCanExecuteChanged()
        {
            this.CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
