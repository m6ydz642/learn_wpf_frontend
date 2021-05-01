using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Tranning.Model
{
    class ChartBindingModel
    {
        public string Argument { get; set; }
        public double Value { get; set; }
        public static ObservableCollection<ChartBindingModel> GetDataPoints()
        {
            return new ObservableCollection<ChartBindingModel> {
                    new ChartBindingModel { Argument = "Asia", Value = 5.289D},
                    new ChartBindingModel { Argument = "Australia", Value = 2.2727D},
                    new ChartBindingModel { Argument = "Europe", Value = 3.7257D},
                    new ChartBindingModel { Argument = "North America", Value = 4.1825D},
                    new ChartBindingModel { Argument = "South America", Value = 2.1172D}
                   };
        }
    }
}
