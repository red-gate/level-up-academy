In Unix systems, many processes can be running at once (after all, it's a multi-tasking operating system).

You can display the currently running tasks with `ps`.

`ps`{{execute}}

You'll see there are two commands running. `bash` (the command interpreter) and the `ps` command (you just typed that).

Let's create a long running task

`sleep 3 && echo hello`{{execute}}

This command will run for 3 seconds (which _feels_ like a long time). Whilst this command is running, you can't interact with the terminal. You can press `Ctrl + C` to terminate the command, but that's not much fun. What if you want the command to carry on running?

There's at least a couple of options here. The first is to start the command as a background task.

`sleep 3 && echo hello &`{{execute}}

The `&` suffix means that the command is executed in the background. The shell doesn't wait for the command to finish and the treturn status is zero. 3 seconds after this, you should notice the output "hello" appearing in your terminal.

Another way of dealing with it is to use `Ctrl + Z` to suspend a running task. Run the command again, but immediately (or at least within 3 seconds!), press `Ctrl + Z` to suspend the task.

`sleep 3 && echo hello`{{execute}}

At the terminal, you should see the message `[1+] Stopped` which indicates that the task has been stopped. This means it is not running, so even if you wait three seconds you won't see anything happen in the terminal.

We can move the currently stopped task to a background task with `bg` (and correspondingly to a foreground task with `fg`). Let's execute it in the background:

`bg`{{execute}}

What if you've got something running that you wish wasn't?

`sleep 300000 &`{{execute}}

The command above will take a fair while to run. We can find the process id with

`ps`{{execute}}

Once we've got the PID, we can use the `kill` command to terminate the process. The syntax is `kill <PID>`. this sends a signal to the process. By default it sends the SIGTERM signal which corresponds terminate. If an application doesn't handle that signal, you can see the available signals with:

`kill -l`{{execute}}

If you want to terminate an application and nothing else works, `kill -9 <pid>` will do the trick. This should be used as a last resort as it doesn't allow the receiving process any time to do any clean-up work or anything like that.

### Fork bombs

A fork bomb is a program that harms a system by making it run out of memory. It's instructive to look at these, but don't execute it. If you do, do it on your own machine. Or your colleagues if they leave it logged in (not really).

`:(){ :|: & };:`

What does this string do?

`:()` defines a function called `:`. The body of the function runs the function `:` and sends its output to `:` and runs it in the background with `&`. Essentially this is a endlessly recursive function that spawns unlimited processes until your system dies.

To mitigate these risks, an adminsitrator can set limits in the number of processes that a given user can spawn. See `/etc/security/limits.conf`.







