using AdAdapterLibrary;
using AdAdapterLibrary.dto;
using AdTutorial_1.GenTestData;

namespace AdTutorial_1
{
    internal class MainApp
    {
        static void Main(string[] args)
        {
            MainApp me = new MainApp();
            /*
            //me.searchSimple();
            //me.searchGroup();
            //me.showProperties();
            //me.genTestData();
            */
            
            me.validateUserPassword();


            #region test
            //me.createOU1();
            //me.testRandom();
            //me.testGroupInde();

            #endregion

        }

        //---------------------------------------------------------------------------------------
        #region Chapter_1.1


        public void searchSimple()
        {
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();
            //AdAdapter2Impl adAdapterImpl = new AdAdapter2Impl();
            //adAdapterImpl.searchSimple1();
            //adAdapterImpl.searchSimple2();
            adAdapterImpl.searchSimple3();
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_1.2

        public void searchGroup()
        {
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();
            adAdapterImpl.searchGroup1();
        }

        public void showProperties()
        {
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();
            //adAdapterImpl.showAttributes1();
            adAdapterImpl.showAttributes2();
        }


        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_2.1

        public void executeAddUser()
        {
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();
            //User-Settings definieren
            UserPropertiesDto newUserProperties = new UserPropertiesDto();
            newUserProperties.cn = "p3001";
            newUserProperties.password = "Geheim.0815";
            newUserProperties.givenname = "Moritz";
            newUserProperties.surname = "Baumann";
            newUserProperties.pricipalname = "p3001@OLIMASTER.DE";

            //User anlegen
            adAdapterImpl.addUser(newUserProperties);

        }

        public void executeDeleteUser()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            //User-Settings definieren
            UserPropertiesDto newUserProperties = new UserPropertiesDto();
            newUserProperties.cn = "p3001";

            //User löschen
            adAdapterImpl.deleteUser(newUserProperties);
        }

        public void validateUserPassword()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            //User-Settings definieren
            UserPropertiesDto userProperties = new UserPropertiesDto();
            userProperties.cn = "p2012";
            userProperties.password = "Hugo_456.Babu";
            //userProperties.password = "falsch";

            bool result = adAdapterImpl.validateUserPassword(userProperties);
            if (result)
                Console.WriteLine("Passwort ist korrekt");
            else
                Console.WriteLine("Passwort ist unkorrekt");
        }

        public void changeUserPassword()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            //User-Settings definieren
            UserPropertiesDto userProperties = new UserPropertiesDto();
            userProperties.cn = "p2012";
            userProperties.passwordOld = "Geheim_123";
            userProperties.password = "Hugo_456.Babu";

            adAdapterImpl.changeUserPassword(userProperties);

        }

        public void executeUpdateUser()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            //User-Settings definieren
            UserPropertiesDto userProperties = new UserPropertiesDto();
            userProperties.cn = "p3001";

            UserPropertiesDto newUserProperties = new UserPropertiesDto();
            newUserProperties.surname = "Baumann-Maier";

            adAdapterImpl.updateUser(userProperties, newUserProperties);

        }

        public void executeAddAndDeleteGroup()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            GroupPropertiesDto groupProperties = new GroupPropertiesDto();
            groupProperties.cn = "Gruppe_Episode2";

            //adAdapterImpl.addGroup(groupProperties);
            adAdapterImpl.delGroup(groupProperties);

        }

        public void executeAddUserToGroup()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            GroupPropertiesDto groupProperties = new GroupPropertiesDto();
            groupProperties.cn = "Gruppe_Episode2";
            adAdapterImpl.addGroup(groupProperties);

            //User-Settings definieren
            UserPropertiesDto newUserProperties = new UserPropertiesDto();
            newUserProperties.cn = "p3008";
            newUserProperties.password = "Geheim.0815";
            newUserProperties.givenname = "Maria";
            newUserProperties.surname = "Kaiser";
            newUserProperties.pricipalname = "p3008@OLIMASTER.DE";
            adAdapterImpl.addUser(newUserProperties);

            adAdapterImpl.addUserToGroup(newUserProperties, groupProperties);
        }

        public void executeRemoveUserFromGroup()
        {
            //Adapter instanzieren
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();

            GroupPropertiesDto groupProperties = new GroupPropertiesDto();
            groupProperties.cn = "Gruppe_Episode2";

            //User-Settings definieren
            UserPropertiesDto newUserProperties = new UserPropertiesDto();
            newUserProperties.cn = "p3008";
            adAdapterImpl.removeUserFromGroup(newUserProperties, groupProperties);
        }

        #endregion
        //---------------------------------------------------------------------------------------
        #region Chapter_2.2

        public void loadTestData()
        {

        }

        #endregion


        //---------------------------------------------------------------------------------------
        #region Chapter_2.3

        public void createOU1()
        {
            AdAdapterImpl adAdapterImpl = new AdAdapterImpl();
            adAdapterImpl.createOU("OU=ADTest1");


        }

        #endregion

        //---------------------------------------------------------------------------------------
        #region Chapter_3

        //TestdataGenerator
        public void genTestData()
        {
            TestdataGeneratorAdapter genAdapter = new TestdataGeneratorAdapter();
            genAdapter.genData();

        }

        #endregion


        //---------------------------------------------------------------------------------------
        #region Stuff


        public void testRandom()
        {
            int count = 30;

            int min = (int)(count * 0.12);
            int max = (int)(count * 0.9);

            Random random = new Random();
            int size = 0;
            for (int x = 0; x < 200; x++)
            {
                size = random.Next(min, max + 1);
                Console.WriteLine("Wert: {0}", size);
            }

            int wait = 0;
        }

        public void testGroupInde()
        {
            float v1 = (float)500 / 18;
            int teiler = (int)Math.Round(v1); // --> 500 / 18 ~= 28

            int groupIndex = 0;
            for(int i = 0; i < 500; i++)
            {
                groupIndex = i / teiler;
                if (groupIndex >= 18)
                    groupIndex = 17;
                Console.WriteLine("i = {0} groupIndex = {1}",i,groupIndex);
            }
        }

        #endregion


    }
}
