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
using static WPF_Tranning.View.PopupMainView;

namespace WPF_Tranning.View
{
    /// <summary>
    /// PopupUserControlView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupSubView : UserControl
    {
        public DataGetEventHandler DataGetEvent; // 서브에서 메인에게 전달
        public DataPushEventHandler DataSetEvent; // 메인에서 서브에게 전달
        public PopupSubView()
        {
            InitializeComponent();
            DataContext = new PopupSubMV();
            DataSetEvent += new DataPushEventHandler(GetMainData);
        }

        private void GetMainData(string main)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
         DataGetEvent("서브에서 메인으로 내용 전달");
        }
    }
}
