using System;
using Jurassic;

namespace libMagic
{
    public class ScriptLoader
    {
        private ScriptEngine engine;

        public ScriptLoader()
        {
            this.engine = new Jurassic.ScriptEngine();
            this.engine.EnableDebugging = true;
            this.PopulateNamespace();
        }

        private void PopulateNamespace()
        {
            engine.SetGlobalValue("MailRepository", new Outlook.MailRepositoryConstructor(engine));

            engine.SetGlobalFunction("read", new Func<string>(Console.ReadLine));

            //engine.SetGlobalFunction("print", new Action<string>(Console.WriteLine));
            engine.SetGlobalValue("console", new Jurassic.Library.FirebugConsole(engine));
        }

        public void RunScript(string script)
        {
            engine.Execute(script);
        }
    }
}
