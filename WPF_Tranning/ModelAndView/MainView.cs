using System;
using System.Collections.Generic;
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
    class MainView 
    {
        public ICommand TestBinding  { get; set; }
        public ICommand CheckBinding { get; set; }
        /**********************************************************************/
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
        /**********************************************************************/
        public DataTable _datatable;

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



    }
}
