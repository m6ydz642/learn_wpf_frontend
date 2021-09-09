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
using System.Windows.Input;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{
    class OtherTabsVM : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand IWord { get; set; }
        public ICommand ICheckValue { get; set; }
        public virtual List<object> ISelectedItems { get; set; }

        public OtherTabsVM()
        {
            DataModel.CurrentClassPath = typeof(OtherTabsView).FullName; // 현재 접근한 클래스

            IWord = new RelayCommand(new Action<object>(this.RoadRichEditControl));
            ICheckValue = new RelayCommand(new Action<object>(this.CheckValue));

            ISelectedItems = new List<object>(); // 이렇게 둬도 값 editvalue값 바인딩 해서 가져와짐 (twowayBinding)

        }

        private void CheckValue(object obj)
        {
            // 이상한 바인딩이네 진짜 ㅡㅡ; 
            var list = ISelectedItems;
            for (int i = 0; i < list.Count; i++)
            {
                string multivalue = ISelectedItems[i].ToString();
            }
      

            if (obj is ListBoxEdit edit)
            {
               
            }
        }

        private void RoadRichEditControl(object obj)
        {
            var richEditControl1 = (RichEditControl)obj;
            string htmlText = richEditControl1.HtmlText; // html텍스트 변환
        }


    }
}
