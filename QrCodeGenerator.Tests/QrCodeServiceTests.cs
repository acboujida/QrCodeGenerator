using QrCodeGenerator.Api.Services;

namespace QrCodeGenerator.Tests;

[TestFixture]
public class QrCodeServiceTests
{
    [Test]
    public void GeneratePng_ValidUrl_ReturnsPng()
    {
        var service = new QrCodeService();
        string url = "https://google.com";

        byte[] result = service.GeneratePng(url);

        Assert.That(result, Is.Not.Empty);

        byte[] signature = { 137, 80, 78, 71, 13, 10, 26, 10 };

        Assert.That(result.Take(8), Is.EqualTo(signature));
    }
}