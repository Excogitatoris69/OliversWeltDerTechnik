using OtpCore.dto;
using OtpCore.interfaces;
using System.IO;
using System.Net;
using System.Text;


namespace OtpApplication
{
    public class OtpApplicationService
    {
        private IOtpAdapter otpAdapter;
        private IPersistenceAdapter persistenceAdapter;
        private string issuer;

        public OtpApplicationService(IOtpAdapter otpAdapter, 
            IPersistenceAdapter persistenceAdapter,
            string issuer)
        {
            this.otpAdapter = otpAdapter;
            this.persistenceAdapter = persistenceAdapter;
            this.issuer = issuer;
        }
            

        /// <summary>
        /// Muss beim Start der App ausgeführt werden.
        /// </summary>
        public void init()
        {
            persistenceAdapter.readCredentialsData();
        }

        /// <summary>
        /// Muss beim Beenden der App ausgeführt werden.
        /// </summary>
        public void saveData()
        {
            persistenceAdapter.writeCredentialsData();
        }

        public void registerNewUser(CredentialsDto credentials)
        {
            string base32SecretKey = otpAdapter.createRandomKey();
            credentials.secretkey = base32SecretKey;
            persistenceAdapter.setCredential(credentials);
            persistenceAdapter.writeCredentialsData();
        }

        public CredentialsDto getCredentialsOfUsername(string username)
        {
            return persistenceAdapter.getCredential(username);
        }

        public bool checkUsersPassword(string username, string password)
        {
            bool result = false;
            CredentialsDto credentials = persistenceAdapter.getCredential(username);
            if (credentials != null && credentials.password.Equals(password))
                result = true;
            return result;
        }

        public byte[]? getQRImageAsByteArray(string username, string password)
        {
            CredentialsDto credentials = persistenceAdapter.getCredential(username);
            if (credentials != null && credentials.password.Equals(password))
            {
                byte[] imageAsByte = otpAdapter.getQRCodeImage(username, issuer,
                    Encoding.UTF8.GetBytes(credentials.secretkey));
                //writeImageToFile(imageAsByte, username);
                return imageAsByte;
            }
            else
                return null;
        }

        private void writeImageToFile(byte[] imageAsByte, string username)
        {
            string imageFilePath = @"c:\Daten\oliver\Projekte\visualstudio\Tutorial_TOTP\images\";
            imageFilePath = Path.Combine(imageFilePath, username + ".png");
            try
            {
                File.WriteAllBytes(imageFilePath, imageAsByte);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool validateUser(string username, string code)
        {
            bool result = false;
            CredentialsDto credentials = persistenceAdapter.getCredential(username);
            if (credentials != null)
            {
                byte[] byteData = Encoding.UTF8.GetBytes(credentials.secretkey);
                byte[] byteData2 = Encoding.Default.GetBytes(credentials.secretkey);
                result = otpAdapter.validate(byteData, code);
            }
            return result;
        }

        public string getOptCodeOfAllCredentials()
        {
            StringBuilder sb = new StringBuilder();
            foreach(CredentialsDto credItem in persistenceAdapter.getCredentialsList())
            {
                sb.Append(credItem.username);
                sb.Append(": ");
                sb.Append(otpAdapter.getCode(Encoding.UTF8.GetBytes(credItem.secretkey)));
                sb.Append('\n');
            }
            return sb.ToString();
        }

    }
}
