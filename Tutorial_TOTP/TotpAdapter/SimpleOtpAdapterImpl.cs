using OtpNet;
using QRCoder;
namespace SimpleOtpAdapter
{
    public class SimpleOtpAdapterImpl
    {
        /// <summary>
        /// Registriert einen Benutzer und erstellt einen QR-Code als Bilddatei.
        /// </summary>
        public void registerAccount(string issuer, string username, byte[] secretKey)
        {
            string escapedIssuer = Uri.EscapeDataString(issuer);
            string escapedUsername = Uri.EscapeDataString(username);
            string base32Password = Base32Encoding.ToString(secretKey);

            /* 
             Url erstellen: 

             otpauth://totp/OliversOtpDemo:oliver?secret=JVSWS3SHMVUGK2LNNFZV6MJSGM======&issuer=OliversOtpDemo&digits=6&period=30

            */
            string otpLabel = string.Format("{0}:{1}", escapedIssuer, escapedUsername);
            string otpUri = string.Format("otpauth://totp/{0}?secret={1}&issuer={2}&digits=6&period=30"
                , otpLabel
                , base32Password
                , escapedIssuer
                );

            /* QR erstellen... */
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(otpUri, QRCodeGenerator.ECCLevel.Q);
            PngByteQRCode qrCode = new PngByteQRCode(qrCodeData);
            byte[] qrCodeImage = qrCode.GetGraphic(10);

            /* und als Bilddatei speichern  */
            string imageFilePath = @"E:\eigenes\Projekte\Tutorial_TOTP\images\QR-Image.png";
            File.WriteAllBytes(imageFilePath, qrCodeImage);
        }

        #region example2
        /// <summary>
        /// Validiert einen Code.
        /// </summary>
        public bool validateAccount(byte[] secretKey, string code)
        {
            Totp totp = new Totp(secretKey);
            bool isValid = totp.VerifyTotp(
                code,
                out long timeStepMatched,
                VerificationWindow.RfcSpecifiedNetworkDelay);
            return isValid;
        }


        #endregion
    }
}
