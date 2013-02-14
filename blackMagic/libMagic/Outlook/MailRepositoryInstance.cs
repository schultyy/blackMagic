using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;
using Microsoft.Office.Interop.Outlook;

namespace blackMagic.Outlook
{
    public class MailRepositoryConstructor : ClrFunction
    {
        public MailRepositoryConstructor(ScriptEngine engine)
            : base(engine.Function.InstancePrototype, "MailRepositoryInstance", new MailRepositoryInstance(engine.Object.InstancePrototype))
        {
        }

        [JSConstructorFunction]
        public MailRepositoryInstance Construct()
        {
            return new MailRepositoryInstance(this.InstancePrototype);
        }
    }

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
                                .Single(c => c.Name == root)
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

            var folders = application.Session.Folders.Cast<Folder>()
                .Single(c => c.Name == foldername)
                .Folders
                .Cast<Folder>()
                .Select(c => folderConstructor.Construct(c.Name, c.EntryID))
                .ToArray();
            return Engine.Array.New(folders);
        }

        [JSFunction(Name = "getMails")]
        public ArrayInstance GetMails(string addressbook)
        {
            var folder = application.Session.Folders.Cast<Folder>().Single(c => c.Name == addressbook);
            var mails = folder.Items.Cast<MailItem>()
                .Select(c => new EMail(null)
                                 {
                                     UniqueId = c.EntryID,
                                     Subject = c.Subject
                                 }).ToArray();
            return Engine.Array.New(mails);
        }
    }

    public class FolderConstructor : ClrFunction
    {
        public FolderConstructor(ScriptEngine engine)
            : base(engine.Function.Prototype, "FolderInstance", new FolderInstance(engine.Object.InstancePrototype))
        {

        }

        [JSConstructorFunction]
        public FolderInstance Construct(string name, string uniqueId)
        {
            return new FolderInstance(InstancePrototype)
                       {
                           UniqueId = uniqueId,
                           Name = name
                       };
        }
    }

    public class FolderInstance : ObjectInstance
    {
        [JSField]
        public string UniqueId;

        public string Name { get; set; }

        public FolderInstance(ObjectInstance prototype)
            : base(prototype)
        {
            this.PopulateFields();
            this.PopulateFunctions();
        }

        [JSFunction(Name = "toString")]
        public override string ToString()
        {
            return Name;
        }
    }

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
