# A quick tour of file system commands

You've already met `ls` which lists the contents of a directory.

`cd` changes directory. Let's change our directory to the home directory, indicated by `~`.

`cd ~`{{execute}}

`touch`, errrr, touches a file to modify the last access time. We can also use it to create an empty file.

`touch BANANA`{{execute}}

You can confirm the file has been created by looking at it.

`ls`{{execute}}

The `mv` command moves a file from source to target. For example:

`mv BANANA APPLE`{{execute}}

Does what you might expect (confirm with `ls`). The `rm` command can be used to remove a file

`rm APPLE`{{execute}}

Similar, `mkdir` and `rmdir` create and remove directories.