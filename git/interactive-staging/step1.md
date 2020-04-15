Here we have a git repository with changes to multiple different files, but we only want to stage and commit a few of those files.

Do a `git status` to see all the modifications you have on disk.

Of those files, we only want to stage and commit the changes to `apples.js`, `grapes.js` and `oranges.js`.

How do we solve this problem?

One way to do it is to simply stage the files one by one using `git add`, for example: `git add dir1/apples.js`

Stage the three files, run `git status` to verify that the correct files have been staged, then commit the files. Once the commit is made, verify it with `git log`.
