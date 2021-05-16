using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
    public class RelayCommand : ICommand

    {

        private Action<object> execute;

        private Func<object, bool> canExecute;



        public event EventHandler CanExecuteChanged

        {

            add { CommandManager.RequerySuggested += value; }

            remove { CommandManager.RequerySuggested -= value; }

        }



        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)

        {

            this.execute = execute;

            this.canExecute = canExecute;

        }



        public bool CanExecute(object parameter)

        {

            return this.canExecute == null || this.canExecute(parameter);


        }



        public void Execute(object parameter) // RelayCommand 객체생성될때 같이 호출됨

        {
            this.execute(parameter ?? "널"); // 앙 파라메터띠
        }


		// 이벤트 받을때 쓰는 클래스
		public class RelayCommandEvent<T, V> : ICommand
		{
			private Action<T> execute;
			private Action<T, V> execute2;



			private Func<T, bool> canExecute;

			private Action<object, CellValueChangedEventArgs> action;

			public event EventHandler CanExecuteChanged;

			public RelayCommandEvent(Action<T, V> execute2, Func<T, bool> canExecute = null)
			{
				this.execute2 = execute2;

				this.canExecute = canExecute;
			}



			// 제레닉 타입으로 변경
			public bool CanExecute(T parameter)
			{
				return this.canExecute == null || this.canExecute(parameter);
			}

			public void Execute(T parameter)
			{
				this.Execute(parameter);
			}

			/*******************************************************************/
			// 인터페이스 구현할때 원래 상속받아야 하는 메서드
			void ICommand.Execute(object parameter)
			{
				this.execute((T)parameter);
			}

			bool ICommand.CanExecute(object parameter)
			{
				return this.canExecute == null || this.canExecute((T)parameter);
			}
			/*******************************************************************/
		}

	}
}