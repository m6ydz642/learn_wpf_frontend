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
            DataContext = new GridCotrolBandModelAndView();
        }

        private void GirdControlBand_ToolTipOpening(object sender, ToolTipEventArgs e)
        {
            Ellipse ell = new Ellipse();
            if (sender.GetType().FullName.Equals("System.Windows.Shapes.Ellipse"))
            {
                ell = (Ellipse)sender;
                ell.Fill = Brushes.Blue;
            }
            else if (sender.GetType().FullName.Equals(
                                     "System.Windows.Controls.ToolTip"))
            {
                ToolTip t = (ToolTip)sender;
                Popup p = (Popup)t.Parent;
                ell = (Ellipse)p.PlacementTarget;
                ell.Fill = Brushes.Blue;
            }
        }
    }
}
