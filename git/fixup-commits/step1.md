Here we have a repository with a very messy commit history; do `git log --oneline --decorate`{{execute}} to see it. There is just one file, with numerous small commits each making a minor correction to the previous one.

All of these small fixup commits are just noise, and the history would be much cleaner if it were reduced to just one commit.

In the next step, we will see how these commits could have been marked as fixups and automatically combined with the parent commits, but in this step we will see how to do the same thing manually.

First, start an interactive rebase with `git rebase -i master`. This will bring up an editor containing a rebase script.

We can move commits up and down the script to reorder them and we can also mark them as special `fixup` commits. This means that when the rebase runs, these commits will be combined with their parent commit and removed from the commit history.

To see this, mark all of the commits apart from the first one as `fixup`s, by changing the word `pick` to `fixup`, or just `f`.

Save and exit to let the rebase run, and then do `git log --oneline --decorate`{{execute}} again. Notice that the noisy commit history has been cleaned up and we are left with a history that better captures our intent.
