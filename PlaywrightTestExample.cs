using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using System.Threading.Tasks;

namespace PlaywrightTestExample
{
    [TestFixture]
    public class LoginTests : PageTest
    {

        protected IPage Page { get; private set; }
        protected IBrowser Browser { get; private set; }

        [SetUp]
        public async Task SetUp()
        {
            var playwright = await Playwright.CreateAsync();
            Browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            Page = await Browser.NewPageAsync();
        }

        
        [Test]
        public async Task SuccessfulLoginTest()
        {
            // Navigate to the login page
            await Page.GotoAsync("https://test.com/login");

            // Enter the username and password
            await Page.FillAsync("#username", "JohnCena");
            await Page.FillAsync("#password", "$trongpassword123");

            // Click on the login button
            await Page.ClickAsync("#loginButton");

            // Assert that the logout button is present after logging in
            var logoutButton = await Page.WaitForSelectorAsync("#logoutButton");
            Assert.That(logoutButton, Is.Not.Null, "The logout button should be present after a successful login.");
        }

        [TearDown]
        public async Task TearDown()
        {
            await Page.CloseAsync();
            await Browser.CloseAsync();
        }
    }
}
