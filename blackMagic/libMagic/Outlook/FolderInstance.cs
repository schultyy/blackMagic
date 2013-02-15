using Jurassic.Library;

namespace blackMagic.Outlook
{
    public class FolderInstance : ObjectInstance
    {
        [JSField]
        public string UniqueId;

        public string Name { get; set; }

        public FolderInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFields();
            this.PopulateFunctions();
        }

        [JSFunction(Name = "toString")]
        public override string ToString()
        {
            return Name;
        }
    }
}