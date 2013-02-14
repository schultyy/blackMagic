using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Outlook;
using blackMagic.Repositories;

namespace blackMagic.Outlook
{
    public class MailRepository
    {
        private readonly Application application;

        public MailRepository(Application application)
        {
            this.application = application;
        }

        public IEnumerable<IOutlookMail> GetMails(string addressbook)
        {
            if (string.IsNullOrEmpty(addressbook))
                throw new ArgumentNullException("addressbook");

            var folder = application.Session.Folders.Cast<Folder>().Single(c => c.Name == addressbook);
            return folder.Items.Cast<MailItem>()
                .Select(c => new OutlookMail
                                 {
                                     UniqueId = c.EntryID,
                                     Subject = c.Subject,
                                     From = c.SenderEmailAddress,
                                     Recipients = c.Recipients.Cast<Recipient>().Select(r => r.Address).ToArray()
                                 })
                .ToArray();
        }
    }
}
