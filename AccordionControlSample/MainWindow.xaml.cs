using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DevExpress.Xpf.Accordion;
using DevExpress.Xpf.Core;

namespace ChildrenPath
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : ThemedWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            DependencyPropertyDescriptor dpd = DependencyPropertyDescriptor.FromProperty(AccordionControl.IsExpandedProperty, typeof(AccordionControl));
            if (dpd != null)
            {
                dpd.AddValueChanged(Menu, OnIsExpandedChanged);
                dpd.AddValueChanged(LastMenuHistory, OnIsExpandedChanged2);
            }
        }

        private void OnIsExpandedChanged(object sender, EventArgs e)
        {
            if (Menu.IsExpanded)
            {
                tabControl.TabStripPlacement = Dock.Top;
                LastMenuHistory.IsExpanded = true; // fold menu

                Thickness margin = new Thickness(0, 0, -11, 64); //0,0,-11,64
                Menu.Margin = margin;

                MainmenuTabitem.FontSize = 15;
                historyworkTabitem.FontSize = 15;

            }
            else
            {
                tabControl.TabStripPlacement = Dock.Left;
                LastMenuHistory.IsExpanded = false; // unfold
                Thickness margin = new Thickness(-11, 0, -11, 64); //0,0,-11,64
                Menu.Margin = margin;

                MainmenuTabitem.FontSize = 13;
                historyworkTabitem.FontSize = 13;

            }

        }

        private void OnIsExpandedChanged2(object sender, EventArgs e)
        {
            if (LastMenuHistory.IsExpanded)
            {
                tabControl.TabStripPlacement = Dock.Top;
                Menu.IsExpanded = true;

                Thickness margin = new Thickness(0, 0, -11, 64); //0,0,-11,64
                Menu.Margin = margin;

                MainmenuTabitem.FontSize = 15;
                historyworkTabitem.FontSize = 15;
            }
            else
            {
                tabControl.TabStripPlacement = Dock.Left;
                Menu.IsExpanded = false;

                Thickness margin = new Thickness(-11, 0, -11, 64); //0,0,-11,64
                Menu.Margin = margin;

                MainmenuTabitem.FontSize = 13;
                historyworkTabitem.FontSize = 13;
            }

        }


        private void DXTabControl_TabDropping(object sender, TabControlTabDroppingOnEmptySpaceEventArgs e)
        {

        }
    }
}
