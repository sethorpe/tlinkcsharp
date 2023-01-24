using System;
using NUnit.Framework;
using TransLinkAutoHW.Pages;
namespace TransLinkAutoHW.Tests
{
    public class NextBusTests : BaseTest
    {
        [TestCase("99")]
        public void findBusSchedule(string routeNumber)
        {
            var test = new HomePage().Open().clickNextBus().EnterBusRouteStopNumber(routeNumber);
        }

        [TestCase("28", "TransLink Auto Homework")]
        public void saveRouteToFaves(string routeNumber, string faveName)
        {
            var test = new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName);
        }

        [TestCase("25", "TransLink Auto Homework")]
        public void verifyFavoritesLinkExists(string routeNumber, string faveName)
        {
            var savedFaveTitle = new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName)
                .retrieveFavesTitle();
            Assert.That(savedFaveTitle == faveName);
        }

        [TestCase("99", "TransLink Auto Homework")]
        public void verifyRouteTitleIsDisplayed(string routeNumber, string faveName)
        {
            var routeTitle = new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName)
                .retrieveRouteTitle();
            Assert.That(routeTitle == "99 Commercial-Broadway / UBC (B-Line)");
        }

        [TestCase("99", "TransLink Auto Homework", "To Comm'l-Bdway Stn / Boundary B-Line")]
        public void verifyStopNumberDisplayed(string routeNumber, string faveName, string destName)
        {
            var TestStopNumber = new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName)
                .ClickOnDestination(destName);
        }

        [TestCase("99", "TransLink Auto Homework", "To Comm'l-Bdway Stn / Boundary B-Line", "UBC Exchange Bay 7")]
        public void clickBusStopName(string routeNumber, string faveName, string destName, string stopName)
        {
            new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName)
                .ClickOnBusStop(destName, stopName);
        }

        [TestCase("99", "TransLink Auto Homework", "To Comm'l-Bdway Stn / Boundary B-Line", "UBC Exchange Bay 7", "Stop # 61935")]
        [TestCase("25", "Seyi's Favorite Route", "To Brentwood Stn / Nanaimo Stn", "Nanaimo Stn Bay 1","Stop # 60314")]
        [TestCase("28", "Seyi's Fave Route #2", "To Kootenay Loop / Phibbs Exch", "Joyce Stn Bay 4", "Stop # 61609" )]
        public void verifyStopNumber(string routeNumber, string faveName, string destName, string stopName, string stopNumber)
        {
            var actualStopNumber = new HomePage().Open().clickNextBus()
                .EnterBusRouteStopNumber(routeNumber)
                .saveRouteToFavorites(faveName)
                .ClickOnBusStop(destName, stopName)
                .RetrieveStopNumber();
            Assert.AreEqual(stopNumber, actualStopNumber);
        }
    }
}
