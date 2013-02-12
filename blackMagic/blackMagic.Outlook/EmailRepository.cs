using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Outlook;
using blackMagic.Repositories;

namespace blackMagic.Outlook
{
    public class EmailRepository : RepositoryBase<MailItem>
    {
        public string InboxName { get; set; }

        public EmailRepository(Application application)
            : base(application)
        {
            InboxName = "Inbox";
        }

        public override void Insert(MailItem item)
        {
            throw new NotImplementedException();
        }

        public override void Update(MailItem item)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<MailItem> GetAll()
        {
            var inbox = application.Session.Folders.Cast<MAPIFolder>().Single(c => c.Name == InboxName);
            return inbox.Items.Cast<MailItem>();
        }

        public override MailItem GetSpecific(Predicate<MailItem> predicate)
        {
            throw new NotImplementedException();
        }

        public override void Remove(MailItem item)
        {
            throw new NotImplementedException();
        }
    }
}
