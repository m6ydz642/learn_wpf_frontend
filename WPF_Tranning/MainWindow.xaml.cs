using AnotherPageProject;
using AnotherPageProject.View;
using AnotherWindow.ModelAndView;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using Ookii.Dialogs.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Forms;
using WPF_Tranning.Model;
using WPF_Tranning.ModelAndView;
using static System.Windows.Forms.Design.AxImporter;

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


            if (!Properties.Settings.Default.DontShow)
            {
                //create instance of ookii dialog
                TaskDialog dialog = new TaskDialog();

                //create instance of buttons
                TaskDialogButton butYes = new TaskDialogButton("Yes");
                TaskDialogButton butNo = new TaskDialogButton("No");
                TaskDialogButton butCancel = new TaskDialogButton("Cancel");

                //checkbox 
                dialog.VerificationText = "Dont Show Again"; //<--- this is what you want.

                //customize the window
                dialog.WindowTitle = "Confirm Action";
                dialog.Content = "You sure you want to close?";
                dialog.MainIcon = TaskDialogIcon.Warning;

                //add buttons to the window
                dialog.Buttons.Add(butYes);
                dialog.Buttons.Add(butNo);
                dialog.Buttons.Add(butCancel);

                //show window
                TaskDialogButton result = dialog.ShowDialog(this);

                if (dialog.IsVerificationChecked)
                {
                    Properties.Settings.Default.DontShow = dialog.IsVerificationChecked;
                    Properties.Settings.Default.Save();
                }
            }
          /*  Properties.Settings.Default.DontShow = false;
            Properties.Settings.Default.Save();*/


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
