using Jurassic;
using Jurassic.Library;

namespace blackMagic.Outlook
{
    public class MailRepositoryConstructor : ClrFunction
    {
        public MailRepositoryConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "MailRepositoryInstance", new MailRepositoryInstance(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public MailRepositoryInstance Construct()
        {
            return new MailRepositoryInstance(this.InstancePrototype);
        }
    }
}