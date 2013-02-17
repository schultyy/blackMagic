using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace libMagic.IO
{
    public class NativeModuleInstance : ObjectInstance
    {
        public NativeModuleInstance(ScriptEngine engine)
            : base(engine)
        {
            this.PopulateFunctions();
        }

        [JSFunction(Name = "readFileSync")]
        public string ReadFileSync(string filename)
        {
            return System.IO.File.ReadAllText(filename);
        }

        [JSFunction(Name = "writeFileSync")]
        public void WriteFileSync(string filename, string content)
        {
            System.IO.File.WriteAllText(filename, content);
        }
    }
}
