using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning.ModelAndView
{
    public interface IBaseCommand : ICommand

    {

        void OnCanExecuteChanged();

    }




    class AddNumberKeyPad : ICommand

    {

        private GameStartViewModel viewModel;

        public event EventHandler CanExecuteChanged;




        public AddNumberKeyPad(GameStartViewModel viewModel)

        {
            this.viewModel = viewModel;

        }

        public bool CanExecute(object parameter)

        {

            return true;

        }

        // 1,2,,,, 숫자들을 눌렀을때 실행됨

        public void Execute(object parameter)

        {
                viewModel.InputString += parameter;

        }

    }
}
