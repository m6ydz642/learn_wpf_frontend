using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
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

        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {

            /* var test = scorenumber.FieldName;
             MessageBox.Show("test : " + test);*/
           
            CheckBox cb = sender as CheckBox;

            allChecked.Add(cb.Content.ToString());
            MessageBox.Show("sender : " + cb.Content.ToString());

            /* List<CheckBox> checkBoxlist = new List<CheckBox>();
             foreach (CheckBox c in checkBoxlist)
             {
                 if (c.IsChecked == true)
                 {
                     //Code when checkbox is checked
                     var _tempTBL = (TextBlock)c.Content; //Get handle to TextBlock
                     var foo = _tempTBL.Text; //Read TextBlock's text
                                              //foo is now a string of the checkbox's content
                     MessageBox.Show("foo : " + foo);
                 }
                 else
                 {
                     MessageBox.Show("체크된 값 없음");
                 }
             }*/



        }
        List<string> allChecked = new List<string>();
        private void valuebutton_Click(object sender, RoutedEventArgs e)
        {
          
        }



        /*  private ObservableCollection<MainView> scores = new ObservableCollection<MainView>(); // 새로고침 컬 렉션?
          private void chkSelect_Click(object sender, RoutedEventArgs e)
          {

              scoreListViewDB.ItemsSource = scores;
              foreach (var item in scores)
              {
                  item.IsSelected = true;
              }

          }*/

        /* private void checked_it(object sender, RoutedEventArgs e)
         {
             List<CheckBox> checkBoxlist = new List<CheckBox>();
             foreach (CheckBox c in checkBoxlist)
             {
                 if(c.IsChecked == true)
                 {
                     //Code when checkbox is checked
                     var _tempTBL = (TextBlock)c.Content; //Get handle to TextBlock
                     var foo = _tempTBL.Text; //Read TextBlock's text
                                              //foo is now a string of the checkbox's content
                 }

             }

         }*/



    }
}
