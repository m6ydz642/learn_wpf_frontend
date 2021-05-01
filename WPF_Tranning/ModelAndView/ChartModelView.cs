using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tranning.Model;

namespace WPF_Tranning.ModelAndView
{
    class ChartModelView
    {
        // 참고사이트 : https://docs.devexpress.com/WPF/9757/controls-and-libraries/charts-suite/chart-control/getting-started/lesson-1-bind-chart-series-to-data
        public DataTable Data { get; set; }

        ChartBindingModel model;
        public DataTable GetDataPoints()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Argument");
            dt.Columns.Add("Value");

            dt.Rows.Add( model.Argument = "Asia", model.Value = 5.289D);
            dt.Rows.Add( model.Argument = "Australia", model.Value = 2.2727D);
            dt.Rows.Add( model.Argument = "Europe", model.Value = 3.7257D);
            dt.Rows.Add( model.Argument = "North America", model.Value = 3.7257D);
            dt.Rows.Add( model.Argument = "South America", model.Value = 2.1172D);


            return dt;
        
        }
        public ChartModelView()
        {
            model = new ChartBindingModel();
            Data = GetDataPoints();
            
        }
    }
}
