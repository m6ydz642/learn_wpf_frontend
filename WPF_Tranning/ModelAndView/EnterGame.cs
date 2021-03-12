using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
    public class EnterGame : IBaseCommand
    {
        public int _countGame;

  

        private GameStartViewModel viewModel;

        // CheckValue checkInput = new CheckValue();
        GameStart game = new GameStart();

        public EnterGame()
        {
            _countGame = 0;
           
        }

        public EnterGame(GameStartViewModel viewModel)
        {
            this.viewModel = viewModel;
        }


        //   public CheckValue checkvalue;



        /*   public EnterGame(CheckValue checkvalue)
           // EnterGame의 기본생성자에서 생성한 EnterGame 객체를
           // CheckValue생성자로 가져옴

           {
               this.checkvalue = checkvalue;
           }*/




        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested += value; }
        }

    
        public bool CanExecute(object parameter) // 0이면 false해서 비활성화 처리
        {
 
            return viewModel.InputString.Length >= 3;
        }
        

        public void Execute(object parameter)
        {

            string value = viewModel.InputString; // 그냥 입력받았던거 그대로 컬럼명에 보여주는 단순한 표시용도임 실제 계산은 프로그램 내에서 할거


            // checkvalue.inputNumber(value); // 입력받은 값을 배열로 만들어서 가지고 나감
            // 객체생성 생성자에서 만들어 와서 해보려고 했는데 오류나서 일단 객체 생성해봄

            inputNumber(value); // 입력받은 값을 배열로 만들어서 가지고 나감

            viewModel._datatable.Rows.Add(++_countGame, value, calcScore());
            viewModel.InputString = ""; // 엔터치면 초기화 함

            calcScore();
          
        }


        public void OnCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }



        public int[] _inputNumberSave { get; set; }

        public int[] _SaveScore { get; set; }


        public int[] inputNumber(string value)
        { // 사용자 입력값
            char[] inputNumberString = value.ToCharArray();



            _inputNumberSave = new int[3]; // new 연산자로 영역 생성안하니까 null뜨면서 안들


            _inputNumberSave[0] = inputNumberString[0] - 48;  // char[] 타입을 int [] 타입으로
                                                              // 이거 어이없는게 0번(48) 아스키코드를 빼면 현재 숫자가 나옴
                                                              // 아스키코드로 49는 1인데 이걸 변환하려면 -48을 하면 1이나옴 ㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋㅋ


            _inputNumberSave[1] = inputNumberString[1] - 48;
            _inputNumberSave[2] = inputNumberString[2] - 48;

            // setter에 동시에 넣으면서
            return _inputNumberSave; // setter값을 리턴함 비교는 컴퓨터가 만든 랜덤값과 할거임
        }




        public string calcScore() // 게임 점수계산 함수
        {
            int Strike = 0;// 게임시작시 사용자에게 한 회차끝나고 보여지는 점수 
            int Ball = 0; // 게임시작시 사용자에게 한 회차끝나고 보여지는 점수 
            string Message = "";
            //   inputNumber = _inputNumberSave;
            int[] RandNum = game._randomNumberArray;

            for (int i = 0; i < _inputNumberSave.Length; i++)
            {
                //    Console.WriteLine("calcScore - saveScore내용 : " + RandNum[i]);
                if (RandNum[i] == _inputNumberSave[i])
                {
                    Strike++; // 사용자에게 보여지는 점수판
                }

                for (int j = 0; j < _inputNumberSave.Length; j++)
                {
                    if (RandNum[i] == _inputNumberSave[j] && i != j)
                    {
 
                        Ball++;
                    }
                }
                Message = Strike + "스트라이크 " + Ball + "볼";
            }


            return Message;
        }

    }
}
