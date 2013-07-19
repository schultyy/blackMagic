using Jurassic;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class AttachmentConstructor : ClrFunction
    {
        public AttachmentConstructor(ScriptEngine engine)
            : base(engine.Function.Prototype,
                   "AttachmentInstance",
                   new AttachmentInstance(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public AttachmentInstance Construct()
        {
            return new AttachmentInstance(InstancePrototype);
        }
    }
}