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
    public class PopupMainMV : INotifyPropertyChanged

    {
        public event PropertyChangedEventHandler PropertyChanged;
/*
        public delegate void DataPushEventHandler(string value);  // 메인폼 --> 자식폼 으로 값 전달 델리게이트
        public delegate void DataGetEventHandler(string item); // 자식폼 --> 메인폼으로 값 전달 델리게이트*/

        // public DataGetEventHandler DataSendEvent;

        //  public ICommand IPopupClick { get; set; }

        public PopupMainMV()
        {
            DataModel.CurrentClassPath = typeof(OtherTabsView).FullName; // 현재 접근한 클래스
            // DataSendEvent = new DataGetEventHandler(this.Form3DataAction);
            //     IPopupClick = new RelayCommand(new Action<object>(this.ClickPopup));
        }
/*        private void Form3DataAction(string item)
        {

        }*/


   /*     private void ClickPopup(object obj)
        {
            Window window = new Window();
            PopupUserControlView popupUserControlView = new PopupUserControlView();
         //   this.DataSendEvent += new DataPushEventHandler(popupUserControlView.SetActionValue1);
         // 자식에서 부모에게만 전달하는걸로 변경
            window.Content = popupUserControlView;
            window.Show();
        }*/

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
