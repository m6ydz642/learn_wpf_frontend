using AnotherWindow;
using AnotherWindow.ModelAndView;
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
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AnotherPageProject.View
{
    /// <summary>
    /// AnotherWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class AnotherPage : UserControl
    {
        public static string _className { get; set; }
        public AnotherPage()
        {
            InitializeComponent();

            if (DataContext is AnotherPageModelAndView view)
            {
                

            }
            Type type = Type.GetType(typeof(AnotherPage).FullName);
            _className = type.ToString();
        }

       /* public object SelfObject(object obj)
        {
            _className = obj.ToString();
            return obj;

        }*/

    }
}
