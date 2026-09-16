using OtpCore.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtpCore.interfaces
{
    /// <summary>
    /// Speichert Credentials in Datei und verwalter diese.
    /// </summary>
    public interface IPersistenceAdapter
    {
        /// <summary>
        /// Liefert ein CredentialsDto eines Username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public CredentialsDto getCredential(string username);

        /// <summary>
        /// Speichert ein Crednetial oder macht Update.
        /// </summary>
        /// <param name="credentials"></param>
        public void setCredential(CredentialsDto credentials);

        /// <summary>
        /// Liest alle Credentials aus Datei.
        /// </summary>
        public void readCredentialsData();

        /// <summary>
        /// Speichert alle Credentials in Datei.
        /// </summary>
        public void writeCredentialsData();

        /// <summary>
        /// LIefert LIste aller Credentials
        /// </summary>
        /// <returns></returns>
        public List<CredentialsDto> getCredentialsList();

    }

}
