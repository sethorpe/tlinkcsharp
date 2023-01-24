using AventStack.ExtentReports;
using CommonLib.YamlV1;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using CommonLib.LoggingV1;
using TransLinkAutoHW.Configuration.DriverConfig;
namespace TransLinkAutoHW.Pages
{
    public abstract class BasePage
    {
        protected string url;
        protected ExtentTest Step;
        protected IWebDriver driver;
        protected WebDriverWait wait;
        protected readonly Config MyConfig = new Config();
        protected ActionsLibrary ActionLib = new ActionsLibrary();

        protected BasePage()
        {
            driver = DriverClass.GetDriver();
            Step = ReportingManager.GetExtentTest();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            url = GetUrl();
        }

        private string GetUrl()
        {
            MyConfig.LoadYaml();

            switch (MyConfig.site.TestEnvironment)
            {
                case Const.TestEnvironment.QA_ENVIRONMENT:
                    url = "https://translink.ca";
                    break;
            }
            return url;
        }

        protected void IsPageLoaded(By locator, string pageName)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, Step, $"Failed to load the { pageName } properly.");
                throw new ArgumentException($"Failed to load the { pageName } properly. " + e.Message);
            }
        }

        protected bool isElementDisplayed(By locator)
        {
            try
            {
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(locator));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        protected bool isElementNotDisplayed(By locator)
        {
            try
            {
                return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(locator));
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
