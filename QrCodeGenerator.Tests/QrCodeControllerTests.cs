using Microsoft.AspNetCore.Mvc;
using Moq;
using QrCodeGenerator.Api.Controllers;
using QrCodeGenerator.Api.Dto;
using QrCodeGenerator.Api.Services;

namespace QrCodeGenerator.Tests;

[TestFixture]
public class QrCodeControllerTests
{
    [TestCase("invalid url")]
    [TestCase("ftp://example.com")]
    public void GenerateQrCode_InvalidUrl_ReturnsBadRequest(string url)
    {
        var mockService = new Mock<IQrCodeService>();
        var controller = new QrCodeController(mockService.Object);
        var request = new GenerateQrCodeRequest(url);

        var result = controller.GenerateQrCode(request);

        Assert.That(result, Is.InstanceOf<BadRequestObjectResult>());
        
        mockService.Verify(s=> s.GeneratePng(It.IsAny<string>()), Times.Never());
    }

    [Test]
    public void GenerateQrCode_ValidUrl_ReturnsPngFile()
    {
        var mockService = new Mock<IQrCodeService>();
        byte[] fakePng = {1, 2, 3, 4};
        mockService.Setup(s => s.GeneratePng(It.IsAny<string>())).Returns(fakePng);
        var controller = new QrCodeController(mockService.Object);
        var request = new GenerateQrCodeRequest("https://google.com");

        var result = controller.GenerateQrCode(request);

        Assert.That(result, Is.InstanceOf<FileContentResult>());

        var file = (FileContentResult)result;

        Assert.That(file.ContentType, Is.EqualTo("image/png"));
        Assert.That(file.FileDownloadName, Is.EqualTo("qrcode.png"));
        Assert.That(file.FileContents, Is.EqualTo(fakePng));

        mockService.Verify(s => s.GeneratePng("https://google.com"), Times.Once());
    }
}