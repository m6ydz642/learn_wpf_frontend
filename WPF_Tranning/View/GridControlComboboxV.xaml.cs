using DevExpress.Xpf.Grid;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using WPF_Tranning.ModelAndView;

namespace WPF_Tranning.View
{
    /// <summary>
    /// GridControlComboboxV.xaml에 대한 상호 작용 논리
    /// </summary>
    /// 

    public partial class GridControlComboboxV : UserControl
    {

        public string GetString { get; set; }
        public GridControlComboboxV()
        {
            InitializeComponent();

            if (DataContext is GridControlComboboxVM gridControlComboboxVM)
            {
                gridControlComboboxVM.Loading();
                gridControlComboboxVM.Help = "test";
                GetString = gridControlComboboxVM.Help;
            }
        }

        private MainWindow main;


        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PrintVisualTree(0, this);
            if (DataContext is GridControlComboboxVM gridControlComboboxVM)
            {
                gridControlComboboxVM.Help = "비하인드 코드 값 전달";
                gridControlComboboxVM.ButtonObject = TreeButton;
                 
            }
            Window mainWindow = Application.Current.MainWindow;
            MainWindow rootWindow = Application.Current.MainWindow as MainWindow;

            var TreeView = LogicalTreeHelper.FindLogicalNode(mainWindow, "TreeMainView") as TreeView;
            var txtBox2 = LogicalTreeHelper.GetChildren(mainWindow) as UserControl;
            var items = TreeView.Items;

            rootWindow.GridControlCombobox.Items.ToString();
            // var userControls = FindVisualChildren<MainWindow>(mainWindow);

            foreach (var tb in FindVisualChildren<Button>(this))
            {

            }
        }

        void PrintLogicalTree(int depth, object obj)
        {
            // Print the object with preceding spaces that represent its depth
            Debug.WriteLine(new string(' ', depth) + obj);

            // Sometimes leaf nodes aren't DependencyObjects (e.g. strings)
            if (!(obj is DependencyObject)) return;

            // Recursive call for each logical child
            foreach (object child in LogicalTreeHelper.GetChildren(
              obj as DependencyObject))
                PrintLogicalTree(depth + 1, child);
        }


        void PrintVisualTree(int depth, DependencyObject obj)
        {
            // Print the object with preceding spaces that represent its depth
            Debug.WriteLine(new string(' ', depth) + obj);

            // Recursive call for each visual child
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
                PrintVisualTree(depth + 1, VisualTreeHelper.GetChild(obj, i));
        }

    }
}
