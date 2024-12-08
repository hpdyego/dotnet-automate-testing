using System.Net.Http;
using System.Text;
using System.Text.Json;
using Xunit;
using Service.Tests.Helpers;
using OpenQA.Selenium.Chrome;

namespace Service.Tests;

public class TestRunnerWithSummary
{
    private readonly SlackNotifier _notifier;
    private readonly HtmlReport _htmlReport;
    private readonly ScreenshotHelper _screenshotHelper;

    public TestRunnerWithSummary()
    {
        _notifier = new SlackNotifier("https://hooks.slack.com/services/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");
        _htmlReport = new HtmlReport("C:\\repo\\dotnet-testing\\Results\\TestExecutionReport.html");
        _screenshotHelper = new ScreenshotHelper("C:\\repo\\dotnet-testing\\Results\\");
    }

    [Fact]
    public async Task RunTestsAndReportToSlack()
    {
        int passedTests = 0;
        int failedTests = 0;

        var testResults = new List<(string TestName, string Status, string ScreenshotPath)>();

        // Lista de métodos de teste
        var tests = new List<(string TestName, Action Test)>
        {
            ("VerifyGoogleHomePageTitle", new SampleTests().VerifyGoogleHomePageTitle),
            ("VerifySearchFunctionality", new SampleTests().VerifySearchFunctionality)
        };

        foreach (var (testName, test) in tests)
        {
            try
            {
                using (var driver = new ChromeDriver())
                {
                    test.Invoke();
                    passedTests++;
                    string screenshotPath = _screenshotHelper.TakeScreenshot(driver, testName);
                    testResults.Add((testName, "Passed", screenshotPath));
                }
            }
            catch (Exception ex)
            {
                failedTests++;
                string screenshotPath = _screenshotHelper.TakeScreenshot(new ChromeDriver(), testName); // Captura o estado ao falhar
                testResults.Add((testName, $"Failed: {ex.Message}", screenshotPath));
            }
        }
        // foreach (var test in tests)
        // {
        //     try
        //     {
        //         test.Invoke();
        //         passedTests++;
        //         results.Add($"✅ {test.Method.Name} - Passed");
        //     }
        //     catch (Exception ex)
        //     {
        //         failedTests++;
        //         results.Add($"❌ {test.Method.Name} - Failed: {ex.Message}");
        //     }
        // }

        // Gerar o relatório em HTML
        _htmlReport.GenerateReport(testResults);

        // Criar resumo
        string summary = $"Test Execution Summary:\n" +
                        "-------------------------------------\n" +
                         $"- Total: {passedTests + failedTests}\n" +
                         "-------------------------------------\n" +
                         $"- ✅Passed: {passedTests}\n" +
                         $"- ❌Failed: {failedTests}\n" +
                         "-------------------------------------\n";

        // Enviar mensagem ao Slack
        await _notifier.SendMessageAsync(summary, testResults.Select(r => $"{r.Status}: {r.TestName}").ToList());
    }
}
