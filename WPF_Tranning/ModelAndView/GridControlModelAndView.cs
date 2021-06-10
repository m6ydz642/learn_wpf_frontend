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



/*using GridControl = DevExpress.Xpf.Grid.GridControl; 
// ExportToXlsx사용사 참조 모호오류떠서 바인딩할때 인자로 받아 쓰던 그리드 컨트롤은 xpf.grid로 직접 지정*/

namespace WPF_Tranning
{

    class GridControlModelAndView : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand AddColumn { get; set; }
        public ICommand CheckBinding { get; set; }
        public ICommand SelectEvent { get; set; }
        public ICommand CellValueChangedCommand { get; set; }
        public ICommand SaveColumn { get; set; }
        public ICommand CheckBox { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand ComboSelectedEvent { get; set; }
        public ICommand SaveExcel { get; set; }
        public ICommand GetBindingScoreInfomation { get; set; }
        public ICommand SaveExcelGrid { get; set; }
        public ICommand ComboboxLoaded { get; set; }
        public ICommand UnloadDataCheck { get; set; }

        public string ComboSelected { get; set; }
        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        private List<string> _textboxList;
        public List<string> TextBoxList
        {
            get { return _textboxList; }
            set
            {
                _textboxList = value;
                  OnPropertyChanged("TextBoxList"); // 왜 여기서는 안써도 되지 ㅡ.ㅡ? 
            }
        }

        private DataSet _scoreDataSet;
        public DataTable TestData;

        private Tranning_Model _dataModel;
        public Tranning_Model TranningDataModel
        {
            get { return _dataModel; }
            set
            {
                _dataModel = value;
              //  OnPropertyChanged("DataModel"); // 왜 여기서는 안써도 되지 ㅡ.ㅡ? 
            }
        }

        public string Help { get; set; }
        public void Loading()
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
                Status = "Starting...",
                Title = "",
                Subtitle = "Powered by DevExpress"
            }
            ).ShowOnStartup();

            ComboBoxSelect = GetSelectTestCode(); // DB로 넣었다 치고 데이터 가져와보기
            TestData = MakeDataSet().Tables[0]; // 바인딩할 용도는 아니고 잠시 DataSet로 직접 데이터 DB처럼 해보기
            GetScoreInfomation = GetScoreInfo().Tables[0]; // 내용꺼낼 용도 데이터 테이블
            GetBindingScoreData = new DataTable();
            _scoreDataSet = GetScoreInfo();
            manager.Close();

            TestCom a = new TestCom();
            a.Test();
        }
      
        public GridControlModelAndView()
        {
            Loading();
            TranningDataModel = new Tranning_Model();
            AddColumn = new RelayCommand(new Action<object>(this.AddContentEvent));
            SelectEvent = new RelayCommand(new Action<object>(this.SelectEventFun));
            CellValueChangedCommand = new RelayCommand(new Action<object>(this.CellValueChange));
            SaveColumn = new RelayCommand(new Action<object>(this.SaveColumnFunction));
            CheckBinding = new RelayCommand(new Action<object>(this.CheckBoxFun));
            CheckBox = new RelayCommand(new Action<object>(this.CheckBoxFun));
            Loaded = new RelayCommand(new Action<object>(this.LoadedBinding));
            ComboSelectedEvent = new RelayCommand(new Action<object>(this.ComboSelectBinding));
            SaveExcel = new RelayCommand(new Action<object>(this.SaveExcelFun));
            SaveExcelGrid = new RelayCommand(new Action<object>(this.SaveExcelGridFunction));
            GetBindingScoreInfomation = new RelayCommand(new Action<object>(this.GetBindingScoreInfo));
            ComboboxLoaded = new RelayCommand(new Action<object>(this.GetComboboxLoaded));
            UnloadDataCheck = new RelayCommand(new Action<object>(this.UnloadCheckEvent));
            TextBoxInit();



            ToolTipMessage();

           

            DataModel.CurrentClassPath = typeof(GridControlView).FullName; // 현재 접근한 클래스
        }

        private void TextBoxInit()
        {
            TextBoxList = new List<string>();
            string column = "";
            List<string> Value = new List<string>();

            Value.Add("테스트 \r\n데이터 벨류 : ");
            Value.Add("데이터 \r\n벨류2 : ");
            Value.Add("데이터 \r\n벨류3 : ");
            Value.Add("데이터 \r\n벨류4 : ");
            int i = 0;
            foreach (DataRow row in TextBoxData().Rows)
            {
                column = row.Field<string>("Value");
                TextBoxList.Add(Value[i] + column);
                i++;
            }
        }

        private void UnloadCheckEvent(object obj)
        {
            DataTable Status;

            Status = GetScoreInfomation.GetChanges(); // 추가, 수정 여부 구분해서 뜸
            var datatstatus = Status;
            if (datatstatus != null)
            {
                MessageBox.Show("저장되지 않은 내용이 있습니다");
                return;
            }
        }



        private void GetComboboxLoaded(object obj) // 콤보박스 로딩 이벤트
        {
            var convert = (ComboBoxEdit)obj;
            string test = (string)convert.EditValue;
            int test2 = convert.SelectedIndex;

            // 코드, 이름 에서 이름, 코드 로 반대로 되어있다고 가정할 경우에는 아래 함수를 이용하면 됨
            DataRowView oDataRowView = convert.SelectedItem as DataRowView;

            if (oDataRowView != null)
            {
                string Value = oDataRowView.Row["Name"].ToString(); // 임시 데이터 set으로 함
                string Value2 = oDataRowView.Row["Code"].ToString(); // 임시 데이터 set으로 함

              //  MessageBox.Show("selectbox 로딩 이벤트\r\n\r\n이름 : " + Value2 + "\r\n" + "코드명 : " + Value);
            }
            else
            {
                MessageBox.Show("가져올 데이터가 없습니다!");
            }
        }

        #region 데이터 직접 만들기
        /**********************************************************************************************/
        private DataTable GetSelectTestCode()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Code");
            dt.Columns.Add("Name");

            dt.Rows.Add("1H80000", "코드1");
            dt.Rows.Add("1H80010", "코드2");
/*
            dt.Rows.Add("코드1", "1H80000"); // 반대의 경우로 테스트
            dt.Rows.Add("코드2", "1H80010");*/
            


            return dt;
        }
        public DataTable TextBoxData()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("컬럼1");
            dt.Columns.Add("컬럼2");
            dt.Columns.Add("컬럼3");
            dt.Columns.Add("Value");
            dt.Rows.Add("abc", "def", "설치1과", "1234" );
            dt.Rows.Add("abc", "def", "설치1과", "5678" );
            dt.Rows.Add("abc", "def", "설치1과", "9,10,11,12" );
            dt.Rows.Add("abc", "def", "설치1과", "13,14,15,16" );
      
            return dt;

        }
        public DataTable ExcelexportTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("컬럼1");
            dt.Columns.Add("컬럼2");
            dt.Rows.Add("abc");
            dt.Rows.Add("def");
            dt.Rows.Add("설치1과");
            dt.Rows.Add("설치1과");
            dt.Rows.Add("설치1과");
            dt.Rows.Add("머지방해 데이터");
            dt.Rows.Add("설치2과");
            dt.Rows.Add("설치2과");
            dt.Rows.Add("설치2과");
            dt.Rows.Add("설치2과");
            dt.Rows.Add("설치3과");
            dt.Rows.Add("설치3과");
            dt.Rows.Add("설치5과");
            dt.Rows.Add("설치5과");
            dt.Rows.Add("설치5과");
            return dt;

        }

        public DataTable ExceldaDate()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("날짜1");
            dt.Columns.Add("날짜2");
            dt.Columns.Add("날짜3");
            dt.Columns.Add("날짜4");
            dt.Columns.Add("날짜5");

            dt.Rows.Add("2020-05-08", "2021-06-01", "2021-06-01", "2021-06-01", "2021-06-01");
            dt.Rows.Add("2020-05-10", "2021-02-01", "2021-06-01", "2021-06-01", "2021-02-01");
            dt.Rows.Add("2020-06-08", "2021-01-01", "2021-06-01", "2021-06-01", "2021-01-01");
            dt.Rows.Add("2020-05-08", "2021-06-01", "2021-06-01", "2021-06-01", "2021-06-01");
            dt.Rows.Add("2020-01-08", "2021-03-01", "2021-06-01", "2021-08-01", "2021-06-07");
            dt.Rows.Add("2020-02-08", "2021-02-01", "2021-07-01", "2021-06-01", "2021-02-01");
            dt.Rows.Add("2020-03-08", "2021-06-01", "2021-06-01", "2021-06-01", "2021-06-01");
            dt.Rows.Add("2020-04-08", "2021-05-01", "2021-06-01", "2021-06-01", "2021-06-19");
            dt.Rows.Add("2020-05-08", "2021-05-01", "2020-06-01", "2021-06-01", "2021-05-01");
            dt.Rows.Add("2020-05-01", "2021-06-09", "2021-06-01", "2021-06-01", "2021-06-08");
            dt.Rows.Add("          ", "2021-06-09", "2021-06-01", "2021-06-01", "2021-06-08");
            dt.Rows.Add("2021-07-01", "2021-06-09", "2021-06-01", "2021-06-01", "2021-06-08");
            dt.Rows.Add("2020-05-01", "2021-08-10", "2021-06-01", "2021-06-01", "2021-08-10");
    
            return dt;

        }



        private DataTable ExcelDataBlankTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("컬럼2");
            dt.Rows.Add("merge안시키고 공백처리하기");
            dt.Rows.Add("merge안시키고 공백처리하기");
            dt.Rows.Add("merge안시키고 공백처리하기");
            dt.Rows.Add("merge안시키고 공백처리하기");
            dt.Rows.Add("merge안시키고 공백처리하기");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가");
            dt.Rows.Add("중복추가2");
            dt.Rows.Add("중복추가2");
            dt.Rows.Add("중복추가2");
           
            return dt;
        }
        private DataSet MakeDataSet()
        {
            DataSet ds = new DataSet();

            ds.Tables.Add("Manager");
            ds.Tables["Manager"].Columns.Add("Manager_id"); // 데이터 셋이라 테이블로 해야 됨
            ds.Tables["Manager"].Columns.Add("Manager_name");


            string[] str = new string[2];

            str[0] = "1";
            str[1] = "김매니저";
            ds.Tables["Manager"].Rows.Add(str);

            str[0] = "2";
            str[1] = "메튜";
            ds.Tables["Manager"].Rows.Add(str);
            return ds; 
        }

        /**********************************************************************************************/
        #endregion

        private void ToolTipMessage()
        {
            Help = "도움말 입니다! \t\n테스트"; // 일반 스트링
            TranningDataModel.GridExcelHelp = "그리드 컨트롤의 엑셀파일을 다운로드할 수 있습니다";
        }

        private bool FileIsUse(string strFilePath, ref string strErr)
            // 호출될때 strErr 값은 ""로 들어가지만 ref 로 참조중이라 예외 뜨면 strErr값에 값 들어감 
        {
            try
            {
                using (System.IO.FileStream fs = new System.IO.FileStream
                    (strFilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
                { //파일 닫기...
                  fs.Close(); 
                } 
            } catch (Exception ex) { 
                strErr = ex.Message.ToString(); // 예외 걸려서 메시지 들어오는 부분을 호출되는 매개변수 ref가 참조함
                return false; 
            } return true; // 예외에 해당하지 않으면
        }

       
        private void SaveExcelGridFunction(object obj)
        {
            // var convert = (DevExpress.XtraGrid.GridControl)obj;

            string path = "output.xlsx";
            string strErr = "";
            bool status = FileIsUse(path, ref strErr); // ref 키워드 없애고 예외시 false만 리턴해도 됨

            if (status)
            {
                var convert = (TableView)obj; // GridControl이 아니라 TableView였음 ㅡㅡ;;
              
                convert.ExportToXlsx(path);
                MessageBox.Show("GridControl을 통째로 엑셀 다운로드를 시작합니다");
                Process.Start(path); // 자동 실행
            }
            else
            {
                MessageBox.Show("파일이 이미 열려있어서 실행할 수 없습니다\r\n파일을 닫아주십시오");
            }

        // https://supportcenter.devexpress.com/ticket/details/t370581/export-to-excel-a-gridcontrol
        // Gridcontrol 통째로 엑셀 내려받는 샘플 프로젝트 

        // https://docs.devexpress.com/WPF/DevExpress.Xpf.Grid.TableView.ExportToXlsx.overloads
        // table view라고 되어있는 곳도 있음

   
        }



        #region 클릭이벤트

        private void ComboSelectBinding(object obj) // 콤보 박스 선택시 이벤트 호출
        {
            var convert = (DevExpress.Xpf.Editors.ComboBoxEdit)obj;
            DataRowView oDataRowView = convert.SelectedItem as DataRowView;

            if (oDataRowView != null)
            {
                string Value = oDataRowView.Row["Code"].ToString(); // 임시 데이터 set으로 함
                string Value2 = oDataRowView.Row["Name"].ToString();

                MessageBox.Show("selectbox 선택 이벤트\r\n\r\n이름 : " + Value2 + "\r\n" + "코드명 : " + Value);
            }
            else
            {
                MessageBox.Show("가져올 데이터가 없습니다");
            }

        }

        private void SaveExcelFun(object obj)
        {
            using (var workbook = new XLWorkbook())
            {
                // https://github.com/closedxml/closedxml

                try
                {
                    DataTable exceltest = ExcelexportTable();
                    DataTable blankexcel = ExcelDataBlankTable();
                    DataTable dateexcel = ExceldaDate();

                    var worksheet = workbook.Worksheets.Add("Sample Sheet");
                    string filepath = @"C:\\Users\\m6ydz642\\source\\repos\\WPF_Tranning\\WPF_Tranning\\HelloWorld.xlsx";
                    worksheet.Cell("A1").Value = "Hello World!";
                    worksheet.Cell("A2").FormulaA1 = "=MID(A1, 7, 6)"; // FormulaA1 (A1) 의 셀을 참조에 7번째부터 6자리수 까지 출력

             


                    // 임의로 같은 값 만들기
                    worksheet.Cell("A3").Value = "test";
                    worksheet.Cell("A4").Value = "test";

                    worksheet.Range("C1:D2").Merge();
                    worksheet.Cell("C1").Value = "와우";

                    worksheet.Cell("E10").Value = "셀 테두리 테스트";
                    worksheet.Cell("E10").Style.Border.RightBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell("E10").Style.Border.TopBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell("E10").Style.Border.BottomBorder = XLBorderStyleValues.Thin;
                    worksheet.Cell("E10").Style.Border.LeftBorder = XLBorderStyleValues.Thin;

                    worksheet.Cell("G1").Value = "test";
                    worksheet.Cell("H1").Value = "test";
                    worksheet.Cell("I1").Value = "test2";
                    worksheet.Cell("J1").Value = "test";
                    worksheet.Cell("K1").Value = "test";
                    worksheet.Cell("K1").Value = "test2";
                    worksheet.Cell("L1").Value = "test";
                    worksheet.Cell("M1").Value = "test";
                    worksheet.Cell("N1").Value = "test2";
                    worksheet.Cell("AA1").Value = "test3";

                    worksheet.Cell("E10").Style.Border.LeftBorder = XLBorderStyleValues.None;


                    for (int i=0; i<4; i++)
                    {
                        // 리스트 내용 [0] ~ [4]까지 바인딩
                        worksheet.Cell("E1" + (i+1)).Value = TextBoxList[i];
                    }

                    if (worksheet.Cell("A3").Value.Equals(worksheet.Cell("A4").Value)) // 값 끼리 같으면 merge
                        worksheet.Range("A3:A4").Merge();
                  
                 

                    for (int i = 0; i<exceltest.Rows.Count; i++)
                    {
                        string rows = exceltest.Rows[i].Field<string>("컬럼1");
                        worksheet.Range("A" + (i + 10)).Value = rows;
                    }
                    for (int i = 0; i < blankexcel.Rows.Count; i++)
                    {
                        string rows = blankexcel.Rows[i].Field<string>("컬럼2");
                        worksheet.Range("B" + (i + 10)).Value = rows;
                    }

                    for (int i = 0; i < dateexcel.Rows.Count; i++)
                    {
                        string rows = dateexcel.Rows[i].Field<string>("날짜1");
                        worksheet.Range("G" + (i + 30)).Value = rows;

                        string rows2 = dateexcel.Rows[i].Field<string>("날짜2");
                        worksheet.Range("H" + (i + 30)).Value = rows2;

                        string rows3 = dateexcel.Rows[i].Field<string>("날짜3");
                        worksheet.Range("I" + (i + 30)).Value = rows3;     
                        
                        string rows4 = dateexcel.Rows[i].Field<string>("날짜4");
                        worksheet.Range("J" + (i + 30)).Value = rows4;


                        string rows5 = dateexcel.Rows[i].Field<string>("날짜5");

                        worksheet.Range("K" + (i + 30)).Value = rows5;
                    }

                    int count = 0;
                    string first = "";
                    string last = "";
                    string tempcell = "";

                    #region 중복제거 Linq
                    var newDt = exceltest.AsEnumerable()
                              .GroupBy(x => x.Field<string>("컬럼1"))
                              .Select(y => y.First())
                               .CopyToDataTable();
                    #endregion

                    #region 같은 값 동적 merge
                    List<string> duplicateArray = new List<string>();

                    for (int j = 0; j < exceltest.Rows.Count; j++)
                        {
                        // 같은 값 동적 merge 
                        // 데이터 영역 0, 1, 2번째
                        string beforedata = worksheet.Cell("A" + (j + 10)).Value.ToString(); // 머지 원본
                        string afterdata = worksheet.Cell("A" + (j + 11)).Value.ToString(); // 머지대상 그다음
                        string jumpdata = worksheet.Cell("A" + (j + 12)).Value.ToString(); // 머지대상 그 다음 데이터

                        // cell 이름
                        string beforecell = worksheet.Cell("A" + (j + 10)).ToString(); // cell 이름
                        string aftercell = worksheet.Cell("A" + (j + 11)).ToString(); // cell 이름


                        bool check = false;

                        if (beforedata.Equals(afterdata)) // 0번째 1번째 검사 
                        {
                            duplicateArray.Add(beforecell);
                            duplicateArray.Add(aftercell);
                            check = true;
                        }
                        if (beforedata.Equals(afterdata) && check) // 0번째 1번째가 맞고 위에 if문에 들어왔었으면
                        {
                            if (!jumpdata.Equals(beforedata)) // 1, 2 번째 데이터 검사 (한줄씩 밀어서 검사)
                            {
                                first = duplicateArray.First();
                                last = duplicateArray.Last();
                                worksheet.Range(first + ":" + last).Style.Alignment.Vertical = XLAlignmentVerticalValues.Top; // 위쪽정렬
                                worksheet.Range(first + ":" + last).Merge();
            
                                check = false;
                                duplicateArray = new List<string>();
                            }
                        }
                  

        
                    }
                    #endregion

                    #region 같은 값 공백처리 
                    List<string> duplicateArray2 = new List<string>();

                    for (int j = 0; j < exceltest.Rows.Count; j++)
                    {


                        // 같은 값 동적 merge 
                        // 데이터 영역 0, 1, 2번째
                        string beforedata = worksheet.Cell("B" + (j + 10)).Value.ToString(); // 머지 원본
                        string afterdata = worksheet.Cell("B" + (j + 11)).Value.ToString(); // 머지대상 그다음
                        string jumpdata = worksheet.Cell("B" + (j + 12)).Value.ToString(); // 머지대상 그 다음 데이터

                        // cell 이름
                        string beforecell = worksheet.Cell("B" + (j + 10)).ToString(); // cell 이름
                        string aftercell = worksheet.Cell("B" + (j + 11)).ToString(); // cell 이름


                        bool check = false;

                        if (beforedata.Equals(afterdata) && !beforedata.Equals("")) // 0번째 1번째 검사 
                        {
                            duplicateArray2.Add(aftercell); // 원본데이터를 넣으면 대상에 포함되어 공백처리되기 때문에 그 다음 데이터부터 넣음
                            worksheet.Cell(beforecell).Style.Fill.BackgroundColor = XLColor.FromArgb(171, 195, 223); // 공백대상 첫번째 색상

                            check = true;
                        }
                        if (beforedata.Equals(afterdata) && check) // 0번째 1번째가 맞고 위에 if문에 들어왔었으면
                        {
                            if (!jumpdata.Equals(beforedata) && !beforedata.Equals("")) // 1, 2 번째 데이터 검사 (한줄씩 밀어서 검사)
                            {
                                first = duplicateArray2.First();
                                last = duplicateArray2.Last();
                                worksheet.Range(first + ":" + last).Style.Fill.BackgroundColor = XLColor.FromArgb(171, 195, 223); // 공백데이터 범위 색상
                                worksheet.Range(first + ":" + last).Style.Border.TopBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(first + ":" + last).Style.Border.LeftBorder = XLBorderStyleValues.Thin;
                                worksheet.Range(first + ":" + last).Value = "공백"; // 공백처리

                                if (afterdata.Equals("중복추가"))
                                {
                                    worksheet.Cell(first).Style.Border.LeftBorder = XLBorderStyleValues.None;

                                }
                            }
                                check = false;
                                duplicateArray2 = new List<string>();
                            
                        }



                    }
                    #endregion

                    #region 동적으로 셀 값 찾아 선긋기
                    var value = worksheet.CellsUsed(cell => cell.GetString() == "test"); // test라는 셀을 찾아서 저장
                    var value2 = worksheet.CellsUsed(cell => cell.GetString() == "test2");
                    var value3 = worksheet.CellsUsed(cell => cell.GetString() == "test3");
                    // var test = worksheet.Search("test", CompareOptions.OrdinalIgnoreCase); // 이걸로 하니 셀 전체를 찾아서 안됨
                    // var test2 = worksheet.Search("test", CompareOptions.OrdinalIgnoreCase); // 이걸로 하니 셀 전체를 찾아서 안됨

                    List<string> DynamicColumnsThin = new List<string>(); // 선긋기용 컬럼 저장
                    List<string> DynamicColumnsDotted = new List<string>(); // 점선용 컬럼 저장

                    foreach (var row in value)
                    {
                        string cell = row.ToString();
                        string[] columns = cell.Split('1'); // 컬럼으로 쓰기위해 셀번호 뺌 (ColumnsUsed) 해서 컬럼 찾아내 사용할 수 있는 함수가 없음
                        // 예) G1에서 G만 가져감 어짜피 밑에 for문 에서 G ( i + 6) 셀번호 만큼 반복시켜서 쓸거라 컬럼만 있으면 됨
                        DynamicColumnsThin.Add(columns[0]);
                    }
                    foreach (var row in value2)
                    {
                        string cell = row.ToString();
                        string[] columns = cell.Split('1');
   
                        DynamicColumnsDotted.Add(columns[0]);

                    }
                    foreach (var row in value3)
                    {
                        string cell = row.ToString();
                        string[] columns = cell.Split('1');

                        DynamicColumnsDotted.Add(columns[0]);

                    }

                    // 동적 컬럼 꺼내온걸로 길이 개수만큼 추가

                    for (int i = 0; i < exceltest.Rows.Count; i++)
                    {
                        for (int k = 0; k < DynamicColumnsThin.Count; k++)
                        {
                            worksheet.Cell(DynamicColumnsThin[k] + (i + 10)).Style.Border.LeftBorder = XLBorderStyleValues.Thin;


                            for (int m = 0; m < DynamicColumnsDotted.Count; m++)
                            {
                                worksheet.Cell(DynamicColumnsDotted[m] + (i + 10)).Style.Border.LeftBorder = XLBorderStyleValues.Dotted;
                            }
                        }
                    }

                    #endregion


                    #region 동적 날짜 테두리 칠하기 (일반 버전)
                    // 테스트 데이터 생성, 마지막 색깔에 선 긋기

                    /*                    worksheet.Cell("G30").Value = "2021-06-09";
                                        worksheet.Cell("G31").Value = "2021-06-08";
                                        worksheet.Cell("H30").Value = "2021-05-08";
                                        worksheet.Cell("H29").Value = "2021-05-07";
                                        worksheet.Cell("I29").Value = "2021-05-01";*/

                    string Nowtime = DateTime.Now.ToString("yyyy-MM-dd");

                
                    for (int j = 0; j < dateexcel.Rows.Count; j++)
                    {
                
                        string Value = worksheet.Cell("G" + (j + 30)).Value.ToString();
                        string Value2 = worksheet.Cell("H" + (j + 30)).Value.ToString();
                       /* if (!Value.Equals(""))
                        {
                            int celldate = DateTime.Compare(DateTime.Parse(Nowtime), DateTime.Parse(Value)); // 현재보다 미래면 -1 
                            int celldate2 = DateTime.Compare(DateTime.Parse(Nowtime), DateTime.Parse(Value2)); // 현재보다 미래면 -1 
                            if (celldate < 0) { // 1이면 과거
                                worksheet.Cell("G" + (j + 30)).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
    
                                //     worksheet.Cell("C10").Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                            }

                            if (celldate2 < 0)
                            { // 1이면 과거
    
                                worksheet.Cell("H" + (j + 30)).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                                //     worksheet.Cell("C10").Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                            }
                        }*/

                    }

                    #endregion



                    #region 동적 날짜 테두리 칠하기 (리스트 버전)
                 
                    // 테스트 데이터 생성, 마지막 색깔에 선 긋기

                    /*                    worksheet.Cell("G30").Value = "2021-06-09";
                                        worksheet.Cell("G31").Value = "2021-06-08";
                                        worksheet.Cell("H30").Value = "2021-05-08";
                                        worksheet.Cell("H29").Value = "2021-05-07";
                                        worksheet.Cell("I29").Value = "2021-05-01";*/

                    List<string> LastDate = new List<string>();
                    Nowtime = DateTime.Now.ToString("yyyy-MM-dd");


                    for (int j = 0; j < dateexcel.Rows.Count; j++)
                    {

                        string Value = worksheet.Cell("G" + (j + 30)).Value.ToString();
                        string Value2 = worksheet.Cell("H" + (j + 30)).Value.ToString();
                        /*if (!Value.Equals(""))
                        {
                            int celldate = DateTime.Compare(DateTime.Parse(Nowtime), DateTime.Parse(Value)); // 현재보다 미래면 -1 
                            int celldate2 = DateTime.Compare(DateTime.Parse(Nowtime), DateTime.Parse(Value2)); // 현재보다 미래면 -1 
                            if (celldate < 0)
                            { // 1이면 과거
                                worksheet.Cell("G" + (j + 30)).Style.Border.BottomBorder = XLBorderStyleValues.Medium;

                                //     worksheet.Cell("C10").Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                            }

                            if (celldate2 < 0)
                            { // 1이면 과거

                                worksheet.Cell("H" + (j + 30)).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                                //     worksheet.Cell("C10").Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                            }
                        }*/

                    }


                    #endregion


                    #region 셀 전체탐색 후 과거보다 큰곳에 하단 선 그리기
                    worksheet.Column("G").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로
                    worksheet.Column("H").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로
                    worksheet.Column("I").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로
                    worksheet.Column("I").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로
                    worksheet.Column("J").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로
                    worksheet.Column("K").Style.DateFormat.Format = "mm-dd"; // 셀 서식 날짜로

                    var dynamicColumn = worksheet.CellsUsed();
                    foreach (var row in dynamicColumn)
                    {
                        string cell = row.ToString();
                        string Value = worksheet.Cell(cell).Value.ToString();
                      //  Regex regex = new Regex("^[A-Z]");
                      //  string[] splitNumber = regex.Split(cell);



                        DateTime Celltime; // 참조될 Cell Type
                        if (DateTime.TryParse(Value, out Celltime)) // 날짜 타입이면
                        {
                            int celldate = DateTime.Compare(DateTime.Parse(Nowtime),Celltime); // 현재보다 미래면 -1 
                            if (celldate < 0)
                            {
                              //  worksheet.Cell(cell).Style.Border.TopBorder = XLBorderStyleValues.Medium;
                              // 이전셀을 못구해서 잠시 보류
                            }
                        

                        }
                    }
                    #endregion


                    DateBottomBorder(worksheet, dateexcel, "G"); // 하단 줄긋기 함수
                    DateBottomBorder(worksheet, dateexcel, "H");
                    DateBottomBorder(worksheet, dateexcel, "I");
                    DateBottomBorder(worksheet, dateexcel, "J");
                    DateBottomBorder(worksheet, dateexcel, "K");

                    DateBottomBorder(worksheet, dateexcel, "G", "H");
                    DateBottomBorder(worksheet, dateexcel, "H", "I");
                    DateBottomBorder(worksheet, dateexcel, "I", "J");
                    DateBottomBorder(worksheet, dateexcel, "J", "K");





                    workbook.SaveAs(filepath);
                    MessageBox.Show("엑셀을 저장후 실행 합니다\r\n파일경로 : " + filepath);
                    Process.Start(filepath); // 자동실행
                }
                catch (System.IO.IOException e)
                {
                    MessageBox.Show("파일이 사용중입니다\r\n다른곳에서 파일이 사용중이거나 기타 오류가 발생하여 사용할 수 없습니다");
                }
                catch (Exception e)
                {

                }
            }
        }


        #region 셀 전체탐색 후 과거보다 오른쪽에 선 그리기
        public void DateBottomBorder(IXLWorksheet worksheet, DataTable dateexcel, string BeforeCell, string NextCell)
        {
            string saveCellNumber = "";
            string saveCellNumber2 = "";
            string BeforeBottomCell = "";
            string NextBottomCell = "";

            for (int b = dateexcel.Rows.Count; b > 0; b--)
            {
                string Value = worksheet.Cell(BeforeCell + (b + 28)).Value.ToString();
                string Value2 = worksheet.Cell(NextCell + (b + 28)).Value.ToString();

      /*          BeforeBottomCell = worksheet.Cell(BeforeCell + (b + 28)).ToString(); // cell 이름
                NextBottomCell  = worksheet.Cell(NextCell + (b + 28)).ToString(); // cell 이름*/

                DateTime Celltime; // 참조될 Cell Type
                DateTime Celltime2; // 참조될 Cell Type

                string BeforeBottomCellValue = worksheet.Cell(BeforeCell + (b + 28)).Style.Border.BottomBorder.ToString(); // 하단 셀 찾기
                string NextBottomCellValue = worksheet.Cell(NextCell + (b + 28)).Style.Border.BottomBorder.ToString(); // 하단 셀 찾기

                   if (NextBottomCellValue.Equals("Medium"))
                { // 다음셀에서 사용할 미디움 발견 즉시 바로 번호 저장
                    saveCellNumber2 = (b + 28).ToString();

                }

                if (BeforeBottomCellValue.Equals("Medium"))
                { // 이전셀에서 미디움 발견 즉시 바로 번호 저장
                    saveCellNumber = (b + 28).ToString();

                }
             

                //   worksheet.Cell("I" + (b + 29)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                //   worksheet.Range(beforecell + ":" + returnIcolumn).Style.Border.LeftBorder = XLBorderStyleValues.Medium;

                /*if (DateTime.TryParse(Value, out Celltime) && DateTime.TryParse(Value2, out Celltime2)) // 날짜 타입이면
                {
                    int celldate = DateTime.Compare(DateTime.Parse(Nowtime), Celltime); // 현재보다 미래면 -1 
                    int celldateCompare = DateTime.Compare(DateTime.Parse(Nowtime), Celltime2); // 현재보다 미래면 -1 

                    if (celldateCompare < 0) // 과거인 날짜 찾기
                    {
                        worksheet.Cell("H"+ (b + 29)).Style.Border.RightBorder = XLBorderStyleValues.Medium;
                                    // worksheet.Range("H41:I34").Style.Border.RightBorder = XLBorderStyleValues.Medium;

                    }
                    else
                    {
                        break;
                    }


                }*/
            }
             // int MinusnSaveCellnumber = Int32.Parse(saveCellNumber2) + 1;
             int MinusnSaveCellnumber = Int32.Parse(saveCellNumber2);

            worksheet.Range(NextCell + MinusnSaveCellnumber.ToString() + ":" + (NextCell + saveCellNumber)).Style.Border.LeftBorder = XLBorderStyleValues.Medium;
            // 반복문을 거꾸로 했기때문에 역순임

        }
#endregion



#region 날짜 조건 하단 선 긋기 함수

public string DateBottomBorder(IXLWorksheet worksheet, DataTable dateexcel, string Cell)
        {
            string Nowtime = DateTime.Now.ToString("yyyy-MM-dd");
            string beforecell = "";
            for (int j = 0; j < dateexcel.Rows.Count; j++)
            {

                // 데이터 영역 0, 1, 2번째
                string beforedata = worksheet.Cell(Cell + (j + 30)).Value.ToString();
                string afterdata = worksheet.Cell(Cell + (j + 31)).Value.ToString();
                string jumpdata = worksheet.Cell(Cell+ (j + 32)).Value.ToString();

                // cell 이름
                beforecell = worksheet.Cell(Cell + (j + 29)).ToString(); // cell 이름
                string aftercell = worksheet.Cell(Cell + (j + 31)).ToString(); // cell 이름


                DateTime Celltime;
                if (DateTime.TryParse(beforedata, out Celltime))// 날짜타입이 맞으면
                {
                    int celldate = DateTime.Compare(DateTime.Parse(Nowtime), Celltime);
                    if (celldate < 0)
                    {
                        worksheet.Cell(beforecell).Style.Border.BottomBorder = XLBorderStyleValues.Medium;
                    }


                }

       

            }
            return beforecell;// 선 그은 셀 리턴함
        }
        #endregion
        /******************************************************************************/
        private void GetBindingScoreInfo(object obj) // 바인딩 요청 클릭시 가져오는 데이터
        {
            GetBindingScoreData = GetScoreInfo().Tables[0]; // 다른그리드 컨트롤에서 가져오는 GetScoreInfomation 데이터 테이블을 가져와도 되지만   
                                                            // 다른 그리드 컨트롤을 안쓰고 한개만 만들었다 가정 
          //  GetBindingScoreData = GetScoreInfomation; // 도 사용가능
        }
        private void SaveColumnFunction(object obj) // 저장
        {

            DataTable Status;


            Status = GetScoreInfomation.GetChanges(); // 추가, 수정 여부 구분해서 뜸
            var datatstatus = Status;
            if (datatstatus == null)
            {
                MessageBox.Show("저장할 항목이 없습니다");
            }
                /* if (changestatus == null || addstatus != null || changestatus == null && addstatus == null)
                {
                    MessageBox.Show("저장할 항목이 없습니다");
                }*/
            else
            {

                var convert = (GridControl)obj;
                ModifyScoreInfo(GetScoreInfomation); // 테이블 통째로 전달하여 수정, 추가 처리함
                MessageBox.Show("데이터가 저장되었습니다");
                //    GetScoreInfomation = _getScoreInfomation; // 수정할때 들어가있던 datatable DB다시 호출 (새로 고침)
                //  GetScoreInfomation = GetScoreInfo().Tables[0]; // 내용꺼낼 용도 데이터 테이블
                GetScoreInfomation.AcceptChanges(); // 커밋하여 더 이상 중복저장 되지 않게 함
            }

        }

     

        private void CheckBoxFun(object obj)
        {
            foreach (DataRow row in _getScoreInfomation.Rows) // 실제 지정 컬럼은 _selectdata에 있음
            {
                int score_id = (int)row.Field<int>("Score_id");
                bool check = row.Field<bool>("체크박스");
            }

        }

        private void CellValueChange(object obj)
        {
            var convert = (GridControl)obj;
            // convert.ItemsSource = GetData();
            // 필드명으로 수정불가한 부분 메시지창으로 띄울 예정임
          //  MessageBox.Show("셀 변경됨 : " + convert.ToString());

        }

        private void SelectEventFun(object sender)
        {
            var convert = (GridControl)sender;
            string cellConvert = convert.GetFocusedRowCellValue("Score_id")?.ToString() ?? null;
            //   int? CellScore_id = (int?)convert.GetFocusedRowCellValue("Score_id") ?? 0;// 셀 선택이벤트, Score_id 값만 가져옴

            int CellScore_id = 0;
            if (!cellConvert.Equals(""))
            {
              CellScore_id = Int32.Parse(cellConvert);

                //  _selectScore = SelectDB(2).Tables[0]; // 일단은 2번선택한거처럼 해놈 아직 특정 값만 받는거 안함
                // https://supportcenter.devexpress.com/ticket/details/t806467/gridcontrol-stay-on-selected-row-after-refresh-using-datatable-as-itemssource
                // 하려다가 말음
            }
            else
            {
                CellScore_id = 0;
            }
    
            _scoreDataSet = SelectDB(CellScore_id);
            Select_Score = _scoreDataSet.Tables[0]; 
            // select_score (선택되어 출력 될 데이터 상대 그리드 컨트롤)
            // 하단의 foreach를 통한 출력보다 한번만에 출력 할 수 있도록 해줌

      /*      foreach (DataRow row in _scoreDataSet.Tables[0].Rows)
              {
                string score2 = row.Field<string>("Score").ToString(); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                _selectScore.Rows.Add(false, CellScore_id, score2);

             }*/

         
        }

        public void AddContentEvent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
            var convert = (GridControl)obj;
            DataRow oRow = GetScoreInfomation.NewRow();
            GetScoreInfomation.Rows.Add(oRow);
         //   convert.View.ShowEditor();
        }


        /******************************************************************************/
        #endregion



        #region 로드 이벤트 
        /******************************************************************************/

        private void LoadedBinding(object obj) // 그리드 컨트롤 로딩시
        {
            var convert = (GridControl)obj;
            convert.SelectItem(0); // 포커스 0번으로 선택시켜 자동 선택 처리함
        }
        /******************************************************************************/

        #endregion




        #region 데이터 바인딩 + DB
        /******************************************************************************/
        private DataTable _comboboxSelect;

        public DataTable ComboBoxSelect // 콤보박스
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _comboboxSelect; }
            set
            {
                _comboboxSelect = value;
                OnPropertyChanged("ComboBoxSelect");
            }
        }

        public DataTable _selecttable;
        public DataTable SelectTable
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selecttable; }
            set
            {
                _selecttable = value;

                //   OnPropertyChanged("SelectTable");
            }
        }

        private DataTable _selectScore;
        public DataTable Select_Score
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selectScore; }
            set
            {
                _selectScore = value;

                OnPropertyChanged("Select_Score"); // 이거없으면 프로퍼티 체인지 값이 바뀌었다고 안알려져서 적용이 안됨
            }
        }

        private DataTable _getScoreInfomation;

        public DataTable GetScoreInfomation
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _getScoreInfomation; }
            set
            {
                _getScoreInfomation = value;

                //    OnPropertyChanged("GetScoreInfomation"); // 최초로 가져올 데이터는 property change해서 알려줄 필요가 없다
            }
        }

        public DataSet GetScoreInfo()
        {
            string selectQuery = ConfigurationManager.AppSettings["selectScore"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, connection); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }

        public DataSet UpdateDB(int score_id, string score)
        {
            string selectQuery = ConfigurationManager.AppSettings["Score_Modify"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlCommand cmd = new SqlCommand("Score_Modify", connection);
            cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
            cmd.Parameters.Add("@Score_id", SqlDbType.Int).Value = score_id; // 스트링으로 전달받아도 타입이 int로 들어가네?
            cmd.Parameters.Add("@Score", SqlDbType.VarChar).Value = score;         // 프로시저 전달받을 매개변수


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }

        public DataSet SelectDB(int score_id)
        {
            string selectQuery = ConfigurationManager.AppSettings["Score_Select"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlCommand cmd = new SqlCommand("Score_Select", connection);
            cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
            cmd.Parameters.Add("@Score_id", SqlDbType.Int).Value = score_id;


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;


        }


        public DataSet ModifyScoreInfo(DataTable table)
        {
            string selectQuery = ConfigurationManager.AppSettings["Save_Score"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlCommand cmd = new SqlCommand("Save_Score", connection);
            cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
            cmd.Parameters.Add("@Get_SaveScore", SqlDbType.Structured).Value = table;


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;


        }


        private DataTable _getBindingScoreData;
        private DateTime j;

        public DataTable GetBindingScoreData // 바인딩 요청시 가져올 데이터 (초반엔 빈 데이터임)
        {
            get { return _getBindingScoreData; }
            set
            {
                _getBindingScoreData = value;
                OnPropertyChanged("GetBindingScoreData");// 최초로 가져올 데이터는 property change해서 알려줄 필요가 없지만
                                              // 버튼이나 콤보박스 같은 이벤트를 통해 데이터를 불러오려면 알려줘야 함


            }
        }
        /******************************************************************************/
        #endregion




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
