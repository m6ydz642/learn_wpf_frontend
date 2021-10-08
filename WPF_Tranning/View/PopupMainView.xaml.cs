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
        public delegate void DataPushEventHandler(string value);  // 메인폼 --> 자식폼 으로 값 전달 델리게이트
        public delegate void DataGetEventHandler(string item); // 자식폼 --> 메인폼으로 값 전달 델리게이트


        public PopupMainView()
        {
            InitializeComponent();
            DataContext = new PopupMainMV();
           
        }
        private void GetSubPopupData(string item)
        {

        }
        private void ButtonEdit_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            PopupSubView popupSubView = new PopupSubView();
            popupSubView.DataGetEvent = new DataGetEventHandler(this.GetSubPopupData);
            window.Content = popupSubView;
            window.Show();
        }
    }
}
