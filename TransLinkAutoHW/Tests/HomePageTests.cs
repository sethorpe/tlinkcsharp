using System;
using NUnit.Framework;
using TransLinkAutoHW.Pages;
namespace TransLinkAutoHW.Tests
{
    public class HomePageTests : BaseTest
    {
        [Test]
        public void launchHomePage()
        {
            var homePage = new HomePage().Open().isLoaded();
        }

        [Test]
        public void goToNextBusPage()
        {
            var homePage = new HomePage().Open().clickNextBus();
        }
    }
}
