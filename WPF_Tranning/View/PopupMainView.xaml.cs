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
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning.View
{
    /// <summary>
    /// ChartBindingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupMainView : UserControl
    {
        public delegate void DataPushEventHandler(string main);  // 메인폼 --> 서브폼으로 값 전달 델리게이트
        public delegate void DataGetEventHandler(string sub); // 서브폼 --> 메인폼으로 값 전달 델리게이트

       // public DataPushEventHandler DataSetEvent; // 메인에서 서브에게 전달

        public PopupMainView()
        {
            InitializeComponent();
            DataContext = new PopupMainMV();
           
        }
        private void GetSubPopupData(string sub)
        {
        }


        private void ButtonEdit_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            PopupSubView popupSubView = new PopupSubView();
            popupSubView.DataGetEvent += new DataGetEventHandler(this.GetSubPopupData); // 서브에서 메인으로 받음
            // popupSubView.DataSetEvent += new DataPushEventHandler(this.SetMainPopupData); // 메인에서 서브로 전달
            popupSubView.DataSetEvent("메인에서 서브로 전달"); // 위에꺼 굳이 할 필요없음 (팝업 SubView에서 New EventHandler해서
            window.Content = popupSubView;
            window.Show();
        }

     
    }
}
