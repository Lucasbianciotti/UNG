using Models.Request;
using QRCoder;

namespace Web.LocalClass
{
    public static class CodeClass
    {
        public static string GenerateCodeQR(Station_Request station, Equipment_Request model)
        {
            try
            {
                station.IP_Private = station.IP_Private.Split('.')[0].TrimStart('0')
                    + "."
                    + station.IP_Private.Split('.')[1].TrimStart('0')
                    + "."
                    + station.IP_Private.Split('.')[2].TrimStart('0')
                    + "."
                    + station.IP_Private.Split('.')[3].TrimStart('0');

                string qr = "UNG_CONFIG={\"wifi\":{\"red\":\"" + station.SSID_Int +
                "\",\"password\":\"" + station.PASS_Int +
                "\",\"security\":" + station.PASS_Int_SecurityType +
                "},\"server\":{\"ip\":\"" + station.IP_Private +
                "\",\"host\":\"" + station.Host +
                "\",\"port\":" + station.Port +
                ",\"token\":\"\"}" +
                ",\"engine\":{\"initCommands\":[\"dssdd\"]}," +
                "\"droneID\":" + model.ID +
                ",\"tocken\":\"tocken\"}";

                return qr;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public static string ConvertCodeQRToSRC(string qr)
        {
            try
            {
                QRCodeGenerator qrGenerator = new();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr, QRCodeGenerator.ECCLevel.L);
                PngByteQRCode qrCode = new(qrCodeData);
                string qrCodeImageAsBase64 = Convert.ToBase64String(qrCode.GetGraphic(20));
                qr = "data:image/png;base64," + qrCodeImageAsBase64;

                return qr;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }
    }
}
