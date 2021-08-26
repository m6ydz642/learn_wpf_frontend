using DevExpress.Xpf.RichEdit;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{
    class FileCopyVM : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand IWord { get; set; }
        public FileCopyVM()
        {
            DataModel.CurrentClassPath = typeof(FileCopyV).FullName; // 현재 접근한 클래스

            IWord = new RelayCommand(new Action<object>(this.RoadRichEditControl));


        }

        private void RoadRichEditControl(object obj)
        {
            var richEditControl1 = (RichEditControl)obj;
            string htmlText = richEditControl1.HtmlText; // html텍스트 변환
        }


    }
}
