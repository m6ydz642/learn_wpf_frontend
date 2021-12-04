using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_Tranning
{
    /// <summary>
    /// App.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class App : Application
    {
        static Type[] types;
        protected async override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            types = new Type[] { typeof(GridControl), typeof(System.Windows.Forms.PropertyGrid) };
            await ThemeManager.PreloadThemeResourceAsync("Office2019Colorful");
        }

    }
}

