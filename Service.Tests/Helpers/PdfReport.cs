using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using iText.IO.Image;

public class PdfReport
{
    private readonly string _pdfFilePath;

    public PdfReport(string pdfFilePath)
    {
        _pdfFilePath = pdfFilePath;
    }

    public void GenerateReport(List<(string TestName, string Status, string ScreenshotPath)> testResults)
    {
        // Certifique-se de que o PdfWriter não usa criptografia
        var writerProperties = new WriterProperties()
            .SetStandardEncryption(null, null, 0, EncryptionConstants.DO_NOT_ENCRYPT_METADATA);

        using (var pdfWriter = new PdfWriter(_pdfFilePath, writerProperties))
        using (var pdfDocument = new PdfDocument(pdfWriter))
        using (var document = new Document(pdfDocument))
        {
            document.Add(new Paragraph("Test Execution Report").SetFontSize(18));
            document.Add(new Paragraph($"Generated: {DateTime.Now}").SetFontSize(12));

            foreach (var (testName, status, screenshotPath) in testResults)
            {
                document.Add(new Paragraph($"Test: {testName}").SetFontSize(14));
                document.Add(new Paragraph($"Status: {status}").SetFontSize(12));
                
                if (!string.IsNullOrEmpty(screenshotPath) && File.Exists(screenshotPath))
                {
                    var imageData = ImageDataFactory.Create(screenshotPath);
                    var image = new Image(imageData).SetMaxWidth(500);
                    document.Add(image);
                }

                document.Add(new Paragraph("\n").SetFontSize(10)); // Espaço entre testes
            }
        }
    }
}
