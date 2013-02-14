var mailFolder = "Inbox";

print(mailFolder);

var repository = new MailRepository();

print(repository);

var folders = repository.getFolderNames();

for (var i = 0; i < folders.length; i++) {
    print(folders[i]);
}