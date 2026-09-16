namespace OtpCore.interfaces
{
    public interface IOtpAdapter
    {
        /// <summary>
        /// Prüft den Code und gibt True zurück, wenn Code valide ist.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        public bool validate(byte[] secretKey, string code);

        /// <summary>
        /// Erstewllt aus dem URI ein QR-Bild und liefert es als Byte-Array.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public byte[] getQRCodeImage(string username, string issuer, byte[] secretKey);
        
        /// <summary>
        /// Erstellt einen zufälligen Schlüssel und formatiert ihn als Base32Encoding.
        /// </summary>
        /// <returns></returns>
        public string createRandomKey();

        /// <summary>
        /// Liefert Otp-Code eines SecretKey.
        /// </summary>
        /// <param name="secretKey"></param>
        /// <returns></returns>
        public string getCode(byte[] secretKey);

    }

}
