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
    console.log(items[i].Subject);
}