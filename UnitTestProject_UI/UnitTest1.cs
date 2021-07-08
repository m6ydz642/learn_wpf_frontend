
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.ComponentModel;
using System.Data;
using WPF_Tranning.ModelAndView;

namespace UnitTestProject_UI
{

    [TestClass]
    public class UnitTest1
    {
     

        [TestMethod]
        public void test()
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

   



    }

}
