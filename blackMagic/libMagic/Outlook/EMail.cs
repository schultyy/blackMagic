using Jurassic.Library;

namespace blackMagic.Outlook
{
    public class EMail : ObjectInstance
    {
        public string UniqueId { get; set; }

        public string Subject { get; set; }

        public EMail(ObjectInstance prototype)
            : base(prototype)
        {

        }
    }
}