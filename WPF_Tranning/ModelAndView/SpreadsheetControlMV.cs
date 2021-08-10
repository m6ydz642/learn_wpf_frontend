using DevExpress.Xpf.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{
    class SpreadsheetControlMV
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand IGridSheetLoaded { get; set; }
        public SpreadsheetControlMV()
        {
            DataModel.CurrentClassPath = typeof(ChartBindingView).FullName; // 현재 접근한 클래스
            IGridSheetLoaded = new RelayCommand(new Action<object>(this.GridSheetControlLoaded));
        }

        private void GridSheetControlLoaded(object obj)
        {
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());
            
            if (obj is SpreadsheetControl sheetcontrol)
            {
                using (BinaryReader reader = new BinaryReader
                    (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WPF_Tranning.Resources.output.xlsx")))
                {
                    sheetcontrol.LoadDocument(reader.BaseStream); // resources파일에 추가된 엑셀을 stream에 읽어 그리드 엑셀에 표시
                                                                  // (엑셀 리소스 포함 리소스 처리)
                }
            }
        }
    }
}
