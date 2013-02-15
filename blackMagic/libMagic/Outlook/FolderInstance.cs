using Jurassic.Library;

namespace libMagic.Outlook
{
    public class FolderInstance : ObjectInstance
    {
        public string UniqueId
        {
            get { return this.GetPropertyValue("UniqueId") as string; }
            set { SetPropertyValue("UniqueId", value, true); }
        }

        public string Name
        {
            get { return this.GetPropertyValue("Name") as string; }
            set { SetPropertyValue("Name", value, true); }
        }

        public string FolderType
        {
            get { return GetPropertyValue("FolderType") as string; }
            set { SetPropertyValue("FolderType", value, true); }
        }

        public FolderInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFields();
            this.PopulateFunctions();
        }
    }
}