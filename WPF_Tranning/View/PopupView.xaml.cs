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
    public partial class PopupView : UserControl
    {
        public PopupView()
        {
            InitializeComponent();
            DataContext = new PopupViewMV();

        }

        private void ButtonEdit_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
            Window window = new Window();
            PopupUserControlView popupUserControlView = new PopupUserControlView();
            //   this.DataSendEvent += new DataPushEventHandler(popupUserControlView.SetActionValue1);
            // 자식에서 부모에게만 전달하는걸로 변경
            window.Content = popupUserControlView;
            window.Show();
        }
    }
}
