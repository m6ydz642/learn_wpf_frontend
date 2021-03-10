using System;
using System.Windows.Input;

namespace WPF_Tranning
{
    internal class Append : ICommand
    {
        private GameStartViewModel gameStartViewModel;

        public Append(GameStartViewModel gameStartViewModel)
        {
            this.gameStartViewModel = gameStartViewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}