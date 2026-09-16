using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtpCore.dto
{
    public class CredentialsDto
    {
        public string username { get; set; } // Name oder Emailadresse
        public string password { get; set; }  //selbstgewähltes Passwort
        public string secretkey { get; set; }  // Generierter String als Base32Encoding
    }
}
