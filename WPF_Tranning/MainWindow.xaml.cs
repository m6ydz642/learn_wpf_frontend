using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;

namespace WPF_Tranning
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainView(); // main view호출   
        }

        private void pnlMainGrid_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void cbAllFeatures_Checked(object sender, RoutedEventArgs e)
        {

            bool newVal = (cbAllFeatures.IsChecked == true);
            cbFeatureAbc.IsChecked = newVal;
            cbFeatureXyz.IsChecked = newVal;
            cbFeatureWww.IsChecked = newVal;
        }

        public bool Uncheck()
        {
            cbFeatureAbc.IsChecked = false; // false로 만들면 해제됨
            cbFeatureXyz.IsChecked = false;
            cbFeatureWww.IsChecked = false;
            return false;
        } 
        private void cbAllFeatures_Unchecked(object sender, RoutedEventArgs e)
        {
            Uncheck();

            
        }

      

        private void cbFeature_CheckedChanged(object sender, RoutedEventArgs e)
        {
        
           // cbAllFeatures.IsChecked = null;
            if ((cbFeatureAbc.IsChecked == true) && (cbFeatureXyz.IsChecked == true) && (cbFeatureWww.IsChecked == true))
                cbAllFeatures.IsChecked = true;
            if ((cbFeatureAbc.IsChecked == false) && (cbFeatureXyz.IsChecked == false) && (cbFeatureWww.IsChecked == false))
                cbAllFeatures.IsChecked = false;
        }
    }
}
