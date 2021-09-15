using AnotherPageProject;
using AnotherPageProject.View;
using AnotherWindow.ModelAndView;
using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WPF_Tranning.Model;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainModelAndView(); // 바인딩 설정 (없으면 바인딩 안먹힘)
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Window_Unloaded(object sender, RoutedEventArgs e)
        {
            //  this.MyUserControl.MethodToBeCalledWhenUnloaded().
        }

        private void SaveFile(string className){
            string path = DataModel.CurrentClassPath; // 이제 text로저장하든 db로 내보내던 하면 됨

            if (className == null || className.Equals("")) {
                className = path;
                
                string CurrentDirectory = Directory.GetCurrentDirectory(); // 개발자 디버그 경로
                string textFile = "\\SaveLastMenu.txt";
                System.IO.File.WriteAllText(CurrentDirectory + textFile, className, Encoding.Default); // 클래스 명 파일씀
            }
            else
            {
                string CurrentDirectory = Directory.GetCurrentDirectory(); // 개발자 디버그 경로
                string textFile = "\\SaveLastMenu.txt";
                System.IO.File.WriteAllText(CurrentDirectory + textFile, className, Encoding.Default); // 클래스 명 파일씀
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
             
            
            if (AnotherPage._className != null)
            {
                SaveFile(AnotherPage._className); // AnotherPageModelAndView를 쓰고 끄는 경우 이 클래스를 저장
            }
            else
            {
                SaveFile(null);
            }


            #region 메소드 정보가져오기 테스트
            string method =  System.Reflection.MethodBase.GetCurrentMethod().ReflectedType.Name+ " > " 
                + System.Reflection.MethodBase.GetCurrentMethod().Name;
            // 메소드 정보 가져오기 테스트
            #endregion
        }
    }
}
