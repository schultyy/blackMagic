var mailFolder = "Inbox";

var repository = new MailRepository();

var folderNames = repository.getFolderNames("Personal Folders");

print("-------------------------------");

for (var i = 0; i < folderNames.length; i++ ) {
    var folders = repository.getChildrenFor(folderNames[i]);

    for (var x = 0; i < folders.length; x++) {
        print(folders[x].toString());
    }    
}
