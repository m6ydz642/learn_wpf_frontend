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
using System.IO;
using System.Text;
using AnotherPageProject.View;


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
        public ICommand AnotherPage { get; set; }
        public ICommand ISpreadSheetControl { get; set; }
        
      
        

        public string Help { get; set; }
        private string _classpath { get; set; }
        private string _unloadClass { get; set; }
        public GridControlBandView AutoMenu { get; set; }
        public MainModelAndView()
        {

            LoadTextFile();
            GridControlMenu = new RelayCommand(new Action<object>(this.GridControlMenuUri));
            StartPage = new RelayCommand(new Action<object>(this.LoadingStartPage));
            ChartBindingMenu = new RelayCommand(new Action<object>(this.LoadingChartBinding));
            PivotGridControl = new RelayCommand(new Action<object>(this.LoadingPivotGridControl));
            SearchScoreMenu = new RelayCommand(new Action<object>(this.SearchScoreMenuBinding));
            GridControlBandMenu = new RelayCommand(new Action<object>(this.GridControlBandMenuBinding));
            GridControlBandMenuTree = new RelayCommand(new Action<object>(this.GridControlBandMenuTreeBinding));
            EndPage = new RelayCommand(new Action<object>(this.EndPageEvent));
            AnotherPage = new RelayCommand(new Action<object>(this.AnotherPageEvent));
            ISpreadSheetControl = new RelayCommand(new Action<object>(this.SpreadSheetControl));
        }

  
        private void AnotherPageEvent(object obj)
        {
            var convert = (NavigationFrame)obj;
           // convert.Source = new AnotherPage(DataModel.CurrentClassPath);
            convert.Source = new AnotherPage();
        }

        public void EndPageEvent(object obj)
        {

        }
        private bool FileNotFound(ref string strFilePath, ref string strErr)
        {
            string TextValue = null;
            try
            {
                string CurrentDirectory = Directory.GetCurrentDirectory(); // 개발자 디버그 경로
                TextValue = System.IO.File.ReadAllText(CurrentDirectory + "\\SaveLastMenu.txt");

            }
            catch (System.IO.FileNotFoundException ex)
            {
                strErr = ex.Message.ToString(); // 예외 걸려서 메시지 들어오는 부분을 호출되는 매개변수 ref가 참조함
                return false;
            }
            strFilePath = TextValue;
            return true; // 예외에 해당하지 않으면
        }



        private void LoadTextFile()
        {

         
            string strErr = "";
            string TextValue = "";
            string CurrentDirectory = Directory.GetCurrentDirectory(); // 개발자 디버그 경로

            bool status = FileNotFound(ref TextValue, ref strErr);

            if (status) // 파일을 찾을 수 없으면 씀
            {
                TextValue = System.IO.File.ReadAllText(CurrentDirectory + "\\SaveLastMenu.txt");
            }

            else
            {
                string FileName = "\\SaveLastMenu.txt";
                // System.IO.File.WriteAllText(CurrentDirectory, textValue, Encoding.Default); // 권한문제 있어서 못씀
                StreamWriter writer;
                writer = File.CreateText(CurrentDirectory + FileName);        //Text File이 저장될 위치(파일명
            }
            _classpath = TextValue;

        }
        private void LoadingStartPage(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new GridControlView(); // 메인으로 지정할 페이지

    
            convert.Source = GetInstance(_classpath); // 텍스트로 불러와 마지막 실행 메뉴 저장 
            /*  
             다이렉트로 바로 쓸경우  
             string path = "WPF_Tranning.View.PivotGridControlView";
             Type type = Type.GetType(path);
             object list = Activator.CreateInstance(type);
            */


            // UserControl만 이 방식으로 됨 ㅡ.ㅡ
            // page, window 안됨
        }


        private void SpreadSheetControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            convert.Source = new SpreadsheetControlView();
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
            if (type == null) { 
                // null처리, 파일의 클래스가 강제로 바뀌거나, 파일을 읽을 수 없는 경우, 강제로 수정된 경우 포함
                type = Type.GetType("WPF_Tranning.GridControlBandView"); // 강제로 설정
                MessageBox.Show(type + " 의 인스턴스가 발견되지 않아 GridControlBandView로 새로 생성합니다");
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
        }


    }
}
