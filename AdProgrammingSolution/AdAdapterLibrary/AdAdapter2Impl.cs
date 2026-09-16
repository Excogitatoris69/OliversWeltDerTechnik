using System;
using System.DirectoryServices.Protocols;
using System.Net;

namespace AdAdapterLibrary
{
    public class AdAdapter2Impl
    {

        public void searchSimple1()
        {

            
            try
    {
                // 1. Verbindung definieren (Nutzt den Domänennamen auf Port 389)
                LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier(
                    "olimaster.de", 389, true, false
                    );
                //LdapDirectoryIdentifier identifier = new LdapDirectoryIdentifier("192.168.1.101", 389);
                
    
        using (LdapConnection connection = new LdapConnection(identifier))
        {
            // WICHTIG: Authentifizierung konfigurieren
            // AuthType.Negotiate nutzt Ihren aktuell angemeldeten Windows-Domänen-User
            connection.AuthType = AuthType.Negotiate; 
        
            // Diese Zeilen aktivieren die Verschlüsselung/Signierung (verhindert "Server down")
            connection.SessionOptions.Signing = true;
            connection.SessionOptions.Sealing = true;

            // 2. Verbindung real herstellen (Hier fliegt eine Exception, falls es fehlschlägt)
            connection.Bind();
            Console.WriteLine("Verbindung erfolgreich hergestellt!");

            // 3. Die Suchanfrage (SearchRequest) erstellen
            // Parameter 1: Wo soll gesucht werden (null = Root der Domäne)
            // Parameter 2: Der LDAP-Filter (identisch zum alten DirectorySearcher)
            // Parameter 3: Der Suchradius (Subtree durchsucht alles unterhalb des Startpunkts)
            // Parameter 4: Welche Attribute zurückgegeben werden sollen (null = alle)
            string searchFilter = "(&(objectCategory=user)(cn=p2002))";
            // 1. Suchbasis definieren (Der exakte Pfad zur gewünschten OU)
            string searchBaseDn = "OU=ADUser,DC=OLIMASTER,DC=DE";



                    SearchRequest request = new SearchRequest(
                searchBaseDn,
                searchFilter, 
                SearchScope.Subtree, 
                new string[] { "displayName", "mail", "distinguishedName" } // Gewünschte Attribute
            );

            // 4. Suche ausführen
            SearchResponse response = (SearchResponse)connection.SendRequest(request);

            // 5. Ergebnisse auswerten
            if (response.Entries.Count > 0)
            {
                SearchResultEntry entry = response.Entries[0];
            
                // So lesen Sie Attribute im neuen Format aus:
                string name = entry.Attributes["displayName"]?[0]?.ToString() ?? "Kein Name";
                string mail = entry.Attributes["mail"]?[0]?.ToString() ?? "Keine Mail";
            
                Console.WriteLine($"Gefunden: {name} ({mail})");
                Console.WriteLine($"DN: {entry.DistinguishedName}");
            }
            else
            {
                Console.WriteLine("Benutzer wurde im AD nicht gefunden.");
            }
        }
    }
    catch (LdapException ex)
    {
        // Detaillierte LDAP-Fehlermeldungen auswerten
        Console.WriteLine($"LDAP-Fehler ({ex.ErrorCode}): {ex.Message}");
        if (ex.ServerErrorMessage != null)
        {
            Console.WriteLine($"Server-Meldung: {ex.ServerErrorMessage}");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Allgemeiner Fehler: {ex.Message}");
    }

            
        }
    }
}
