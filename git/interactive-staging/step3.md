The best way to solve the problem of staging only a subset of changed files is to use `git add --interactive`.

Once again, the setup here is the same; a repository full of changed files, of which we only want to stage a few. This time, we want to stage `bananas.js`, `grapes.js`, and `strawberries.js`.

This time use `git add --interactive` to open an interactive prompt which will allow us to select which files we want to stage.

Once inside the prompt, choose `2`, or `u` to stage individual files. To stage multiple files, separate each file number with a space. Once the files are selected (they will have an asterisk next to them), press enter to return to the main menu, then `q` to exit out of the session.

Verify that you have staged the right files by running `git status` and then commmit them as before with `git commit`. As usual, you can use `git log` and `git show` to check your commit.
