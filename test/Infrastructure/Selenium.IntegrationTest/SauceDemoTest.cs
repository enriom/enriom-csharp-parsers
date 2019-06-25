﻿using Optivem.Infrastructure.Selenium.IntegrationTest.App;
using Optivem.Test.Xunit;
using Optivem.Test.Xunit.Selenium;
using Xunit;

namespace Optivem.Infrastructure.Selenium.IntegrationTest
{
    public class SauceDemoTest : FixtureTest<ChromeDriverFixture>
    {
        public SauceDemoTest(ChromeDriverFixture fixture) : base(fixture)
        {
        }

        [Fact(Skip = "In progress")]
        public void TestValidLogin()
        {
            var driver = Fixture.TestDriver;
            var app = new SauceDemoApp(driver);

            var loginScreen = app.OpenLoginScreen();
            loginScreen.UserName.EnterText("standard_user");
            loginScreen.Password.EnterText("secret_sauce");

            // TODO: VC: Continue

            /*
            loginScreen.Login.Click();

            var inventoryScreen = app.InventoryScreen;

            inventoryScreen.ProductSort.SelectByText("Name (Z to A)");
            */
        }
    }
}