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
using static WPF_Tranning.ModelAndView.PopupViewMV;

namespace WPF_Tranning.View
{
    /// <summary>
    /// PopupUserControlView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupUserControlView : UserControl
    {
        public DataGetEventHandler DataGetEvent; // 자식에서 부모창에게 전달
        public PopupUserControlView()
        {
            InitializeComponent();
            DataContext = new ClickPopupViewMV();

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
            DataGetEvent("내용 전달");
        }
    }
}
