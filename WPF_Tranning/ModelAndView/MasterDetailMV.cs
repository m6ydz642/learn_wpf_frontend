using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.RichEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{

    public class DynamicColumns
    {
        public string FieldName { get; set; }
        public string Header { get; set; }

    }

    public class MasterDetailMV : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;

        public MasterDetailMV()
        {
            DataModel.CurrentClassPath = typeof(MasterDetailView).FullName; // 현재 접근한 클래스
            DeBugTrace.TraceWriteLine("비동기 메소드 시작");

            // 로딩바 문제 있어서 뺌
            //LoadingBar loadingBar = new LoadingBar();
            //SplashScreenManager manager= loadingBar.CallLoading();

            CallAsync();
            // CallSync();

            // 로딩바 문제 있어서 뺌
            //if (manager != null)
            //    manager.Close();


            DeBugTrace.TraceWriteLine("비동기 메소드 종료");
        }
        #region 프로퍼티
        private List<MainData> _data;
        public List<MainData> Data
        {
            get { return _data; }
            set
            {
                _data = value;
                OnPropertyChanged("Data");
            }
        }

        private List<DynamicColumns> _columns;
        public List<DynamicColumns> Columns
        {
            get { return _columns; }
            set
            {
                _columns = value;

                OnPropertyChanged("Columns");
            }
        }

        private List<DynamicColumns> _subcolumns;
        public List<DynamicColumns> SubColumns
        {
            get { return _subcolumns; }
            set
            {
                _subcolumns = value;

                OnPropertyChanged("SubColumns");
            }
        }

        private DataTable _OrderDetails;
        public DataTable OrderDetails  
        {
            get {return _OrderDetails; }
            set
            {
                _OrderDetails = value;

                 OnPropertyChanged("OrderDetails");
            }
        }
                
        private DataTable _customers;
        public DataTable Customers
        {
            get {return _customers; }
            set
            {
                _customers = value;

                 OnPropertyChanged("Customers");
            }
        }
        private List<Orders> _orders;
        public List<Orders> OrdersList
        {
            get { return _orders; }
            set
            {
                _orders = value;

                OnPropertyChanged("OrdersList");
            }
        }
        private List<MainData> _listdataset;
        #endregion

        /// <summary>
        /// 동기
        /// </summary>
        private void CallSync()
        {
            Data = MakeCustomers();
        }

        /// <summary>
        /// 비동기
        /// </summary>
        async private void CallAsync()
        {
            await Task.Run(() => {
                Data = MakeCustomers();
                DeBugTrace.TraceWriteLine("비동기 메소드 작동중");
            });
        }
        private DataTable MakeOrderDetails()
        {
            // DB에서 가져왔다 가정
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderId");
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("Freight");
            dt.Columns.Add("ShipName");
            dt.Columns.Add("ShipCountry");
            dt.Rows.Add("1", "갤럭시1","10000","10","20");
            dt.Rows.Add("3", "갤럭시2","10000","10","20");
            dt.Rows.Add("4", "갤럭시3","10000","10","20");
            dt.Rows.Add("1", "갤럭시4","10000","10","20");
            dt.Rows.Add("8", "아이폰1","10000","10","20");
            dt.Rows.Add("7", "아이폰2", "10000","10","20");
            dt.Rows.Add("6", "아이폰3", "10000","10","20");
            dt.Rows.Add("5", "아이폰4", "10000","10","20");
            return dt;
        }

        private DataTable MakeDataTable()
        {
            // DB에서 가져왔다 가정

            DataTable dt = new DataTable();
            dt.Columns.Add("OrderId");
            dt.Columns.Add("FullName");
            dt.Columns.Add("Title");
            dt.Columns.Add("Country");
            dt.Columns.Add("BirthDate");
            dt.Columns.Add("Email");


            /*dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10");*/
            dt.Rows.Add("1", "Nancy Davolio", "Sales Representative", "USA", "2000-07-10", "nancy@example.com");
            dt.Rows.Add("3", "test Davolio2", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("4", "test Davolio3", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("5", "test Davolio4", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("6", "test Davolio5", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("7", "test Davolio6", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("8", "test Davolio8", "Sales Representative", "Japen", "2000-07-10", "test@example.com");

            for (int i = 9; i < 100000; i++)
            {
                dt.Rows.Add(i.ToString(), "test Davolio" + i.ToString(), "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            }


            return dt;
        }
        private List<MainData> MakeCustomers()
        {

            DataTable dt = MakeDataTable();
            DataTable dt2 = MakeOrderDetails();


            List<string> DataColumns = GetColumnName(dt);
            List<string> DataSubColumns = GetColumnName(dt2);

            MakeDynamicSubColumns_Header(DataSubColumns);
            MakeDynamicColumns_Header(DataColumns);

            List<MainData> list = MakeMainList_SubDetails(dt, dt2);



            return list;
        }

        private List<MainData> MakeMainList_SubDetails(DataTable dt, DataTable dts)
        {
            List<MainData> _listdataset = new List<MainData>();

            foreach (DataRow row in dt.Rows)
            {
                string orderid = row.Field<string>("OrderId");

                MainData mainData = new MainData()
                {
                    FullName = row.Table.Columns.Contains("FullName") == false ? "널" : row.Field<string>("FullName"),
                    Title = row.Table.Columns.Contains("Title") == false ? "널" : row.Field<string>("Title"),
                    Country = row.Table.Columns.Contains("Country") == false ? "널" : row.Field<string>("Country"),
                    BirthDate = row.Table.Columns.Contains("BirthDate") == false ? "널" : row.Field<string>("BirthDate"),
                    Email = row.Table.Columns.Contains("Email") == false ? "널" : row.Field<string>("Email"),
                    OrderList = new List<Orders>()
                };


                foreach (DataRow subrows in dts.Rows)
                {
                    string SubOrderId = subrows.Field<string>("OrderId");
                    if (orderid == SubOrderId)
                    {
                        mainData.OrderList.Add(
                        new Orders()
                        {
                            OrderDate = subrows.Table.Columns.Contains("OrderDate") == false ? "널" : subrows.Field<string>("OrderDate"),
                            Freight = subrows.Table.Columns.Contains("Freight") == false ? "널" : subrows.Field<string>("Freight"),
                            ShipName = subrows.Table.Columns.Contains("ShipName") == false ? "널" : subrows.Field<string>("ShipName"),
                            ShipCountry = subrows.Table.Columns.Contains("ShipCountry") == false ? "널" : subrows.Field<string>("ShipCountry")
                        });
                    }
                }
                _listdataset.Add(mainData);

            }

            return _listdataset;
        }




        private void MakeDynamicSubColumns_Header(List<string> columns)
        {
            SubColumns = new List<DynamicColumns>();
            string header = string.Empty;
            for (int i = 0; i < columns.Count; i++)
            {
                switch (columns[i])
                {
                    case "OrderDate":
                        header = "주문일";
                        break;

                    case "Freight":
                        header = "화물";
                        break;

                    case "ShipName":
                        header = "배송명";
                        break;
                    case "ShipCountry":
                        header = "배송국가";
                        break;
                    case "ShipCity":
                        header = "배송지역";
                        break;
                    default:
                        header = string.Empty;
                        break;
                }

                SubColumns.Add(new DynamicColumns
                {
                    FieldName = columns[i],
                    Header = header

                });
            }
        }
        private void MakeDynamicColumns_Header(List<string> columns)
        {
            Columns = new List<DynamicColumns>();
            string header = string.Empty;
            for (int i = 0; i < columns.Count; i++)
            {
                switch (columns[i])
                {
                    case "FullName":
                        header = "풀네임";
                        break;
                         
                    case "Country":
                        header = "나라";
                        break;

                    case "Title":
                        header = "제목";
                        break; 
                    case "BirthDate":
                        header = "생일";
                        break;       
                    case "Email":
                        header = "이메일";
                        break;
                    default :
                        header = string.Empty;
                             break;
                }

                Columns.Add(new DynamicColumns
                {
                    FieldName = columns[i],
                    Header = header
                    
                });
            }
        }

        /// <summary>
        /// DataTable에서 컬럼 이름 얻어오는 메서드
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        private List<string> GetColumnName(DataTable dt)
        {

            List<string> list = new List<string>();

            string ColumnName = string.Empty;
            foreach (DataColumn column in dt.Columns)
            {
                ColumnName = column.ColumnName;
                list.Add(ColumnName);
            }
            return list;

        }

 

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }


    }
}
