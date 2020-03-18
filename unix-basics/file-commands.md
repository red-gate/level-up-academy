# Linux file system

In Linux, the file system consists of a single file system, all accessible via `/`.

The `ls` command is used to list the contents. Let's list the root directory.

`ls /`{{execute}}

Each of these folders have a meaning. Here's some of the important ones.

* `bin` - short for binaries, this is the directory where many commonly used executable commands reside
* `dev` - contains device specific files, such as the console
* `etc` - contains system configuration files
* `tmp` - storage for temporary files (periodically cleaned)
* `home` - contains user directory information
* `lib` - contains libraries

`ls` has many flags to control the output.

`ls --help`{{execute}}
