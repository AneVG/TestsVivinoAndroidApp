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


            //Click on the first search result and assert it holds correct data

            var listSearcResults = driver.FindElementById("vivino.web.app:id/listviewWineListActivity");          
            var firsResult = listSearcResults.FindElementByClassName("android.widget.LinearLayout");
            firsResult.Click();

            var elementWineName = driver.FindElementById("vivino.web.app:id/wine_name");
            Assert.That(elementWineName.Text, Is.EqualTo("Reserve Red 2006"));

            var elementRating= driver.FindElementById("vivino.web.app:id/rating");
            double rating = double.Parse(elementRating.Text);
            Assert.IsTrue(rating >= 1.00 && rating <= 5.00);

            var tabsSummary = driver.FindElementById("vivino.web.app:id/tabs");
            var tabHighlights = tabsSummary.FindElementByXPath("//android.widget.TextView[1]");
            tabHighlights.Click();

            //Click the text in the "hightlights" tab

           var highlightsDescription = driver.FindElementByAndroidUIAutomator(
                "new UiScrollable(new UiSelector().scrollable(true))" +
                ".scrollIntoView(new UiSelector().resourceIdMatches(" +
                "\"vivino.web.app:id/highlight_description\"))");

            Assert.That(highlightsDescription.Text, Is.EqualTo("Among top 1% of all wines in the world"));
           
        }
    }
}
