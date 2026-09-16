using OtpCore.dto;
using PersistenceAdapter;

namespace OtpTest
{
    [TestClass]
    public sealed class PersistenceAdapterTest
    {
        public string pathDatafile = @"c:\Daten\oliver\Projekte\visualstudio\Tutorial_TOTP\testdaten\otpdata.csv";

        [TestMethod]
        public void Test_01_WriteReadCheckData()
        {
            PersistenceAdapterImpl adapter = new PersistenceAdapterImpl(pathDatafile);

            CredentialsDto cred1 = new CredentialsDto();
            cred1.username = "oliver";
            cred1.password = "Geheim123";
            cred1.secretkey = "xxxxxxxxx";
            CredentialsDto cred2 = new CredentialsDto();
            cred2.username = "nils";
            cred2.password = "Geheim456";
            cred2.secretkey = "yyyyyyyyyy";

            adapter.setCredential(cred1);
            adapter.setCredential(cred2);
            adapter.writeCredentialsData();

            adapter.readCredentialsData();
            CredentialsDto cred1b = adapter.getCredential(cred1.username);
            CredentialsDto cred2b = adapter.getCredential(cred2.username);
            
            Assert.IsTrue(cred1b.username.Equals(cred1.username));
            Assert.IsTrue(cred1b.password.Equals(cred1.password));
            Assert.IsTrue(cred1b.secretkey.Equals(cred1.secretkey));

            Assert.IsTrue(cred2b.username.Equals(cred2.username));
            Assert.IsTrue(cred2b.password.Equals(cred2.password));
            Assert.IsTrue(cred2b.secretkey.Equals(cred2.secretkey));

        }
    }
}
