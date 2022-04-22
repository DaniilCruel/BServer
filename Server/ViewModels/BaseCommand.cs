using System;
using System.Windows.Input;

namespace Server.ViewModels
{

    /// Конструирование команд

    public class BaseCommand : ICommand
    {
        private Action<object> execute;
        private Func<object, bool> canExecute;

        public BaseCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) =>
            this.canExecute == null || this.canExecute(parameter);
        

        public void Execute(object parameter) =>
            this.execute(parameter);
    }

}
