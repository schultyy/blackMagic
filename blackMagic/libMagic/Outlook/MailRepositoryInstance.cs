using System;
using System.Collections.Generic;
using System.Linq;
using Jurassic;
using Jurassic.Library;
using Microsoft.Office.Interop.Outlook;

namespace libMagic.Outlook
{
    public class MailRepositoryInstance : ObjectInstance
    {
        private Application application;

        public MailRepositoryInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFunctions();
            this.application = new Application();
        }

        [JSFunction(Name = "getFolderNames")]
        public ArrayInstance GetFolderNames(object root)
        {
            string[] folders = null;
            if (root == Null.Value)
                folders = application.Session.Folders.Cast<Folder>().Select(c => c.Name).ToArray();
            else
            {
                folders = application.Session.Folders
                                .Cast<Folder>()
                                .Single(c => c.Name == (string)root)
                                .Folders.Cast<Folder>()
                                .Select(c => c.Name)
                                .ToArray();
            }
            return this.Engine.Array.New(folders);
        }

        [JSFunction(Name = "getChildrenFor")]
        public ArrayInstance GetChildrenFor(string foldername)
        {
            if (string.IsNullOrEmpty(foldername))
                throw new ArgumentNullException("foldername");

            var folderConstructor = new FolderConstructor(Engine);

            var rootFolder = application.Session.Folders.Cast<Folder>()
                .SingleOrDefault(c => c.Name == foldername);
            if (rootFolder == null)
                return Engine.Array.New();
            var folders = rootFolder
                .Folders
                .Cast<Folder>()
                .ToArray();
            if (!folders.Any())
                return Engine.Array.New();
            var dtos = folders
                .Select(c => folderConstructor.Construct(c.Name, c.EntryID, c.DefaultItemType.ToString()))
                .ToArray();
            return Engine.Array.New(dtos);
        }

        [JSFunction(Name = "getMailsForFolder")]
        public ArrayInstance GetMailsForFolder(string uniqueId)
        {
            return GetMailsForFolderInternal(uniqueId, null);
        }

        private ArrayInstance GetMailsForFolderInternal(string uniqueId, Folder parent)
        {
            if (string.IsNullOrEmpty(uniqueId))
                throw new ArgumentNullException("uniqueId");

            Folder thatFolder = null;

            IEnumerable<Folder> collection;

            if (parent == null)
                collection = application.Session.Folders.Cast<Folder>().ToArray();
            else
                collection = parent.Folders.Cast<Folder>().ToArray();

            thatFolder = collection.SingleOrDefault(c => c.EntryID == uniqueId);

            if (thatFolder == null)
            {
                foreach (var f in collection)
                    return GetMailsForFolderInternal(uniqueId, f);
            }
            else
            {
                var mails = thatFolder.Items.Cast<MailItem>()
                                            .Select(c => new EmailInstance(Engine.Object)
                                                             {
                                                                 UniqueId = c.EntryID,
                                                                 Subject = c.Subject,
                                                                 Sender = c.SenderEmailAddress,
                                                                 Recipients = Engine.Array.New(c.Recipients.Cast<Recipient>().Select(r => r.Address).ToArray()),
                                                                 SendOn = Engine.Date.Construct(c.SentOn.ToUniversalTime().Subtract(
                                                                                                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                                                                                    .TotalMilliseconds),
                                                                 ReceivedOn = Engine.Date.Construct(c.ReceivedTime.ToUniversalTime().Subtract(
                                                                                                        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                                                                                        .TotalMilliseconds)
                                                             }).ToArray();
                return Engine.Array.New(mails);
            }

            return Engine.Array.New();
        }
    }
}
