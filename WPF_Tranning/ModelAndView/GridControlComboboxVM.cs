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
    public class ColorEditConverter : IMultiValueConverter
    {
        public object StringData;

        public object ObjectData;
       public ColorEditConverter(object stringData, object objectData)
        {
            this.StringData = stringData;
            this.ObjectData = objectData;
        }
        public ColorEditConverter() { }

        public object Convert(object[] value, Type targetType, object parameter, CultureInfo culture)
        {
            return new ColorEditConverter(value[0], value[1]);

        }
            public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
    class GridControlComboboxVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ISelectGridControl { get; set; }

        public GridControlComboboxVM()
        {
            ISelectGridControl = new RelayCommand(new Action<object>(this.SelectedGridcontrol));
            DataModel.CurrentClassPath = typeof(GridControlComboboxV).FullName; // 현재 접근한 클래스


        }
        private ICommand _ISelectGridControl;
  

        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        private List<string> _comboboxcontent;
        public List<string> ComboboxContent
        {
            get { return _comboboxcontent; }
            set
            {
                _comboboxcontent = value;
                  OnPropertyChanged("ComboboxContent"); 
            }
        }   
        
        private DataTable _content;
        public DataTable Content
        {
            get { return _content; }
            set
            {
                _content = value;
                  OnPropertyChanged("Content"); 
            }
        }
        private string _score_id;
        public Button obbutton { get; set; }
        public string Help { get; set; }
        public void Loading()
        {
            var manager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                IsIndeterminate = false
            });
            manager.Show();
            manager.ViewModel.Progress = 100;

            _content = new DataTable();
            _content.Columns.Add("Score_id");
            _content.Columns.Add("Score");
            _content.Columns.Add("Combobox");
            _content.Rows.Add("1", "Score", "내용없음");
            _content.Rows.Add("2", "Score2", "내용없음2");
            _content.Rows.Add("3", "Score3", "색상을 선택하십시오");
            OnPropertyChanged("Content");

            _comboboxcontent = new List<string>();

       

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


        private void SelectedGridcontrol(object obj)
        {

            ColorEditConverter colorEditConverter = null;
            GridControl gridControl2 = null;
            if (obj is ColorEditConverter editConverter)
            {
                colorEditConverter = editConverter;
            }

            if (colorEditConverter != null && 
                colorEditConverter.ObjectData is GridControl gridControl1)
                gridControl2 = gridControl1;


            if (gridControl2 is GridControl gridControl)
            {
                int[] rowhandle = gridControl.GetSelectedRowHandles();
                DataRowView itemarray = (DataRowView)gridControl.GetRow(rowhandle[0]);

                string Score_id  = itemarray.Row.Field<string>("Score_id");
                List<string> data = new List<string>();
                _score_id = Score_id;


                if (Score_id == "1")
                {
                    data.Add("스코어1");
                    data.Add("스코어2");
                    data.Add("스코어3");
                    ColorEdit colorEdit = (ColorEdit)colorEditConverter.StringData;
                    colorEdit.Visibility = Visibility.Hidden;
                }

                if (Score_id == "2")
                {
                    ColorEdit colorEdit = (ColorEdit)colorEditConverter.StringData;
                    colorEdit.Visibility = Visibility.Hidden;
                    data.Add("테스트1");
                    data.Add("테스트2");
                    data.Add("테스트3");
                }

                if (Score_id == "3")
                {
                    ColorEdit colorEdit = (ColorEdit)colorEditConverter.StringData;
                    colorEdit.Visibility = Visibility.Visible;
                    colorEdit.ColorChanged += ColorEdit_ColorChanged;
                }

                _comboboxcontent = data;

                OnPropertyChanged("ComboboxContent");
            }
        }

        private void ColorEdit_ColorChanged(object sender, RoutedEventArgs e)
        {
           var coloredit =  (ColorEdit)sender;
            string GetColor = coloredit.Color.ToString();

            DataRow[] row = _content.Select();

            var FindRow = row.Single(x => x.Field<string>("Score_id") == _score_id);

            DataRow changeDataRow = FindRow;
            changeDataRow["Combobox"] = GetColor;
            OnPropertyChanged("ComboboxContent");


        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
