using DevExpress.Mvvm;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
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


    public abstract class WinFormsApp
    {
        const string WinformsJITPrecompilerThreadName = "Framework.WinForms JIT Precompiler";

        protected WinFormsApp()
        {
            CreateJITPrecompilerThread().Start();
        }

        protected virtual void BackgroundThreadInitialize()
        { }

        Thread CreateJITPrecompilerThread()
        {
            var result = new Thread(BackgroundThreadInitialize)
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true,
                Name = WinformsJITPrecompilerThreadName
            };

            result.SetApartmentState(ApartmentState.STA);

            return result;
        }

        public static bool IsBackgroundInitializeThread()
        {
            return Thread.CurrentThread.Name == WinformsJITPrecompilerThreadName;
        }
    }
    public class MyApp : WinFormsApp
    {
        public MyApp() : base()
        { }

        public int Run()
        {
            // Do whatever here to really start your application (whatever is in Program.Main() now)  
            return 0;
        }

        protected override void BackgroundThreadInitialize()
        {
            new OtherTabsView();
            //... Repeat for any complex controls  
        }
    }


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

            Thread thread = Thread.CurrentThread; this.DataContext = new { ThreadId = thread.ManagedThreadId };
           // CallThread();

            InitializeComponent();


            bool test = WinFormsApp.IsBackgroundInitializeThread();
            if (test) return;

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

        private void CallThread()
        {
            Thread thread = new Thread(() => { MainWindow window = new MainWindow(); window.Closed += (sender2, e2) => window.Dispatcher.InvokeShutdown(); window.Show(); System.Windows.Threading.Dispatcher.Run(); }); thread.SetApartmentState(ApartmentState.STA); thread.Start();

        }
            private void Timer_Tick(object sender, EventArgs e)
        {
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
    }
}
