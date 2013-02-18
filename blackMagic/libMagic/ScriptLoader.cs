using System;
using System.Linq;
using System.IO;
using Jurassic;
using Jurassic.Library;
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

            engine.SetGlobalValue("console", new FirebugConsole(engine));

            //for native modules, we use native_require to keep global namespace tidy
            engine.SetGlobalFunction("native_require", new Func<string, ObjectInstance>(identifier =>
                                                                                           {
                                                                                               switch (identifier)
                                                                                               {
                                                                                                   case "nativeModule":
                                                                                                       return new NativeModuleInstance(engine);
                                                                                                   default:
                                                                                                       throw new ArgumentOutOfRangeException(string.Format("Unsupported module name: {0}", identifier));
                                                                                               }
                                                                                           }));
            //Require must be globally available
            engine.ExecuteFile("Builtin\\require.js");
        }

        public void RunScript(string script)
        {
            engine.Execute(script);
        }
    }
}
