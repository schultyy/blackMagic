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
            this.PopulateNamespace();
        }

        private void PopulateNamespace()
        {
            engine.SetGlobalValue("MailRepository", new Outlook.MailRepositoryConstructor(engine));

            engine.SetGlobalFunction("read", new Func<string>(Console.ReadLine));

            engine.SetGlobalFunction("print", new Action<string>(Console.WriteLine));
        }

        public void RunScript(string script)
        {
            engine.Execute(script);
        }
    }
}
