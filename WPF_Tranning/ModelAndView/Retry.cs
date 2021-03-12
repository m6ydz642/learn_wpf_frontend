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
            MessageBox.Show("게임을 다시시작합니다");
            viewModel._countGame = 0;
            viewModel._datatable.Rows.Clear(); // 그리드 뷰 초기화 (그리드 뷰를 초기화 하는게 아니라 그리드 뷰를
            // 보여주게 설정해주는 변수를 초기화함

        }


        public void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }

    }
}
