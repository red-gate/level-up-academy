The real power of UNIX systems comes from combining. The underlying philosophy of UNIX is that power comes from combining commands together.

Let's look at a number of ways we can do that.

## The Pipe operator

The `|` operator feeds the output of the left hand side into the input of the right hand side. For example, the following sorts the output of listing the root directory backwards.

`ls / | sort -r`{{execute}}

You can append any number of pipe operators into to create commands. Let's find out what the most common starting letter of directory names is. This introduces a few new commands. Use `man` to find out what they do!

`ls / | cut -c1 | sort -rn | uniq -c | sort -rn`{{execute}}

`tr` is a textual replacement command. Let's use that to translate the `ls /` command to be more shouty by textually replacing all lowerclass characters with their upper class equivalents (as always, use `man tr` to find out more information).

`ls -al | tr [:lower:] [:upper:]`{{execute}}

The `sed` command is a powerful stream editor for filtering and transforming text. Perhaps the most common use of this is to perform regular expression search and replace.

`ls -al | sed s/root/ROOT/g`{{execute}}

In general, you don't want to use `ls` like this and there's a UNIX command called `find` that allow you to do this in a much more structured way. As an exercise, can you perform the above commands using `find`? (remember `man find` will give you help!).

## Redirecting standard input / output

`>` redirects standard output to a file. A common use of this is to put contents into a file:

`ls -al > output`{{execute}}

If you `cat` the contents of output, you'll see a file.

`>>` appends standard output to a file.

`ls -al >> output`{{execute}}

Now if you `cat` the contents of this file, you'll see that it is doubled up.

`2>` and `2>>` do exactly the same thing, but for standard error.

## Building commands from standard input

The `xargs` command is the swiss-army knife of combining. It allows you to build and execute commands from standard input.

Let's put everything we've learnt together and generate a listing of all users on the system. You can find all users in the `/etc/passwd` file. The name is the part up until the first `:`.

`cut -d: -f1 < /etc/passwd`{{execute}}

This displays a list of user names. Let's sort that.

`cut -d: -f1 < /etc/passwd | sort`{{execute}}

Excellent - that's a list of users in alphabetical order. Now lets send that output to the `echo` command and create a more compact representation.

`cut -d: -f1 < /etc/passwd | sort | xargs echo`{{execute}}

