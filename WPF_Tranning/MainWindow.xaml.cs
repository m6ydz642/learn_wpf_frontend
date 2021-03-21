using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace WPF_Tranning
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainView(); // 바인딩 설정 (없으면 바인딩 안먹힘)
            // GameStartViewModel 호출함

        }

        public List itemList {get;set;}
     
        private void chkSelect_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("check : " + sender);
        }


        private ObservableCollection<MainView> scores = new ObservableCollection<MainView>();

        private void valuebutton_Click(object sender, RoutedEventArgs e)
        {
            scoreListViewDB.ItemsSource = scores;
            foreach (var item in scores)
            {
                item.IsSelected = true;
            }
            
            
        }
    }
}
