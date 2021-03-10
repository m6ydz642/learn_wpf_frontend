using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning
{

    public class GameStartViewModel : INotifyPropertyChanged
    {
    
        public ICommand ClickKeyPad // 키패드 클릭
        {
            get; set;
        }

        //View와 바인딩된 속성값이 바뀔때 이를 WPF에게 알리기 위한 이벤트
        public event PropertyChangedEventHandler PropertyChanged;

        //출력될 문자들을 담아둘 변수

        string inputString = "";

        //계산기화면의 출력 텍스트박스에 대응되는 필드
        string displayText = "";

        public GameStartViewModel() // 기본생성자
        {

            //    this.ClickKeyPad = new RelayCommand(new Action<object>(this.KeyPadClick)); // 아직은 안쓰는데 조만간 뭐 전달한다고 쓸듯
            this.ClickKeyPad = new AddNumberKeyPad(this); // 키패드 클릭시 값 전달
                                                          //숫자 버튼을 클릭할 때 실행
                                                          // this.ClickKeyPad = new AddNumberKeyPad(this);


        }

       

        /*  public ICommand ClickCommand

          {

              get

              {

                  return this._clickCommand ??

                  (this._clickCommand = new RelayCommand(ExecuteClick, CanExecuteClick));

              }


          }
  */

        public bool checkKeypadLength(string inputString) // 나중에 ModelAndView에 checkValue해서 클래스로 넣어서 호출할거임
        {
            bool check = false;
             if(inputString.Length < 6) // 공백 + 숫자 자리 = 2 * 3 = 6
            {
                check = true;
            }
            else
            {
                MessageBox.Show("3자리까지 입력가능합니다");
            }
            return check;
        }

        public string InputString
        {
            internal set
            {
                if (inputString != value && checkKeypadLength(InputString))
                {
                    inputString = value + " ";
                    OnPropertyChanged("InputString"); // 값이 들어왔으면 PropertyChanged를 호출함

                    if (value != "")
                    {
                        DisplayText = value; // 공백추가
                    }

                }
              

            }
            get { return inputString; }

        }


        public string DisplayText

        {

            internal set

            {

                if (displayText != value)

                {

                    displayText = value;

                    OnPropertyChanged("DisplayText"); // 값이 들어왔으면 PropertyChanged를 호출함
                    // 디스플레이 표시

                }

            }

            get { return displayText; }

        }
  
      
       

     


        private void KeyPadClick(object obj) // 1~9클릭시, 이 함수 안씀 ㅋㅋ

        {
           
        }

        protected void OnPropertyChanged(string propertyName)

        {

            if (PropertyChanged != null)

            {
            //    MessageBox.Show("프로퍼티 체인지!!!");
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            }
        }


    }

    /*   private void number_click(object sender, RoutedEventArgs e) { 
       {
       // keypad.Content = ""; // 초반에 창 변경할 때 Content내용에 숫자를 입력하세요 가 있어서 초기화 해야 됨 
       // place holder처럼 할랬는데 호출할때 마다 초기화 되서 안됨 ㅠ

       int inputNumberLength = keypad.Content.ToString().Length + 2;  // 초반엔 입력 초기값이 0부터 시작해서 입력값을 미리 한개 입력해놈 (+2)로, 클릭하기 전이라 2로 초기값을 줌
                                                                      // 0 -> 4- > 6순으로 올라감 
               _clickCommand.ky();
       // 처음에 초기값으로 2받은거랑 두번째에 숫자 클릭할때 값이  2번째에 합쳐셔서 나타나기 때문임

       if (inputNumberLength <= 6)
       {
           var btn = sender as Button; // btn 전에 한번 검사
           if (btn == null) return;
           keypad.Content = (string)keypad.Content + (string)btn.Content + " "; // 여기서는 또 Content로 하네 ㅡ.ㅡ 걍 text가 편한데
       }
       else
       {
           MessageBox.Show("더 이상 수를 입력할 수 없습니다");
       }

   }*/



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



        public void Execute(object parameter)

        {
            this.execute(parameter ?? "널이얌"); // 앙 파라메터띠
        }

       
    }
}
