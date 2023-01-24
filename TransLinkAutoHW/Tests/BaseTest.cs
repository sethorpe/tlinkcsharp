using CommonLib.YamlV1;
using CommonLib.LoggingV1;
using NUnit.Framework;
using WebDriverManager.DriverConfigs.Impl;
using TransLinkAutoHW.Configuration.DriverConfig;
using TransLinkAutoHW.Pages;
using System;
namespace TransLinkAutoHW.Tests
{
    public class BaseTest
    {
        protected readonly Config config = new Config();

        [OneTimeSetUp]
        public void RunBeforeAllTests()
        {
            config.LoadYaml();
            ReportingManager.InitializeReport("TransLink Auto HW Report");
        }

        [SetUp]
        public void RunBeforeEachTest()
        {
            DriverFactory.InitializeDriver();
            ReportingManager.StartTest(TestContext.CurrentContext.Test.Name);
        }

        [TearDown]
        public void RunAfterEachTest()
        {
            ReportingManager.EndTest();
            DriverFactory.QuitDriver();
        }

        [OneTimeTearDown]
        public void RunAfterAllTests()
        {
            ReportingManager.FlushReport();
        }

    }
}
