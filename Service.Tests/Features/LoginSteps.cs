using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using TechTalk.SpecFlow;

[Binding]
public class LoginSteps
{
    private IWebDriver _driver;

    [Given(@"I have navigated to the login page")]
    public void GivenIHaveNavigatedToTheLoginPage()
    {
        _driver = new ChromeDriver();
        _driver.Navigate().GoToUrl("https://h3sgi.e-boticario.com.br/");
    }

    [When(@"I enter ""(.*)"" as username and ""(.*)"" as password")]
    public void WhenIEnterAsUsernameAndAsPassword(string username, string password)
    {
        _driver.FindElement(By.Id("username")).SendKeys(username);
        _driver.FindElement(By.Id("password")).SendKeys(password);
    }

    [When(@"I click the login button")]
    public void WhenIClickTheLoginButton()
    {
        _driver.FindElement(By.Id("loginButton")).Click();
    }

    [Then(@"I should see the dashboard")]
    public void ThenIShouldSeeTheDashboard()
    {
        var dashboardElement = _driver.FindElement(By.Id("dashboard"));
        Assert.True(dashboardElement.Displayed, "Dashboard is not displayed");
        _driver.Quit();
    }

    [Then(@"I should see an error message ""(.*)""")]
    public void ThenIShouldSeeAnErrorMessage(string expectedMessage)
    {
        var errorElement = _driver.FindElement(By.Id("errorMessage"));
        Assert.Equal(expectedMessage, errorElement.Text);
        //Assert.AreEqual(expectedMessage, errorElement.Text, "Error message does not match");
        _driver.Quit();
    }
}
