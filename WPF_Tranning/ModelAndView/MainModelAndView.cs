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

namespace WPF_Tranning
{


    class MainModelAndView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GridControlMenu { get; set; }
        public ICommand StartPage { get; set; }
        public ICommand ChartBindingMenu { get; set; }
        
      
        

        public string Help { get; set; }

        public MainModelAndView()
        {
            GridControlMenu = new RelayCommand(new Action<object>(this.GridControlMenuUri));
            StartPage = new RelayCommand(new Action<object>(this.LoadingStartPage));
            ChartBindingMenu = new RelayCommand(new Action<object>(this.LoadingChartBinding));
        }

        private void LoadingChartBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new ChartBindingView();
        }

        private void LoadingStartPage(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new GridControlView();
            // UserControl만 이 방식으로 됨 ㅡ.ㅡ
            // page, window 안됨
        }

        private void GridControlMenuUri(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new Uri("../View/GridControlView.xaml", UriKind.RelativeOrAbsolute); // uri로 페이지 이동
            string test = convert.Source.ToString();

        }
    }
}
