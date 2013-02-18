# blackMagic

## What?
Provides basic JavaScript access to the Outlook 2007 Object model. I created this project to have quick access to the Outlook API without writing a full blown C# program. The API may not be that convenient, but it should be usable.

## Remarks

The thing here is, that you can not just expose the .Net interop classes to JS, this results in exceptions (it is even a headache, if you want to share an interop object between two assemblies). Jurassic also requires all exposed classes to inherit from ObjectInstance ([Except supported types](http://jurassic.codeplex.com/wikipage?title=Supported%20types&referringTitle=Documentation)). So every interop object needs to be converted in some sort of an DTO (data transfer object). This is takes some time but gives you the chance to expose only the parts you really need. If you ever designed a web service, the concept should look familiar to you. 
At the moment, the most code is C# because it takes some code to bring Outlook to JS, same for file system access.
The fs-API looks like the node fs-API, but with the constraint, that only blocking methods are available.

## Modules
The module system should look like as it is specified by [Common.js](http://www.commonjs.org/specs/modules/1.0/) , if there is something missing, send a pull request.
Using a module in your code is very simple, just ```require``` are module:
```JavaScript
var fs = require("fs");
```
### So I want to build a new module
Right, at first, write your module (for example math.js):
```JavaScript
exports.add = function(a,b){ return a + b; };
```
It is important, that all objects and functions, that should be publicly visible, are a property of exports.
Copy your file to the folder Builtin. When requiring a module, blackMagic looks in this folder for a file with the matching filename.
# Dependencies
* [Jurassic JavaScript Compiler](http://jurassic.codeplex.com/)
* nunit
* Office Interop Assemblies for Office 2007

# API
## MailRepository
### Instantiation
```JavaScript
var repository = new MailRepository();
```
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
```JavaScript
var fs = require("fs");
```
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
