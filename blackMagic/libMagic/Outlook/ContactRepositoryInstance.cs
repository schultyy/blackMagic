using System;
using System.Collections.Generic;
using System.Linq;
using Jurassic.Library;
using Microsoft.Office.Interop.Outlook;

namespace libMagic.Outlook
{
    public class ContactRepositoryInstance : ObjectInstance
    {
        private readonly Application application;

        public ContactRepositoryInstance(ObjectInstance instancePrototype)
            : base(instancePrototype)
        {
            this.PopulateFunctions();
            this.application = new Application();
        }

        [JSFunction(Name = "getAddressbooks")]
        public ArrayInstance GetAddressbooks()
        {
            var rootFolders = application.Session.Folders;
            var addressbooks = new List<object>();
            foreach (Folder rootFolder in rootFolders)
            {
                addressbooks.AddRange(rootFolder.Folders.Cast<Folder>()
                                                 .Where(c => c.DefaultItemType == OlItemType.olContactItem)
                                                 .Select(c => string.Format("{0}/{1}", rootFolder.Name, c.Name))
                                                 .ToArray());
            }

            return Engine.Array.New(addressbooks.ToArray());
        }

        [JSFunction(Name = "getContacts")]
        public ArrayInstance GetContacts(string addressbook)
        {
            if (string.IsNullOrEmpty(addressbook))
                throw new ArgumentNullException("addressbook");

            if (!addressbook.Contains("/"))
                throw new ArgumentException("Addressbook does not contain a path like a/b/c");


            var components = addressbook.Split(new[] { '/' });
            var foldername = components.Last();
            var rootFolder = application.Session.Folders.Cast<Folder>().SingleOrDefault(c => c.Name == components.First());

            if(rootFolder == null)
                throw new ArgumentException(string.Format("No root folder {0} found", components.First()));

            var folder = rootFolder.Folders.Cast<Folder>()
                                .Where(c => c.DefaultItemType == OlItemType.olContactItem)
                                .SingleOrDefault(c => c.Name == foldername);

            if (folder == null)
                throw new ArgumentException(string.Format("No addressbook with name {0} found", addressbook));

            var contacts = folder.Items.Cast<ContactItem>()
                  .Select(c => new ContactInstance(Engine.Object)
                      {
                          FirstName = c.FirstName,
                          LastName = c.LastName,
                          EntryID = c.EntryID,
                          WebPage = c.WebPage,
                          User1 = c.User1,
                          User2 = c.User2,
                          User3 = c.User3,
                          User4 = c.User4,
                          FTPSite = c.FTPSite,
                          Address = c.BusinessAddress,
                          Contact = c
                      })
                  .ToArray();
            return Engine.Array.New(contacts);
        }
    }
}