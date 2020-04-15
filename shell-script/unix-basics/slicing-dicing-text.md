UNIX has a million and one commands for slicing and dicing text.

`sort` does exactly what you might expect.  Let's sort the list of chemical elements.

`sort -r ~/tutorial/chemical-elements.txt`{{execute}}

`grep` searches for text in files. It is extremely powerful! Let's start with the basics, find all lines matching containing the exact string `ium` in `~/tutorial/chemical-elements.txt`

`grep ium ~/tutorial/chemical-elements.txt`{{execute}}

We can add the `v` flag to find the lines NOT containing ium.

`grep -v ium ~/tutorial/chemical-elements.txt`{{execute}}

There's much more to learn about `grep`. Reading the manual pages is highly recommended.

One of the most powerful is `cut`. This allows you to treat each line as a delimited list and extract data appropriately. Let's extract out the first and third fields (skipping the second).

`cut -c1,3 ~/tutorial/chemical-elements.txt`{{execute}}

`sed` is a stream editor and can perform operations on a stream of text in a variety of ways.  If you look at the file `countries.txt`, you'll see it has a bunch of blank lines. This is really annoying. Since `sed` is a stream editor, we can remove these lines.

`sed '/^$/d' ~/tutorial/countries.txt`{{execute}}

This uses a regular expression (match the beginning of the line followed by the end of a line) with an operation `d` to delete the line. Note that this doesn't edit the data it just streams to a new file. `sed` has a `-i` option (inplace) to make changes to the file directly (or redirect the output into another file).

