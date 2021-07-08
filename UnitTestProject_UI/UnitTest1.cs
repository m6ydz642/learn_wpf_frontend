
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using WPF_Tranning.ModelAndView;

namespace UnitTestProject_UI
{

    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void UITest()
        {

            string PathToTheApp = @"C:\Users\m6ydz642\source\repos\WPF_Tranning\WPF_Tranning\bin\Debug\WPF_Tranning.exe";
            var options = new AppiumOptions();
            options.AddAdditionalCapability(capabilityName: "app", capabilityValue: PathToTheApp);
            options.AddAdditionalCapability(capabilityName: "deviceName", capabilityValue: "WindowsPC");
            options.AddAdditionalCapability(capabilityName: "platformName", capabilityValue: "Windows");
            options.AddAdditionalCapability(capabilityName: "ms:experimental-webdriver", capabilityValue: true);
            //   options.AddAdditionalCapability(capabilityName: "appTopLevelWindow", capabilityValue: $"0x{WindowHandle.ToInt64():X8}");

            var driver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), options);



            var bNewEmployee = driver.FindElementByName("DB 정규식 체크");
            bNewEmployee.Click();

            driver.FindElementByName("확인").Click();
            driver.FindElementByName("그리드 컨트롤").Click();


            WindowsElement newEmployeeWindow = null;
            while (newEmployeeWindow == null)
                newEmployeeWindow = driver.FindElementByName("차트바인딩 연습");

            //   newEmployeeWindow.FindElementByName("First Name").FindElementByClassName("TextEdit").SendKeys("John");
            newEmployeeWindow.FindElementByName("스코어 검색, 콤보박스").Click();
            newEmployeeWindow.FindElementByName("TextBox").Click();





        }


        [TestMethod]
        public void MockTest()
        {
            List<ScoreTableModel> datalist = new List<ScoreTableModel>()
            {
                new ScoreTableModel() { Score_id = "123", Score = "test" },
                new ScoreTableModel() {Score_id="1234", Score="test2"}
            };

            Mock<DBContext> mocks = new Mock<DBContext>();


            mocks.Setup(b => b.GetDatas()).Returns(datalist);

            SearchScoreViewAndModel process = new SearchScoreViewAndModel(mocks.Object);
            string value = process.GetSum();
            Assert.IsNotNull(process.GetSum());
            DataTable testdt = process.GetScoreInfomation = new DataTable(); // DB랑 연결 안되어있어서 아무것도 없음


        }

        [TestMethod]
        public void MockDataTableTest()
        {
            /*       DataTable datatable = new DataTable();
                   datatable.Columns.Add("123");
                   datatable.Rows.Add("점수띠");
                   Mock<DBContextDataTable> mocks = new Mock<DBContextDataTable>();*/
            List<ScoreTableModel> datalist = new List<ScoreTableModel>()
            {
                new ScoreTableModel() { Score_id = "1", Score = "점수 100점" },
                new ScoreTableModel() {Score_id=  "2", Score = "점수 10점"}
            };
            Mock<DBContextDataTable> datatablemock = new Mock<DBContextDataTable>();


            datatablemock.Setup(b => b.GetDataTable()).Returns(datalist);

            SearchScoreViewAndModel process = new SearchScoreViewAndModel(datatablemock.Object);
            DataTable testtable = process.GetTableReturn();
            Assert.IsNotNull(process.GetTableReturn());


        }

    }

}





