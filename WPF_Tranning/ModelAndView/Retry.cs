using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
    class Retry : IBaseCommand
    {
        private GameStartViewModel viewModel;

        // OnCanExecuteChanged 메소드의
        // ommandManager.InvalidateRequerySuggested()를 호출하면
        // CanExecuteChanged 이벤트가 호출되어
        // CanExecute로 해당 Command가 있는 버튼을 활성화 또는 비활성화

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

        public Retry(GameStartViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            return viewModel.StatusGateStart; // 일단 게임 초반은 false상태로 시작하기때문에 시작하자마자 재시작 버튼은 비활성화 상태임 

        }

  

        public void Execute(object parameter)
        {
         /*   if ( ) // 엔터키 눌렀을때 재시작하는걸로 변경해야 함
            {
        
 
            }
            else
            {   
                CanExecute(parameter); // 비활성화 false로 하기 위해 호출함
                // CanExcuate가 호출될때 inputString.length가 지워지고 있는 상황이라서 길이가 
                // 0이 되면 비활성화 처리 됨
            }*/

        }


        public void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

    }
}
