
using SimpleParser;

namespace Tutorial2
{
    public class MainApp
    {
        static void Main(string[] args)
        {
            MainApp me = new MainApp();
            me.call();
        }


        public void call()
        {
            string pathiniFile = @"E:\eigenes\Projekte\Tutorial_Antlr\AntlrTutorialSolution\SimpleParser\testdata\test_config.ini";
            ExtendedIniParserImpl extendedIniParser = new ExtendedIniParserImpl();
            extendedIniParser.parseIniFile(pathiniFile);

            Dictionary<string, string> data = extendedIniParser.configData;
            Console.WriteLine("Inhalt der Konfig-Datei");
            Console.WriteLine("--------------------------");
            foreach(string key in data.Keys)
            {
                Console.WriteLine(string.Format("{0} = {1}",key, data[key]));
            }


        }

    }
}
