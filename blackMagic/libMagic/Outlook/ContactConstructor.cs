using Jurassic;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class ContactConstructor : ClrFunction
    {
        public ContactConstructor(ScriptEngine engine)
            : base(engine.Function.Prototype,
                   "ContactInstance",
                   new ContactInstance(engine.Object.InstancePrototype))
        {

        }

        [JSConstructorFunction]
        public ContactInstance Construct()
        {
            return new ContactInstance(InstancePrototype);
        }
    }
}