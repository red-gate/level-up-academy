UNIX is an operating system organized around the following core characteristics:

* A centrally organized kernel which manages systems and processes
* Non kernel software is organized into separate processes
* Pre-emptive multi-tasking
* A file system with a single root at `/`
* *EVERYTHING* is a file

There's a very distinct philosophy associated with Unix. Here's what Doug McIlroy said:

* Make each program do one thing well. To do a new job, build afresh rather than complicate old programs by adding new "features".
* Expect the output of every program to become the input to another, as yet unknown, program. Don't clutter output with extraneous information. Avoid stringently columnar or binary input formats. Don't insist on interactive input.
* Design and build software, even operating systems, to be tried early, ideally within weeks. Don't hesitate to throw away the clumsy parts and rebuild them.
* Use tools in preference to unskilled help to lighten a programming task, even if you have to detour to build the tools and expect to throw some of them out after you've finished using them.

![Example set of permissions](https://github.com/fffej/katacoda-scenarios/raw/master/learn-bash/images/art-of-unix.jpg)

If you want to find out more, I highly recommend reading "The Art of UNIX Programming" by Eric S. Raymond. Either buy it, or read the [freely available PDF](http://catb.org/esr/writings/taoup/html/graphics/taoup.pdf).
