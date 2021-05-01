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

            this.DataContext = new MainView(); // 바인딩 설정 (없으면 바인딩 안먹힘)
                                               // GameStartViewModel 호출함
    
        }

        private void scoreListViewD2B_Loaded(object sender, RoutedEventArgs e) 
            // 비하인드 코드 해당 그리드 컨트롤 로딩시 발생하는 이벤트 인데
            // 지금은 안씀
        {
            var h = scoreListViewD2B.GetRowHandleByListIndex(3);
            scoreListViewD2B.SelectItem(5);

 
        }


        private void GridControl_Loaded(object sender, RoutedEventArgs e)
        {

            
        }


        private void checkbox_Checked(object sender, RoutedEventArgs e)
        {

            /* var test = scorenumber.FieldName;
             MessageBox.Show("test : " + test);*/
           
            CheckBox cb = sender as CheckBox;

            allChecked.Add(cb.Content.ToString());
            MessageBox.Show("sender : " + cb.Content.ToString());

        }

       List<string> allChecked = new List<string>();
        private void valuebutton_Click(object sender, RoutedEventArgs e)
        {
          
        }

        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void columnadd2_Click(object sender, RoutedEventArgs e)
        {
         //   scoreListViewD2B.Columns.Add("컬럼");
        }

        private void TableView_ValidateRow(object sender, GridRowValidationEventArgs e)
        {
            /*    var issue = (Issue)e.Row;
                using (var context = new IssuesContext())
                {
                    var result = context.Issues.SingleOrDefault(b => b.Id == issue.Id);
                    if (result != null)
                    {
                        result.Subject = issue.Subject;
                        result.Priority = issue.Priority;
                        result.Votes = issue.Votes;
                        result.Priority = issue.Priority;
                        context.SaveChanges();
                    }
                }*/
            // https://docs.devexpress.com/WPF/401667/controls-and-libraries/data-grid/data-editing-and-validation/modify-cell-values/edit-entire-row

 
     
        }

        private void scoreListViewD2B_SelectedItemChanged(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void TableView_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            MessageBox.Show("셀값 변경됨");
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
