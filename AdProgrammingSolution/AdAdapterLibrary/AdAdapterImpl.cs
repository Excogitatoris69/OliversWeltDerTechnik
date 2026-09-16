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

        #region Chapter_2.0

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
        #endregion


        #region Chapter_2.1

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
                    { /*header*/
                        first = false;
                        continue;
                    }
                    else
                    {
                        try
                        {
                            string[] csvDataArray = line.Split(';', StringSplitOptions.RemoveEmptyEntries);
                            newGroup = new GroupPrincipal(principalContextGroup, csvDataArray[0]); //Name der Gruppe
                            newGroup.Description = csvDataArray.Length == 2 ? csvDataArray[1] : csvDataArray[0];//description. wenn kein description, nimm name
                            newGroup.Save();
                            /*
                            */
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
            string? csvDataValue=null;
            string password = null;
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
                        string[] headerArray = line.Split(';'); //Header-Spalten merken
                        /* Header-Spalten und Position merken, dann haben wir es später einfacher (1) */
                        for (int x = 0; x < headerArray.Length; x++)
                        {
                            headerDic[headerArray[x]] = x;
                        }
                        continue;
                    }
                    else
                    {
                        try 
                        {

                            /* Daten-Spalten */
                            string[] csvDataArray = line.Split(';'); 
                            /* Wenn ken Passwort enthalten ist, setzen wir ein neues.  */
                            if (headerDic.ContainsKey("password"))
                                password = csvDataArray[headerDic["password"]];
                            else
                                password = "Geheim.123";
                            /*
                             * Mit csvDataArray[headerDic["cn"]] holen wir die Daten aus der Spalte, in der cn steht. Die Position dazu erhalten wir aus 
                             * dem headerDic, in das wir beim Lesen der Header-Zeile die Position zu einem Header-Namen gespeichert hatten. Siehe (1)
                             */
                            newUser = new UserPrincipal(principalContextUser, csvDataArray[headerDic["cn"]], password, true);
                            newUser.Save();
                            DirectoryEntry directoryEntry = newUser.GetUnderlyingObject() as DirectoryEntry; //Entry holen, um die rstlichen Attribute einzutragen

                            string[] headerFieldsArray = { "givenname", "lastname_sn", "initials", "displayname",
                                "description", "company", "department", "physicaldeliveryofficename", "state_co", "country_c",
                                "mail", "telephonenumber", "mobile", "facsimiletelephonenumber", "streetaddress", "postalcode", 
                                "city_l", "userPrincipalName" };
                            string? propertyName = null;
                            foreach (string headerField in headerFieldsArray)
                            {
                                /*
                                 * Eine Header-Spalte kann den gleichen Namen haben, wie im AD (z.b. givenname),
                                 * oder einen anderen (wie z.b. lastname). In diesem Fall enthält die Header-Spalte beide Namen, getrennt durch einen Unterstrich
                                 * wie z.b. lastname_sn. Somit kann ich sowohl die Bedeutung der Spalte erkennen, als auch den AD-Attributenamen erhalten.
                                 */
                                if (headerField.Contains('_'))
                                {
                                    propertyName = headerField.Split('_')[1]; // im hinteren Teil steht der AD-Attributename
                                }
                                else
                                    propertyName = headerField; // Der Name ist gleich dem AD-Attributenamen

                                csvDataValue = csvDataArray[headerDic[headerField]]; // Wert aus Zeilenspalte und Position aus headerDic
                                setProperty(directoryEntry, propertyName, csvDataValue);
                            }
                            directoryEntry.CommitChanges();
                            newUser.Save();
                        }
                        catch (Exception e1)
                        {
                            Console.WriteLine("Datensatz in Zeile {0} wurde nicht verarbeitet. Fehler: {1}", lineNumber, e1.Message);
                        }
                    }
                }
            }
        }

        // Hilfsmethode zum sicheren Setzen von DirectoryEntry-Attributen
        private void setProperty(DirectoryEntry de, string propertyName, string propertyValue)
        {
            if (de.Properties.Contains(propertyName))
            {
                de.Properties[propertyName][0] = propertyValue;
            }
            else
            {
                de.Properties[propertyName].Add(propertyValue);
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
            int lineNumber = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    lineNumber++;
                    if (string.IsNullOrEmpty(line)) break;
                    if (first)
                    { /*header*/
                        first = false;
                        continue;
                    }
                    else
                    {
                        try 
                        { 
                            string[] csvDataArray = line.Split(';');
                            string[] userlistArray = csvDataArray[1].Split(",");//Liste der User (cn), durch Komma getrennt
                            foundGroup = GroupPrincipal.FindByIdentity(principalContextGroup, IdentityType.Name, csvDataArray[0]);  //Gruppenname
                            if (foundGroup != null)
                            {
                                foreach (string userItem in userlistArray)
                                {
                                    foundUser = UserPrincipal.FindByIdentity(principalContextUser, IdentityType.Name, userItem);
                                    if (foundUser != null)
                                    {
                                        foundGroup.Members.Add(foundUser);
                                    }
                                    else
                                    {
                                        Console.WriteLine("User {0} wurde nicht gefunden. Zeile {1} . ", lineNumber, userItem);
                                    }
                                }
                                foundGroup.Save();
                            }
                            else
                            {
                                Console.WriteLine("Gruppenname wurde nicht gefunden. Zeile {0} wurde nicht verarbeitet. ", lineNumber);
                            }
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
        /// Erstellt ein Backup aller User oder Gruppen eines AD in einer CSV-Datei.
        /// Anzugeben sind ein Pfad zur Datei sowie ein Filter.
        /// Der Filter hat z.b. die Form '(&(objectCategory=user)(cn=p2*))', um die Menge zu begrenzen.
        /// </summary>
        /// <param name="path"></param>
        /// <param name="searchFilter"></param>
        public void backupAdData(string path, string searchFilter)
        {

            try
            {
                DirectorySearcher directorySearcher = new DirectorySearcher();
                StringBuilder filtersb = new StringBuilder();
                directorySearcher.Filter = searchFilter; // filtersb.ToString();
                SearchResultCollection searchResultCollection = directorySearcher.FindAll();

                /* Diese Attribute sollen ignoriert werden */
                HashSet<string> blacklistAttributeHashset = new HashSet<string>();
                blacklistAttributeHashset.Add("lastlogon");
                blacklistAttributeHashset.Add("badpwdcount");
                blacklistAttributeHashset.Add("codepage");
                blacklistAttributeHashset.Add("usncreated");
                blacklistAttributeHashset.Add("pwdlastset");
                blacklistAttributeHashset.Add("whenchanged");
                blacklistAttributeHashset.Add("useraccountcontrol");
                blacklistAttributeHashset.Add("objectclass");
                blacklistAttributeHashset.Add("badpasswordtime");
                blacklistAttributeHashset.Add("dscorepropagationdata");
                blacklistAttributeHashset.Add("objectcategory");
                blacklistAttributeHashset.Add("whencreated");
                blacklistAttributeHashset.Add("objectguid");
                blacklistAttributeHashset.Add("objectsid");
                blacklistAttributeHashset.Add("lastlogoff");
                blacklistAttributeHashset.Add("instancetype");
                blacklistAttributeHashset.Add("logoncount");
                blacklistAttributeHashset.Add("samaccounttype");
                blacklistAttributeHashset.Add("usnchanged");
                blacklistAttributeHashset.Add("primarygroupid");
                blacklistAttributeHashset.Add("accountexpires");
                blacklistAttributeHashset.Add("countrycode");
                blacklistAttributeHashset.Add("grouptype"); //group

                /* alle Attribute sammeln und CSV-Header erstellen*/
                HashSet<string> attributeHashset = new HashSet<string>();
                if(searchResultCollection is not null)
                {
                    foreach (SearchResult searchResultItem in searchResultCollection)
                    {
                        // Alle geladenen Attribute dynamisch durchlaufen
                        foreach (string propName in searchResultItem.Properties.PropertyNames)
                        {
                            if(!attributeHashset.Contains(propName) && !blacklistAttributeHashset.Contains(propName))
                                attributeHashset.Add(propName);
                        }
                    }
                    StringBuilder sbHeader = new StringBuilder();
                    bool first = true;
                    foreach(string attribute in attributeHashset)
                    {
                        if ((first)) 
                            first = false;
                        else
                            sbHeader.Append(';');
                        sbHeader.Append(attribute);
                    }

                    /* Schreibe CVS Datei */
                    StringBuilder csvLineData = new StringBuilder();
                    StringBuilder csvCellData = new StringBuilder();
                    string currentAttribute = null;
                    using (StreamWriter sw = new StreamWriter(path))
                    {
                        sw.WriteLine(sbHeader.ToString());
                        foreach (SearchResult searchResultItem in searchResultCollection)
                        {
                            try
                            {
                                /* neue zeile */
                                csvLineData.Clear();
                                first = true;
                                /* alle zellen */
                                foreach (string attributeItem in attributeHashset)
                                {
                                    currentAttribute = attributeItem;
                                    if ((first))
                                        first = false;
                                    else
                                        csvLineData.Append(';');
                                    csvLineData.Append('"');
                                    if (searchResultItem.Properties[attributeItem] != null && searchResultItem.Properties[attributeItem].Count > 0)
                                    {
                                        if (searchResultItem.Properties[attributeItem].Count > 1)
                                        {
                                            /* Attribute mit mehr als einem Eintrag werden als Semikolon-getrennter String erstellt */
                                            csvCellData.Clear();
                                            for (int i = 0; i < searchResultItem.Properties[attributeItem].Count; i++)
                                            {
                                                if (i > 0) csvCellData.Append(";");//wenn mehr als eines, dann mit Semikolon trennen ab dem zweiten
                                                csvCellData.Append(searchResultItem.Properties[attributeItem][i].ToString());
                                            }
                                            csvLineData.Append(csvCellData.ToString());
                                        }
                                        else
                                            csvLineData.Append(searchResultItem.Properties[attributeItem][0].ToString());
                                    }
                                    csvLineData.Append('"');
                                }
                                sw.WriteLine(csvLineData.ToString());
                            }
                            catch (Exception e2)
                            {
                                Console.WriteLine($"Fehler bei Backup. currentAttribute='{currentAttribute}', csvLineData='{csvLineData.ToString()}', csvCellData='{csvCellData}'. Fehler: {e2.Message}");
                            }
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                Console.WriteLine($"Fehler bei Backup: {e1.Message}");
                throw;
            }
            //int wait = 0;//BP
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
