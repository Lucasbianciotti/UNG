using CommonClass;
using CommonModels.Request;
using QRCoder;

namespace Class
{
    public static class QRCodeClass
    {
        public static async Task<Equipment_Request> GenerateCodeQR(Station_Request station, Equipment_Request model)
        {
            try
            {
                //station.IP_Private = ConvertToIPClass.ConvertToIP(station.IP_Private);

                model.QRcode = "UNG_CONFIG={\"wifi\":{\"red\":\"" + station.SSID_Int + "\",\"password\":\"" + station.PASS_Int + "\",\"security\":" + station.PASS_Int_SecurityType + "}" +
                ",\"server\":{\"ip\":\"" + station.IP_Private + "\",\"host\":\"" + station.Host + "\",\"port\":" + station.Port + ",\"token\":\"\"}" +
                ",\"engine\":{\"initCommands\":[]}" +
                ",\"droneID\":" + model.ID +
                ",\"tocken\":\"tocken\"}";

                model.QRcodeSRC = await ConvertCodeQRToSRC(model.QRcode);

            }
            catch (Exception ex)
            {

            }

            return model;
        }

        public static string ShowQRCode(Station_Request station, string code)
        {
            if (station.ShowQR)
                return code;
            else
                return String.Empty;
        }

        private static async Task<string> ConvertCodeQRToSRC(string qr)
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
