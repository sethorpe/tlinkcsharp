using System;
using OpenQA.Selenium;
using System.Threading;
namespace TransLinkAutoHW.Configuration.DriverConfig
{
    public class DriverClass
    {
        private DriverClass() { }

        private static readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        public static void SetDriver(IWebDriver browserDriver)
        {
            _driver.Value = browserDriver;
        }

        public static IWebDriver GetDriver()
        {
            return _driver.Value;
        }

    }
}

