using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace WPF_Tranning.View
{
    /// <summary>
    /// TwoGridControlView.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class TwoGridControlView : UserControl
    {
        public TwoGridControlView()
        {
            InitializeComponent();

            if (DataContext is TwoGridControlVM twoGridControlVM)
            {
                twoGridControlVM.Loading();
            }


        }




    }
}
