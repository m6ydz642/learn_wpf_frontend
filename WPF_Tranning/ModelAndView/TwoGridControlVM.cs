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



namespace WPF_Tranning
{

    class TwoGridControlVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler Foo;
        public ICommand AddColumn { get; set; }
        public ICommand AddColumn2 { get; set; }
        public ICommand CheckBinding { get; set; }
        public ICommand SelectEvent { get; set; }
        public ICommand CellValueChangedCommand { get; set; }
        public ICommand SaveColumn { get; set; }
        public ICommand CheckBox { get; set; }
        public ICommand IGridControl1Loaded { get; set; }
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
        public Button obbutton { get; set; }
        public GridControl GridControl1 { get; set; }
        public string Help { get; set; }
   

        public TwoGridControlVM()
        {
            // Loading();
            TranningDataModel = new Tranning_Model();
            AddColumn = new RelayCommand(new Action<object>(this.AddContentEvent));
            AddColumn2 = new RelayCommand(new Action<object>(this.AddContentEvent2));
            SelectEvent = new RelayCommand(new Action<object>(this.SelectEventFun));
            CellValueChangedCommand = new RelayCommand(new Action<object>(this.CellValueChange));
             CheckBinding = new RelayCommand(new Action<object>(this.CheckBoxFun));
            CheckBox = new RelayCommand(new Action<object>(this.CheckBoxFun));
            IGridControl1Loaded = new RelayCommand(new Action<object>(this.GridControlLoadedBinding));
            ComboSelectedEvent = new RelayCommand(new Action<object>(this.ComboSelectBinding));
   
            GetBindingScoreInfomation = new RelayCommand(new Action<object>(this.GetBindingScoreInfo));
            ComboboxLoaded = new RelayCommand(new Action<object>(this.GetComboboxLoaded));



            ToolTipMessage();

            DataModel.CurrentClassPath = typeof(GridControlView).FullName; // 현재 접근한 클래스
        }




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

            GetScoreInfomation = GetTestData().Tables[0];
            //  GetScoreInfomation = GetScoreInfo().Tables[0]; // 내용꺼낼 용도 데이터 테이블
            manager.Close();
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

        }

      

        /******************************************************************************/
        private void GetBindingScoreInfo(object obj) // 바인딩 요청 클릭시 가져오는 데이터
        {
         //  GetBindingScoreData = GetScoreInfo().Tables[0]; // 다른그리드 컨트롤에서 가져오는 GetScoreInfomation 데이터 테이블을 가져와도 되지만   
                                                            // 다른 그리드 컨트롤을 안쓰고 한개만 만들었다 가정 
            GetBindingScoreData = GetScoreInfomation; // 도 사용가능
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
          //  var convert = (GridControl)obj;
            // convert.ItemsSource = GetData();
            // 필드명으로 수정불가한 부분 메시지창으로 띄울 예정임
          //  MessageBox.Show("셀 변경됨 : " + convert.ToString());

        }

        private void SelectEventFun(object sender)
        {
   /*         var convert = (GridControl)sender;
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
          //  Select_Score = _scoreDataSet.Tables[0]; 
*/


         
        }

        public void AddContentEvent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
            DataTable dt = (DataTable)GridControl1.ItemsSource;
            DataTable tmp = new DataTable();

            try
            {

                tmp.Columns.Add("Check");
                tmp.Columns.Add("Score_id");
                tmp.Columns.Add("Score");


                foreach (DataRow row in dt.Rows)
                {
                    string check = row.Field<string>("Check");
                    string Score_id = row.Field<string>("Score_id");
                    string Score = row.Field<string>("Score");

                    if (check == "True")
                    {
                        tmp.Rows.Add(check, Score_id, Score);
                    }
                }
            } catch (Exception e) {
            }

            Select_Score = tmp;
        }
        public void AddContentEvent2(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
        

        }


        /******************************************************************************/
        #endregion



        #region 로드 이벤트 
        /******************************************************************************/

        private void GridControlLoadedBinding(object obj) // 그리드 컨트롤 로딩시
        {
            var convert = (GridControl)obj;
            convert.SelectItem(0); // 포커스 0번으로 선택시켜 자동 선택 처리함
            GridControl1 = convert;
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
            get { return _getScoreInfomation; }
            set
            {
                _getScoreInfomation = value;

                 OnPropertyChanged("GetScoreInfomation"); // 최초로 가져올 데이터는 property change해서 알려줄 필요가 없다
            }
        }

        public DataSet GetScoreInfo()
        {
            DataSet dataSet = null;
            try
            {
                string selectQuery = ConfigurationManager.AppSettings["selectScore"];
                SqlConnection connection = new SqlConnection(AppconfigDBSetting);
                connection.Open(); // DB연결


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, connection); // DB통로
                dataSet = new DataSet();
                sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            } catch (Exception e)
            {
                MessageBox.Show("데이터가 없어 테스트 데이터로 전환합니다");
                dataSet = GetTestData();
                GetScoreInfomation = dataSet.Tables[0];
            }
            return dataSet;
        }
        private DataSet GetTestData()
        {
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add("TestDataScore");
            dataSet.Tables["TestDataScore"].Columns.Add("Check");
            dataSet.Tables["TestDataScore"].Columns.Add("Score_id");
            dataSet.Tables["TestDataScore"].Columns.Add("Score");

            dataSet.Tables["TestDataScore"].Rows.Add(false,"1", "10점");
            dataSet.Tables["TestDataScore"].Rows.Add(false, "2", "40점");
            dataSet.Tables["TestDataScore"].Rows.Add(false, "3", "50점");
            

            return dataSet;
            
        }

      
        public DataSet SelectDB(int score_id)
        {
            DataSet dataSet = null;
            try
            {
                string selectQuery = ConfigurationManager.AppSettings["Score_Select"];
                SqlConnection connection = new SqlConnection(AppconfigDBSetting);
                connection.Open(); // DB연결

                SqlCommand cmd = new SqlCommand("Score_Select", connection);
                cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
                cmd.Parameters.Add("@Score_id", SqlDbType.Int).Value = score_id;


                SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
                sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            }catch(Exception e)
            {
                dataSet = new DataSet();
                DataTable dataTable = GetScoreInfomation.Copy();
                dataSet.Tables.Add(dataTable);
            }
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
