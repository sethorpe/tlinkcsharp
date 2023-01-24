using CommonLib.LoggingV1;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;

namespace TransLinkAutoHW.Pages
{
    public class NextBusPage : BasePage
    {
        // Page Objects
        private By nextBusText = By.Id("next-bus");
        private By nextBusSearchTerm = By.Id("NextBusSearchTerm");
        private By findMyNextBus = By.XPath("//button[contains(text(),'Find my next bus')]");
        private By addToFaves = By.ClassName("AddDelFav");
        private By addToFavesDialog = By.Id("add-to-favourites_dialog");
        private By favoriteNameField = By.XPath("//textarea[@name='newFavourite']");
        private By addToFavoritesBtn = By.XPath("//button[contains(text(),'Add to favourites')]");
        private By myFavesBtn = By.LinkText("My Favs");
        private By favoritesLink = By.ClassName("verticallyCenteredItem");
        private By routeTitle = By.ClassName("txtRouteTitle");
        private By NBPageFrame = By.CssSelector("body:nth-child(2) main:nth-child(16) div.gridContainer.NarrowFlexColumnBlockLayout.noBottomPadding:nth-child(8) section.CopyMain > iframe:nth-child(1)");
        private By stopNumber = By.ClassName("stopNo");
        
        

        // Page Actions
        public bool isLoaded()
        {
            IsPageLoaded(nextBusText, "Next Bus page");
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, nextBusText));
            UtilLibrary.PassStep(Step, "Next Bus Page is displayed");
            UtilLibrary.AttachScreenShotToResultFile(driver, Step, "Next Bus page");
            UtilLibrary.UnHighlightElement(driver, UtilLibrary.GetWebElement(driver, nextBusText));
            return true;
        }

        public NextBusPage EnterBusRouteStopNumber(string routeNumber)
        {
            IsPageLoaded(nextBusText, "Next Bus");
            ActionLib.ClickOnElement(driver, Step, nextBusSearchTerm, "Bus Route / Stop Number");
            ActionLib.EnterText(driver, Step, nextBusSearchTerm, routeNumber, "Bus Route / Stop Number");
            ClickOnFindMyNextBus();
            return new NextBusPage();
        }

        public void ClickOnFindMyNextBus()
        {
            ActionLib.ClickOnElement(driver, Step, findMyNextBus, "Find my next bus button");
        }

        public void ClickOnAddFavBtn()
        {
            ActionLib.ClickOnElement(driver, Step, addToFaves, "Add to Faves Button");
        }

        public NextBusPage saveRouteToFavorites(string faveName)
        {
            ClickOnAddFavBtn();
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, favoriteNameField));
            ActionLib.EnterText(driver, Step, favoriteNameField, faveName, "Favorites Name");
            ActionLib.ClickOnElement(driver, Step, addToFavoritesBtn, "Add to Favorites");
            return new NextBusPage();
        }

        public void ClickMyFavsBtn()
        {
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, myFavesBtn));
            ActionLib.ClickOnElement(driver, Step, myFavesBtn, "My Favs");
        }

        public void ClickOnFaveLink()
        {
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, favoritesLink));
            ActionLib.ClickOnElement(driver, Step, favoritesLink, "Favorites Link");
        }

        public string retrieveFavesTitle()
        {
            ClickMyFavsBtn();
            return ActionLib.GetText(driver, Step, favoritesLink, "Favorites Title");
            
        }

        public string retrieveRouteTitle()
        {
            ClickMyFavsBtn();
            ClickOnFaveLink();
            ActionLib.SwitchToFrame(driver, Step, NBPageFrame, "Switching to NB Page Frame");
            //driver.SwitchTo().Frame(driver.FindElement(NBPageFrame));
            return ActionLib.GetText(driver, Step, routeTitle, "RouteTitle");
        }

        public NextBusPage ClickOnDestination(string destName)
        {
            string foundDestname;
            ClickMyFavsBtn();
            ClickOnFaveLink();
            ActionLib.SwitchToFrame(driver, Step, NBPageFrame, "Next Bus Page Frame");
            IWebElement element = driver.FindElement(By.Id("schedule"));
            IList<IWebElement> elements = element.FindElements(By.TagName("a"));

            foreach (IWebElement e in elements)
            {
                System.Console.WriteLine(e.Text);
                if (e.Text == destName){
                    foundDestname = e.Text;
                    System.Console.WriteLine("Found Destination Name: " + foundDestname);
                    UtilLibrary.HighlightElement(driver, e);
                }
            }

            driver.FindElement(By.PartialLinkText(destName)).Click();
            UtilLibrary.AttachScreenShotToResultFile(driver, Step, "Destination Screen Result");
            ActionLib.SwitchToFrame(driver, Step, NBPageFrame, "List of Stops");
            

            return new NextBusPage();
        }

        public NextBusPage ClickOnBusStop(string destName, string stopName)
        {
            ClickOnDestination(destName);
            IWebElement element = driver.FindElement(By.Id("schedule"));
            IList<IWebElement> elements = element.FindElements(By.TagName("a"));

            foreach (IWebElement e in elements)
            {
                if(e.Text == stopName)
                {
                    UtilLibrary.HighlightElement(driver, e);
                }
            }
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView();", driver.FindElement(By.PartialLinkText(stopName)));
            js.ExecuteScript("arguments[0].click()", driver.FindElement(By.PartialLinkText(stopName)));
            UtilLibrary.PassStep(driver, Step, "Clicked on Stop Name");
            return new NextBusPage();
        }

        public String RetrieveStopNumber()
        {
            //Switch to iframe
            ActionLib.SwitchToFrame(driver, Step, NBPageFrame, "List of Stops");
            UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, stopNumber));
            return ActionLib.GetText(driver, Step, stopNumber, "Stop Number");
        }
    }

    
}
