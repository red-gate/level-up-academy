# Linux File Permissions

Each file has some properties associated with it.

Let's explore those by listing all files in `/usr/bin/` that begin with an `x`.

`ls -al /usr/bin/x*`{{execute}}

You'll notice a bunch of files are listed. Let's examine what each column means:

The first column looks somewhat like `-rwxr-xr-x`. Let's break this down. 

![Example set of permissions](https://github.com/fffej/katacoda-scenarios/raw/master/learn-bash/images/file_permissions.png)

The first character is either `-`, `d` or `l` which mean file, directory or link. The remaining 9 characters correspond to permissions for owner, group and other users. 

Each group of three characters corresponds to permissions to read/write/execute. In our example, `-rwxr-xr-x` is a file. The owner can (r)ead (w)rite and e(x)ecute the file. The group owning the file is similar but cannot (w)rite. Similarly, other uses can only (r)ead and e(x)ecute.

After the permissions, comes a number. This represents the number of files inside the directory (or 1 if it's just a file). Next up are the owner(e.g. `root`) and group (e.g. `root`). Next up is the size of the file in bytes, the date of last modification and finally, the name of the file.

## Changing file permissions

The `chmod` command allows you to change file permissions. The syntax is `chmod <option> file`. Options are specified using three numbers corresponding to the permissions above (user, group, others).

* 7 : rwx
* 6 : rw-
* 5 : r-x
* 4 : r--
* 3 : -wx
* 2 : -w-
* 1 : --x
* 0 : ---

For exampel,` chmod 345 foo.txt` would set the permissions to write/execute for the user, read only for the group and read and execute for others.

The `chown` command allows you to change the owner of the command. For example `chown <new owner> filename`. Obviously you'll need the appropriate permission to do this!

