Rather than creating a new commit that just corrects or adds to a previous commit, it is often better to mark the new commit as a `fixup` commit, indicating that it should become part of the commit that it fixes rather than a commit in its own right.

This repository consists of one file, `shopping-list.md`.

Make 3 commits by running the following commands:

`echo "Bananas" >> shopping-list.md`{{execute}}
`git commit -a --fixup master`{{execute}}
`echo "Apples" >> shopping-list.md`{{execute}}
`git commit -a --fixup master`{{execute}}
`echo "Oranges" >> shopping-list.md`{{execute}}
`git commit -a --fixup master`{{execute}}

Now run `git log --oneline --decorate`{{execute}} and look at the three commits that we just created. Notice that they are all prefixed with `fixup!`. This indicates that these commits are temporary, and should be combined with the commit that they fix when the branch is next rebased.

Now run `git rebase -i master~ --autosquash`{{execute}} to do an interactive rebase. Notice that the `fixup!` commits have automatically been moved to the right place in the script and marked as `fixup` commits. Enter `:wq` to save and exit the script, then run `git log --oneline --decorate`{{execute}} to look at the new commit history. Our messy series of commits has been squashed down to just one commit that better captures the intent of what we were trying to do.
