using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.RichEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
        //public ICommand IGridSheetLoaded { get; set; }

        public MasterDetailMV()
        {
            DataModel.CurrentClassPath = typeof(MasterDetailView).FullName; // 현재 접근한 클래스
            //IGridSheetLoaded = new RelayCommand(new Action<object>(this.GridSheetControlLoaded));

            OrderDetails = MakeOrderDetails();
           // Customers = MakeCustomers2();
         //   Orders = MakeCustomers2();
            Data = MakeCustomers();
         //   OrdersList = MakeOrderLists();
        }

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


        /*     private DataTable _data;
             public DataTable Data
             {
                 get { return _data; }
                 set
                 {
                     _data = value;

                     OnPropertyChanged("Data");
                 }
             }
     */
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

        private DataTable MakeOrderDetails()
        {
            // DB에서 가져왔다 가정
            DataTable dt = new DataTable();
            dt.Columns.Add("OrderDate");
            dt.Columns.Add("Freight");
            dt.Columns.Add("ShipName");
            dt.Columns.Add("ShipCountry");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            dt.Rows.Add("갤럭시","10000","10","20");
            return dt;
        }

        private List<MainData> MakeCustomers()
        {
            // DB에서 가져왔다 가정
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName");
            dt.Columns.Add("Title");
            dt.Columns.Add("Country");
            dt.Columns.Add("BirthDate");
            dt.Columns.Add("Email");
          

            /*dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10");*/
                dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10", "nancy@example.com");
                dt.Rows.Add("test Davolio2", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
                dt.Rows.Add("test Davolio3", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
                dt.Rows.Add("test Davolio4", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
                dt.Rows.Add("test Davolio5", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
                dt.Rows.Add("test Davolio6", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
                dt.Rows.Add("test Davolio8", "Sales Representative", "Japen", "2000-07-10", "test@example.com");
            dt.Rows.Add("test Davolio9", "Sales Representative", "Japen", "2000-07-10", "test@example.com");

            List<string> DataColumns = GetColumnName(dt);

            MakeDynamicColumns_Header(DataColumns);

          //  List<MainData> list = new List<MainData>();
            _listdataset = new List<MainData>();

            foreach (DataRow row in dt.Rows)
            {
                MainData mainData = new MainData()
                {
                    FullName = row.Table.Columns.Contains("FullName") == false ? "널" : row.Field<string>("FullName"),
                    Title = row.Table.Columns.Contains("Title") == false ? "널" : row.Field<string>("Title"),
                    Country = row.Table.Columns.Contains("Country") == false ? "널" : row.Field<string>("Country"),
                    BirthDate = row.Table.Columns.Contains("BirthDate") == false ? "널" : row.Field<string>("BirthDate"),
                    Email = row.Table.Columns.Contains("Email") == false ? "널" : row.Field<string>("Email"),
                    OrderList = new List<Orders>()
                };

                DataTable dts = MakeOrderDetails();
                List<string> DataSubColumns = GetColumnName(dts);
                MakeDynamicSubColumns_Header(DataSubColumns);
                foreach (DataRow subrows in dts.Rows)
                {
                    string test = subrows.Field<string>("OrderDate");
                    mainData.OrderList.Add( 
                        new Orders()
                        {
                            OrderDate = subrows.Table.Columns.Contains("OrderDate") == false ? "널" : subrows.Field<string>("OrderDate"),
                            Freight = subrows.Table.Columns.Contains("Freight") == false ? "널" : subrows.Field<string>("Freight"),
                            ShipName = subrows.Table.Columns.Contains("ShipName") == false ? "널" : subrows.Field<string>("ShipName"),
                            ShipCountry = subrows.Table.Columns.Contains("ShipCountry") == false ? "널" : subrows.Field<string>("ShipCountry")
                        });
                }
                _listdataset.Add(mainData);

            }

            return _listdataset;
        }


    /*    private List<Orders> MakeOrderLists()
        {
            DataTable dt = MakeOrderDetails();
            List<string> DataSubColumns = GetColumnName(dt);
            MakeDynamicSubColumns_Header(DataSubColumns);

            List<Orders> list = new List<Orders>();


            foreach (DataRow row in dt.Rows)
            {
                Orders orders = new Orders()
                {
                    OrderDate = row.Table.Columns.Contains("OrderDate") == false ? "널" : row.Field<string>("OrderDate"),
                    Freight = row.Table.Columns.Contains("Freight") == false ? "널" : row.Field<string>("Freight"),
                    ShipName = row.Table.Columns.Contains("Country") == false ? "널" : row.Field<string>("ShipName"),
                    ShipCountry = row.Table.Columns.Contains("BirthDate") == false ? "널" : row.Field<string>("ShipCountry")
                };
                list.Add(orders);
            }

            return list;

        }*/


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



        
        //private List<Orders> MakeCustomers()
        //{
        //    List<Orders> list = new List<Orders>();
        //    list.Add(new Orders
        //    {
        //        OrderDate = "2021-10-31",
        //        Freight = "13",
        //        ShipName = "Something",
        //        ShipCountry = "USA"
        //    });

        //    return list;

        //}
        private List<Orders> MakeCustomers2()
        {
            // Datatable dt = new DataTable();
            //dt.Columns.Add("ContactName");
            //dt.Columns.Add("Country");
            //dt.Columns.Add("City");
            //dt.Columns.Add("Address");
            //dt.Columns.Add("Phone");
            //             dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10");


            List<Orders> list = new List<Orders>();
            list.Add(new Orders {
                OrderDate = "2021-10-31",
                Freight = "13",
                ShipName ="Something",
                ShipCountry = "USA"} );
            
       
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
