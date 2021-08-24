using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{
    class FileCopyVM
    {

       
        public FileCopyVM()
        {
            DataModel.CurrentClassPath = typeof(FileCopyV).FullName; // 현재 접근한 클래스
        }
    }
}
