using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
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
    /// Interaction logic for OtherTabsView.xaml
    /// </summary>
    public partial class OtherTabsView : UserControl
    {
        public int selectindexlistbox1 { get; set; }
        List<object> SelectedItems { get; set; }
        OtherTabsVM vm;
        public OtherTabsView()
        {
            InitializeComponent();


            this.DataContext = new OtherTabsVM();
            SelectedItems = new List<object>();

            List<string> item = new List<string>();
    

        item.Add("버튼1");
            item.Add("버튼2");
            item.Add("버튼3");

               listboxedit.ItemsSource = item;

            List<string> item2 = new List<string>();
            item2.Add("멀티버튼1");
            item2.Add("멀티버튼2");
            item2.Add("멀티버튼3");

            listboxedit2.ItemsSource = item2;

            // item리스트로 안넣고 직접 xaml에서 받아와 사용하기

            vm = (OtherTabsVM)DataContext; // ViewModel 객체 가져와 쓰기



        }

        private void ListBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            listboxedit.Items.BeginUpdate();
            string button = listboxedit.EditValue.ToString();
            selectindexlistbox1 = listboxedit.SelectedIndex;
            listboxedit.Items.EndUpdate();


            var mulitpleselect2 = listboxedit2.SelectedIndex = 2; // 강제선택
            var mulitpleselect = listboxedit.SelectedIndex = 2; // 강제선택
        }

        private void listboxedit2_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            listboxedit2.Items.BeginUpdate();
            int radioitemindex = selectindexlistbox1;
            int itemindex = listboxedit2.SelectedIndex;
            string buttonvalue1 = listboxedit.EditValue.ToString();
            string buttonvalue2 = listboxedit2.EditValue.ToString();
            var objectvalue = listboxedit2.EditValue;
            var mulitpleselect = listboxedit2.EditValue;



            // 또 다른 버전 (selecteditems)
            var test = listboxedit2.SelectedItems;
            var castingobserble = (ObservableCollection<object>)test;

            for (int i = 0; i < listboxedit2.SelectedItems.Count; i++)
            {
                // var value = mulitpleselect.Select(x=>x.ToString().Equals("테스트1"));
                var value = castingobserble[i].ToString();
            }

            // 뷰 모델 값 확인
            object a = vm.ISelectedItems;
            string b = a.ToString();
            
            listboxedit2.Items.EndUpdate();
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            var ListTest = listboxedit2.EditValue;
            var listcasting = (List<object>)ListTest; // list로 형변환 ㅡㅡ; 

            List<string> selected = new List<string>();

            string list = listcasting[0].ToString();
            string list2 = listcasting[1].ToString();
            string list3 = listcasting[2].ToString();

            for (int i = 0; i < listcasting.Count; i++)
            {
                selected.Add(listcasting[i].ToString());
            }
   

        }
    }
}
