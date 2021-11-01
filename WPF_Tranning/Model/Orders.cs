using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Tranning.Model
{
    public class Orders
    {
        public string OrderDate { get; set; }
        public string Freight { get; set; }
        public string ShipName { get; set; }
        public string ShipCountry { get; set; }
        public List<Orders> OrdersList { get; set; }


        public Orders()
        {

        }

        public Orders(string productName, string unitPrice, string quantity, string total)
        {
            this.OrderDate = productName;
            this.Freight = unitPrice;
            this.ShipName = quantity;
            this.ShipCountry = total;
        }
    }
}
