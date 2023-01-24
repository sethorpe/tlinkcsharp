using CommonLib.YamlV1;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Firefox;
using Microsoft.Edge.SeleniumTools;
namespace TransLinkAutoHW.Configuration.DriverConfig
{
    public class DriverFactory
    {
        private DriverFactory() { }

        // Create a new browser driver for each test method
        public static void InitializeDriver()
        {
            Config myConfig = new Config();
            myConfig.LoadYaml();
            switch (myConfig.settings.Browser)
            {
                case Const.TestBrowsers.CHROME:
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    var chromeOptions = new ChromeOptions();
                    chromeOptions.AddArgument("start-maximized");
                    chromeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    DriverClass.SetDriver(new ChromeDriver(chromeOptions));
                    DriverClass.GetDriver().Manage().Window.Maximize();
                    break;

                case Const.TestBrowsers.FIREFOX:
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    var firefoxOptions = new FirefoxOptions();
                    firefoxOptions.AddArgument("start-maximized");
                    firefoxOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    DriverClass.SetDriver(new FirefoxDriver(firefoxOptions));
                    break;

                case Const.TestBrowsers.EDGE:
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    var edgeOptions = new EdgeOptions();
                    edgeOptions.UseChromium = true;
                    edgeOptions.PageLoadStrategy = PageLoadStrategy.Normal;
                    DriverClass.SetDriver(new EdgeDriver(edgeOptions));
                    DriverClass.GetDriver().Manage().Window.Maximize();
                    break;

            }
        }

        // Quit the browser driver
        public static void QuitDriver()
        {
            if (DriverClass.GetDriver() != null)
            {
                DriverClass.GetDriver().Quit();
            }
        }
    }
}
