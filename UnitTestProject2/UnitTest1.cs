using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using WPF_Tranning.ModelAndView;

namespace UnitTestProject2
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CheckRegexTest()
        {
            GridCotrolBandModelAndView gridcontrolModel = new GridCotrolBandModelAndView();
            PrivateObject obj = new PrivateObject(gridcontrolModel); // private 메소드에 접근하게 해줌
            var retval = obj.Invoke("CheckRegex", 123); // private 메소드에 접근하게 해줌, 함수명, 인자
            gridcontrolModel.ComboMode = "데이터 모드 1"; // 모드 선택 (함수 내에서 모드를 찾음)
            Assert.IsTrue((bool)retval); // checkRegex 함수명으로 부터 12라는 인자를 반환받은 값이 false인지 true인지 확인시켜줌

            // WPF Tranning 프로젝트에있는 Appconfig 여기로 복사해서 추가해야 예외 안뜸 ㅡ.ㅡ;;
             
        }
    }
}
