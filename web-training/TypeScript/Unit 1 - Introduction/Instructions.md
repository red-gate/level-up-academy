Introduction and basic typing.
Start with (JSFiddle?) and possibly show locally with ts file and use compiler on command line. 

# Brain Storming 15/04

- Common issues that TS fixes
- Replace JSFiddle and instead work locally?
- Hello world in TypeScript and see what the resulting JavaScript would look like
- eslint?
- tweak [JavaScript week 1 exercises](https://github.com/red-gate/level-up-academy/blob/master/web-training/JavaScript/Week%201%20-%20Basic%20Syntax/Instructions.md) to incorporate typescript
- Point out enabling strict by default
> The --strict flag in the CLI, or "strict": true in a tsconfig.json toggles them all on simultaneously
- Given an example with a misspelled DOM event handler, "can you spot a bug in this code?"; we can have the computer do it for us


# Tasks
- Piers: Convert W1 JS to TS
- Alex: Come up with example of errors that can be detected via static analysis
- Mark: missspelled DOM event handler example
- Sami: Installing, running and getting feedback from typescript when developing locally

# Task

- Have a look at `index.js` in the `Examples` folder, and read through the code. What do you think the code does? Can you spot any issues?
- Try renaming `index.js` to `index.ts`. What issues does typescript find? What doesn't it find?

## Exercise 1

### Part a
- Create a function that takes a string name, a number n, and returns the string `"Hello {*name*}!"` *n* times.
- Call that function and output the result to the console log
- How well does your IDE handle swapping the arguments to Greet around `Greet(5, "piers")`

### Part b
- Create a function that calculates the fibonnaci sequence recursively

### Part c
- Memoize the fibonnaci program you wrote in Part b by using an array to store previously calculated values. This function syntax should help:
`function Fibonnaci(index: number, previousValues?: number[]): number {}`
