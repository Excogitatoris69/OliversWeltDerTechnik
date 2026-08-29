using AdAdapterLibrary.dto;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Text;

namespace AdAdapterLibrary
{


    public class AdAdapterImpl
    {


        //---------------------------------------------------------------------------------------
        #region Chapter_1.1

        /// <summary>
        /// Sehr einfache Suche. Sucht nur das Attribute cn
        /// </summary>
        public void searchSimple1()
        {
            //build searcher
            DirectorySearcher directorySearcher = new DirectorySearcher();

            //Ausgabe-Attribute hinzufügen
            // SQL: SELECT cn FROM ...
            directorySearcher.PropertiesToLoad.Add("cn");

            //filter (where)
            // SQL: ... WHERE CN='p2001'
            directorySearcher.Filter = "(cn=p2002)";

            //Suche starten
            SearchResultCollection searchResultCollection = directorySearcher.FindAll();
            foreach (SearchResult searchResultItem in searchResultCollection)
            {
                Console.WriteLine(searchResultItem.Properties["cn"][0].ToString());
            }
        }


        /// <summary>
        /// Suche nach User. Sucht mehrere Attribute.
        /// </summary>
        public void searchSimple2()
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();

            //Ausgabe-Attribute hinzufügen
            string[] attributeList = { "cn", "distinguishedname", "givenname", "sn", "memberof" };
            foreach (string attributeItem in attributeList)
            {
                directorySearcher.PropertiesToLoad.Add(attributeItem);
            }
            
            //directorySearcher.Filter = "(&(objectCategory=user)(cn=p2002))";
            directorySearcher.Filter = "(&(objectCategory=user)(cn=p200*))";

            //Suche starten
            SearchResultCollection searchResultCollection = directorySearcher.FindAll();
            foreach (SearchResult searchResultItem in searchResultCollection)
            {
                foreach (string attributeItem in attributeList)
                {
                    if (searchResultItem.Properties[attributeItem] != null && searchResultItem.Properties[attributeItem].Count > 0)
                        Console.WriteLine(searchResultItem.Properties[attributeItem][0].ToString());
                }
                Console.WriteLine("------------------------------------------");
            }
        }

        /// <summary>
        /// Suche nach User. Vollständige Liste der Memberof.
        /// </summary>
        public void searchSimple3()
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();

            //Ausgabe-Attribute hinzufügen
            string[] attributeList = { "cn", "distinguishedname", "givenname", "sn", "memberof" };
            foreach (string attributeItem in attributeList)
            {
                directorySearcher.PropertiesToLoad.Add(attributeItem);
            }

            directorySearcher.Filter = "(&(objectCategory=user)(cn=p2002))";
            //directorySearcher.Filter = "(&(objectCategory=user)(cn=p200*))";

            //Suche starten
            SearchResultCollection searchResultCollection = directorySearcher.FindAll();
            foreach (SearchResult searchResultItem in searchResultCollection)
            {
                foreach (string attributeItem in attributeList)
                {
                    if (searchResultItem.Properties[attributeItem] != null && searchResultItem.Properties[attributeItem].Count > 0)
                    {
                        if (searchResultItem.Properties[attributeItem].Count > 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < searchResultItem.Properties[attributeItem].Count; i++)
                            {
                                if (i > 0) sb.Append(";");//wenn mehr als eines, dann mit Semikolon trennen ab dem zweiten
                                sb.Append(searchResultItem.Properties[attributeItem][i].ToString());
                            }
                            Console.WriteLine(sb.ToString());
                        }
                        else
                            Console.WriteLine(searchResultItem.Properties[attributeItem][0].ToString());
                    }
                }
                Console.WriteLine("------------------------------------------");
            }
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_1.2

        
        /// <summary>
        /// Suche nach Group. Vollständige Liste der Member.
        /// </summary>
        public void searchGroup1()
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();

            //Ausgabe-Attribute hinzufügen
            string[] attributeList = { "cn", "member"};
            foreach (string attributeItem in attributeList)
            {
                directorySearcher.PropertiesToLoad.Add(attributeItem);
            }

            directorySearcher.Filter = "(&(objectCategory=group)(cn=GRP_RES_Produktonssysteme))";

            //Suche starten
            SearchResultCollection searchResultCollection = directorySearcher.FindAll();
            foreach (SearchResult searchResultItem in searchResultCollection)
            {
                foreach (string attributeItem in attributeList)
                {
                    if (searchResultItem.Properties[attributeItem] != null && searchResultItem.Properties[attributeItem].Count > 0)
                    {
                        if (searchResultItem.Properties[attributeItem].Count > 1)
                        {
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < searchResultItem.Properties[attributeItem].Count; i++)
                            {
                                if (i > 0) sb.Append(";");
                                sb.Append(searchResultItem.Properties[attributeItem][i].ToString());
                            }
                            Console.WriteLine(sb.ToString());
                        }
                        else
                            Console.WriteLine(searchResultItem.Properties[attributeItem][0].ToString());
                    }
                }
                Console.WriteLine("------------------------------------------");
            }
        }

        /// <summary>
        /// Suche nach Person _ohne_ Angabe von Attributen.
        /// Ausgabe der LIste aller Attribute und deren Wert.
        /// </summary>
        public void showAttributes1()
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();

            directorySearcher.Filter = "(&(objectCategory=user)(cn=p2002))";

            SearchResult result = directorySearcher.FindOne();
            if (result != null)
            {
                ResultPropertyCollection propertyCollection = result.Properties;
                foreach (String propertyName in propertyCollection.PropertyNames)
                {
                    foreach (object? data in propertyCollection[propertyName])
                    {
                        Console.WriteLine("{0}={1}",propertyName, data.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Suche nach Person _mit_ Angabe von Attributen.
        /// Ausgabe der LIste aller Attribute und deren Wert.
        /// </summary>
        public void showAttributes2()
        {
            DirectorySearcher directorySearcher = new DirectorySearcher();

            directorySearcher.Filter = "(&(objectCategory=user)(cn=p2002))";

            //Ausgabe-Attribute hinzufügen
            string[] attributeList = { "cn", "distinguishedname", "givenname", "sn", "memberof" };
            foreach (string attributeItem in attributeList)
            {
                directorySearcher.PropertiesToLoad.Add(attributeItem);
            }

            SearchResult result = directorySearcher.FindOne();
            if (result != null)
            {
                ResultPropertyCollection propertyCollection = result.Properties;
                foreach (String propertyName in propertyCollection.PropertyNames)
                {
                    foreach (object? data in propertyCollection[propertyName])
                    {
                        Console.WriteLine("{0}={1}", propertyName, data.ToString());
                    }
                }
            }
        }


        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_2.1

        private string domainName = "OLIMASTER.DE";
        private string pathUser = "OU=ADUser,DC=OLIMASTER,DC=DE";
        private string pathGroup = "OU=ADGroups,DC=OLIMASTER,DC=DE";
        private string adAdmin = null;
        private string adAdminPw = null;
        private PrincipalContext principalContextUser = null;
        private PrincipalContext principalContextGroup = null;


        public AdAdapterImpl()
        {
            init();
        }

        private void init()
        {
            adAdmin = "Administrator"; adAdminPw = "Bagger-123";
            try
            {
                principalContextUser = new PrincipalContext(ContextType.Domain, domainName, pathUser, adAdmin, adAdminPw);
                principalContextGroup = new PrincipalContext(ContextType.Domain, domainName, pathGroup, adAdmin, adAdminPw);
            }
            catch (Exception e1)
            {
                string msg = e1.Message;
                throw;
            }

        }


        public void addUser(UserPropertiesDto userProperties)
        {
            UserPrincipal newUser = new UserPrincipal(principalContextUser, userProperties.cn, userProperties.password, true);
            newUser.GivenName = userProperties.givenname;
            newUser.Surname = userProperties.surname;
            newUser.UserPrincipalName = userProperties.pricipalname;
            newUser.DisplayName = string.Format("{0} {1}", userProperties.givenname, userProperties.surname);
            newUser.PasswordNeverExpires = true;
            newUser.Save();
        }

        public void deleteUser(UserPropertiesDto userProperties)
        {
            UserPrincipal foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userProperties.cn);

            foundUser.Delete();
        }

        public void updateUser(UserPropertiesDto userProperties, UserPropertiesDto newUserProperties)
        {
            UserPrincipal foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userProperties.cn);

            if (newUserProperties.cn != null)
                foundUser.Name = newUserProperties.cn;
            if (newUserProperties.givenname != null)
                foundUser.GivenName = newUserProperties.givenname;
            if (newUserProperties.surname != null)
                foundUser.Surname = newUserProperties.surname;
            if (newUserProperties.password != null)
                foundUser.ChangePassword(newUserProperties.passwordOld, newUserProperties.password);
            if (newUserProperties.pricipalname != null)
                foundUser.UserPrincipalName = newUserProperties.pricipalname;

            foundUser.Save();
        }


        public void addGroup(GroupPropertiesDto groupProperties)
        {
            GroupPrincipal newGroup = new GroupPrincipal(principalContextGroup, groupProperties.cn);

            newGroup.Save();
        }

        public void delGroup(GroupPropertiesDto groupProperties)
        {
            GroupPrincipal foundGroup = GroupPrincipal.FindByIdentity(principalContextGroup, IdentityType.Name, groupProperties.cn);

            foundGroup.Delete();
        }

        public void addUserToGroup(UserPropertiesDto userProperties, GroupPropertiesDto groupProperties)
        {
            GroupPrincipal foundGroup = GroupPrincipal.FindByIdentity(principalContextGroup, IdentityType.Name, groupProperties.cn);
            UserPrincipal foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userProperties.cn);
            foundGroup.Members.Add(foundUser);
            foundGroup.Save();
        }

        public void removeUserFromGroup(UserPropertiesDto userProperties, GroupPropertiesDto groupProperties)
        {
            GroupPrincipal foundGroup = GroupPrincipal.FindByIdentity(principalContextGroup, IdentityType.Name, groupProperties.cn);
            UserPrincipal foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userProperties.cn);
            foundGroup.Members.Remove(foundUser);
            foundGroup.Save();
        }

        public bool validateUserPassword(UserPropertiesDto userProperties)
        {
            return principalContextUser.ValidateCredentials(userProperties.cn, userProperties.password);
        }

        public void changeUserPassword(UserPropertiesDto userProperties)
        {
            UserPrincipal foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userProperties.cn);
            foundUser.ChangePassword(userProperties.passwordOld, userProperties.password);
        }


        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_2.2

        /*
        //Ist bereits in Chapter_2.1 enthalten und deshalb hier nur zur Dokumentation und auskommentiert!

        private string domainName = "OLIMASTER.DE";
        private string pathUser = "OU=ADUser,DC=OLIMASTER,DC=DE";
        private string pathGroup = "OU=ADGroups,DC=OLIMASTER,DC=DE";
        private string adAdmin = null;
        private string adAdminPw = null;
        private PrincipalContext principalContextUser = null;
        private PrincipalContext principalContextGroup = null;


        public AdAdapterImpl()
        {
            init();
        }

        private void init()
        {
            adAdmin = "Administrator";
            adAdminPw = "Bagger-123";
            principalContextUser = new PrincipalContext(ContextType.Domain, domainName, pathUser, adAdmin, adAdminPw);
            principalContextGroup = new PrincipalContext(ContextType.Domain, domainName, pathGroup, adAdmin, adAdminPw);
        }
        */

        /// <summary>
        /// Lädt Datei mit Gruppen in AD.
        /// Dateiaufbau: CSV, 2-spaltig: 
        /// Header: cn;description
        /// </summary>
        /// <param name="path"></param>
        public void loadGroupFile(string path)
        {
            string? line = null;
            bool first = true;
            GroupPrincipal newGroup = null;
            int lineNumber = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    lineNumber++;
                    if (string.IsNullOrEmpty(line)) break;
                    if (first)
                    { //header
                        first = false;
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string[] tokens = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            newGroup = new GroupPrincipal(principalContextGroup, tokens[0]);
                            newGroup.Description = tokens.Length == 2 ? tokens[1] : tokens[0];//wenn kein description, nimm name
                            newGroup.Save();
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine("Datensatz in Zeile {0} wurde nicht verarbeitet. Fehler: {1}", lineNumber, e1.Message);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Lädt Datei mit User in AD.
        /// Dateiaufbau: CSV 
        /// Header: cn;givenname;lastname;initials;displayname;description;company;department;office;state;
        ///         country;mail;tel;mobile;fax;address;postalcode;city;pricipalname;password
        /// </summary>
        /// <param name="path"></param>
        public void loadUserFile(string path)
        {
            string line = null;
            bool first = true;
            UserPrincipal newUser = null;
            Dictionary<string, int> headerDic = new Dictionary<string, int>();
            using (StreamReader sr = new StreamReader(path))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    if (first)
                    { //header
                        first = false;
                        string[] tokens = line.Split(';');
                        //namen und Index merken, dann haben wir es später einfacher
                        for (int x = 0; x < tokens.Length; x++)
                        {
                            headerDic[tokens[x]] = x;
                        }
                        continue;
                    }
                    else
                    {
                        string[] tokens = line.Split(';');
                        newUser = new UserPrincipal(principalContextUser, tokens[headerDic["cn"]], tokens[headerDic["password"]], true);
                        newUser.Save();
                        DirectoryEntry directoryEntry = newUser.GetUnderlyingObject() as DirectoryEntry;
                        
                        string[] headerFields = { "cn", "givenname", "lastname_sn", "initials", "displayname",
                            "description", "company", "department", "physicaldeliveryofficename", "state_co", "country_c",
                            "mail", "telephonenumber", "mobile", "facsimiletelephonenumber", "streetaddress", "postalcode", 
                            "city_l", "userPrincipalName", "password" };
                        string? propertyName = null;
                        foreach (string headerField in headerFields)
                        {
                            if (headerField.Contains('_'))
                            {
                                propertyName = headerField.Split('_')[1];
                            }
                            else
                                propertyName = headerField;
                            directoryEntry.Properties[propertyName].Value = tokens[headerDic[headerField]];
                        }
                        newUser.Save();
                    }
                }
            }
        }

        /// <summary>
        /// Lädt Datei mit Group-MemberOf in AD.
        /// Dateiaufbau: CSV 
        /// Header: groupcn;userlist
        /// userlist: p2001,p2002,...
        /// Bsp. Zeile: GRP_ROLE_Einkauf;p2001,p2002...
        /// </summary>
        /// <param name="path"></param>
        public void loadGroupMemberOfFile(string path)
        {
            string line = null;
            bool first = true;
            GroupPrincipal foundGroup = null;
            UserPrincipal foundUser = null;
            using (StreamReader sr = new StreamReader(path))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line)) break;
                    if (first)
                    { //header
                        first = false;
                        continue;
                    }
                    else
                    {
                        string[] tokens = line.Split(';');
                        foundGroup = GroupPrincipal.FindByIdentity(principalContextGroup, IdentityType.Name, tokens[0]);
                        if (foundGroup != null)
                        {
                            string[] userlist = tokens[1].Split(",");
                            foreach (string userItem in userlist)
                            {
                                foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userItem);
                                if (foundUser != null)
                                {
                                    foundGroup.Members.Add(foundUser);
                                    foundGroup.Save();
                                }
                            }
                        }
                    }
                }
            }
        }


        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_2.3

        public void createOU(string name)
        {

            string pathRoot = @"LDAP://DC=OLIMASTER,DC=DE";
            //PrincipalContext principalContext = new PrincipalContext(ContextType.Domain, domainName, pathRoot, adAdmin, adAdminPw);
            DirectoryEntry rootEntry = new DirectoryEntry(pathRoot, adAdmin, adAdminPw);
            DirectoryEntry directoryEntry = rootEntry.Children.Add(name, "organizationalUnit");
            directoryEntry.Properties["description"].Value = "Mein Test";
            directoryEntry.CommitChanges();



            /*

                static bool OuExists(string ldapPath, string ouName)
                {
                    using (DirectoryEntry entry = new DirectoryEntry(ldapPath))
                    {
                        using (DirectorySearcher searcher = new DirectorySearcher(entry))
                        {
                            // Filter: Sucht nach Objekten vom Typ 'organizationalUnit' mit dem spezifischen Namen
                            searcher.Filter = $"(&(objectClass=organizationalUnit)(ou={ouName}))";

                            // Suchtiefe: 'OneLevel' sucht nur direkt unter dem Pfad. 
                            // Nutze 'Subtree' für eine Tiefensuche im gesamten Verzeichnis.
                            searcher.SearchScope = SearchScope.OneLevel;

                            // FindOne() gibt null zurück, wenn nichts gefunden wurde
                            SearchResult result = searcher.FindOne();

                            return result != null;
                        }
                    }
                }    
            
             */

        }


        #endregion
        //---------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------

        //---------------------------------------------------------------------------------------

    }
}
