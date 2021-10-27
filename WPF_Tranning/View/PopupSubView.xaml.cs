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
using static WPF_Tranning.View.PopupMainView;

namespace WPF_Tranning.View
{
    /// <summary>
    /// PopupUserControlView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupSubView : Window
    {
        public DataGetEventHandler DataGetEvent; // 서브에서 메인에게 전달
        public DataPushEventHandler DataSetEvent; // 메인에서 서브에게 전달
        public PopUpDataSendEventHandler PopUpDataEvent;
        public PopUpDataSendEventHandler getPopupData; 
        public PopUpDataSendEventHandler PopUpDataDeleteEvent; 

        public List<string> SubMainData { get; set; }
        public List<string> TmpData { get; set; }
        public List<string> FirstData { get; set; }
        public bool datastatus { get; set; }
        public PopupSubView()
        {
            InitializeComponent();
            DataContext = new PopupSubMV();
            DataSetEvent += new DataPushEventHandler(GetMainData);
           //  SubMainData = new List<string>();
        }

        private void GetMainData(string main)
        {

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void send_Click(object sender, RoutedEventArgs e)
        {
             DataGetEvent("서브에서 메인으로 내용 전달");
        }

        private void AddData_Click(object sender, RoutedEventArgs e)
        {

            SubMainData.Add("추가데이터");
            sublstemployee.ItemsSource = null;
            sublstemployee.ItemsSource = SubMainData;
        }

        private void SendData_Click(object sender, RoutedEventArgs e)
        {
            datastatus = true;
            PopUpDataEvent(SubMainData);
        }

        public void GetSubPopupData(List<string> data)
        {

            SubMainData = data;
            sublstemployee.ItemsSource = null;
            sublstemployee.ItemsSource = SubMainData;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (!datastatus)
            {
                PopUpDataEvent(SubMainData);
            }
       

         

        }

        public void GetSubPopupDataFirst(List<string> data)
        {
            FirstData = data;
        }

        public void GetSubPopupData2(List<string> data)
        {
        }

    }
}
