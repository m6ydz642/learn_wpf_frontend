using DevExpress.Spreadsheet;
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
        public ICommand IDeleteSheets { get; set; }
        public SpreadsheetControlMV()
        {
            DataModel.CurrentClassPath = typeof(ChartBindingView).FullName; // 현재 접근한 클래스
            IGridSheetLoaded = new RelayCommand(new Action<object>(this.GridSheetControlLoaded));
            IDeleteSheets = new RelayCommand(new Action<object>(this.deleteSheets_add));
        }
        // private IWorkbook workbook { get; set; } 
        // 전역변수로 해도 되고 이벤트 파라메터에서 다시 형변환해서 써도 되고 
        // MakeDataWorkBooks에서 사용하는 것을 형변환 해서 써도 됨 (어짜피 workbook호출해서 쓰면 SpreadsheetControl에 반영 되어있음)

        private void deleteSheets_add(object obj)
        {
         //   workbook.Worksheets.Remove(workbook.Worksheets["TestSheet1"]);
         if (obj is SpreadsheetControl control)
            {
                IWorkbook workbook = control.Document; // 기존 로딩했던 문서를 삭제시 다시 불러와서 작업 할 수 있게 로딩함
                workbook.Worksheets.Remove(workbook.Worksheets["TestSheet1"]);

                // 삭제후 다른 데이터로 동일 시트 생성 및 추가 

                workbook.Worksheets.Add().Name = "TestSheet1";
                workbook.Worksheets.ActiveWorksheet.Cells.Style.NumberFormat = "mm/dd/테스트";
                workbook.Worksheets.ActiveWorksheet.Cells["D11"].Value = "삭제 후 추가";
                workbook.Worksheets.ActiveWorksheet.Cells["D15"].Value = "삭제 후 workbook값 추가";
                string getSheetsName = workbook.Worksheets.ActiveWorksheet.ToString(); // 값 확인용
                Worksheet worksheet = workbook.Worksheets["TestSheet1"]; // sheets이름 얻어와 확인 (workbook으로 해도 되고)
            }
        }

        private void GridSheetControlLoaded(object obj)
        {
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());
            
            if (obj is SpreadsheetControl sheetcontrol) // 형변환
            {
                using (BinaryReader reader = new BinaryReader
                    (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WPF_Tranning.Resources.output.xlsx")))
                {
                    sheetcontrol.LoadDocument(reader.BaseStream); // resources파일에 추가된 엑셀을 stream에 읽어 그리드 엑셀에 표시
                                                                  // (엑셀 리소스 포함 리소스 처리)
                    MakeDataWorksheets(sheetcontrol);
                    MakeDataWorkBooks(sheetcontrol);
                }
            }
        }

        private void MakeDataWorkBooks(SpreadsheetControl sheetcontrol)
        {
            // workbook 방식 생성 
            IWorkbook workbook = sheetcontrol.Document;

          
            workbook.Worksheets.Add().Name = "TestSheet1";
            workbook.Worksheets.ActiveWorksheet.Cells.Style.NumberFormat = "mm/dd/테스트";
            workbook.Worksheets.ActiveWorksheet.Cells["D11"].Value = "2021-08-11"; 
            workbook.Worksheets.ActiveWorksheet.Cells["D15"].Value = "workbook값 추가"; 
            string getSheetsName = workbook.Worksheets.ActiveWorksheet.ToString(); // 값 확인용
            Worksheet worksheet = workbook.Worksheets["TestSheet1"]; // sheets이름 얻어와 확인 (workbook으로 해도 되고)


            string getWorksheetValue = worksheet.Cells["D11"].Value.ToString();
            worksheet.Cells["D13"].Value = "Workbook->WorkSheet값 추가";
        }

        private void MakeDataWorksheets(SpreadsheetControl sheetcontrol)
        {
            Worksheet worksheet = sheetcontrol.Document.Worksheets[0];
            CellRange range = worksheet.Range["D5:D10"];

            worksheet.Cells["D5"].Value = "2021-08-11";
            worksheet.Cells["D10"].Value = DateTime.Now.ToString("yyyy-MM-dd");

            /*  CellRange range = worksheet.Range["D5:D10"];
                Formatting formatting = range.BeginUpdateFormatting();
                range.EndUpdateFormatting(formatting);
            */
        }
    }
}
