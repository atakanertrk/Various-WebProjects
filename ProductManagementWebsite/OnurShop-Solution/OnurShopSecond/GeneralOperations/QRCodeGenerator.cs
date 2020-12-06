using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnurShopSecond
{
    public class QRCodeGenerator
    {
        public byte[] _imageByte { get; set; }
        public QRCodeGenerator(string urlPath, int selectedProductId)
        {
            try
            {
                QRCoder.QRCodeGenerator qrGenerator = new QRCoder.QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(urlPath, QRCoder.QRCodeGenerator.ECCLevel.Q);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeBitmap = qrCode.GetGraphic(20);
                _imageByte = (byte[])new ImageConverter().ConvertTo(qrCodeBitmap, typeof(byte[]));
            }
            catch (Exception ex)
            {
                throw new Exception("QR code couldnt generated", ex);
            }
        }
    }
}
