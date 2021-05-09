using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;

namespace WPF_Tranning.ModelAndView 
{
    class GridCotrolBandModelAndView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    
        public string[] Week { get; set; }
        public string[] WeekDay { get; set; }
        public ICommand GirdControlBandLoaded { get; set; }
        public ICommand UnloadCommand { get; set; }
      //  public string CurrentClassPath { get; set; }


        private string _mask;
        public string MaskRegex
        {
            get
            {

                return _mask;
            }
            set
            {
                _mask = value;
                 OnPropertyChanged("MaskRegex");
            }
        }

        public GridCotrolBandModelAndView()
        {


            GirdControlBandLoaded = new RelayCommand(new Action<object>(this.GirdControlBandLoadedEvent));
            UnloadCommand = new RelayCommand(new Action<object>(this.UnloadEvent));
            
          //  MaskRegex = "\\d[2]\\.\\d{2}"; // 콤보박스에 따라 정규식 입력 바뀌게 하기, 데이터 테이블 다른걸로 로딩
            MaskRegex = "[0-9]{2}|[0-9]{3}"; // 최대 2자리 또는 3자리

            // https://docs.devexpress.com/WindowsForms/1499/controls-and-libraries/editors-and-simple-controls/common-editor-features-and-concepts/masks/mask-type-simplified-regular-expressions 
            // 정규식 설명

            Week = new string[8];
            WeekDay = new string[8];
            int getweek =  GetWeek();
            int getday = GetDay();
   
            DataWeek = TestData();
            DataColumn = TestData2();
            int i = 0;
            foreach (DataRow row in DataWeek.Rows) {

               
                WeekDay[i] = row.Field<string>("WeekDay"); // 동적으로 넣었다 치고
                i++;
          }

            //   DataModel.CurrentClassPath = typeof(GridControlBandView).FullName; // 현재 접근한 클래스
            DataModel.CurrentClassPath = GetType().FullName; // 현재 접근한 클래스
                                                             



        }

        private void UnloadEvent(object obj)
        {
         //   MessageBox.Show("그리드 컨트롤 밴드 클래스 메인으로 전달 : " + DataModel.CurrentClassPath);
          //  MainModelAndView a = new MainModelAndView(DataModel.CurrentClassPath);
          //  a.EndPageEvent(obj);
            
        }

        private void GirdControlBandLoadedEvent(object obj)
        {
            var conveter = (GridControl)obj;
            
        }

        public DataTable TestData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("WeekDay");

          
                dt.Rows.Add("1/1"); // 동적으로 가져왔다 치고
                dt.Rows.Add("1/2");
                dt.Rows.Add("1/3");
                dt.Rows.Add("1/4");
                dt.Rows.Add("1/5");
                dt.Rows.Add("1/6");
                dt.Rows.Add("1/7");
       

            return dt;
        }

        public DataTable TestData2()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Nm");
            dt.Columns.Add("W1");
            dt.Columns.Add("W2");
            dt.Columns.Add("W3");
            dt.Columns.Add("W4");
            dt.Columns.Add("W5");
            dt.Columns.Add("W6");
            dt.Columns.Add("W7");

            dt.Rows.Add("IP", "널", "널2");
            dt.Rows.Add("FSB", "널", "널2");
            dt.Rows.Add("TBP", "널", "널2");
            dt.Rows.Add("Memb_설치", "널", "널2");
            dt.Rows.Add("Memb_용접", "널", "널2");


            return dt;
        }
        private DataTable _dataWeek;
        public DataTable DataWeek // 주차
        {
            get {

                 return _dataWeek; }
            set
            {
                _dataWeek = value;
              //  OnPropertyChanged("ComboBoxSelect");
            }
        }

        private DataTable _dataColumn;
        public DataTable DataColumn // 주차
        {
            get
            {

                return _dataColumn;
            }
            set
            {
                _dataColumn = value;
                //  OnPropertyChanged("ComboBoxSelect");
            }
        }


        private int GetWeek()
        {

            int month = Int32.Parse(DateTime.Now.ToString("MM"));
            int day = Int32.Parse(DateTime.Now.ToString("dd"));
            int year = Int32.Parse(DateTime.Now.ToString("yyyy"));


            DateTime calculationDate = new DateTime(year, month, day);   //주차를 구할 일자

            DateTime calculationDate1 = new DateTime(year, 1, 1); //기준일

            Calendar calenderCalc = CultureInfo.CurrentCulture.Calendar;

            //DayOfWeek.Sunday 인수는 기준 요일

            int usWeekNumber = calenderCalc.GetWeekOfYear(calculationDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) - calenderCalc.GetWeekOfYear(calculationDate1, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) + 1;

            // GetWeekOfYear함수는 해당 년도의 주차를 구해주기 때문에 위와 같이 해주면 해당월의 주차가 계산됨

            for (int i=0; i<7; i++)
            {
                Week[i] = usWeekNumber-i + "주";
            }
            return usWeekNumber;
        }

        private int GetDay()
        {

            int month = Int32.Parse(DateTime.Now.ToString("MM"));
            int day = Int32.Parse(DateTime.Now.ToString("dd"));
            int year = Int32.Parse(DateTime.Now.ToString("yyyy"));


            DateTime calculationDate = new DateTime(year, month, day);   //주차를 구할 일자

            DateTime calculationDate1 = new DateTime(year, 1, 1); //기준일

            Calendar calenderCalc = CultureInfo.CurrentCulture.Calendar;

            //DayOfWeek.Sunday 인수는 기준 요일

            int usWeekNumber = calenderCalc.GetWeekOfYear(calculationDate, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) - calenderCalc.GetWeekOfYear(calculationDate1, CalendarWeekRule.FirstDay, DayOfWeek.Sunday) + 1;

            // GetWeekOfYear함수는 해당 년도의 주차를 구해주기 때문에 위와 같이 해주면 해당월의 주차가 계산됨

            for (int i = 0; i < 7; i++)
            {
                Week[i] = usWeekNumber - i + "주";
            }
            return usWeekNumber;
        }
        private void OnPropertyChanged(string propertyName)
        {
            //  MessageBox.Show("OnPropertyChanged 호출");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
