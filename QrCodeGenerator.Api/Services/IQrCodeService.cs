namespace QrCodeGenerator.Api.Services;

public interface IQrCodeService
{
    byte[] GeneratePng(string url);
}