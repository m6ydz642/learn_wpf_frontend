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
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning
{


    public class MainModelAndView : INotifyPropertyChanged
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
        public ICommand IOtherTabsViewControl { get; set; }
        public ICommand IPopupControl { get; set; }
        public ICommand IMultibindingView { get; set; }
        public ICommand ITwoGridControlView { get; set; }
        public ICommand IMasterDetail { get; set; }
        public ICommand IGridControlCombobox { get; set; }




        public string Help { get; set; }
        private string _classpath { get; set; }
        private string _unloadClass { get; set; }
        private string _getNameSpace { get; set; }

        private string _loadingSelectPage { get; set; }
        public GridControlBandView AutoMenu { get; set; }
        public MainModelAndView()
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
                Status = "WPF Loading...",
                Title = "",
                Subtitle = "WPF Tranning Project"
            }
            ).ShowOnStartup();

            LoadTextFile();
            GridControlMenu = new RelayCommand(new Action<object>(this.GridControlMenuUri));
            StartPage = new RelayCommand(new Action<object>(this.LoadingStartPage));
            ChartBindingMenu = new RelayCommand(new Action<object>(this.LoadingChartBinding));
            PivotGridControl = new RelayCommand(new Action<object>(this.LoadingPivotGridControl));
            SearchScoreMenu = new RelayCommand(new Action<object>(this.SearchScoreMenuBinding));
            GridControlBandMenu = new RelayCommand(new Action<object>(this.GridControlBandMenuBinding));
            GridControlBandMenuTree = new RelayCommand(new Action<object>(this.GridControlBandMenuTreeBinding));
            EndPage = new RelayCommand(new Action<object>(this.EndPageEvent));
            AnotherPage = new RelayCommand(new Action<object>(this.LoadedAnotherPage));
            ISpreadSheetControl = new RelayCommand(new Action<object>(this.SpreadSheetControl));
            IOtherTabsViewControl = new RelayCommand(new Action<object>(this.OtherTabsViewControl));
            IPopupControl = new RelayCommand(new Action<object>(this.PopupControl));
            IMultibindingView = new RelayCommand(new Action<object>(this.MultibindingView));
            ITwoGridControlView = new RelayCommand(new Action<object>(this.TwoGridControlView));
            IMasterDetail = new RelayCommand(new Action<object>(this.MasterDetailView));
            IGridControlCombobox = new RelayCommand(new Action<object>(this.GridControlComboboxView));

            _getNameSpace = "WPF_Tranning.View";
            _loadingSelectPage = "LoadingSelectPage";


            manager.Close();
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
            string checkPage = "LoadingStartPage";
            convert.Source = CreateInstance(_classpath, checkPage); // 텍스트로 불러와 마지막 실행 메뉴 저장 
            /*  
             다이렉트로 바로 쓸경우  
             string path = "WPF_Tranning.View.PivotGridControlView";
             Type type = Type.GetType(path);
             object list = Activator.CreateInstance(type);
            */


            // UserControl만 이 방식으로 됨 ㅡ.ㅡ
            // page, window 안됨
        }

        private void OtherTabsViewControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new OtherTabsView();
            object getInstance = CreateInstance(_getNameSpace + "." + "OtherTabsView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }

     
        private void SpreadSheetControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            //  convert.Source = new SpreadsheetControlView();
            object getInstance = CreateInstance(_getNameSpace + "." + "SpreadsheetControlView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }

        private void LoadingPivotGridControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new PivotGridControlView();
            object getInstance = CreateInstance(_getNameSpace + "." + "PivotGridControlView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }

        private void LoadingChartBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new ChartBindingView();
            object getInstance = CreateInstance(_getNameSpace + "." + "ChartBindingView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }

     
      
        private void GridControlMenuUri(object obj) // 유저컨트롤을 사용하면 이방식으로 바인딩 까지 같이 됨
        {
            // 다른 방식
            //var convert = (NavigationFrame)obj;
            //convert.Source = new Uri("../View/GridControlView.xaml", UriKind.RelativeOrAbsolute); // uri로 페이지 이동

            var convert = (NavigationFrame)obj;
            // convert.Source = new GridControlView();
            object getInstance = CreateInstance(_getNameSpace + "." + "GridControlView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;

        }

        private void SearchScoreMenuBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new SearchScoreView();
            object getInstance = CreateInstance(_getNameSpace + "." + "SearchScoreView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }

 
        private void GridControlBandMenuBinding(object obj)
        {
            var convert = (NavigationFrame)obj;
            // convert.Source = new GridControlBandView();
            object getInstance = CreateInstance(_getNameSpace + "." + "GridControlBandView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }


        private void PopupControl(object obj)
        {
            var convert = (NavigationFrame)obj;
            object getInstance = CreateInstance(_getNameSpace + "." + "PopupMainView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }    
        private void MultibindingView(object obj)
        {
            var convert = (NavigationFrame)obj;
            object getInstance = CreateInstance(_getNameSpace + "." + "StyleMultibinding_GridControlView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }


        private void TwoGridControlView(object obj)
        {
            var convert = (NavigationFrame)obj;
            object getInstance = CreateInstance(_getNameSpace + "." + "TwoGridControlView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;

        }

        private void MasterDetailView(object obj)
        {
            var convert = (NavigationFrame)obj;
            object getInstance = CreateInstance(_getNameSpace + "." + "MasterDetailView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;
        }
           public static object testDelegate(object item)
      {
          return item;
      }
      private void GridControlComboboxView(object obj)
      {
          if (obj is NavigationFrame navigationFrame)
          {
              object getInstance = CreateInstance(_getNameSpace + "." + "GridControlComboboxV", _loadingSelectPage);



              if (getInstance != null)
                  navigationFrame.Source = getInstance;

              // delegate 전달받을거
              /*Assembly assembly = typeof(GridControlComboboxVM).Assembly;

              try
              {
                  Type someDelegateHandler =
                      assembly.GetType("WPF_Tranning.ModelAndView.GridControlComboboxVM+DataGetEventHandlerMain", true, true); // DataGetEventHandler
                  var ctor = someDelegateHandler.GetConstructors()[0];

                  MethodInfo someDelegateImplementationMethod =
                  typeof(MainModelAndView).GetMethod("testDelegate",
               BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

                  // create a delegate that points to my method
                  Delegate someDelegateImplementationDelegate =
                      Delegate.CreateDelegate(someDelegateHandler, null, someDelegateImplementationMethod);

                  object[] args = new object[] { someDelegateImplementationDelegate };
                  object instance = ctor.Invoke(args);

              }
              catch (Exception e)
              {

              }*/

          }
      }

      // 테스트용
      private void GridControlBandMenuTreeBinding(object obj)
      {
          var convert = (TreeViewItem)obj;
          string header = convert.Header.ToString(); // 헤더 가져옴
      }

      private void LoadedAnotherPage(object obj)
      {
          /*   var convert = (NavigationFrame)obj;
             convert.Source = new AnotherPage();
             // 다른프로젝트는  이방식 안되서 보류*/

        var convert = (NavigationFrame)obj;
            // convert.Source = new GridControlBandView();
            object getInstance = CreateInstance(_getNameSpace + "." + "GridControlBandView", _loadingSelectPage);

            if (getInstance != null)
                convert.Source = getInstance;


        }

        /// <summary>
        /// 객체 텍스트로 생성
        /// </summary>
        /// <param name="classname"></param>
        /// <returns></returns>
        public object CreateInstance(string classname, string checkPage)
        {
            object getInstance = null;
            Type type = Type.GetType(classname);

    

            if (type == null && checkPage.Equals("LoadingStartPage"))
            {
                // null처리, 파일의 클래스가 강제로 바뀌거나, 파일을 읽을 수 없는 경우, 강제로 수정된 경우 포함
                type = Type.GetType("WPF_Tranning.GridControlBandView"); // 강제로 설정
                MessageBox.Show(type + "의 인스턴스가 발견되지 않아 GridControlBandView로 새로 생성합니다");
            }
            else if (type == null && checkPage.Equals("LoadingSelectPage"))
            {
                // null처리, 파일의 클래스가 강제로 바뀌거나, 파일을 읽을 수 없는 경우, 강제로 수정된 경우 포함
                MessageBox.Show(type + "의 인스턴스가 발견되지않았습니다","객체오류");
            }
            else
            {
                getInstance = Activator.CreateInstance(type);
            }
       

            return getInstance;
        }

        // 로딩시 꺼짐 현상 있어서 잠시 보류
        private object LoadingPage(string classname, string checkPage)
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
                Status = "WPF Loading...",
                Title = "",
                Subtitle = "WPF Tranning Project"
            }
            ).ShowOnStartup();
            object getInstance = CreateInstance(classname, checkPage);

            manager.Close();

            return getInstance;
        }

    }
}
