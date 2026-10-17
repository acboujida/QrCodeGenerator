using QRCoder;

namespace QrCodeGenerator.Api.Services;

public class QrCodeService : IQrCodeService
{
    public byte[] GeneratePng(string url)
    {
        using var generator = new QRCodeGenerator();

        using var qrCodeData = generator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

        using var qrCode = new PngByteQRCode(qrCodeData);

        return qrCode.GetGraphic(10);
    }
}