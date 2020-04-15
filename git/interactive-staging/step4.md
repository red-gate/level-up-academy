The `git add --interactive` command can also be used to stage untracked files; ie files that are not yet part of the repository.

Once again, the setup here is the same; a repository full of changed files. This time, however, there are also a couple of untracked files that we want to commit first.

Use `git add --interactive` as before to open the interactive prompt, then choose option `4` or `a` and select both untracked files by their number (remember to separate them with a space). Hit enter then `q` to exit the editor.

Now check that you have staged the two previously untracked files and then commit them using `git commit`, as before.
