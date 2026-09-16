using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdTutorial_1.GenTestData
{
    public  class TestdataGeneratorAdapter
    {
        //Eingabedateien
        private string? pathTemplateUserDataFile = null;  // Eingabedatei der User
        private string? pathTemplateGroupDataFile = null;  // Eingabedatei der Groups

        private string? pathUserlistDataFile = null;  // Eingabedatei userlist für Random
        private string? pathGrouplistDataFile = null;  // Eingabedatei grouplist für Random

        //Ausgabedateien
        private string? pathUserDataFile = null; //Ausgabedatei mit allen User
        private string? pathGroupDataFile = null;  //Ausgabedatei mit allen Groups
        private string? pathGroupUserDataFile = null;  // Ausgabedatei mit Groupmember
        private string? pathRandomGroupUserDataFile = null;  // Ausgabedatei mit Groupmember per Random

        //Datenstrukturen
        private List<string> groupnameList; //merkt sich alle Gruppennamen
        private List<string> groupIdList; //merkt sich alle GruppenID
        private List<string> usernameList; //merkt sich alle Username
        private Dictionary<string, List<string>> groupMemberDic;  //GroupMemberDictionary

        private Random random;

        /// <summary>
        /// Alle Aufgaben nacheinander ausführen.
        /// </summary>
        public void genData()
        {
            genGroupImportfile();
            genUserImportfile();
            genGroupMemberImportfile();
            genRandomGroupMemberImportfile();
        }


        public TestdataGeneratorAdapter()
        {
            init();
        }

        /// <summary>
        /// Alle Parameter setzen
        /// </summary>
        private void init()
        {
            pathTemplateUserDataFile  =     @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\vornamen_nachnamen_template.csv";
            pathTemplateGroupDataFile =     @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\gruppen_template.csv";

            pathUserlistDataFile =          @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\userlist.csv";
            pathGrouplistDataFile =         @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\groupResourcenimport.csv";

            pathUserDataFile =              @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\userimport.csv";
            pathGroupDataFile =             @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\groupimport.csv";
            pathGroupUserDataFile =         @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\groupuserimport.csv";
            pathRandomGroupUserDataFile =   @"E:\eigenes\Projekte\Tutorial_AD-Programmierung\testdaten\randomGroupUserImport.csv";
            

            groupnameList = new List<string>();
            groupIdList = new List<string>();
            usernameList = new List<string>();
            groupMemberDic = new Dictionary<string, List<string>>();
            random = new Random();
        }

        



        /// <summary>
        /// Erstellt aus den Template-Daten eine Gruppen-Importdatei im Format CSV(Semikolon).
        /// </summary>
        private void genGroupImportfile()
        {
            string[] headerFields = { "cn", "description" };
            List<string[]> groupList = readTemplateFile(pathTemplateGroupDataFile);//ID;NAME
            StringBuilder sb = new StringBuilder();
            bool first = true;
            using (StreamWriter sw = new StreamWriter(pathGroupDataFile))
            {
                //header
                foreach (string fieldName in headerFields)
                {
                    if (first) first = false; else sb.Append(';');
                    sb.Append(fieldName);
                }
                sw.WriteLine(sb.ToString());
                //data
                foreach (string[] groupItem in groupList)
                {
                    sb.Clear();
                    string groupName = string.Format("GRP_ROLE_{0}", groupItem[1]);//Gruppenname zusammenbauen
                    groupnameList.Add(groupName);//Gruppenname merken für später
                    groupIdList.Add(groupItem[0]);//ID merken für später
                    //CSV bauen
                    sb.Append(groupName); //Gruppenname
                    sb.Append(";");
                    sb.Append(string.Format("Abteilung {1} ({0})", groupItem[0], groupItem[1])); //Description zusammenbauen
                    sw.WriteLine(sb.ToString());
                }
            }
        }

        /// <summary>
        /// Erstellt aus den Template-Daten eine User-Importdatei im Format CSV(Semikolon).
        /// </summary>
        private void genUserImportfile()
        {
            string[] headerFields = { "cn", "givenname", "lastname_sn", "initials", "displayname",
                "description", "company", "department", "physicaldeliveryofficename", "state_co", "country_c", 
                "mail", "telephonenumber", "mobile", "facsimiletelephonenumber", "streetaddress", "postalcode", "city_l", "userPrincipalName", "password" };
            List<string[]> userList = readTemplateFile(pathTemplateUserDataFile); //Nr.;Anrede;Vorname;Nachname
            StringBuilder sb = new StringBuilder();
            bool first = true;
            using (StreamWriter sw = new StreamWriter(pathUserDataFile))
            {
                //header
                foreach (string fieldName in headerFields)
                {
                    if (first) first = false; else sb.Append(';');
                    sb.Append(fieldName);
                }
                sw.WriteLine(sb.ToString());

                //Vorbereitung cn: Nummer, beginnend bei Wert 2001
                int userlistSize = userList.Count;
                string[] cn = new string[userlistSize];
                for (int x = 0; x < userlistSize; x++)
                {
                    cn[x] = string.Format("{0}", 2001 + x);
                }
                //Berechne Anzahl Gruppen pro User
                //Gruppen sollen gleichmäßig auf Benutzer aufgeteilt werden, so dass ungefähr jeder Benutzer in der gleichen Anzahl Gruppen enthalten ist.
                float v1 = (float)userlistSize / groupnameList.Count;
                int teiler = (int)Math.Round(v1); // --> 500 / 18 = 28

                //data
                int userIndex = 0;
                int groupIndex = 0;
                foreach (string[] userItem in userList)
                {
                    sb.Clear();
                    groupIndex = userIndex / teiler; // ergibt die Werte 0,1,2,..,17
                    if (groupIndex >= groupIdList.Count)
                        groupIndex = groupIdList.Count - 1;

                    string userName = string.Format("p{0}", cn[userIndex]);
                    usernameList.Add(userName);//merken für später
                    sb.Append(userName); //Name
                    sb.Append(";");
                    sb.Append(userItem[2]); //givenname
                    sb.Append(";");
                    sb.Append(userItem[3]); //lastname
                    sb.Append(";");
                    sb.Append(userItem[1]); //initial
                    sb.Append(";");
                    sb.Append(string.Format("{0}, {1}", userItem[3], userItem[2])); //Displayname: Nachname, Vorname
                    sb.Append(";");
                    sb.Append(string.Format("{0} {1}, ({2})", userItem[2], userItem[3], groupIdList[groupIndex])); //Description: Vorname Nachname, Department
                    sb.Append(";");
                    sb.Append("MeineFirma");//company
                    sb.Append(";");
                    sb.Append(groupIdList[groupIndex]); //department  
                    sb.Append(";");
                    sb.Append(string.Format("2-{0}", cn[userIndex])); //office
                    sb.Append(";");
                    sb.Append("Germany");//state
                    sb.Append(";");
                    sb.Append("DE");//country
                    sb.Append(";");
                    sb.Append(string.Format("p{0}@meinefirma.de", cn[userIndex]));//mail
                    sb.Append(";");
                    sb.Append(string.Format("+49894444{0}", cn[userIndex]));//tel
                    sb.Append(";");
                    sb.Append(string.Format("+49895555{0}", cn[userIndex]));//mobile
                    sb.Append(";");
                    sb.Append(string.Format("+49896666{0}", cn[userIndex]));//fax
                    sb.Append(";");
                    sb.Append("Maximilianstraße 1234");//address
                    sb.Append(";");
                    sb.Append("80539");//postalcode
                    sb.Append(";");
                    sb.Append("München");//city
                    sb.Append(";");
                    sb.Append(string.Format("p{0}@OLIMASTER.DE", cn[userIndex]));//pricipalname
                    sb.Append(";");
                    sb.Append("Geheim_123");//password
                    sw.WriteLine(sb.ToString());
                    
                    //Group-Member merken, da wir das später noch benötigen
                    if (!groupMemberDic.ContainsKey(groupnameList[groupIndex]))
                    {
                        groupMemberDic[groupnameList[groupIndex]] = new List<string>();
                    }
                    groupMemberDic[groupnameList[groupIndex]].Add(userName);
                    userIndex++;
                }
            }
        }

        /// <summary>
        /// Erstellt eine Importdatei im Format CSV(Semikolon) mit der Zuordnung von User zu Group.
        /// groupcn;userlist
        /// GRP_ROLE_Einkauf;p2001,p2002,..,p2500
        /// </summary>
        private void genGroupMemberImportfile()
        {
            string[] headerFields = { "groupcn", "userlist" };
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            bool first = true;
            using (StreamWriter sw = new StreamWriter(pathGroupUserDataFile))
            {
                //header
                foreach (string fieldName in headerFields)
                {
                    if (first) first = false; else sb.Append(';');
                    sb.Append(fieldName);
                }
                sw.WriteLine(sb.ToString());
                //data
                foreach (string keyItem in groupMemberDic.Keys)
                {
                    sb.Clear();
                    sb2.Clear();
                    first = true;
                    foreach (string userItem in groupMemberDic[keyItem])
                    {
                        if (first) first = false; else sb2.Append(',');
                        sb2.Append(userItem);
                    }
                    sb.Append(keyItem);
                    sb.Append(";");
                    sb.Append(sb2.ToString());
                    sw.WriteLine(sb.ToString());
                }
            }
        }


        /// <summary>
        /// Erstellt eine CSV-Datei mit zufälligen Kombinationen aus GroupMember.
        /// Benötigt wird eine Liste mit User, eine Liste mit Groups und eine Ausgabedatei.
        /// Aufbau Ausgabedatei: groupcn;userlist
        /// userlist: p2001,p2002,...
        /// Bsp. Zeile: GRP_ROLE_Einkauf;p2001,p2002...
        /// </summary>
        /// <param name="pathUserList"></param>
        /// <param name="pathGroupList"></param>
        private void genRandomGroupMemberImportfile()
        {

            Dictionary<string, List<string>> dic = new Dictionary<string, List<string>>();//groupname, listOfUser
            StringBuilder sb = new StringBuilder();
            bool first = true;
            List<string> userlist = readFile(pathUserlistDataFile);
            List<string> grouplist = readFile(pathGrouplistDataFile);
            int minGroupCount = (int)(grouplist.Count * 0.12);
            int maxGroupCount = (int)(grouplist.Count * 0.9);
            //prepare dic
            foreach (string groupNameItem in grouplist)
            {
                dic.Add(groupNameItem, new List<string>());
            }
            foreach (string userItem in userlist)
            {
                string[] randomGroupList = getRandomItems(minGroupCount, maxGroupCount, grouplist);
                foreach (string groupName in randomGroupList)
                {
                    dic[groupName].Add(userItem);
                }
            }
            //write file
            using (StreamWriter sw = new StreamWriter(pathRandomGroupUserDataFile))
            {
                sw.WriteLine("groupcn;userlist");//header
                foreach (string group in dic.Keys)
                {
                    sb.Clear();
                    first = true;
                    sb.Append(group);
                    sb.Append(';');
                    foreach (string user in dic[group])
                    {
                        if (first) first = false; else sb.Append(",");
                        sb.Append(user);
                    }
                    if (dic[group].Count > 0)
                    {
                        sw.WriteLine(sb.ToString());
                        Console.WriteLine(string.Format("Gruppe {0}: {1}", group, dic[group].Count));
                    }
                    else
                        Console.WriteLine("Kein Eintrag in Gruppe: " + group);
                }
            }

        }

        /// <summary>
        /// Erstellt aus einer Datalist eine zufällige Auswahl mit unterschiedlicher Anzahl
        /// zwischen min und max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="datalist"></param>
        /// <returns></returns>
        private string[] getRandomItems(int min, int max, List<string> datalist)
        {
            //Random random = new Random();
            int size = random.Next(min, max + 1);
            string[] result = new string[size];
            HashSet<string> selected = new HashSet<string>();
            for (int idx = 0; idx < size; idx++)
            {
                while (true)//duplikate vermeiden
                {
                    int pos = random.Next(0, datalist.Count);
                    //if (!result.Contains(datalist[pos]))//duplikate vermeiden
                    if (!selected.Contains(datalist[pos]))//duplikate vermeiden
                    {
                        result[idx] = datalist[pos];
                        selected.Add(datalist[pos]);
                        break;
                    }
                }
            }
            return result;
        }

        private List<string> readFile(string pathFile)
        {
            string? line = null;
            bool first = true;
            List<string> resultList = new List<string>(550);
            using (StreamReader sr = new StreamReader(pathFile))
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
                        string item = line.Split(';')[0];
                        resultList.Add(item);
                    }
                }
            }
            return resultList;
        }

        /// <summary>
        /// Hilfsfunktion liest eine CSV-Datei ein und liefert den Inhalt als Liste.
        /// </summary>
        /// <param name="pathFile"></param>
        /// <returns></returns>
        private List<string[]> readTemplateFile(string pathFile)
        {
            List<string[]> resultList = new List<string[]>();
            string? line = null;
            bool firstLine = true;
            using (StreamReader sr = new StreamReader(pathFile))
            {
                while (true)
                {
                    line = sr.ReadLine();
                    if (string.IsNullOrEmpty(line))
                        break;
                    else
                    {
                        if (firstLine)
                        {
                            firstLine = false;
                            continue;
                        }
                        string[] tokens = line.Split(';', StringSplitOptions.TrimEntries);
                        resultList.Add(tokens);
                    }
                }
            }
            return resultList;
        }


    }
}
