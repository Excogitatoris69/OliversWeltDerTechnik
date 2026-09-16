using OtpCore.interfaces;
using OtpNet;
using QRCoder;
using System.Net;
using System.Text;

namespace OtpAdapter
{
    public class OtpAdapterImpl : IOtpAdapter
    {

        public byte[] getQRCodeImage(string username, string issuer, byte[] secretKey)
        {
            string escapedIssuer = Uri.EscapeDataString(issuer);
            string escapedUser = Uri.EscapeDataString(username);
            string base32SecretKey = Base32Encoding.ToString(secretKey);
            string otpUri = $"otpauth://totp/{escapedIssuer}:{escapedUser}?secret={base32SecretKey}&issuer={escapedIssuer}&digits=6&period=30";

            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(otpUri, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(10);
            return qrCodeImage;
        }

        public bool validate(byte[] secretKey, string code)
        {
            Totp totp = new Totp(secretKey);
            bool isValid = totp.VerifyTotp(
                code,
                out long timeStepMatched,
                VerificationWindow.RfcSpecifiedNetworkDelay);
            return isValid;
        }

        public string getCode(byte[] secretKey)
        {
            Totp totp = new Totp(secretKey);
            string totpCode = totp.ComputeTotp(DateTime.UtcNow);
            return totpCode;
        }


        public string createRandomKey()
        {
            return Base32Encoding.ToString(KeyGeneration.GenerateRandomKey());
        }

    }
}
