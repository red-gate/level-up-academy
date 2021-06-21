# Exercise 3 - Honing in

We're now ready to add a handful of tests and then learn how to improve our experience with a test suite.

- Copy the function and test files from Exercise 2, using a single `Copy-Item`. If you need help at this point, use `Get-Help Copy-Item -Online` to quickly navigate to the documentation.
- Create a test fullfilling `'Earth is the third planet in our Solar System'`.
- Create a test fullfilling `'Pluto is not part of our Solar System'`. Here, you might want to take a look at the `Where-Object` CmdLet.
- Create a test fullfilling `'Planets have this order: Mercury, Venus, Earth, Mars, Jupiter, Saturn, Uranus, Neptune'`. Here, you might want to take a look at the `-join` operator.

As we start to build up a number of tests, it gets harder and harder to focus on just the last one we're working on. To make our lives easier, navigate to `File > Preferences > Settings` in Visual Studio Code, search for "pester" and uncheck the `PowerShell > Pester: Use Legacy Code Lens` option.

Returning to your test file, now notice how you can run and debug individual tests!

- Debug the Pluto test.
