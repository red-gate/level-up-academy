Bash is the name for the terminal environment you are using. 

For inputting to the shell, Bash uses [readline](https://en.wikipedia.org/wiki/GNU_Readline) which has a whole bunch of really useful keyboard shortcuts.

By default, it uses Emacs style shortcuts. Here's some of the most important shortcuts:

Note that sadly many of these don't work in this browser based version, but they are worth remembering for the Terminal proper.

* `Tab` - Autocompletes from the current cursor position
* `Ctrl + r` - Reverse search (use this to find older commands you've executed)
* `Ctrl + t` - Transpose - use this when you type `sl` and mean `ls`
* `Ctrl + k` - Clears content after the cursor and copies it to the clipboard
* `Ctrl + y` - Pastes the contents of the clipboard after the cursor

If you remember nothing else from this, learn `Ctrl + r` and press Tab lots!

You can find the complete list of commands by doing:

`man readline`{{execute}}