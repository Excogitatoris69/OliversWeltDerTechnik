using OtpNet;
using System.Text;
using SimpleOtpAdapter;

namespace OtpMainApp
{
    internal class MainApp
    {
        SimpleOtpAdapterImpl otpAdapterImpl;

        static void Main(string[] args)
        {
            MainApp me = new MainApp();
            me.otpAdapterImpl = new SimpleOtpAdapterImpl();

            string issuer = "OliversOtpDemo";
            string user = "oliverderMeister";
            /*
             * string user = "oliver@OliversOtpDemo.de";
             */

            /* Das secretKey kann der Benutzer vergeben oder es wird automatisch 
               vom System generiert und in einer internen Datenbank gespeichert.
               Alternative:
               byte[] secretKey =  KeyGeneration.GenerateRandomKey();
            */
            byte[] secretKey = Encoding.UTF8.GetBytes("Mein-Sehr-Langes-Geheimis_123");

            //--------------------------------------------------
            //me.otpAdapterImpl.registerAccount(issuer, user, secretKey);
            me.validate(secretKey, "585695");
        }

        #region example2
        /// <summary>
        /// Testet einen Code. Dieser wird z.b. von der App Authenticator erstellt.
        /// Zur Eingabe hat man max. 30 Sekunden Zeit.
        /// </summary>
        public void validate(byte[] secretKey, string code)
        {
            bool result = otpAdapterImpl.validateAccount(secretKey, code);
            if (result)
                Console.WriteLine("Code is valid.");
            else
                Console.WriteLine("Code is not valid.");
        }

        #endregion
    }
}
