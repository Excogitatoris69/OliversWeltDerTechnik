using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using ExtendedIniParser;

namespace SimpleParser
{
    /*
     * Nuget-Package Antlr installieren!
     */


    public class ExtendedIniParserBaseImpl : ExtendedIniGrammarBaseVisitor<string>
    {
        public void parseIniFile(string pathInifile)
        {
            ICharStream charStream = CharStreams.fromPath(pathInifile);
            ExtendedIniGrammarLexer lexer = new ExtendedIniGrammarLexer(charStream);
            CommonTokenStream commonTokenStream = new CommonTokenStream(lexer);

            ExtendedIniGrammarParser parser = new ExtendedIniGrammarParser(commonTokenStream);
            ExtendedIniGrammarParser.DocumentContext elementContext = parser.document();
            Visit(elementContext);

            //int wait = 0;
        }
    }



    public class ExtendedIniParserImpl : ExtendedIniParserBaseImpl
    {



        //---------------------------------------------
        #region Example1
        
        /*
        public override string VisitAssignment([NotNull] ExtendedIniGrammarParser.AssignmentContext context)
        {
            var contextAll = context.GetText();
            var childcount = context.ChildCount;

            string[] data = new string[context.ChildCount];
            for (int x = 0; x < context.ChildCount; x++)
            {
                data[x] = context.GetChild(x).GetText();
            }

            return base.VisitAssignment(context);
        }
        
        
        
        public override string VisitChapter([NotNull] ExtendedIniGrammarParser.ChapterContext context)
        {
            var contextAll = context.GetText();
            var childcount = context.ChildCount;
            var child0 = context.GetChild(0).GetText();

            return base.VisitChapter(context);
        }
        public override string VisitValue([NotNull] ExtendedIniGrammarParser.ValueContext context)
        {
            var contextAll = context.GetText();
            var childcount = context.ChildCount;
            var child0 = context.GetChild(0).GetText();

            return base.VisitValue(context);
        }

        public override string VisitDocument([NotNull] ExtendedIniGrammarParser.DocumentContext context)
        {
            var contextAll = context.GetText();
            var childcount = context.ChildCount;

            string[] data = new string[context.ChildCount];
            for (int x = 0; x < context.ChildCount; x++)
            {
                data[x] = context.GetChild(x).GetText();
            }

            return base.VisitDocument(context);
        }

        public override string VisitElement([NotNull] ExtendedIniGrammarParser.ElementContext context)
        {
            var contextAll = context.GetText();
            var childcount = context.ChildCount;
            var child0 = context.GetChild(0).GetText();

            return base.VisitElement(context);
        }
        */
        

        #endregion


        //---------------------------------------------
        #region Example2

        /*
        public override string VisitAssignment([NotNull] ExtendedIniGrammarParser.AssignmentContext context)
        {
            string fieldName = context.fieldName.Text;
            string fieldValue = context.fieldValue.GetText();


            return base.VisitAssignment(context);
        }

        public override string VisitChapter([NotNull] ExtendedIniGrammarParser.ChapterContext context)
        {
            string chapterName = context.GetText();

            return base.VisitChapter(context);
        }
        */

        #endregion

        //---------------------------------------------
        #region Example3
        
        private Dictionary<string, string> _configData = null;
        private string currentChapter = "Common";

        public Dictionary<string, string> configData 
        { 
            get { return _configData; }
        }

        public ExtendedIniParserImpl()
        {
            _configData = new Dictionary<string, string>();
        }

        public override string VisitAssignment([NotNull] ExtendedIniGrammarParser.AssignmentContext context)
        {
            string fieldName = context.fieldName.Text;
            string fieldValue = context.fieldValue.GetText();

            string fullqualifiedFiedlname = string.Format("{0}.{1}", currentChapter, fieldName);
            _configData.Add(fullqualifiedFiedlname, fieldValue);
            return base.VisitAssignment(context);
        }

        public override string VisitChapter([NotNull] ExtendedIniGrammarParser.ChapterContext context)
        {
            string chapterName = context.GetText();
            currentChapter = chapterName.Substring(1,chapterName.Length-2);
            return base.VisitChapter(context);
        }
        

        #endregion

    }
}
