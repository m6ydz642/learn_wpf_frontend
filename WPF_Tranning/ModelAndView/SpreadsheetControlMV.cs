using ClosedXML.Excel;
using DevExpress.Mvvm;
using DevExpress.Spreadsheet;
using DevExpress.Xpf.Core;
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
using System.Windows;
using System.Windows.Input;
using WPF_Tranning.Model;
using WPF_Tranning.View;

namespace WPF_Tranning.ModelAndView
{    // v1:
    public class Source
    {

    }

    class SpreadsheetControlMV
    {
        public event EventHandler Foo;

        public void Bar()
        {
            EventHandler handler = Foo;
            if (handler != null) handler(this, RoutedEventArgs.Empty);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public ICommand IGridSheetLoaded { get; set; }
        public ICommand IDeleteSheets { get; set; }
        public ICommand IExportClosedXML_Sheets { get; set; }
        public ICommand ICreateNewDocument { get; set; }
        public ICommand IRollBack { get; set; }
        public ICommand IBinaryLoadExcel { get; set; }
        public ICommand IBinaryLoadExcel2 { get; set; }

        public SpreadsheetControlMV()
        {
            DataModel.CurrentClassPath = typeof(ChartBindingView).FullName; // 현재 접근한 클래스
            IGridSheetLoaded = new RelayCommand(new Action<object>(this.GridSheetControlLoaded));
            IDeleteSheets = new RelayCommand(new Action<object>(this.deleteSheets_add));
            IExportClosedXML_Sheets = new RelayCommand(new Action<object>(this.ExportClosedXML_Sheets));
            ICreateNewDocument = new RelayCommand(new Action<object>(this.CreateNewDocument));
            IRollBack = new RelayCommand(new Action<object>(this.RollBack));
            IBinaryLoadExcel = new RelayCommand(new Action<object>(this.BinaryCreateNewDocument));
            IBinaryLoadExcel2 = new RelayCommand(new Action<object>(this.BinaryCreateNewDocument2));
            DataModel.CurrentClassPath = typeof(SpreadsheetControlView).FullName; // 현재 접근한 클래스

        }

     
   

        private void RollBack(object obj)
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
            } // event test branch test2
        }

        private void GridSheetControlLoaded(object obj)
        {
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());
            var manager = SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                IsIndeterminate = false
            });
            manager.Show();
            manager.ViewModel.Progress = 100;

            SplashScreenManager.CreateThemed(new DXSplashScreenViewModel
            {
                Copyright = "All rights reserved",
                IsIndeterminate = true,
                Status = "SpreadSheetControl Loading...",
                Title = "",
                Subtitle = "WPF Tranning Project"
            }
            ).ShowOnStartup();

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
            manager.Close();
        }

        private void CreateNewDocument(object obj)
        {
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());

            if (obj is SpreadsheetControl sheetcontrol) // 형변환
            {
                using (BinaryReader reader = new BinaryReader
                    (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WPF_Tranning.Resources.output.xlsx")))
                {
                    sheetcontrol.CreateNewDocument();
                    sheetcontrol.LoadDocument(reader.BaseStream);
           
                    MakeDataNewDocuments(sheetcontrol);
                }
            }
        }
        private void BinaryCreateNewDocument2(object obj)
        {
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());

            if (obj is SpreadsheetControl sheetcontrol) // 형변환
            {
                //   sheetcontrol.CreateNewDocument();
                sheetcontrol.LoadDocument(ExcelExport_ClosedXML2());
            }
        }

  
        private void BinaryCreateNewDocument(object obj)
        {
            if (obj is RoutedEventArgs a)
            {
                var button = (System.Windows.Controls.Button)a.Source;
            }
            string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());

            if (obj is SpreadsheetControl sheetcontrol) // 형변환
            {
                 //   sheetcontrol.CreateNewDocument();
                 //   sheetcontrol.LoadDocument(ExcelExport_ClosedXML());
            }
            this.Foo += Source_OnFoo;
            Bar();

        }
        private void Source_OnFoo(object sender, EventArgs e)
        {
            
        }

        private void MakeDataNewDocuments(SpreadsheetControl sheetcontrol)
        {
            IWorkbook workbook = sheetcontrol.Document;

            Worksheet worksheet = sheetcontrol.Document.Worksheets[0];
            worksheet.Name = "CreateDocument";
            worksheet.Cells["D5"].Value = "새로추가";
            worksheet.Cells["D6"].Value = "새로추가";
            worksheet.Cells["D7"].Value = "새로추가";
            worksheet.Cells["D8"].Value = "새로추가";
            worksheet.Cells["D9"].Value = "새로추가";
            string getSheetsName = workbook.Worksheets.ActiveWorksheet.ToString(); // 값 확인용
          //   Worksheet worksheet = workbook.Worksheets["CreatNewDocument"]; // sheets이름 얻어와 확인 (workbook으로 해도 되고)


  
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
        }
        private void ExportClosedXML_Sheets(object obj)
        {
            // export와 동시에 바로 spreadworksheet로 보여주는 메소드
            if (obj is SpreadsheetControl sheetcontrol) // 형변환
            {
                //   sheetcontrol.CreateNewDocument();
                sheetcontrol.LoadDocument(ExcelExport_ClosedXML());
            }
        }

        private Stream ExcelExport_ClosedXML2()
        {
            using (var workbook = new XLWorkbook())
            { 
                string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());

                var worksheet = workbook.Worksheets.Add("Test_Sheet");

                using (BinaryReader reader = new BinaryReader
                    (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WPF_Tranning.Resources.output.xlsx")))
                {
                    using (BinaryWriter writer = new BinaryWriter(new FileStream("파일이름.xlsx", FileMode.Create)))
                    {
                        int count = (int)reader.BaseStream.Length;
                        byte[] buffer = reader.ReadBytes(count);
                        writer.Write(buffer);
                    }
                }

                worksheet.Cell("D1").Value = "Hello World!2";
                worksheet.Cell("D2").FormulaA1 = "=MID(A1, 7, 6)"; // FormulaA1 (A1) 의 셀을 참조에 7번째부터 6자리수 까지 출력
                                                                   // 임의로 같은 값 만들기
                worksheet.Cell("D3").Value = "엑셀 export2";
                worksheet.Cell("D4").Value = "2021-08-12";
                worksheet.Cell("D5").Value = "바이너리 로드 성공2";


                for (int i = 0; i < 1000; i++)
                {
                    worksheet.Cell("D" + (6 + i)).Value = "바이너리 속도 테스트2";
                }

                return GetStream(workbook);
            }
         
        }

        private Stream ExcelExport_ClosedXML()
        {
         
                string path = "파일이름1234.xlsx";
           
        
                string[] resourceNames = (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceNames());

            using (BinaryReader reader = new BinaryReader
                (System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("WPF_Tranning.Resources.output.xlsx")))
            {
                using (BinaryWriter writer = new BinaryWriter(new FileStream(path, FileMode.Create)))
                {
                    int count = (int)reader.BaseStream.Length;
                    byte[] buffer = reader.ReadBytes(count);
                    writer.Write(buffer);
                }
            }
                var workbook = new XLWorkbook(path); // BinaryWriter에서 BinaryReader로
                                                     // 양식을 읽어 FileStream으로 만든 파일을 가져옴
            var worksheet = workbook.Worksheet(1); // 가져온 첫번째 엑셀시트 불러옴
                try
                {
                    worksheet.Cell("D1").Value = "Hello World!";
                    worksheet.Cell("D2").FormulaA1 = "=MID(A1, 7, 6)"; // FormulaA1 (A1) 의 셀을 참조에 7번째부터 6자리수 까지 출력
                                                                       // 임의로 같은 값 만들기
                    worksheet.Cell("D3").Value = "엑셀 export";
                    worksheet.Cell("D4").Value = "2021-08-12";
                    worksheet.Cell("D5").Value = "바이너리 로드 성공";


                    for (int i = 0; i < 1000; i++)
                    {
                        worksheet.Cell("D" + (6 + i)).Value = "바이너리 속도 테스트";
                    }
                    workbook.SaveAs(path); // 파일 + 양식 저장
                }

                
                catch (Exception e)
                {

                }
                Stream fs = new MemoryStream();
                fs.Position = 0;
                workbook.SaveAs(fs); // 스트림 저장
            
            return fs;
            
        }

        public Stream GetStream(XLWorkbook excelWorkbook)
        {
            Stream fs = new MemoryStream();
            excelWorkbook.SaveAs(fs);
            excelWorkbook.SaveAs("파일이름.xlsx");
            fs.Position = 0;
            return fs;
        }

    }
}
