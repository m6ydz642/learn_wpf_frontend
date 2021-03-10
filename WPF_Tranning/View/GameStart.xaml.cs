using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Tranning
{
    /// <summary>
    /// GameStart.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GameStart : Page
    {
        public GameStart()
        {
            InitializeComponent();
        }
        
     /*     private void number_click(object sender, RoutedEventArgs e)
        {
            // keypad.Content = ""; // 초반에 창 변경할 때 Content내용에 숫자를 입력하세요 가 있어서 초기화 해야 됨 
            // place holder처럼 할랬는데 호출할때 마다 초기화 되서 안됨 ㅠ

            int inputNumberLength = keypad.Content.ToString().Length + 2 ;  // 초반엔 입력 초기값이 0부터 시작해서 입력값을 미리 한개 입력해놈 (+2)로, 클릭하기 전이라 2로 초기값을 줌
            // 0 -> 4- > 6순으로 올라감 
            
            // 처음에 초기값으로 2받은거랑 두번째에 숫자 클릭할때 값이  2번째에 합쳐셔서 나타나기 때문임

            if (inputNumberLength <= 6)
            {
                var btn = sender as Button; // btn 전에 한번 검사
            if (btn == null) return;
            keypad.Content = (string)keypad.Content + (string) btn.Content + " "; // 여기서는 또 Content로 하네 ㅡ.ㅡ 걍 text가 편한데
            }
            else
            {
                MessageBox.Show("더 이상 수를 입력할 수 없습니다");
            }
        }
        */
    }
}
