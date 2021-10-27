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

namespace WPF_Tranning.View
{
    /// <summary>
    /// ChartBindingView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PopupMainView : UserControl
    {
        public delegate void DataPushEventHandler(string main);  // 메인폼 --> 서브폼으로 값 전달 델리게이트
        public delegate void DataGetEventHandler(string sub); // 서브폼 --> 메인폼으로 값 전달 델리게이트
        public delegate void PopUpDataSendEventHandler(List<string> data); // 서브폼 --> 메인폼으로 값 전달 델리게이트
        public PopUpDataSendEventHandler FirstDataEvent;

        PopupSubMV PopupSubMv;
        public List<string> MainData { get; set; }
        public List<string> FirstData { get; set; }
        public PopupMainView()
        {
            InitializeComponent();
            DataContext = new PopupMainMV();

            // local로 같은 페이지에 포함 시켰을때 동일한 new 생성 시점으로 호출되어 값 전달 SubViewModel->PopupView.xaml 비하인드로 전달 잘됨
            /*var test = (PopupSubMV)local.DataContext;
            test.DataSetVM_Main += new DataGetEventHandler(this.GetViewModelSubPopupData);*/ // 서브뷰모델 에서 비하인드 메인으로 받음
            MainData = new List<string>();
            MainData.Add("데이터1");
            lstemployee.ItemsSource = MainData;
            FirstData = MainData;
        }
        /*        private void GetSubPopupData(string sub)
                {
                }

                private void GetViewModelSubPopupData(string sub)
                {

                }*/

        private void GetSubPopupData2(List<string> data)
        {
            MainData = data;
            lstemployee.ItemsSource = null;
            lstemployee.ItemsSource = MainData;
        }
        private void ButtonEdit_DefaultButtonClick(object sender, RoutedEventArgs e)
        {
         //   Window window = new Window();
            PopupSubView popupSubView = new PopupSubView();
   /*         popupSubView.DataGetEvent += new DataGetEventHandler(this.GetSubPopupData); // 서브에서 메인으로 받음
            // popupSubView.DataSetEvent += new DataPushEventHandler(this.SetMainPopupData); // 메인에서 서브로 전달
            popupSubView.DataSetEvent("메인에서 서브로 전달"); // 위에꺼 굳이 할 필요없음 (팝업 SubView에서 New EventHandler해서
            popupSubView.DataSetEvent("메인에서 서브로 전달"); // 위에꺼 굳이 할 필요없음 (팝업 SubView에서 New EventHandler해서*/
            popupSubView.PopUpDataEvent += new PopUpDataSendEventHandler(popupSubView.GetSubPopupData);
            /*  popupSubView.getPopupData += new PopUpDataSendEventHandler(GetSubPopupData2);
              FirstDataEvent += new PopUpDataSendEventHandler(popupSubView.GetSubPopupDataFirst);

              FirstDataEvent(FirstData);*/
            popupSubView.PopUpDataEvent(MainData);
            popupSubView.PopUpDataEvent -= new PopUpDataSendEventHandler(popupSubView.GetSubPopupData2);

            popupSubView.Show();

            /* var content = ((FrameworkElement)myFrame.Content);
             var PopupSubMvdataContext = (PopupSubMV)content.DataContext;*/

            // 얘들은 new 호출 시점이 달라 안됨 (local로 한번 new, 팝업 띄울때 한번 new 해서 2번 됨)
            /* var test = (PopupSubMV)local.DataContext;
             test.DataSetVM_Main += new DataGetEventHandler(this.GetViewModelSubPopupData); // 서브뷰모델 에서 비하인드 메인으로 받음*/

            /*  window.Content = popupSubView;
               window.SizeToContent = SizeToContent.WidthAndHeight;
              window.Show();*/

            /*       Window window1 = new Window(); // othertabview도 
                   OtherTabsView otherTabsView = new OtherTabsView();
                   window1.Content = otherTabsView;
                   window1.SizeToContent = SizeToContent.WidthAndHeight;
                   window1.ResizeMode = ResizeMode.CanResize;
                   window1.Show();*/
        }

        private void GetSubPopupData(List<string> data)
        {
            MainData = data;
        }

        private void GetSubPopupDataFirst(List<string> data)
        {

        }
    }
}
