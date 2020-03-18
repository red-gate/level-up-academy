So far, we've just typed a single command and looked at the results. What if you want to do something a bit more complicated? The answer is shell scripting. A shell script is just a list of commands that's executed by the shell (in our case `bash`).

Each shell script begins with a shebang (#!) which provides the path to the shell executing the ship.

Let's create our first script.

`echo '#!/bin/bash' > helloworld.sh`{{execute}}
`echo 'echo Hello world' >> helloworld.sh`{{execute}}

This'll create a file with a couple of lines in it. If you try and execute the command at the moment, you'll get an error message ("Permission Denied").

`./helloworld.sh`{{execute}}

If you look at the permissions (`ls -l helloworld.sh`) then you'll see that the execute bit isn't set. Let's set the execute flag for our user.

`chmod 755 helloworld.sh`{{execute}}

You can verify it's now set with `ls -l helloworld.sh`. After all this work, you can now execute the script.

`./helloworld.sh`{{execute}}

Bash's scripting language is a complete programming language. Let's write a simple program. For the examples below, use the text editor of your choice to create the files.

```
#!/bin/bash
# Comments begin with a single # and continue to the end of the line

FOO="bar" # set the value of a local variable to bar
echo "Value of variable is $FOO"

# Loops. Wildcards expand out to be files in the directory
for i in 1 2 a "banana" * 
do
  echo "Looping $i"
done

# if statement
if [ "$FOO" != "bar" ] 
then
  echo "Success"
else
  echo "Test failed"
fi

# Command line arguments
echo "My first argument is $1"
echo "All my arguments are $*"
```

Create this file, set the executable bit and give it a run to see if it does what you expect.

For more information, read this [Shell Scripting Tutorial](https://www.shellscript.sh/).