using ClosedXML.Excel;

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;
using DevExpress.Utils;
using DevExpress.XtraEditors;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.WindowsUI;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Media.Imaging;
using WPF_Tranning.View;
using System.Windows.Controls;
using System.Collections.Generic;
using System.Reflection;

namespace WPF_Tranning
{


    class MainModelAndView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GridControlMenu { get; set; }
        public ICommand StartPage { get; set; }
        public ICommand ChartBindingMenu { get; set; }
        public ICommand PivotGridControl { get; set; }
        public ICommand SearchScoreMenu { get; set; }
        public ICommand GridControlBandMenu { get; set; }
        public ICommand GridControlBandMenuTree { get; set; }
        public ICommand EndPage { get; set; }
        
      
        

        public string Help { get; set; }
        private string _path { get; set; }
        private string _unloadClass { get; set; }
        public GridControlBandView AutoMenu { get; set; }
        public MainModelAndView()
        {

            _path = "WPF_Tranning.GridControlBandViews"; // 지정된 텍스트 파일을 읽어와 처리하도록 변경예정
            GridControlMenu = new RelayCommand(new Action<object>(this.GridControlMenuUri));
            StartPage = new RelayCommand(new Action<object>(this.LoadingStartPage));
            ChartBindingMenu = new RelayCommand(new Action<object>(this.LoadingChartBinding));
            PivotGridControl = new RelayCommand(new Action<object>(this.LoadingPivotGridControl));
            SearchScoreMenu = new RelayCommand(new Action<object>(this.SearchScoreMenuBinding));
            GridControlBandMenu = new RelayCommand(new Action<object>(this.GridControlBandMenuBinding));
            GridControlBandMenuTree = new RelayCommand(new Action<object>(this.GridControlBandMenuTreeBinding));
            EndPage = new RelayCommand(new Action<object>(this.EndPageEvent));
        }
  
        public void EndPageEvent(object obj)
        {
           
        }

        private void LoadingStartPage(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new GridControlView(); // 메인으로 지정할 페이지
            convert.Source = GetInstance(_path); // 마지막 값 넣기 
            /*  
             * 다이렉트로 바로 쓸경우  
             *string path = "WPF_Tranning.View.PivotGridControlView";
              Type type = Type.GetType(path);
              object list = Activator.CreateInstance(type);*/


            // UserControl만 이 방식으로 됨 ㅡ.ㅡ
            // page, window 안됨
        }


     
        private void LoadingPivotGridControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new PivotGridControlView();
        }

        private void LoadingChartBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new ChartBindingView();
        }

        public object GetInstance(string classname)
        {
            Type type = Type.GetType(classname);
            if (type == null) { // null처리, 강제로 클래스명 바뀐경우 (혹시나)
                type = Type.GetType("WPF_Tranning.View.GridCotrolBandModelAndView");
            }
            return Activator.CreateInstance(type);
        }
      
        private void GridControlMenuUri(object obj) // 유저컨트롤을 사용하면 이방식으로 바인딩 까지 같이 됨
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new Uri("../View/GridControlView.xaml", UriKind.RelativeOrAbsolute); // uri로 페이지 이동
            string test = convert.Source.ToString();

        }

        private void SearchScoreMenuBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new SearchScoreView();
        }

        private void GridControlBandMenuTreeBinding(object obj)
        {
            var convert = (TreeViewItem)obj;
            string header = convert.Header.ToString(); // 헤더 가져옴
        }
        private void GridControlBandMenuBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new GridControlBandView();
            // convert.Source = AutoMenu;
            string test = convert.ToString();
        }


    }
}
