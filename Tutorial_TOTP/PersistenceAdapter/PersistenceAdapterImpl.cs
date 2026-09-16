using OtpCore.dto;
using OtpCore.interfaces;
using System.Net;
using System.Text;

namespace PersistenceAdapter
{
    public class PersistenceAdapterImpl : IPersistenceAdapter
    {
        private string pathDatafile=null;
        private Dictionary<string, CredentialsDto> data = null;


        public PersistenceAdapterImpl(string pathDatafile)
        {
            data = new Dictionary<string, CredentialsDto>();
            this.pathDatafile = pathDatafile;
        }


        public List<CredentialsDto> getCredentialsList()
        {
            return data.Values.ToList();
        }

        public CredentialsDto getCredential(string username)
        {
            CredentialsDto credentials = null;
            bool result = data.TryGetValue(username, out credentials);
            return credentials;
        }
        public void setCredential(CredentialsDto credentials)
        {
            if (data.ContainsKey(credentials.username))
            {
                CredentialsDto foundCredentials = getCredential(credentials.username);
                foundCredentials.password = credentials.password;
                
            }
            else
            {
                data.Add(credentials.username, credentials);
            }
        }

        /// <summary>
        /// Liest Daten aus Datei.
        /// Format: CSV:  username;password;secretkey
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void readCredentialsData()
        {
            string line = null;
            data.Clear();
            if (!File.Exists(pathDatafile))
            {
                File.WriteAllText(pathDatafile, string.Empty);
            }
            using (StreamReader sr = new StreamReader(pathDatafile))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (line != null)
                    {
                        string[] tokens = line.Split(';');
                        CredentialsDto credentials = new CredentialsDto();
                        credentials.username = tokens[0];
                        credentials.password = tokens[1];
                        credentials.secretkey = tokens[2];
                        data.Add(credentials.username, credentials);
                    }
                    else
                        break;
                }
            }
        }

        /// <summary>
        /// Schreibt Daten in Datei.
        /// </summary>
        public void writeCredentialsData()
        {
            StringBuilder sb = new StringBuilder();
            using(StreamWriter sw = new StreamWriter(pathDatafile))
            {
                foreach(CredentialsDto item in data.Values)
                {
                    sb.Clear();
                    sb.Append(item.username);
                    sb.Append(';');
                    sb.Append(item.password);
                    sb.Append(';');
                    sb.Append(item.secretkey);
                    sw.WriteLine(sb.ToString());
                }
            }
        }
    }
}
