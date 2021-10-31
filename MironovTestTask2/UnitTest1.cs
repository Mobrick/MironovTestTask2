using System;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V93.Input;

namespace MironovTestTask2
{
    public class Tests
    {
        private IWebDriver driver;
        private readonly string test_url = "https://careers.veeam.ru/vacancies";
        private const string DropdownBlockAddress = "//*[@id='root']/div/div[1]/div/div[2]/div[1]/div";

        [SetUp]
        public void start_Browser()
        {
            // Google Chrome launch
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
        }

        [TestCase("���������� ���������", "����������", 6)]
        public void test_search(string department, string language, int expectedNumberOfVacancies)
        {
            // Transition to the page were tests will be made
            driver.Navigate().GoToUrl(test_url);
            Thread.Sleep(5000);

            // Test case department selection
            driver.FindElement(By.XPath($"{DropdownBlockAddress}/div[2]")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath($"{DropdownBlockAddress}/div[2]//*[text()='{department}']")).Click();
            Thread.Sleep(500);

            // Test case language selection
            driver.FindElement(By.XPath($"{DropdownBlockAddress}/div[3]")).Click();
            Thread.Sleep(500);
            driver.FindElement(By.XPath($"{DropdownBlockAddress}/div[3]//*[text()='{language}']")).Click();
            Thread.Sleep(500);

            // Vacancies count
            var num = driver.FindElements(By.XPath("//*[@id='root']/div/div[1]/div/div[2]/div[2]/div/*")).Count;

            Assert.AreEqual(expectedNumberOfVacancies, num);
        }

        [TearDown]
        public void close_Browser()
        {
            Thread.Sleep(2000);
            driver.Close();
        }
    }
}