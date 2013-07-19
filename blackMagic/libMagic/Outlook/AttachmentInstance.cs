using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class AttachmentInstance : ObjectInstance
    {
        public string DisplayName
        {
            get { return this.GetPropertyValue(() => DisplayName) as string; }
            set { this.SetPropertyValue(() => DisplayName, value); }
        }

        public string Filename
        {
            get { return this.GetPropertyValue(() => Filename) as string; }
            set { this.SetPropertyValue(() => Filename, value); }
        }

        public int Size
        {
            get { return (int)this.GetPropertyValue(() => Size); }
            set { this.SetPropertyValue(() => Size, value); }
        }

        public int Index
        {
            get { return (int)this.GetPropertyValue(() => Index); }
            set { this.SetPropertyValue(() => Index, value); }
        }

        public AttachmentInstance(ScriptEngine engine)
            : base(engine)
        {
        }

        public AttachmentInstance(ObjectInstance prototype)
            : base(prototype)
        {
        }
    }
}
