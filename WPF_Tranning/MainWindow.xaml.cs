using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

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
    }
}
