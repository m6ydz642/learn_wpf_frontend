using DevExpress.Mvvm;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading;
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
using System.Windows.Threading;
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
            // new MyApp().Run();
            /*    DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(10);
                timer.Tick += Timer_Tick;
                timer.Start();*/

            // CallThread();

            ProfileOptimization.SetProfileRoot(Environment.CurrentDirectory);

            ProfileOptimization.StartProfile("App.JIT.Profile");

            Dispatcher.BeginInvoke(new Action(() => InitializeComponent()));




            this.DataContext = new OtherTabsVM();
            SelectedItems = new List<object>();

            ObservableCollection<string> item = new ObservableCollection<string>();


            item.Add("버튼1");
            item.Add("버튼2");
            item.Add("버튼3");

        //    listboxedit.ItemsSource = item;

            ObservableCollection<string> item2 = new ObservableCollection<string>();
            item2.Add("수신" + "/" + "1234@naver.com" + "/" + "네이버");
            item2.Add("참조" + "/" + "12345@kakao.com" + "/" + "카카오");
            item2.Add("수신" + "/" + "123478@naver.com" + "/" + "네이버");


        //    listboxedit2.ItemsSource = item2;

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
            var ListTest2 = listboxedit2.EditValue;
            var ListTest = listboxedit.EditValue;

            //    var listcasting = (List<object>)ListTest; // list로 형변환 ㅡㅡ;
            var list2casting = (List<object>)ListTest2;

            //List<string> selected = new List<string>();

            // 값보기용
            /*string list = listcasting[0].ToString();
            string list2 = listcasting[1].ToString();
            string list3 = listcasting[2].ToString();*/



            /*      for (int i = 0; i < list2casting.Count; i++)
                  {
                      selected.Add(list2casting[i].ToString());
                  }*/
            System.Windows.Forms.BindingSource bs = new System.Windows.Forms.BindingSource();

            for (int j = 0; j < list2casting.Count; j++)
            {
                string[] split = list2casting[j].ToString().Split('/');

                string alldata = list2casting[j].ToString();
                string SelectType = "비밀참조"; // radio box로 선택했다 치고
                string replacelist = SelectType + "/" + split[1] + "/" + split[2];

                list2casting[j] = list2casting[j].ToString().Replace(alldata, replacelist);
                listboxedit2.Items.BeginUpdate();

                // 변환안하고 다이렉트로 사용
                // string receiveDirectData = listboxedit2.Items[j].ToString();
                //  listboxedit2.ItemsSource = listboxedit2.Items[j].ToString().Replace(receiveDirectData, list);
                listboxedit2.ItemsSource = null;
                listboxedit2.ItemsSource = list2casting;


                // listboxedit.ItemsSource = listboxedit.Items.ToString().Replace("버튼3", "테스트변경");
                //listboxedit.ItemsSource = listboxedit.Items[0];


            }

        }

        private void DXTabItem_Loaded(object sender, RoutedEventArgs e)
        {
            var manager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                IsIndeterminate = false
            });
            manager.Show();
            manager.ViewModel.Progress = 100;

            SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                Copyright = "All rights reserved",
                IsIndeterminate = true,
                Status = "RichEditControl Loading...",
                Title = "",
                Subtitle = "WPF Tranning Project"
            }
            ).ShowOnStartup();

            manager.Close();
        }

        private void DXTabItem_Loaded_1(object sender, RoutedEventArgs e)
        {


            richEditControl1.BeginInvoke(new Action(() =>
            {
                if (richEditControl1.Document.Fields.Count != 0)
                {
                    richEditControl1.Document.Fields.Update();
                }
            }));

            Task.Run(async () =>
            {
                await GetAllProductsAsync();
            });
        }

        private async Task GetAllProductsAsync()
        {

        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {


        }
    }
}
