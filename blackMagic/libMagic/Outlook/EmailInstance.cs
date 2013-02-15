using Jurassic.Library;

namespace libMagic.Outlook
{
    public class EmailInstance : ObjectInstance
    {
        public string UniqueId
        {
            get { return GetPropertyValue("UniqueId") as string; }
            set { SetPropertyValue("UniqueId", value, true); }
        }

        public string Subject
        {
            get { return GetPropertyValue("Subject") as string; }
            set { SetPropertyValue("Subject", value, true); }
        }

        public EmailInstance(ObjectInstance prototype)
            : base(prototype)
        {

        }
    }
}