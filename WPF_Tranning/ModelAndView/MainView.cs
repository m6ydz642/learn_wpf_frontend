using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace WPF_Tranning
{
   
    class MainView : INotifyPropertyChanged
    {
        public ICommand TestBinding  { get; set; }
        public ICommand CheckBinding { get; set; }

        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public DataTable _datatable;

        public DataTable _selecttable;

        bool checkedVar = false;

        public event PropertyChangedEventHandler PropertyChanged;

        private int _score_id;
        private string _score;
        private bool mIsSelected;

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

        public bool IsSelected
        {
            get { return mIsSelected; }
            set
            {
                mIsSelected = value;
                OnPropertyChanged("IsSelected");
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

        public DataTable SelectTable
        {
            get {/* MessageBox.Show("데이터 테이블");*/ return _selecttable; }
            set
            {
                _selecttable = value;

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


        public MainView()
        {
            TestBinding = new RelayCommand(new Action<object>(this.OnClickEvent));
            _selecttable = connectDB().Tables[0]; // select한 값 넣음
            CheckBinding = new RelayCommand(new Action<object>(this.OnClickEvent));
  

            /*DataRow[] rows = _selecttable.Select();
            for (int i = 0; i < rows.Length; i++)
            {
                ScoreID = (int)rows[i]["Score_ID"];
            }*/

            // ScoreID = 100;

        }

      /*  public MainView(MainWindow window)
        {
            this.window = window;

        }
*/

        public void OnClickEvent(object obj) // new Action<Object>타입으로 넣어서 여기도 대리자 형에 맞게 넣어야 됨
        {
            MessageBox.Show("onclick 이벤트 호출 : " + obj);

        }

        public void test(object obj)
        {
            MessageBox.Show("test");
        }

        private void Notify(string propertyName)
        {
            // take a copy to prevent thread issues
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
