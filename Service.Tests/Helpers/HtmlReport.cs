using System;
using System.Collections.Generic;
using System.IO;

public class HtmlReport
{
    private readonly string _htmlFilePath;

    public HtmlReport(string htmlFilePath)
    {
        _htmlFilePath = htmlFilePath;
    }

    public void GenerateReport(List<(string TestName, string Status, string ScreenshotPath)> testResults)
    {
        using (var writer = new StreamWriter(_htmlFilePath))
        {
            writer.WriteLine("<!DOCTYPE html>");
            writer.WriteLine("<html lang='en'>");
            writer.WriteLine("<head>");
            writer.WriteLine("<meta charset='UTF-8'>");
            writer.WriteLine("<meta name='viewport' content='width=device-width, initial-scale=1.0'>");
            writer.WriteLine("<title>Test Execution Report</title>");
            writer.WriteLine("<style>");
            writer.WriteLine("body { font-family: Arial, sans-serif; margin: 20px; }");
            writer.WriteLine("h1 { color: #333; }");
            writer.WriteLine(".test { margin-bottom: 20px; }");
            writer.WriteLine(".status { font-weight: bold; }");
            writer.WriteLine(".passed { color: green; }");
            writer.WriteLine(".failed { color: red; }");
            writer.WriteLine("img { max-width: 600px; margin-top: 10px; border: 1px solid #ddd; }");
            writer.WriteLine("</style>");
            writer.WriteLine("</head>");
            writer.WriteLine("<body>");

            writer.WriteLine("<h1>Test Execution Report</h1>");
            writer.WriteLine($"<p>Generated: {DateTime.Now}</p>");

            foreach (var (testName, status, screenshotPath) in testResults)
            {
                writer.WriteLine("<div class='test'>");
                writer.WriteLine($"<p><span class='status {(status == "Passed" ? "passed" : "failed")}'>{status}</span>: {testName}</p>");

                if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
                {
                    string relativePath = Path.GetFileName(screenshotPath);
                    writer.WriteLine($"<img src='{relativePath}' alt='Screenshot of {testName}' />");
                }

                writer.WriteLine("</div>");
            }

            writer.WriteLine("</body>");
            writer.WriteLine("</html>");
        }
    }
}
