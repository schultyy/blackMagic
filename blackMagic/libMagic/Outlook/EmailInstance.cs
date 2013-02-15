using System;
using Jurassic.Library;

namespace libMagic.Outlook
{
    public class EmailInstance : ObjectInstance
    {
        public string UniqueId
        {
            get { return this.GetPropertyValue(() => UniqueId) as string; }
            set { this.SetPropertyValue(() => UniqueId, value); }
        }

        public string Subject
        {
            get { return this.GetPropertyValue(() => Subject) as string; }
            set { this.SetPropertyValue(() => Subject, value); }
        }

        public string Sender
        {
            get { return this.GetPropertyValue(() => Sender) as string; }
            set { this.SetPropertyValue(() => Sender, value); }
        }

        public string[] Recipients
        {
            get { return this.GetPropertyValue(() => Recipients) as string[]; }
            set { this.SetPropertyValue(() => Recipients, value); }
        }

        public DateTime ReceivedOn
        {
            get { return (DateTime)this.GetPropertyValue(() => ReceivedOn); }
            set { this.SetPropertyValue(() => ReceivedOn, value); }
        }

        public DateTime SendOn
        {
            get { return (DateTime)this.GetPropertyValue(() => SendOn); }
            set { this.SetPropertyValue(() => SendOn, value); }
        }

        public EmailInstance(ObjectInstance prototype)
            : base(prototype)
        {

        }
    }
}