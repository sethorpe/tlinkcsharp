using CommonLib.LoggingV1;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace TransLinkAutoHW.Pages
{
    public class HomePage : BasePage
    {
        // Page Objects
        private By siteLogo = By.ClassName("SiteLogo");
        private By nextBus = By.LinkText("Next Bus");


        // Page Actions
        public HomePage Open()
        {
            try
            {
                driver.Manage().Cookies.DeleteAllCookies();
                driver.Navigate().GoToUrl(url);
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, Step, "Failed to launch TransLink website");
                throw new ArgumentException("Failed to launch TransLink website", e);
            }
            UtilLibrary.PassStep(driver, Step, "Launched TransLink Homepage");
            return new HomePage();
        }
        public bool isLoaded()
        {
            IsPageLoaded(siteLogo, "HomePage");
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, siteLogo));
            UtilLibrary.PassStep(Step, "Site Logo is displayed; Home Page is loaded completely");
            UtilLibrary.AttachScreenShotToResultFile(driver, Step, "Home Page");
            UtilLibrary.UnHighlightElement(driver, UtilLibrary.GetWebElement(driver, siteLogo));
            return true;
        }

        public NextBusPage clickNextBus()
        {
            ActionLib.ClickOnElement(driver, Step, nextBus, "Next Bus Link");
            return new NextBusPage();
        }
    }
}
