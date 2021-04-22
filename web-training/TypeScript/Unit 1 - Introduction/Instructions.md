# Relevant pre-reading
We're basing this Unit on [The Basics](https://www.typescriptlang.org/docs/handbook/2/basic-types.html) section of [The TypeSCript Handbook](https://www.typescriptlang.org/docs/handbook).

## Exercise 1

- Use `npm install` to install TypeScript as a development dependency (see `package.json` for details)
- TypeScript reads configuration options from a `tsconfig.json` file in the current directory, if such a file is present. Take a look at the `compilerOptions` to see some recommended configuration:
  - We have enabled `strict` to ensure that TypeScript will issue an error on any variables whose type is implicitly inferred as `any` (that is, untyped) and making `null` and `undefined` more explicit, sparing us from worrying about whether we forgot to handle them.
  - By setting `noEmitOnError` we ensure that our TypeSCript compiler does not generate invalid JavaScript files when it identifies that something is wrong.
  - TypeScript generates JavaScript ES3 (from 1999) for backwards compatibility by default. For our examples, we've chosen to to [rely on ES2016](https://www.w3schools.com/js/js_versions.asp). As described in [this StackOverflow post](https://stackoverflow.com/questions/61835971/es7-es8-es9-es10-browser-support), "Browser vendors don't implement specific versions, but specific features. Almost every modern browser is still missing features from ES2017-ES2020."
- Ensure that things are set up correctly by running `npm run ex1`. You should see a `"Hello World"` message being printed to the console.

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
