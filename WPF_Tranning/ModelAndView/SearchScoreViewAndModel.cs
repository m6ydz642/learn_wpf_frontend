using DevExpress.Xpf.Editors;
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
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{
    public class ScoreTableModel{
        public string Score_id;
        public string Score;
    }

    public interface DBContext
    {
        List<ScoreTableModel> GetDatas();
    }

    public interface DBContextDataTable
    {
        List<ScoreTableModel> GetDataTable();
    }
    public class SearchScoreViewAndModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand ComboboxLoaded { get; set; }
        public ICommand ComboSelectedEvent { get; set; }

        /**********************************************************************/
        string AppconfigDBSetting;
        /**********************************************************************/

        public SearchScoreViewAndModel()
        {
            AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결
            ComboboxLoaded = new RelayCommand(new Action<object>(this.GetComboboxLoaded));
            ComboSelectedEvent = new RelayCommand(new Action<object>(this.ComboSelectBinding));
            GetScoreInfomation = GetScoreInfo().Tables[0]; // 콤보박스 리스트에 들어갈 스코어 전체 정보 (표시는 Score_id만 되게 xaml에서 설정 해놈)

            DataModel.CurrentClassPath = typeof(SearchScoreView).FullName; // 현재 접근한 클래스

        }
        /************************************************************************************/
        // mock 객체 테스트 용
        private DBContext result;
        private DBContextDataTable datatableresult;


        public SearchScoreViewAndModel(DBContext context) // list타입
        {
            result = context;
        }

        public SearchScoreViewAndModel(DBContextDataTable context) // 데이터 테이블 타입
        {
            datatableresult = context;
        }



        public string GetSum() // 스트링 리턴
        { 
            string result = "";
            foreach (ScoreTableModel b in this.result.GetDatas())
            {
                result += b.Score_id;
            }
            return result;
        }

        public DataTable GetTableReturn() // 테이블 리턴
        {
            DataTable dt = new DataTable() ;


            dt.Columns.Add("Score_id");
            dt.Columns.Add("Score점수");

            foreach (ScoreTableModel model in datatableresult.GetDataTable())
            {
     
                dt.Rows.Add(model.Score_id, model.Score);
            }
            return dt;
        }

        /************************************************************************************/
        private void GetComboboxLoaded(object obj) // 콤보박스 로딩 이벤트
        {
            var convert = (ComboBoxEdit)obj;
            string test = (string)convert.EditValue;
       //     int test2 = convert.SelectedIndex;

 
            DataRowView oDataRowView = convert.SelectedItem as DataRowView;

            if (oDataRowView != null)
            {
                string Score_id = oDataRowView.Row["Score_id"].ToString(); 
                string Score = oDataRowView.Row["Score"].ToString();
                ComboBoxSelect = GetSearchScoreInfo(Int32.Parse(Score_id)).Tables[0];

            }
            else
            {
                MessageBox.Show("가져올 데이터가 없습니다!");
            }
        }

        public void ComboSelectBinding(object obj) // 콤보 박스 선택시 이벤트 호출
        {
            var convert = (ComboBoxEdit)obj;
            DataRowView oDataRowView = convert.SelectedItem as DataRowView;

            if (oDataRowView != null)
            {
                string Score_id = oDataRowView.Row["Score_id"].ToString(); // 임시 데이터 set으로 함
                string Score = oDataRowView.Row["Score"].ToString();
                ComboBoxSelect = GetSearchScoreInfo(Int32.Parse( Score_id) ).Tables[0];
            }
            else
            {
                MessageBox.Show("가져올 데이터가 없습니다");
            }

        }


        #region 바인딩


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


        #endregion


        #region DB연결 
        public DataSet GetSearchScoreInfo(int score_id)
        {
            string selectQuery = ConfigurationManager.AppSettings["Search_Score"]; // appkey 이름 호출 
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlCommand cmd = new SqlCommand("Score_Search", connection); // 프로시저 호출
            cmd.CommandType = CommandType.StoredProcedure; // 프로시저 타입 선언
            cmd.Parameters.Add("@Score_id", SqlDbType.Int).Value = score_id; 


            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(cmd); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }


        public DataSet GetScoreInfo() // 스코어 전체 정보
        {
            string selectQuery = ConfigurationManager.AppSettings["selectScore"];
            SqlConnection connection = new SqlConnection(AppconfigDBSetting);
            connection.Open(); // DB연결

            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(selectQuery, connection); // DB통로
            DataSet dataSet = new DataSet();
            sqlDataAdapter.Fill(dataSet); // dataset으로 채움
            return dataSet;
        }



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
