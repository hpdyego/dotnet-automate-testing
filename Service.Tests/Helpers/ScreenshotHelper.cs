using OpenQA.Selenium;
using System;
using System.IO;

namespace Service.Tests.Helpers;

public class ScreenshotHelper
{
    private readonly string _imageFilePath;

    public ScreenshotHelper(string imageFilePath)
    {
        _imageFilePath = imageFilePath;
    }

    public string TakeScreenshot(IWebDriver driver, string testName)
    {
        string screenshotsDir = _imageFilePath; //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Screenshots");
        if (!Directory.Exists(screenshotsDir))
            Directory.CreateDirectory(screenshotsDir);

        string filePath = Path.Combine(screenshotsDir, $"{testName}_{DateTime.Now:yyyyMMdd_HHmmss}.png");
        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        //screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        screenshot.SaveAsFile(filePath);
        return filePath;
    }
}
