using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;

namespace WPF_Tranning
{

    class MainView : INotifyPropertyChanged
    {
        public ICommand AddColumn { get; set; }
        public ICommand CheckBinding { get; set; }
        public ICommand SelectEvent { get; set; }
        public ICommand CellValueChangedCommand { get; set; }
        public ICommand SaveColumn { get; set; }
        public ICommand CheckBox { get; set; }
        public ICommand Loaded { get; set; }
        public ICommand ComboSelect { get; set; }

        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public DataTable _datatable;
    
        public DataSet _scoreDataSet;

        public string Help { get; set; }

        public MainView()
        {
            model = new ScoreModel();
            AddColumn = new RelayCommand(new Action<object>(this.AddContent));
            SelectEvent = new RelayCommand(new Action<object>(this.SelectEventFun));
            CellValueChangedCommand = new RelayCommand(new Action<object>(this.CellValueChange));
            SaveColumn = new RelayCommand(new Action<object>(this.SaveColumnFunction));
            CheckBinding = new RelayCommand(new Action<object>(this.CheckBoxFun));
            CheckBox = new RelayCommand(new Action<object>(this.CheckBoxFun));
            Loaded = new RelayCommand(new Action<object>(this.LoadedBinding));
            ComboSelect = new RelayCommand(new Action<object>(this.ComboSelectBinding));


            _selectdata = new DataTable();

            _selectdata = connectDB().Tables[0]; // 내용꺼낼 용도 데이터 테이블
            _originalDB = connectDB().Tables[0]; // 원본데이터
            _scoreDataSet = new DataSet();
            _scoreDataSet = connectDB();
            // _selectScore = connectDB().Tables[0]; 
            _selectScore = new DataTable();
            _selectScore.Columns.Add("체크박스");
            _selectScore.Columns.Add("Score_id");
            _selectScore.Columns.Add("Score");


            Help = "도움말 입니다! \t\n테스트";


        }

 

        private void LoadedBinding(object obj)
        {
            var convert = (GridControl)obj;
            convert.SelectItem(0); // 포커스 0번으로 선택시켜 자동 선택 처리함
                                   
    
        }

      
        public event PropertyChangedEventHandler PropertyChanged;

        private int _score_id;
        private string _score;


  

        public ScoreModel model;

        private string _continentName;
        public string ContinentName


        {
            get { return _continentName; }
            set { _continentName = value; OnPropertyChanged("ContinentName"); }
        }
       



        public int Score_id
        {
            get { return _score_id; }
            set
            {
                _score_id = value;
                OnPropertyChanged("Score_id");
            }
        }

        public string Score
        {
            get { return _score; }
            set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        private bool? _mutualChb;
        public bool? MutualChb
        {
            get { return (_mutualChb != null) ? _mutualChb : false; }
            set
            {
                _mutualChb = value;
                OnPropertyChanged("MutualChb");
            }
        }
        private void OnPropertyChanged(string propertyName)
        {
        
            if (PropertyChanged != null)
            {
                MessageBox.Show("프로퍼티 체인지");
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }


        #region 데이터 바인딩 + DB
        /******************************************************************************/
        public DataTable _selecttable;
        public DataTable SelectTable
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selecttable; }
            set
            {
                _selecttable = value;

                //   Notify("SelectTable");
            }
        }     
        
        private DataTable _selectScore;
        public DataTable Select_Score
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selectScore; }
            set
            {
                _selectScore = value;

                Notify("Select_Score"); // 이거없으면 프로퍼티 체인지 값이 바뀌었다고 안알려져서 적용이 안됨
            }
        }

        private DataTable _selectdata;

        public DataTable SelectContent // 컨텐트 부분 내용
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selectdata; }
            set
            {
                _selectdata = value;

                //   Notify("SelectContent");
            }
        }


        public DataTable DataTable
        {
            get { return _datatable; }
            set
            {
                _datatable = value;
                MessageBox.Show("데이터 테이블");
                //   RaisePropertyChanged("DataTable");
            }
        }

       

        public DataSet connectDB()
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


        public DataSet SaveDB(DataTable table)
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




        private DataTable _originalDB;
        public DataTable OriginalDB // DB원본
        {
            get { return _originalDB; }
            set
            {
                _originalDB = value;

           
            }
        }
        /******************************************************************************/
        #endregion

        #region 클릭이벤트
        /******************************************************************************/
        private void SaveColumnFunction(object obj)
        {
            /*  int i = 0;
              foreach (DataRow row in _selectdata.Rows) // 실제 지정 컬럼은 _selectdata에 있음
              {

                      int score_id = (int)row.Field<int>("Score_id"); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                      string score = row.Field<string>("Score").ToString(); // 수정된 내용을 _selectdata 테이블로 부터 전달받음

                  // 비교용 (기존데이터)
                  DataRow[] a = _originalDB.Select();
                  if (i < _selectdata.Rows.Count-1) // insert포함해서 총 갯수가 8개인데 원본대상은 7개이면 if문 안에 i배열에서 범위초과되서 +1 더해서 조건 안들어가게 
                  {
                      // int score_id2 = connectDB().Tables[0].Rows[0].Field<int>("Score_id");
                      string score2 = a[i].Field<string>("Score").ToString(); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                      int score_id2 = (int)a[i].Field<int>("Score_id"); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                      i++;

                      if (score2.Equals(score) && score_id.Equals(score_id2))
                      {
                          continue;
                      }
                      else
                      {
                          UpdateDB(score_id, score); // update 처리

                      }
                  }
                  else
                  {
                      UpdateDB(score_id, score); // insert처리
                  }


              }*/
            // string value = _selectdata.GetChanges(DataRowState.Modified); // 저장부분 구분하는거 잠시 보류


            SaveDB(_selectdata); // 테이블 통째로 전달
            var convert = (GridControl)obj;
  
        }

        private void ComboSelectBinding(object obj) // 콤보 박스 선택시 이벤트 호출
        {
            var convert = obj;
            MessageBox.Show("selectbox 선택 : " + convert);
        }

        private void CheckBoxFun(object obj)
        {
            foreach (DataRow row in _selectdata.Rows) // 실제 지정 컬럼은 _selectdata에 있음
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
            _selectScore.TableName = "ScoreTable";
         
        }

        public void AddContent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
            var convert = (GridControl)obj;
            DataRow oRow = _selectdata.NewRow();
            _selectdata.Rows.Add(oRow);

            // convert.CurrentItem = (convert).GetRowByListIndex( (int)convert.ItemsSource
            convert.View.ShowEditor();
        }

    

        private void Notify(string propertyName)
        {
           //  MessageBox.Show("Notify호출");
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }



        /******************************************************************************/
        #endregion


    }
}
