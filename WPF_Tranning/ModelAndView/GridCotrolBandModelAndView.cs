using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using WPF_Tranning.Model;

namespace WPF_Tranning.ModelAndView
{
    public class TestConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
             string value1 = null;
             string value2 = null;
            try
            {
                string getvalue = value.ToString();
                value1 = string.Format("{0:0.00}", double.Parse(getvalue));
                value2  = string.Format("{0:0.00}", double.Parse(getvalue));
                
                if (value1.IndexOf(".") == null)
                    return value2;

            }catch (Exception e)
            {
                value1 = "";
                return value1;
            }

           return value1;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object test = "";
            return test;
        }
    }
    public class GridCotrolBandModelAndView : INotifyPropertyChanged
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
        
        private ObservableCollection<string> _weekday;
        public ObservableCollection<string> WeekDay
        {
            get
            {
                return _weekday;
            }
            set
            {
                _weekday = value;
                OnPropertyChanged("WeekDay");
            }
        }
        private ObservableCollection<string> _week;

        public ObservableCollection<string> Week
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
        }
        public ICommand GirdControlBandLoaded { get; set; }
        public ICommand UnloadCommand { get; set; }
        public ICommand ComboSelectedEvent { get; set; }
        public ICommand CheckRegexIntCommand { get; set; }
        public ICommand CheckRegexDoubleCommand { get; set; }
        public ICommand CheckRegexDoubleDBCommand { get; set; }
        public ICommand CellValueChangedCommand { get; set; }
        public ICommand CheckRegexDoubleDBSaveCommand { get; set; }
        public ICommand CheckDoubleCellChangeEventCommand { get; set; }
        
        private string _combomode;
        public string ComboMode {

            get
            {
                return _combomode;
            }
            set
            {
                _combomode = value;
                OnPropertyChanged("ComboMode");
            }
        }



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

        private string _regex;
        public string _Regex
        {          
                get
            {

                    return _regex;
                }
                set
            {
                    _regex = value;
                    OnPropertyChanged("_Regex");
                }
            }
        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public GridCotrolBandModelAndView()
        {
            Loading();
            ComboBoxSelect = new ObservableCollection<string>();


            Week = new ObservableCollection<string>();
            WeekDay = new ObservableCollection<string>();
        
            GirdControlBandLoaded = new RelayCommand(new Action<object>(this.GirdControlBandLoadedEvent));
            UnloadCommand = new RelayCommand(new Action<object>(this.UnloadEvent));
            ComboSelectedEvent = new RelayCommand(new Action<object>(this.ComboSelectBinding));
            CheckRegexIntCommand = new RelayCommand(new Action<object>(this.CheckIntRegexEvent));
            CheckRegexDoubleCommand = new RelayCommand(new Action<object>(this.CheckDoubleRegexEvent));
          //  CellValueChangedCommand = new RelayCommandEvent<object, CellValueChangedEventArgs> (this.CellValueChangedEvent);
            CellValueChangedCommand = new RelayCommand(new Action<object>(this.CellValueChangedEvent));
            CheckRegexDoubleDBCommand = new RelayCommand(new Action<object>(this.CheckRegexDBDoubleDBEvent));
            CheckRegexDoubleDBSaveCommand = new RelayCommand(new Action<object>(this.CheckRegexDBDoubleSaveDBEvent));
            CheckDoubleCellChangeEventCommand = new RelayCommand(new Action<object>(this.CheckDoubleCellChangeEvent));


            //  MaskRegex = "\\d[2]\\.\\d{2}"; // 콤보박스에 따라 정규식 입력 바뀌게 하기, 데이터 테이블 다른걸로 로딩
            // _Regex = "";
            // https://docs.devexpress.com/WindowsForms/1499/controls-and-libraries/editors-and-simple-controls/common-editor-features-and-concepts/masks/mask-type-simplified-regular-expressions 
            // 정규식 설명

        // https://www.regextester.com/97491
        // 정규식 테스트 홈페이지

        DataModel.CurrentClassPath = typeof(GridControlBandView).FullName; // 현재 접근한 클래스
        }

 

        private void CheckRegexDBDoubleSaveDBEvent(object obj)
        {
            MessageBox.Show("decimal 데이터를 저장합니다");
            DataTable check = GetDoubleScoreDataTable.GetChanges();
            if (check != null)
            {

                SaveDecimalDB(GetDoubleScoreDataTable);
            }
        }

        private void CheckRegexDBDoubleDBEvent(object obj)
        {


            #region 소수점 컬럼 수동검사
            int count = 0;

           /* foreach (DataRow row in GetDoubleScoreDataTable.Rows)
            {
                decimal value = row.Field<decimal>("Score_double");
                if (!CheckRegex(value.ToString()))
                {
                    count++;
                }
                
            }*/ // 람다식으로 변경
            
            foreach (DataRow row in GetDoubleScoreDataTable.Rows)
            {
                decimal value = row.Field<decimal>("Score_double");

                #region 데이터 모드2에서 람다식 정규식 체크
                Func<string, bool> CheckRegex = (text) =>
                {
                    bool result = false;
                    Regex rgx = new Regex(_Regex);
                    if (ComboMode.Equals("데이터모드 2"))
                    {
                        result = rgx.IsMatch(text);
                    }
                    return result;
                };
                #endregion

                if (!CheckRegex(value.ToString()))
                {
                    count++;
                }  
            }
            if (count >= 1)
                MessageBox.Show("소수점형식이 맞지 않습니다 2.23형식으로 2자리로 입력해주세요\r\n그래도 맞지 않을경우 데이터 모드2인지 확인해주십시오\r\n" +
                    "정규식 카운트 결과 : " + count);
            #endregion


        }




        /*         
         *         public ICommand MouseWheelCommand // ICommand생성자 안쓰는 방식

     {

         get

         {

             return this._mouseWheelCommand ??

                  (this._mouseWheelCommand = new RelayCommand<MouseWheelEventArgs>(this.ExecuteMouseWheel));

         }

     }
*/

        // 람다식으로 변경해서 안씀
        private bool CheckRegex(string text)
        {
            bool result = false;
            Regex rgx = new Regex(_Regex);
            if (ComboMode.Equals("데이터모드 1"))
            {
                result = rgx.IsMatch(text);
            }

            if (ComboMode.Equals("데이터모드 2"))
            {
                result = rgx.IsMatch(text);
            }

            return result;
        }
        public void Loading()
        {
            var manager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                IsIndeterminate = false
            });
            manager.Show();
            manager.ViewModel.Progress = 100; // 로딩바 퍼센트

            SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                Copyright = "All rights reserved",
                IsIndeterminate = true,
                Status = "Starting...",
                Title = "",
                Subtitle = "Powered by DevExpress"

            }
            ).ShowOnStartup();

            GetDoubleScoreDataTable = GetDoubleScoreInfo().Tables[0];

            manager.Close();


        }

       
        private void CellValueChangedEvent(object obj)
        {
            var convert = (TableView)obj;
            Regex rgx = new Regex(_Regex);

          //   string text = convert.CurrentCellValue.ToString();


            if (ComboMode.Equals("데이터모드 1"))
            {
             /*   string value1 = string.Format("{0:0.00}", double.Parse(getvalue)); // 정규식 없애고 이걸로 테이블로 다시 보내면 될듯
                string value2 = string.Format("{0:0.00}", double.Parse(getvalue));
*/
            }

            if (ComboMode.Equals("데이터모드 2"))
            {
            //   Action result = rgx.IsMatch(text) ? keepgoing : errorDouble;
                // MessageBox.Show("올바른 형식을 확인해주세요 소수점 2자리까지 입력가능합니다 예) 1.12");
            }


            // if else문 축약형
            var a1 = new Action(() => { /* if code block */  });/* if code  */
            var a2 = new Action(() => { /* else code block */ }); /* else code  */
           //  Action resultingAction = test_variable ? a1 : a2;
        }

        private void CheckDoubleRegexEvent(object obj)
        {
            var convert = (GridControl)obj;
            string getvalue = convert.CurrentCellValue.ToString();
            string value1 = string.Format("{0:0.00}", double.Parse(getvalue));
            string value2 = string.Format("{0:0.00}", double.Parse(getvalue));

            /* if (!CheckRegex(getvalue))
             {
                 MessageBox.Show("올바른 형식을 확인해주세요 소수점 2자리까지 입력가능합니다 예) 1.12");

             }*/




        }

        #region 그리드 컨트롤 2번째꺼 
        // 셀 변경 이벤트
        private void CheckDoubleCellChangeEvent(object obj)
        {
            var converter = (TableView)obj;



            // string value = string.Format("{0:0.00}", double.Parse(getvalue));

        }


        private void CheckIntRegexEvent(object obj)
        {
            var convert = (GridControl)obj;
            string value = convert.CurrentCellValue.ToString();

            Func<string, bool> CheckRegex = (text) =>
            {
                bool result = false;
                Regex rgx = new Regex(_Regex);
                if (ComboMode.Equals("데이터모드 1"))
                {
                    result = rgx.IsMatch(text);
                }
                return result;
            };

            if (!CheckRegex(value))
            {
                MessageBox.Show("올바른 형식을 확인해주세요 숫자 1~4자리까지 입력가능합니다");

            }
        }
        #endregion

        private void ComboSelectBinding(object obj) // 콤보 박스 선택시 이벤트 호출
        {
            var convert = (DevExpress.Xpf.Editors.ComboBoxEdit)obj;

            ComboMode = convert.SelectedItem.ToString();
         
            if (ComboMode.Equals("데이터모드 1"))
            {
                // MaskRegex = "[0-9]{2}|[0-9]{3}"; // 최대 2자리 또는 3자리
                DataWeek = MakeTestDataSet().Tables[0]; 
                DataColumn = MakeTestDataSet().Tables[1];
                ComboMode = "데이터모드 1";
                _Regex = "^[0-9]{1,5}$"; // 숫자 0~9까지 1자리부터 5자리까지 ( ^시작 $종료)
                GetWeek_WeekDay();
                //  MessageBox.Show("데이터 모드 1 실행, 정규식 모드 : 숫자 세자리");


            }
            if (ComboMode.Equals("데이터모드 2"))
            {
               //  _Regex = "\\d[2]\\.\\d{2}";
                DataWeek = MakeTestDataSet2().Tables[0]; 
                DataColumn = MakeTestDataSet2().Tables[1];
                ComboMode = "데이터모드 2";
                _Regex = "^[0-9]{1,1}[.][0-9]{1,2}$"; // 소수점 1.23 형식만 사용가능
                GetWeek_WeekDay();
             //   MessageBox.Show("데이터 모드 2 실행, 정규식 모드 : 소수점");


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

      
    

        public void GetData()
        {
            ComboBoxSelect.Add("데이터모드 1");
            ComboBoxSelect.Add("데이터모드 2");
            ComboMode = "데이터모드 1";
    
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
            Week = new ObservableCollection<string>(); // 초기화
            WeekDay = new ObservableCollection<string>();

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


        public DataSet SaveDecimalDB(DataTable table)
        {
            string selectQuery = ConfigurationManager.AppSettings["Save_Double_AppKey"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlCommand cmd = new SqlCommand(selectQuery, connection);
            cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
            cmd.Parameters.Add("@Get_SaveDoubleScore", SqlDbType.Structured).Value = table;


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }

        public DataSet GetDoubleScoreInfo()
        {
            string selectQuery = ConfigurationManager.AppSettings["Score_double"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, connection); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }

        private DataTable _dataWeek;
        public DataTable DataWeek // 주차
        {
            get
            {

                return _dataWeek;
            }
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


        private DataTable _getdoublescoreinfo;
        public DataTable GetDoubleScoreDataTable
        {
            get
            {

                return _getdoublescoreinfo;
            }
            set
            {
                _getdoublescoreinfo = value;
                OnPropertyChanged("GetDoubleScoreDataTable");
            }
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




    }
}
