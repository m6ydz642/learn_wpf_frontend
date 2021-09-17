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
            InitializeComponent();
            GridControlModelAndView model;
            this.DataContext = new GridControlModelAndView(this); // 바인딩 설정 (없으면 바인딩 안먹힘) 
                                                              // xaml 에서 vm 키워드로 설정해도 무방
            if (DataContext is GridControlModelAndView gridControlModelAndView)
            {
              //  gridControlModelAndView.obbutton = obbutton; // 뷰모델로 버튼객체 전달
            }
           

        }




    }
}
