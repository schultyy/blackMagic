using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Jurassic;
using Jurassic.Library;
using Microsoft.Office.Interop.Outlook;

namespace libMagic.Outlook
{
    public class ContactInstance : ObjectInstance
    {
        public ContactInstance(ObjectInstance instancePrototype)
            : base(instancePrototype)
        {
            this.PopulateFunctions();
        }

        public ContactInstance(ScriptEngine engine)
            : base(engine)
        {
            this.PopulateFunctions();
        }


        public string Address
        {
            get { return this.GetPropertyValue(() => Address) as string; }
            set { this.SetPropertyValue(() => Address, value); }
        }

        public string FTPSite
        {
            get { return this.GetPropertyValue(() => FTPSite) as string; }
            set { this.SetPropertyValue(() => FTPSite, value); }
        }

        public string User4
        {
            get { return this.GetPropertyValue(() => User4) as string; }
            set { this.SetPropertyValue(() => User4, value); }
        }

        public string User3
        {
            get { return this.GetPropertyValue(() => User3) as string; }
            set { this.SetPropertyValue(() => User3, value); }
        }

        public string User2
        {
            get { return this.GetPropertyValue(() => User2) as string; }
            set { this.SetPropertyValue(() => User2, value); }
        }

        public string User1
        {
            get { return this.GetPropertyValue(() => User1) as string; }
            set { this.SetPropertyValue(() => User1, value); }
        }

        public string WebPage
        {
            get { return this.GetPropertyValue(() => WebPage) as string; }
            set { this.SetPropertyValue(() => WebPage, value); }
        }

        public string EntryID
        {
            get { return this.GetPropertyValue(() => EntryID) as string; }
            set { this.SetPropertyValue(() => EntryID, value); }
        }

        public string FirstName
        {
            get { return this.GetPropertyValue(() => FirstName) as string; }
            set { this.SetPropertyValue(() => FirstName, value); }
        }

        public string LastName
        {
            get { return this.GetPropertyValue(() => LastName) as string; }
            set { this.SetPropertyValue(() => LastName, value); }
        }

        public ContactItem Contact { get; set; }

        [JSFunction(Name = "save")]
        public void Save()
        {
            Contact.BusinessAddress = this.Address;
            Contact.FTPSite = this.FTPSite;
            Contact.User4 = this.User4;
            Contact.User3 = this.User3;
            Contact.User2 = this.User2;
            Contact.User1 = this.User1;
            Contact.WebPage = this.WebPage;
            Contact.FirstName = this.FirstName;
            Contact.LastName = this.LastName;

            Contact.Save();
        }
    }
}
