using Xunit;
using Xunit.Abstractions;
using Service.Tests.Helpers;

namespace Service.Tests;

public class TestRunner
{
    private readonly SlackNotifier _notifier;

    public TestRunner()
    {
        _notifier = new SlackNotifier("https://hooks.slack.com/services/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");
    }

    [Fact]
    public async Task RunTestsAndReportToSlack()
    {
        // var results = new List<string>();

        // try
        // {
        //     var tests = new SampleTests();
        //     tests.VerifyGoogleHomePageTitle();
        //     results.Add("Test: VerifyGoogleHomePageTitle - Passed");

        //     tests.VerifySearchFunctionality();
        //     results.Add("Test: VerifySearchFunctionality - Passed");
        // }
        // catch (Exception ex)
        // {
        //     results.Add($"Test Failed: {ex.Message}");
        // }

        // string report = string.Join("\n", results);
        // await _notifier.SendMessageAsync($"Test Execution Report:\n{report}", results);
    }
}
