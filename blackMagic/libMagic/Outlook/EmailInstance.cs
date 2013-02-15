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

        public ArrayInstance Recipients
        {
            get { return this.GetPropertyValue(() => Recipients) as ArrayInstance; }
            set { this.SetPropertyValue(() => Recipients, value); }
        }

        public DateInstance ReceivedOn
        {
            get { return this.GetPropertyValue(() => ReceivedOn) as DateInstance; }
            set { this.SetPropertyValue(() => ReceivedOn, value); }
        }

        public DateInstance SendOn
        {
            get { return this.GetPropertyValue(() => SendOn) as DateInstance; }
            set { this.SetPropertyValue(() => SendOn, value); }
        }

        public EmailInstance(ObjectInstance prototype)
            : base(prototype)
        {

        }
    }
}