using DevExpress.Xpf.Grid;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning
{
    /// <summary>
    /// GridControlWeek.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class GridControlBandView : UserControl
    {
        public GridControlBandView()
        {
            InitializeComponent();
            GridOptionsView a = new GridOptionsView();
            a.ShowErrorPanel = (DevExpress.Utils.DefaultBoolean)1;
            doublescoretableview.ItemsSourceErrorInfoShowMode = 0;
            //  DataContext = new GridCotrolBandModelAndView();
            if (DataContext is GridCotrolBandModelAndView model)
            {
                /*              model.ComboBoxSelect.Add("데이터모드 1");
                              model.ComboBoxSelect.Add("데이터모드 2");
                              model.ComboMode = "데이터모드 1";*/
                model.GetData();
            }
            else
            {

            }

        }

        private void UserControl_Unloaded(object sender, RoutedEventArgs e)
        {

        }

        private void GridColumn_Validate(object sender, DevExpress.Xpf.Grid.GridCellValidationEventArgs e)
        {

        }

        private void GridColumn_Validate_1(object sender, DevExpress.Xpf.Grid.GridCellValidationEventArgs e)
        {
             e.ErrorContent = string.Format("비하인드 코드 오류 메시지 (입력 형식 오류표시)");
            
        }

        private void doublescoretableview_InvalidRowException(object sender, DevExpress.Xpf.Grid.InvalidRowExceptionEventArgs e)
        {
            e.ExceptionMode = ExceptionMode.NoAction;
            

        }


    }
}
