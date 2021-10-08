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
using static WPF_Tranning.ModelAndView.PopupMainMV;
using static WPF_Tranning.View.PopupMainView;

namespace WPF_Tranning.ModelAndView
{
    class PopupSubMV : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
        public DataGetEventHandler DataSetVM_Main; // 서브뷰모델에서 메인뷰모델에게 전달

        public ICommand IClick { get; set; }
     


        public PopupSubMV()
        {
            IClick = new RelayCommand(new Action<object>(this.Click));
           // DataSetVM_Main += new DataGetEventHandler(SetMainData);

        }

        private void SetMainData(string main)
        {

        }

        private void Click(object obj)
        {
            DataSetVM_Main("서브뷰모델 에서 메인 뷰모델에게 전달");
        }

        private void OnPropertyChanged(string propertyName)
        {
            //  MessageBox.Show("OnPropertyChanged 호출");
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }
}
