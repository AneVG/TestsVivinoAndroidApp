using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;
using System;

namespace AppiumVivinoSiteMobileTests
{
    public class VivinoTests
    {

        private const string AppiumUri = "http://127.0.0.1:4723/wd/hub";
        private const string VivinoAppPackadge = @"vivino.web.apk";
        private const string VivinoAppStartUpActivity = "com.sphinx_solution.activities.SplashActivity";
        private const string VivinoTestAccountEmail = "dmani@abv.bg";
        private const string VivinoTestAccountPassword = "dmaniv";
        private AndroidDriver<AndroidElement> driver;

        [OneTimeSetUp]
        public void Setup()
        {
            var options = new AppiumOptions() { PlatformName = "Android" };
            options.AddAdditionalCapability("appPackadge", VivinoAppPackadge);
            options.AddAdditionalCapability("appActivity", VivinoAppStartUpActivity);

            driver = new AndroidDriver<AndroidElement>(new Uri(AppiumUri), options);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(120);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }


        [Test]
        public void Test_Login()
        {
            //Login in the Vivino App
            var linklogin = driver.FindElementById("vivino.web.app:id/txthaveaccount");
            linklogin.Click();

            var textBoxLoginEmail = driver.FindElementById("vivino.web.app:id/edtEmail");
            textBoxLoginEmail.SendKeys(VivinoTestAccountEmail);

            var textBoxLoginPassword = driver.FindElementById("vivino.web.app:id/edtPassword");
            textBoxLoginPassword.SendKeys(VivinoTestAccountPassword);

            var buttonLogin = driver.FindElementById("vivino.web.app:id/action_signin");
            buttonLogin.Click();

            // Search for Catarzyna
            var tabExplorer = driver.FindElementById("vivino.web.app:id/wine_explorer_tab");
            tabExplorer.Click();

            var buttonSearch = driver.FindElementById("vivino.web.app:id/search_vivino");
            buttonSearch.Click();

            var textBoxSearch = driver.FindElementById("vivino.web.app:id/editText_input");
            textBoxSearch.SendKeys("Katarzyna Reserve Red 2006");

        }
    }
}
