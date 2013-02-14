using blackMagic.Repositories;

namespace blackMagic.Outlook
{
    public class OutlookMail : IOutlookMail
    {
        public string UniqueId { get; set; }

        public string Subject { get; set; }

        public string[] Recipients { get; set; }

        public string From { get; set; }
    }
}