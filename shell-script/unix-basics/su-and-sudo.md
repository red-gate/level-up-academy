To perform some operations in Unix, you'll need to be an administrator (or root).

`su` (switch user) allows you to run commands with a substitute user and group id. If you don't use any argument, then it starts a new shell with "root" as the user. _root_ is the user name or account that (by default) has access to all commands on a Unix-like operating system.

A typical use would be to switch to the _root_ user and run some commands, such as installing some new packages.

The `sudo` command is built for running a command as root. Let's use it to install a package.

`sudo apt-get install sl`{{execute}}

`sl` is a *vitally* important command that demonstrates when you've mistyped `ls`. Try it and see.

`/usr/games/sl`{{execute}}

Check the man page for more fun options.