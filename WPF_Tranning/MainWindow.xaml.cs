using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WPF_Tranning.Model;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
   
    public partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainModelAndView(); // 바인딩 설정 (없으면 바인딩 안먹힘)

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
          //  this.MyUserControl.MethodToBeCalledWhenUnloaded().
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            string path = DataModel.CurrentClassPath; // 이제 text로저장하든 db로 내보내던 하면 됨
        }
    }
}
