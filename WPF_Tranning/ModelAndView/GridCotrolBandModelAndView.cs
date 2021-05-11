using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;

namespace WPF_Tranning 
{
    class GridCotrolBandModelAndView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

    /*    public string[] _week;
        public string[] Week
        {
            get
            {
                return _week;
            }
            set
            {
                _week = value;
                OnPropertyChanged("Week");

            }
        }*/

/*
        public string[] _weekDay;
        public string[] WeekDay {
            get
            {
                return _weekDay;
            }

            set { _weekDay = value;
                OnPropertyChanged("WeekDay");

            }
        }*/

        public ObservableCollection<string> ComboBoxSelect { get; set; }
        public ObservableCollection<string> WeekDay { get; set; }
        public ObservableCollection<string> Week { get; set; }
        public ICommand GirdControlBandLoaded { get; set; }
        public ICommand UnloadCommand { get; set; }
        public ICommand ComboSelectedEvent { get; set; }
        public string ComboMode { get; set; }



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
            ComboBoxSelect = new ObservableCollection<string>();
            ComboBoxSelect.Add("데이터모드 1");
            ComboBoxSelect.Add("데이터모드 2");
            ComboMode = "데이터모드 1";
            Week = new ObservableCollection<string>();
            WeekDay = new ObservableCollection<string>();
        
            GirdControlBandLoaded = new RelayCommand(new Action<object>(this.GirdControlBandLoadedEvent));
            UnloadCommand = new RelayCommand(new Action<object>(this.UnloadEvent));
            ComboSelectedEvent = new RelayCommand(new Action<object>(this.ComboSelectBinding));

            //  MaskRegex = "\\d[2]\\.\\d{2}"; // 콤보박스에 따라 정규식 입력 바뀌게 하기, 데이터 테이블 다른걸로 로딩
            // MaskRegex = "[0-9]{2}|[0-9]{3}"; // 최대 2자리 또는 3자리

            // https://docs.devexpress.com/WindowsForms/1499/controls-and-libraries/editors-and-simple-controls/common-editor-features-and-concepts/masks/mask-type-simplified-regular-expressions 
            // 정규식 설명


            DataModel.CurrentClassPath = typeof(GridControlBandView).FullName; // 현재 접근한 클래스
        }

        private void ComboSelectBinding(object obj) // 콤보 박스 선택시 이벤트 호출
        {
            var convert = (DevExpress.Xpf.Editors.ComboBoxEdit)obj;

            ComboMode = convert.SelectedItem.ToString();
         
            if (ComboMode.Equals("데이터모드 1"))
            {
                MaskRegex = "[0-9]{2}|[0-9]{3}"; // 최대 2자리 또는 3자리
                DataWeek = MakeTestDataSet().Tables[0]; 
                DataColumn = MakeTestDataSet().Tables[1];
                ComboMode = "데이터모드 1";
                GetWeek_WeekDay();
          
                MessageBox.Show("데이터 모드 1 실행, 정규식 모드 : 숫자 세자리");


            }
            if (ComboMode.Equals("데이터모드 2"))
            {
                MaskRegex = "\\d[2]\\.\\d{2}";
                DataWeek = MakeTestDataSet2().Tables[0]; 
                DataColumn = MakeTestDataSet2().Tables[1];
                ComboMode = "데이터모드 2";

                GetWeek_WeekDay();
                MessageBox.Show("데이터 모드 2 실행, 정규식 모드 : 소수점");


            }

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

        private DataSet MakeTestDataSet()

        {
            DataSet ds = new DataSet();
            ds.Tables.Add("WeekSets");
            ds.Tables["WeekSets"].Columns.Add("WeekDay"); // 데이터 셋이라 테이블로 해야 됨
            ds.Tables["WeekSets"].Rows.Add("1/1"); 
            ds.Tables["WeekSets"].Rows.Add("1/2");
            ds.Tables["WeekSets"].Rows.Add("1/3");
            ds.Tables["WeekSets"].Rows.Add("1/4");
            ds.Tables["WeekSets"].Rows.Add("1/5");
            ds.Tables["WeekSets"].Rows.Add("1/6");
            ds.Tables["WeekSets"].Rows.Add("1/7");

            ds.Tables.Add("CssTable");

            ds.Tables["CssTable"].Columns.Add("Nm");
            ds.Tables["CssTable"].Columns.Add("W1");
            ds.Tables["CssTable"].Columns.Add("W2");
            ds.Tables["CssTable"].Columns.Add("W3");
            ds.Tables["CssTable"].Columns.Add("W4");
            ds.Tables["CssTable"].Columns.Add("W5");
            ds.Tables["CssTable"].Columns.Add("W6");
            ds.Tables["CssTable"].Columns.Add("W7");

            ds.Tables["CssTable"].Rows.Add("IP", "널", "널2");
            ds.Tables["CssTable"].Rows.Add("FSB", "널", "널2");
            ds.Tables["CssTable"].Rows.Add("TBP", "널", "널2");
            ds.Tables["CssTable"].Rows.Add("Memb_설치", "널", "널2");
            ds.Tables["CssTable"].Rows.Add("Memb_용접", "널", "널2");

            return ds;
        }

        private DataSet MakeTestDataSet2()

        {
            DataSet ds = new DataSet();
            ds.Tables.Add("WeekSets");
            ds.Tables["WeekSets"].Columns.Add("WeekDay"); // 데이터 셋이라 테이블로 해야 됨
            ds.Tables["WeekSets"].Rows.Add("2/1");
            ds.Tables["WeekSets"].Rows.Add("2/2");
            ds.Tables["WeekSets"].Rows.Add("2/3");
            ds.Tables["WeekSets"].Rows.Add("2/4");
            ds.Tables["WeekSets"].Rows.Add("2/5");
            ds.Tables["WeekSets"].Rows.Add("2/6");
            ds.Tables["WeekSets"].Rows.Add("2/7");

            ds.Tables.Add("CssTable");

            ds.Tables["CssTable"].Columns.Add("Nm");
            ds.Tables["CssTable"].Columns.Add("W1");
            ds.Tables["CssTable"].Columns.Add("W2");
            ds.Tables["CssTable"].Columns.Add("W3");
            ds.Tables["CssTable"].Columns.Add("W4");
            ds.Tables["CssTable"].Columns.Add("W5");
            ds.Tables["CssTable"].Columns.Add("W6");
            ds.Tables["CssTable"].Columns.Add("W7");

            ds.Tables["CssTable"].Rows.Add("IP", "널2", "널2");
            ds.Tables["CssTable"].Rows.Add("FSB", "널2", "널2");
            ds.Tables["CssTable"].Rows.Add("TBP", "널2", "널2");
            ds.Tables["CssTable"].Rows.Add("Memb_설치2", "널", "널2");
            ds.Tables["CssTable"].Rows.Add("Memb_용접2", "널", "널2");

            return ds;
        }


        #region 데이터 테이블로 만들기 (이제 데이터 셋으로 넣음)
        /*    public DataTable TestData()
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
        }*/
        #endregion

        private DataTable _dataWeek;
        public DataTable DataWeek // 주차
        {
            get {

                 return _dataWeek; }
            set
            {
                _dataWeek = value;
                OnPropertyChanged("DataWeek");
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
                  OnPropertyChanged("DataColumn");
            }
        }

        public void test()
        {
            MessageBox.Show("바인딩 성공");
        }

        public int GetWeek_WeekDay()
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
               Week.Add(usWeekNumber-i + "주");
            }

            foreach (DataRow row in DataWeek.Rows)
            {
                WeekDay.Add(row.Field<string>("WeekDay")); // 동적으로 넣었다 치고
            }

           string first = WeekDay.First<string> (); // 첫번째 list 반환 테스트

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

    
  
            return day;
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
