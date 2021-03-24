using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPF_Tranning.Model;

namespace WPF_Tranning
{

    class MainView : INotifyPropertyChanged
    {
        public ICommand AddColumn { get; set; }
        public ICommand CheckBinding { get; set; }

        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public DataTable _datatable;

        public DataTable _showContent;
  


        bool checkedVar = false;


        public event PropertyChangedEventHandler PropertyChanged;

        private int _score_id;
        private string _score;
        private bool mIsSelected;
        public int _scorecontent; // 체크박스에 들어갈 내용
        public string _name;

        public string Name
        {
            get;set;
        }

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

        public int ScoreContent
        {
            get { return _scorecontent; }
            set
            {
                _scorecontent = value;
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


        public bool CheckedVar

        {

            get { return checkedVar; }

            set

            {

                checkedVar = value;

                Notify("CheckedVar");
                MessageBox.Show("박스");

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

        public DataTable _selectdata;

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
            /*  
              dataGridView1.DataSource = dataSet.Tables[0];*/

            // DataSet 리턴받아 호출하는 곳에서 나머지 Tables등 실행함
            return dataSet;
        }

        ObservableCollection<ScoreModel> _sampleDatas = null;
        public ObservableCollection<ScoreModel> SampleDatas
        {
            get
            {
                if (_sampleDatas == null)
                {
                    _sampleDatas = new ObservableCollection<ScoreModel>();
                }
                return _sampleDatas;
            }
            set
            {
                _sampleDatas = value;
            }
        }


       // 출처: https://afsdzvcx123.tistory.com/entry/C-WPF-C-WPF-MVVM-패턴-예제-프로그램 [BeomBeomJoJo - Programmer]
        public MainView()
        {
            model = new ScoreModel();
            AddColumn = new RelayCommand(new Action<object>(this.AddContent));
 

            _selectdata = new DataTable();
            _selectdata.Columns.Add("스코어 아이디");
            _selectdata.Columns.Add("스코어 점수");

            _selecttable = connectDB().Tables[0]; // 내용꺼낼 용도 데이터 테이블
   
            DataRow[] rows = _selecttable.Select();
            
            int[] score = new int[rows.Length];
            string[] scorecontent = new string[rows.Length];

            for (int i = 0; i < rows.Length; i++)
            {
                score[i] = (int)rows[i]["score_id"]; // 특정 컬럼만 꺼내와 배열에 담음
                scorecontent[i] = (string)rows[i]["Score"];
                _selectdata.Rows.Add(score[i], scorecontent[i]);
            }

            

            //  _selectdata.Rows.Add(connectDB().Tables[0].Rows[a]["Score_id"]); // select한 값 넣음


           

            /*      DataRow[] rows = _selecttable.Select();
                  for (int i = 0; i < rows.Length; i++)
                  {
                      _scorecontent = (int)rows[i]["Score_ID"];
                  }
      */
         /*   for (int idx = 0; idx < connectDB().Tables[0].Rows.Count; idx++) {
                obj.Score_id = (int)connectDB().Tables[0].Rows[idx]["Score_id"];
                obj.Score = connectDB().Tables[0].Rows[idx]["Score"].ToString();
                SampleDatas.Add(obj);
                    }*/
            Name = "이름 세터";


        }

    



        public void AddContent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
           // _selectdata = new DataTable();
            _selectdata.Rows.Add("순번을 입력하세요", "내용을 입력하세요");

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
