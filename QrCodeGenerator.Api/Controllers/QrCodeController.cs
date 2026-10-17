using Microsoft.AspNetCore.Mvc;
using QrCodeGenerator.Api.Dto;
using QrCodeGenerator.Api.Services;

namespace QrCodeGenerator.Api.Controllers;

[ApiController]
[Route("api/qrcode")]
public class QrCodeController : ControllerBase
{

    private readonly IQrCodeService _qrCodeService;

    public QrCodeController(IQrCodeService qrCodeService)
    {
        _qrCodeService = qrCodeService;
    }

    [HttpPost]
    public IActionResult GenerateQrCode(GenerateQrCodeRequest request)
    {
        if (!Uri.TryCreate(request.Url, UriKind.Absolute, out var uri))
        {
            return BadRequest("The link is invalid.");
        }

        if (uri.Scheme != Uri.UriSchemeHttp && uri.Scheme != Uri.UriSchemeHttps)
        {
            return BadRequest("Only HTTPS and HTTP are supported.");
        }

        var bytes = _qrCodeService.GeneratePng(request.Url);

        return File(bytes, "image/png", "qrcode.png");
    }
}