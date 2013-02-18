blackMagic
==========

Dude, this uses the dark side of the force. 
Provides basic JavaScript access to the Outlook 2007 Object model. 
# API

## MailRepository
### Instantiation
var repository = new MailRepository();
### getFolderNames (object root)
If root is null, then returns root level folder names.
### getChildrenFor (string folderName)
Returns child folders for a certain folder. If the certain folder has no children, an empty array will be returned.
### getMailsForFolder (string uniqueId)
Returns a collection of mails for a certain folder.
### saveAttachment (string mailUniqueId, AttachmentInstance attachment, string filename)
Saves an attachment to disk.
### saveEmail(string mailUniqueId, string filename)
Saves an email as msg file to disk
## File system
### Remarks
Provides basic file system access
### Usage
var fs = require("fs");
### readFileSync
### writeFileSync
### exists
Tests, if a certain file or path exists