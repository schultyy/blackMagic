var mailFolder = "Inbox";

var repository = new MailRepository();

var children = repository.getChildrenFor("Personal Folders");

var inbox = null;

for (var i = 0; i < children.length; i++) {
    console.log(children[i].Name);
    if (children[i].Name == mailFolder) {
        inbox = children[i];
        break;
    }
}

if (inbox == null)
    throw new Error("No inbox folder found");

var items = repository.getMailsForFolder(inbox.UniqueId);

for (var i = 0; i < items.length; i++) {

    if (items[i].Attachments.length == 0)
        continue;

    console.log("Subject: " + items[i].Subject);
    console.log("Send from: " + items[i].Sender);
    console.log("Recipients: " + items[i].Recipients.join());
    console.log("SendOn: " + items[i].SentOn);
    console.log("Received on: " + items[i].ReceivedOn);
    console.log("-------------------------------------");

    for (var x = 0; x < items[i].Attachments.length; x++) {
        console.log("Filename: " + items[i].Attachments[x].Filename);
        //public void SaveAttachment(string mailUniqueId, AttachmentInstance attachment, string filename)
        repository.saveAttachment(items[i].UniqueId, items[i].Attachments[x], "C:\\temp\\hello.md");
    }
    repository.saveEmail(items[i].UniqueId, "C:\\temp\\" + items[i].Subject + ".msg");
}