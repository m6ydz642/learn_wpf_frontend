using DevExpress.Xpf.Grid;
using System;
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

        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public DataTable _datatable;
        public DataSet _result;


        public MainView()
        {
            model = new ScoreModel();
            AddColumn = new RelayCommand(new Action<object>(this.AddContent));
            SelectEvent = new RelayCommand(new Action<object>(this.SelectEventFun));
            CellValueChangedCommand = new RelayCommand(new Action<object>(this.CellValueChange));
            SaveColumn = new RelayCommand(new Action<object>(this.SaveColumnFunction));


            _selectdata = new DataTable();
           _selectdata.Columns.Add("스코어 아이디");
            _selectdata.Columns.Add("스코어 점수");

            //  _selecttable = connectDB().Tables[0]; // 내용꺼낼 용도 데이터 테이블
          //  _selectdata = connectDB().Tables[0]; // 내용꺼낼 용도 데이터 테이블

            /*   DataRow[] rows = _selecttable.Select();

               int[] score = new int[rows.Length];
               string[] scorecontent = new string[rows.Length];

               for (int i = 0; i < rows.Length; i++)
               {
                   score[i] = (int)rows[i]["score_id"]; // 특정 컬럼만 꺼내와 배열에 담음
                   scorecontent[i] = (string)rows[i]["Score"];
                   _selectdata.Rows.Add(score[i], scorecontent[i]);
               }*/
            _result = connectDB();

            foreach (DataRow row in _result.Tables[0].Rows) // 실제 지정 컬럼은 _selectdata에 있음
            {
                int score_id = (int)row.Field<int>("Score_id"); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                string score = row.Field<string>("Score").ToString(); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                _selectdata.Rows.Add(score_id, score);

            }




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



        public DataTable _selecttable;
        public DataTable SelectTable
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selecttable; }
            set
            {
                _selecttable = value;

                //   RaisePropertyChanged("DataTable");
            }
        }

        private DataTable _selectdata;

        public DataTable SelectContent // 컨텐트 부분 내용
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selectdata; }
            set
            {
                _selectdata = value;

                //   RaisePropertyChanged("DataTable");
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

        public DataSet UpdateDB(string score_id, string score)
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

        private void SaveColumnFunction(object obj)
        {
            foreach (DataRow row in _selectdata.Rows) // 실제 지정 컬럼은 _selectdata에 있음
            {
                string score_id = (string)row.Field<string>("스코어 아이디"); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                string score = row.Field<string>("스코어 점수").ToString(); // 수정된 내용을 _selectdata 테이블로 부터 전달받음
                
                foreach (DataRow row2 in _result.Tables[0].Rows) {

                    int score_id2 = (int)row2.Field<int>("score_id"); 
                    string score2 = row2.Field<string>("score").ToString();

                    if (score != score2)
                    {
                           UpdateDB(score_id, score); // DB값이랑 입력값이 같지 않으면 업데이트 처리
                    }
                    else
                    {
                        MessageBox.Show("같지 않은 항목 score : " + score + " score2 : " + score2);
                    }
                }
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
           // var convert2 = (CellValueEventArgs)sender;
            MessageBox.Show("선택 내용 " + convert.GetCellValue(0,"1"));
        }

        public void AddContent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
           // _selectdata = new DataTable();
            _selectdata.Rows.Add("", "");

        }

    

        private void Notify(string propertyName)
        {
            MessageBox.Show("Notify호출");
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
