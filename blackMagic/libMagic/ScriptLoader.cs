using System;
using System.Linq;
using System.IO;
using Jurassic;
using libMagic.IO;

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

            engine.SetGlobalValue("console", new Jurassic.Library.FirebugConsole(engine));

            engine.SetGlobalValue("nativeModule", new NativeModuleInstance(engine));

            engine.ExecuteFile("Builtin\\require.js");
        }

        public void RunScript(string script)
        {
            engine.Execute(script);
        }
    }
}
