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

using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Core;
using DevExpress.Mvvm;
using CommomCode;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using WPF_Tranning.View;
using System.Windows.Controls;
using System.Windows.Data;

namespace WPF_Tranning.ModelAndView
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return -(int)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    class StyleMultibinding_GridControlVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        /**********************************************************************/
        // string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/



        public StyleMultibinding_GridControlVM()
        {
            Loading();
            DataModel.CurrentClassPath = typeof(GridControlView).FullName; // 현재 접근한 클래스
        }

        #region 데이터 바인딩 + DB
        /******************************************************************************/
        private DataTable _MainData;

        public DataTable MainData 
        {
            get { return _MainData; }
            set
            {
                _MainData = value;
                OnPropertyChanged("MainData");
            }
        }
        /******************************************************************************/
        #endregion


        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private void MakeDataTable()
        {
            MainData = new DataTable();
            MainData.Columns.Add("Score_id");
            MainData.Columns.Add("Score");

            MainData.Rows.Add("1", "1점");
            MainData.Rows.Add("2", "100점");
            MainData.Rows.Add("3", "50점");
        }
        private void Loading()
        {
            var manager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                IsIndeterminate = false
            });
            manager.Show();
            manager.ViewModel.Progress = 100;

            MakeDataTable();

            SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                Copyright = "All rights reserved",
                IsIndeterminate = true,
                Status = "Starting...",
                Title = "",
                Subtitle = "Powered by DevExpress"
            }
            ).ShowOnStartup();

        }
    }
}
