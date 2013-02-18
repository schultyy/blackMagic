blackMagic
==========

Dude, this uses the dark side of the force. 
Provides basic JavaScript access to the Outlook 2007 Object model. I created this project to have quick access to the Outlook API without writing a full blown .Net program. 

# Dependencies
## Jurassic
JavaScript compiler
## nunit
## Office Interop Assemblies for Office 2007

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
### readFileSync (string filename)
### writeFileSync (string filename, string content)
### exists (string path)
Tests, if a certain file or path exists
### createDirectory (string directoryName)
Creates a new directory
### combine (string[] paths)
Combines an array of paths to a valid directory or file identifier.
## Folder
### Name (string)
### UniqueId (string)
### FolderType (string)
## Email
### UniqueId (string)
### Subject (string)
### Sender (string)
Sender's email address
### SenderName (string)
Sender's display name
### Recipients (string[])
Collection of recipients (email addresses only)
### ReceivedOn (date)
### SendOn (date)
### Attachments (Attachment[])
Collection of attachments that belong to a certain email
## Attachment
### Displayname (string)
### Filename (string)
### Size (int)
### Index (int)