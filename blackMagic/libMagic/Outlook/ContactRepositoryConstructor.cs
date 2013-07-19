using System.Collections.Generic;
using System.Text;
using Jurassic;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class ContactRepositoryConstructor : ClrFunction
    {
        public ContactRepositoryConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "ContactRepositoryInstance",
            new ContactRepositoryInstance(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public ContactRepositoryInstance Construct()
        {
            return new ContactRepositoryInstance(this.InstancePrototype);
        }
    }
}
