using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace Service.Tests.Helpers;

public class TestBase : IDisposable
{
    protected IWebDriver Driver;

    public TestBase()
    {
        var options = new ChromeOptions();
        options.AddArgument("--headless"); // Para rodar sem abrir o navegador
        //options.AddArgument("--disable-gpu");
        options.AddArgument("--window-size=1920,1080");

        Driver = new ChromeDriver(options);
    }

    public void Dispose()
    {
        Driver.Quit();
        Driver.Dispose();
    }
}
