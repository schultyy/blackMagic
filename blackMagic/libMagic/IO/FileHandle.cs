using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace libMagic.IO
{
    public class FileHandleInstance : ObjectInstance
    {
        private FileStream stream;

        public string Filename { get; set; }

        public FileHandleInstance(ScriptEngine engine)
            : base(engine)
        {
        }

        public FileHandleInstance(ObjectInstance prototype)
            : base(prototype)
        {
        }

        public FileHandleInstance(ObjectInstance prototype, string filename)
            : this(prototype)
        {
            this.Filename = filename;
        }

        public void Write(byte[] content)
        {
            this.stream.Write(content, 0, content.Length);
        }

        public void Close()
        {
            stream.Close();
        }
    }

    public class FileHandleConstructor : ClrFunction
    {
        public FileHandleConstructor(ScriptEngine engine)
            : base(engine.Function.Prototype, "FileHandleInstance",
                        new FileHandleInstance(engine.Object.InstancePrototype))
        {
        }

        public FileHandleInstance Construct(string filename, string accessFlags)
        {
            return new FileHandleInstance(InstancePrototype)
                       {
                           Filename = filename
                       };
        }
    }
}
