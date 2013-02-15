using Jurassic;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class FolderConstructor : ClrFunction
    {
        public FolderConstructor(ScriptEngine engine)
            : base(engine.Function.Prototype, "FolderInstance",
                    new FolderInstance(engine.Object.InstancePrototype))
        {

        }

        [JSConstructorFunction]
        public FolderInstance Construct(string name, string uniqueId, string folderType)
        {
            return new FolderInstance(InstancePrototype)
                       {
                           UniqueId = uniqueId,
                           Name = name,
                           FolderType = folderType
                       };
        }
    }
}