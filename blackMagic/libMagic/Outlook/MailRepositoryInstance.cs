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
        private readonly Application application;

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
        public ArrayInstance GetMailsForFolder(ObjectInstance parameters)
        {
            if (parameters.HasProperty("UniqueId"))
                return GetMailsForFolderInternal(parameters.GetPropertyValue("UniqueId").ToString(), null);

            if (parameters.HasProperty("FolderName"))
            {
                var folderPath = parameters.GetPropertyValue("FolderName").ToString().Split('/');

                if (folderPath.Length != 2)
                    throw new NotSupportedException("At the moment only paths like 'Personal Folders/Foo' are supported");

                var root = folderPath.First();
                var folderName = folderPath.Last();

                var children = GetChildrenFor(root);
                foreach (FolderInstance child in children.ElementValues.Cast<FolderInstance>()
                                                            .Where(child => child.GetPropertyValue("Name").ToString() == folderName))
                    return GetMailsForFolderInternal(child.GetPropertyValue("UniqueId").ToString(), null);
                return Engine.Array.New();
            }

            throw new NotSupportedException(string.Format("Requires an object with either 'UniqueId' or 'FolderName' as parameter"));
        }

        [JSFunction(Name = "saveAttachment")]
        public void SaveAttachment(string mailUniqueId, AttachmentInstance attachment, string filename)
        {
            if (string.IsNullOrEmpty(mailUniqueId))
                throw new ArgumentNullException("mailUniqueId");

            if (attachment == null)
                throw new ArgumentNullException("attachment");


            foreach (var rootFolder in application.Session.Folders.Cast<Folder>())
            {
                if (EnumerateMailFolders(rootFolder, mailUniqueId, (mail =>
                                                                   {
                                                                       var attachmentYouSearchedFor = mail.Attachments.
                                                                           Cast<Attachment>()
                                                                           .SingleOrDefault(
                                                                               c => c.Index == attachment.Index);
                                                                       if (attachmentYouSearchedFor == null)
                                                                           throw new ArgumentException(
                                                                               string.Format("No attachment {0} found",
                                                                                             attachment.Filename));
                                                                       attachmentYouSearchedFor.SaveAsFile(filename);
                                                                   })))
                    break;
            }
        }

        [JSFunction(Name = "deleteAttachment")]
        public void DeleteAttachment(string mailUniqueId, AttachmentInstance attachment)
        {
            if (string.IsNullOrEmpty(mailUniqueId))
                throw new ArgumentNullException("mailUniqueId");

            if (attachment == null)
                throw new ArgumentNullException("attachment");

            foreach (var rootFolder in application.Session.Folders.Cast<Folder>())
            {
                if (EnumerateMailFolders(rootFolder, mailUniqueId, mail =>
                                                                      {
                                                                          var attachmentYouSearchedFor = mail.Attachments
                                                                                                .Cast<Attachment>()
                                                                                                .SingleOrDefault(c => c.Index == attachment.Index);
                                                                          if (attachmentYouSearchedFor == null)
                                                                              throw new ArgumentException(
                                                                                  string.Format("No attachment {0} found",
                                                                                                attachment.Filename));
                                                                          attachmentYouSearchedFor.Delete();
                                                                      }))
                    break;
            }
        }

        [JSFunction(Name = "saveEmail")]
        public void SaveEmail(string mailUniqueId, string filename)
        {
            foreach (var rootFolder in application.Session.Folders.Cast<Folder>())
                if (EnumerateMailFolders(rootFolder, mailUniqueId, mail => mail.SaveAs(filename)))
                    break;
        }

        [JSFunction(Name = "updateEmail")]
        public void UpdateEmail(EmailInstance emailInstance)
        {
            foreach (var rootFolder in application.Session.Folders.Cast<Folder>())
            {
                if (EnumerateMailFolders(rootFolder, emailInstance.UniqueId, mail =>
                                                                                {
                                                                                    mail.Subject = emailInstance.Subject;
                                                                                    mail.Body = emailInstance.BodyText;

                                                                                    mail.Save();
                                                                                }))
                    break;
            }
        }

        private bool EnumerateMailFolders(Folder parent,
                                            string mailUniqueId,
                                            Action<MailItem> mailAction)
        {
            MailItem mail = null;

            if (parent.Items.GetFirst() is MailItem)
                mail = parent.Items.Cast<MailItem>().SingleOrDefault(c => c.EntryID == mailUniqueId);

            if (mail == null)
            {
                var childFolders = parent.Folders.Cast<Folder>();
                return childFolders.Any(childFolder => EnumerateMailFolders(childFolder, mailUniqueId, mailAction));
            }
            mailAction(mail);
            return true;
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
                                                                 SenderName = c.SenderName,
                                                                 Sender = c.SenderEmailAddress,
                                                                 Recipients = Engine.Array.New(c.Recipients.Cast<Recipient>().Select(r => r.Address).ToArray()),
                                                                 SendOn = Engine.Date.Construct(c.SentOn.ToUniversalTime().Subtract(
                                                                                                    new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                                                                                    .TotalMilliseconds),
                                                                 ReceivedOn = Engine.Date.Construct(c.ReceivedTime.ToUniversalTime().Subtract(
                                                                                                        new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc))
                                                                                                        .TotalMilliseconds),
                                                                 Attachments = Engine.Array.New(c.Attachments.Cast<Attachment>()
                                                                 .Select(a => new AttachmentInstance(Engine.Object)
                                                                                  {
                                                                                      DisplayName = a.DisplayName,
                                                                                      Filename = a.FileName,
                                                                                      Index = a.Index,
                                                                                      Size = a.Size
                                                                                  }).ToArray())
                                                             }).ToArray();
                return Engine.Array.New(mails);
            }

            return Engine.Array.New();
        }
    }
}
