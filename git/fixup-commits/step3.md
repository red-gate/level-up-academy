Sometimes your commit history is such a mess that the only sensible choice is to take the nuclear option: reset the entire branch and build a new set of commits from the changes.

Here we have a repository with a really messy commit history.

To remove all the commits in the branch but leave the changes in the working directory, run `git reset master`. This does a [mixed mode reset]() resetting the `HEAD` reference back to `master` (need to refresh my memory as to what a mixed mode reset actually does).

If we now run `git log --oneline` we can see that the current branch is now at the same commit as `master`, but `git status` and `git diff` show that we still have all our changes in the working directory.

Now make two two commits, one for each file.

Once we have made the commits, we can do `git log --oneline` again and see that our messy commit history has become just two commits, one for each of the two files that were modified.

Note: In practice it is often very useful to combine this technique with partial staging using `git add --patch` to allow us more fine grained control over which *parts* of modified files should be staged for commit.
