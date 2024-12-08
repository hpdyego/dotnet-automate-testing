using OpenQA.Selenium;
using Service.Tests.Helpers;

namespace Service.Tests;

public class SampleTests : TestBase
{
    [Fact]
    public void VerifyGoogleHomePageTitle()
    {
        Driver.Navigate().GoToUrl("https://www.google.com");
        
        Task.Delay(3000);
        Thread.Sleep(5000);
        
        Assert.Contains("Google", Driver.Title);
    }

    [Fact]
    public void VerifySearchFunctionality()
    {
        Driver.Navigate().GoToUrl("https://www.google.com");
        var searchBox = Driver.FindElement(By.Name("q"));
        searchBox.SendKeys("Selenium WebDriver");
        searchBox.Submit();

        Task.Delay(3000);
        Thread.Sleep(5000);

        var results = Driver.FindElements(By.CssSelector("h3"));
        Assert.True(results.Count > 0, "No results found!");
    }
}
