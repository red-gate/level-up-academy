The `git add` command can also take directories as arguments, allowing multiple files to be staged in one go.

The setup here is the same; a repository full of changed files, of which we only want to stage a few. In this case we want to stage and commit `oranges.js`, and `strawberries.js`.

This time, instead of passing multiple filenames to the `git add` command, give it a directory name instead.

Use `git status` to verify that you have staged the two files, and then commit them. You can verify the commit with `git log` or `git show`.
