# Trivia

Before starting this exercise, ensure that you can build and run both the console application in the`Trivia` project and the test in the `Trivia.Tests` project.

For each of these tasks, the `GoldenMaster` test should remain green. **You are not allowed to modify the expected output in any way.**

## Task 1 - Introduce a DI framework

Add a NuGet reference to Ninject, configure the dependencies of the `Game` class, and use service location (`kernel.Get<>()`) to get an instance of `Game`.

Hint: [`ToMethod()`](https://github.com/ninject/Ninject/wiki/Providers,-Factory-Methods-and-the-Activation-Context#factory-methods), `ToSelf()` and `WithConstructorArgument()` might be useful

## Task 2 - Making configuration easier

The `Players` and `Board` classes are awkward to construct using Ninject. Change both types to take a new dependency in their constructors that will provide the relevant information that they need. Bonus points if you can avoid hard-coding the player and category names into any places other than the `Main` method.

How has this changed the Ninject configuration? What are the benefits of doing this?
