using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
    public class CheckValue
    {
        public bool checkKeypadLength(string inputString) // 나중에 ModelAndView에 checkValue해서 클래스로 넣어서 호출할거임
        {
            bool check = false;
            if (inputString.Length < 6) // 공백 + 숫자 자리 = 2 * 3 = 6
            {
                check = true;
            }
            else
            {
                MessageBox.Show("3자리까지 입력가능합니다");
            }
            return check;
        }

    }


}
