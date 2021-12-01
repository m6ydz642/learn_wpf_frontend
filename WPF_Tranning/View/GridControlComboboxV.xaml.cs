using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning.View
{
    /// <summary>
    /// GridControlComboboxV.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class GridControlComboboxV : UserControl
    {


        public GridControlComboboxV()
        {
            InitializeComponent();
 //             this.DataContext = new GridControlComboboxVM(); 

            if (DataContext is GridControlComboboxVM gridControlComboboxVM)
            {
                gridControlComboboxVM.Loading();
            
            }


        }




    }
}
