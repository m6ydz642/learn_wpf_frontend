using DevExpress.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPF_Tranning.View
{
    /// <summary>
    /// Interaction logic for SpreadsheetControl.xaml
    /// </summary>
    public partial class SpreadsheetControlView : UserControl
    {
        public SpreadsheetControlView()
        {
            InitializeComponent();
        }

        private void spreadsheetcontrol_CellValueChanged(object sender, DevExpress.XtraSpreadsheet.SpreadsheetCellEventArgs e)
        {

        }

        private void spreadsheetcontrol_Loaded(object sender, RoutedEventArgs e)
        {
   

        
        }
    }
}
