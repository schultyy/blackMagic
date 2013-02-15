using Jurassic.Library;

namespace libMagic.Outlook
{
    public class FolderInstance : ObjectInstance
    {
        public string UniqueId
        {
            get { return this.GetPropertyValue(() => UniqueId) as string; }
            set { this.SetPropertyValue(() => UniqueId, value); }
        }

        public string Name
        {
            get { return this.GetPropertyValue(() => Name) as string; }
            set { this.SetPropertyValue(() => Name, value); }
        }

        public string FolderType
        {
            get { return this.GetPropertyValue(() => FolderType) as string; }
            set { this.SetPropertyValue(() => FolderType, value); }
        }

        public FolderInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFields();
            this.PopulateFunctions();
        }
    }
}