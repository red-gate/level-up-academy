## Exercise 1

- Use `npm install` to install TypeScript as a development dependency (see `package.json` for details)
- Ensure that things are set up correctly by running `npm tsc`. You should see a `"Hello World"` message being printed to the console.
- Use the fraction.ts

## Exercise 2

- Have a look at `index.js` in the `Examples` folder, and read through the code. What do you think the code does? Can you spot any issues?
- Try renaming `index.js` to `index.ts`. What issues does typescript find? What doesn't it find?

## Exercise 3

### Part a
- Create a function that takes a string name, a number n, and returns the string `"Hello {*name*}!"` *n* times.
- Call that function and output the result to the console log
- How well does your IDE handle swapping the arguments to Greet around `Greet(5, "piers")`

### Part b
- Create a function that calculates the fibonnaci sequence recursively

### Part c
- Memoize the fibonnaci program you wrote in Part b by using an array to store previously calculated values. This function syntax should help:
`function Fibonnaci(index: number, previousValues?: number[]): number {}`
