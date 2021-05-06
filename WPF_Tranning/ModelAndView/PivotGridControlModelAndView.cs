using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Tranning.ModelAndView
{
    class PivotGridControlModelAndView
    {
        string AppconfigDBSetting = ConfigurationManager.ConnectionStrings["connectDB"].ConnectionString; // DB연결


        public PivotGridControlModelAndView()
        {
            SelectContent = GetScoreInfo().Tables[0]; // 내용꺼낼 용도 데이터 테이블
            // DataRow[] DataRows = SelectContent.Select("DISTINCT Score");
            DataView view = SelectContent.DefaultView;
            //   view.RowFilter = "Score = 'AAAAAA' ";

            //  SelectContent = view.ToTable(true, "체크박스", "Score_id" , "Score");
            //   SelectContent = view.ToTable(true);
            //    SelectContent = SelectContent.DefaultView.ToTable(true, "Score");

        }
        #region 중복검사 다른방법
        /*   private bool ColumnEqual(object A, object B)
           {
               // Compares two values to see if they are equal. Also compares DBNULL.Value.             
               if (A == DBNull.Value && B == DBNull.Value) //  both are DBNull.Value  
                   return true;
               if (A == DBNull.Value || B == DBNull.Value) //  only one is BNull.Value  
                   return false;
               return (A.Equals(B)); // value type standard comparison  
           }
           public DataTable SelectDistinct(DataTable SourceTable, string FieldName)
           {
               // Create a Datatable – datatype same as FieldName  
               DataTable dt = new DataTable(SourceTable.TableName);
               dt.Columns.Add(FieldName, SourceTable.Columns[FieldName].DataType);


               // Loop each row & compare each value with one another  
               // Add it to datatable if the values are mismatch  
               object LastValue = null;
               foreach (DataRow dr in SourceTable.Select("", FieldName))
               {
                   if (LastValue == null || !(ColumnEqual(LastValue, dr[FieldName])))
                   {
                       LastValue = dr[FieldName];
                       dt.Rows.Add(new object[] { LastValue });
                   }
               }
               return dt;
           }*/
        #endregion

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



    }
}
