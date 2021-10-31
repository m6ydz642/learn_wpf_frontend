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
    public class MasterDetailMV : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        //public ICommand IGridSheetLoaded { get; set; }

        public MasterDetailMV()
        {
            DataModel.CurrentClassPath = typeof(MasterDetailView).FullName; // 현재 접근한 클래스
            //IGridSheetLoaded = new RelayCommand(new Action<object>(this.GridSheetControlLoaded));

            OrderDetails = MakeOrderDetails();
            Customers = MakeCustomers();
            Orders = MakeCustomers();
            Data = MakeCustomers();

        }

        private DataTable _data;
        public DataTable Data  
        {
            get {return _data; }
            set
            {
                _data = value;

                 OnPropertyChanged("Data");
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
        private DataTable _orders;
        public DataTable Orders
        {
            get {return _orders; }
            set
            {
                _orders = value;

                 OnPropertyChanged("Orders");
            }
        }

        private DataTable MakeOrderDetails()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ProductName");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Total");
            dt.Rows.Add("갤럭시","10000","10","20");
            return dt;
        }

        private DataTable MakeCustomers()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("FullName");
            dt.Columns.Add("Title");
            dt.Columns.Add("Country");
            dt.Columns.Add("BirthDate");
            dt.Columns.Add("Email");
            dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10", "nancy@example.com");
            return dt;
        }   
        private DataTable MakeCustomers2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("ContactName");
            dt.Columns.Add("Country");
            dt.Columns.Add("City");
            dt.Columns.Add("Address");
            dt.Columns.Add("Phone");
            dt.Rows.Add("Nancy Davolio", "Sales Representative", "USA", "2000-07-10");
            return dt;
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
