using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPF_Tranning.View
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 
   
    public partial class GridControlView : UserControl
    {

        public GridControlView()
        {
            // CallAsyncUI();
            /************************************************************************************/
            // 여러가지 호출방식
            Dispatcher.BeginInvoke(new Action(() => InitializeComponent()));
            Window parentWindow = Application.Current.MainWindow;
            var etest = parentWindow.FindName("Test");
            ColorEditView colorEditView = new ColorEditView();
            //     ((this.Parent) as UserControl).Content = this;

            // InitializeComponent();
            // UI를 그리기전에 obbutton 같이 버튼에 값을 대입하려고 하면 불가 (랜더링이 다 안됐기 때문) 
            /************************************************************************************/

            // GridControlModelAndView model;
            //  this.DataContext = new GridControlModelAndView(this); // 바인딩 설정 (없으면 바인딩 안먹힘) 
            this.DataContext = new GridControlModelAndView(); // 바인딩 설정 (없으면 바인딩 안먹힘) 
            // xaml 에서 DataContext키워드로 설정해도 무방
            if (DataContext is GridControlModelAndView gridControlModelAndView)
            {
                //   gridControlModelAndView.obbutton = obbutton; // 뷰모델로 버튼객체 전달
                gridControlModelAndView.Loading();
            }


        }

        async private void CallAsyncUI()
        {
            await Task.Run(() => {

                InitializeComponent();

            });
        }






    }
}
