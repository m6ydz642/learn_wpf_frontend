using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
    class MainView 
    {
        public ICommand TestBinding  { get; set; }

       public MainView()
        {
            TestBinding = new RelayCommand(new Action<object>(this.OnClickEvent));

        }

        public void OnClickEvent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
            MessageBox.Show("onclick 이벤트 호출");
        }

        public void test(object obj)
        {
            MessageBox.Show("test");
        }

    }
}
