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
using static WPF_Tranning.ModelAndView.PopupViewMV;

namespace WPF_Tranning.ModelAndView
{
    class ClickPopupViewMV : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
         public ICommand IClick { get; set; }
        public DataGetEventHandler DataGetEvent; // 자식에서 부모창에게 전달


        public ClickPopupViewMV()
        {
            IClick = new RelayCommand(new Action<object>(this.Send));

        }

        private void Send(object obj)
        {
            DataGetEvent("자식에서 부모에게 전달내용");
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
