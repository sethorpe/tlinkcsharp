using OpenQA.Selenium.Support.UI;
using System;
//using RelevantCodes.ExtentReports;
using AventStack.ExtentReports;
using CommonLib.LoggingV1;
using OpenQA.Selenium;
using System.Collections.Generic;
using System.Threading;
using System.Net;
using System.Collections.ObjectModel;
using TransLinkAutoHW.Configuration.DriverConfig;
namespace TransLinkAutoHW.Configuration.HelperConfig
{
    public class SeleniumHelpers
    {
        public string Get_ElementInnerText(IWebDriver driver, ExtentTest step, By element, string description)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            string returnValue = "";
            try
            {
                var myElement = wait.Until(d =>
                {
                    var tempElement = d.FindElement(element);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                });
                returnValue = myElement.GetAttribute("innerText");

                ScrollToElement(driver, element, step, description);
                UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, element));
                UtilLibrary.PassStep(driver, step, "Text of : " + "{" + description + "}");
                return returnValue;
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, step, $"Not able to get text from : { description }.");
                throw new ArgumentException($"Not able to get text from : { description }. ", e);
            }
        }
        public string Get_ElementText(IWebDriver driver, ExtentTest step, By element, string description)
        {
            string returnValue = "";
            try
            {
                var myElement = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
                {
                    var tempElement = d.FindElement(element);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                });
                returnValue = myElement.Text;

                ScrollToElement(driver, element, step, description);
                UtilLibrary.PassStep(driver, step, $"Text of : { description }");
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, step, $"Not able to get text from : { description }.");
                throw new ArgumentException($"Not able to get text from : { description }. ", e);
            }
            return returnValue;
        }
        //public void Check_For_500Error(IWebDriver driver, ExtentTest step, string description = null)
        //{
        //    if (driver.Url.Contains("error") || driver.Url.Contains("Error"))
        //    {
        //        step.Log(LogStatus.Fail, $"Failure Reason: 500 Error on page { description } ");
        //        UtilLibrary.AttachScreenShotToResultFile(driver, step, "Error Message");
        //        throw new ArgumentException($"Failure Reason: 500 Error on page { description } ");
        //    }
        //}
        //public List<string> Find_Broken_Links(IWebDriver driver, ExtentTest step, By element, string description)
        //{
        //    var totalLinks = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d => driver.FindElements(element));
        //    var validLinks = new List<IWebElement>();
        //    var inValidLinks = new List<IWebElement>();
        //    var brokenLinks = new List<string>();

        //    foreach (IWebElement link in totalLinks)
        //    {
        //        if (!(link.GetAttribute("href").Contains("javascript")) && link.GetAttribute("href") != null && link.GetAttribute("href").Contains("http"))
        //            validLinks.Add(link);
        //        else
        //            inValidLinks.Add(link);
        //    }

        //    Console.WriteLine($"There are a total of { validLinks.Count } links on { description } section.");
        //    step.Log(LogStatus.Info, $"There are a total of { validLinks.Count } links on { description } section.");

        //    foreach (IWebElement validLink in validLinks)
        //    {
        //        string urlLink = validLink.GetAttribute("href");
        //        int responseCode = 0;
        //        string responseMessage = null;
        //        try
        //        {
        //            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlLink);
        //            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
        //            responseCode = (int)response.StatusCode;
        //            responseMessage = response.StatusDescription;
        //            response.Close();
        //        }
        //        catch (WebException ex)
        //        {
        //            brokenLinks.Add(urlLink);
        //            step.Log(LogStatus.Fail, $"URL: { urlLink }  RESPONSE MESSAGE: Failed to connect/Not working { ex.Status }");
        //            Console.WriteLine($"URL: { urlLink }  RESPONSE MESSAGE: Failed to connect/Not working { ex.Status }");
        //        }

        //        if (responseCode > 200 && responseCode < 400) // Redirection/warning
        //        {
        //            Console.WriteLine($"URL: { urlLink }  RESPONSE CODE: { responseCode }. RESPONSE MESSAGE: { responseMessage }");
        //            step.Log(LogStatus.Warning, $"URL: { urlLink }  RESPONSE CODE: { responseCode }. RESPONSE MESSAGE: { responseMessage }");
        //        }
        //        else if (responseCode > 399 && responseCode < 500) // Client/Server error
        //        {
        //            brokenLinks.Add(urlLink);
        //            Console.WriteLine($"URL: { urlLink }  RESPONSE CODE: { responseCode }. RESPONSE MESSAGE: { responseMessage }");
        //            step.Log(LogStatus.Fail, $"URL: { urlLink }  RESPONSE CODE: { responseCode }. RESPONSE MESSAGE: { responseMessage }");
        //        }
        //    }

        //    if (brokenLinks.Count > 0)
        //    {
        //        step.Log(LogStatus.Fail, $"REASON: There are Broken Links on { description } section. Check the above links!");
        //        //throw new ArgumentException($"REASON: There are Broken Links on { description } section. Check the report please!");
        //    }
        //    else
        //    {
        //        step.Log(LogStatus.Pass, $"All the { validLinks.Count } links are working as expected");
        //    }

        //    return brokenLinks;
        //}
        public bool IsElementDisplayed(IWebDriver driver, ExtentTest step, By element, string description)
        {
            try
            {
                var myElement = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
                {
                    var tempElement = d.FindElement(element);
                    return (tempElement.Displayed) ? tempElement : null;
                });
                UtilLibrary.HighlightElement(driver, myElement);
                ScrollToElement(driver, element, step, description);
                UtilLibrary.PassStep(driver, step, description + " is displayed ");
                return true;
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, step, description + " is NOT displayed ");
                throw new ArgumentException(description + " is NOT displayed " + e.Message);
            }
        }
        public void SelectDropDownValue(IWebDriver driver, ExtentTest step, By element, SelectBy selectBy, string dropDownValue)
        {
            try
            {
                new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(element));
                UtilLibrary.HighlightElement(driver, UtilLibrary.GetWebElement(driver, element));
            }
            catch (Exception e)
            {
                UtilLibrary.FailStep(driver, step, "Dropdown doesn't exist. ", e);
                throw new ArgumentException("Dropdown doesn't exist. ", e.Message);
            }

            switch (selectBy)
            {
                case SelectBy.Text:
                    new SelectElement(driver.FindElement(element)).SelectByText(dropDownValue);
                    break;
                case SelectBy.Value:
                    new SelectElement(driver.FindElement(element)).SelectByValue(dropDownValue);
                    break;
                case SelectBy.Index:
                    new SelectElement(driver.FindElement(element)).SelectByIndex(Convert.ToInt32(dropDownValue));
                    break;
            }
            UtilLibrary.PassStep(driver, step, $"Selected dropDown value: { new SelectElement(driver.FindElement(element)).SelectedOption.Text }");
        }
        public void SwitchTo_Window(IWebDriver driver, ExtentTest step, Window window, string windowName)
        {
            try
            {
                var windowHandles = WaitForNewWindow();
                driver.SwitchTo().Window(windowHandles[Convert.ToInt32(window)]);
                UtilLibrary.PassStep(driver, step, $"Switched to window - { windowName }");
            }
            catch (Exception)
            {
                UtilLibrary.FailStep(driver, step, $"Failed switching to { windowName } window. Please check!");
                throw;
            }
        }
        public void ScrollToElement(IWebDriver driver, By locator, ExtentTest step, string description)
        {
            IJavaScriptExecutor javaScript = driver as IJavaScriptExecutor;

            try
            {
                IWebElement myElement = new WebDriverWait(driver, TimeSpan.FromSeconds(15)).Until(d =>
                {
                    var tempElement = d.FindElement(locator);
                    return (tempElement.Displayed && tempElement.Enabled) ? tempElement : null;
                });
                javaScript.ExecuteScript("arguments[0].scrollIntoView({behavior: 'auto',block: 'center'});", myElement);
            }
            catch (Exception)
            {
                UtilLibrary.FailStep(step, "Not able to scroll to " + description);
                throw new ArgumentException("Not able to scroll to " + description);
            }
        }
        public string Get_Xpath(string xpath, string value)
        {
            return xpath.Replace("%replaceable%", value);
        }

        // {Utility Methods}
        private ReadOnlyCollection<string> WaitForNewWindow()
        {
            var time = 0;

            for (var i = 0; i <= 10; i++)
            {
                if (DriverClass.GetDriver().WindowHandles.Count > 1)
                {
                    break;
                }
                else
                {
                    if (time <= 200)
                    {
                        Thread.Sleep(time);
                        time += 50;
                    }
                    else
                        break;
                }
            }
            return DriverClass.GetDriver().WindowHandles;
        }
        public enum Window { CurrentWindow = 0, SecondWindow = 1, ThirdWindow = 2 }
        public enum SelectBy { Text, Value, Index };
    }
}
