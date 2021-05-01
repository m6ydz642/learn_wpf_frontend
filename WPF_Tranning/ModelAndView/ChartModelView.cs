using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tranning.Model;

namespace WPF_Tranning.ModelAndView
{
    class ChartModelView
    {
        public ObservableCollection<ChartBindingModel> Data { get; private set; }

        public ChartModelView()
        {
            this.Data = ChartBindingModel.GetDataPoints();
        }
    }
}
