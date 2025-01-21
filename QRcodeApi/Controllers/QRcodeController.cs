using Microsoft.AspNetCore.Mvc;
using QRCoder;

namespace QRcodeApi.Controllers
{
    [Route("Api/[controller]")]
    [ApiController]
    public class QRcodeController : ControllerBase

    {
        [HttpGet("generate")]
        public IActionResult GenerateQrCode([FromQuery]  string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return BadRequest("Text cannot be empty or null");
            }
            try
            {
                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrcodedata = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);

                BitmapByteQRCode qrCode = new BitmapByteQRCode(qrcodedata);
                byte[] qrCodeimaage = qrCode.GetGraphic(20);

                return File(qrCodeimaage, "image/png");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

    }
}
