var mailFolder = "Inbox";

var repository = new MailRepository();

var folderNames = repository.getFolderNames(null);

for (var i = 0; i < folderNames.length; i++) {
    print(folderNames[i]);
}

print("-------------------------------");

var folders = repository.getChildrenFor(folderNames[0]);

for (var i = 0; i < folders.length; i++) {
    print(folders[i].toString());
}