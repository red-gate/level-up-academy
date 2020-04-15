Unix has plenty of options for editing text. Pick an editor and invest a few minutes in learning it. There's great web resources available for all of these options.

Regardless of what you choose, always understand enough `vi` to be able to edit text, as it's available on almost all systems. `emacs` is a good choice too, and is especially awesome when customized.

The simplest option is `nano` - it's almost friendly! However, it's worth being aware of both `ed` (so you can be familiar with its breathren `sed`), and `vi` simpler because of its ubiquity.

# nano / pico

`nano` is probably the simplest editor to get started with. It's basic and it works. The keyboard shortcuts are well documented.

`nano`{{execute}}

# ed

The original editor was known as `ed`. `ed` is a line oriented editor and it's available everywhere. It's also been described as _the most user-hostile editor ever created_ ([Peter Salus](https://en.wikipedia.org/wiki/Peter_H._Salus)).

Let's create a hello world in ed. 

`ed`{{execute}}

At startup, `ed` is in command mode. Let's type `a` (and press return) to get it into append-mode. Now type the text you want on a single line (e.g. `Hello world!`) and press return. Now this is a single line in the buffer. Type `.` and press return to exit append mode. Now you are at a command prompt again, type `w helloworld.txt`, press return and press `q` and return to exit.

Now take a deep breath and hope that

`cat helloworld.txt`{{execute}}

Displays the text you entered.

Why is this important? Well, just wait till you see `sed` :)

# vi

`vi` builds upon `ed` and is considered _slightly_ friendlier.

![Exiting Vim](https://github.com/fffej/katacoda-scenarios/raw/master/learn-bash/images/exiting-vim.jpg)

The main thing to realize is that `vi` has both a command mode and an insert mode. 

`vi hello.txt`{{execute}}

Switch to command mode by pressing `Esc`.

To exit `vi`, then `Esc` to get into command mode and type `q!` to exit without changes.

# emacs

Emacs is an extensible ~operating system~ text editor written in Lisp. It can be heavily customized to do more or less anything. It's not installed by default on most systems.

It's not installed by default on most systems (but you can install it with `sudo apt-get update && sudo apt-get install -y emacs` in the terminal if you want to experiment).






