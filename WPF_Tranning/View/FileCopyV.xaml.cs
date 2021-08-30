using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for FileCopyV.xaml
    /// </summary>
    public partial class FileCopyV : UserControl
    {
        public int selectindexlistbox1 { get; set; }
        public FileCopyV()
        {
            InitializeComponent();
            this.DataContext = new FileCopyVM();
            List<string> item = new List<string>();
            item.Add("버튼1");
            item.Add("버튼2");
            item.Add("버튼3");

         //   listboxedit.ItemsSource = item;
         // item리스트로 안넣고 직접 xaml에서 받아와 사용하기

        }

        private void ListBoxEdit_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            listboxedit.Items.BeginUpdate();
            string button = listboxedit.EditValue.ToString();
            selectindexlistbox1 = listboxedit.SelectedIndex;
            listboxedit.Items.EndUpdate();

            var mulitpleselect = listboxedit2.SelectedIndex = 2; // 강제선택
        }

        private void listboxedit2_SelectedIndexChanged(object sender, RoutedEventArgs e)
        {
            listboxedit2.Items.BeginUpdate();
            int radioitemindex = selectindexlistbox1;
            int itemindex = listboxedit2.SelectedIndex;
            string buttonvalue1 = listboxedit.EditValue.ToString();
            string buttonvalue2 = listboxedit2.EditValue.ToString();
            var objectvalue = listboxedit2.EditValue;
            var mulitpleselect = listboxedit2.SelectedItems ;


            for (int i = 0; i < mulitpleselect.Count; i++)
            {
                var value = mulitpleselect.Select(x=>x.ToString().Equals("테스트1"));
               
            }
            
            listboxedit2.Items.EndUpdate();
        }
    }
}
