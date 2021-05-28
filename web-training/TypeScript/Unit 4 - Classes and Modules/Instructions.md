## Exercise 1: Modules with `import` and `export`

There are two main syntaxes available for defining javascript modules, usually called ES6 and commonjs. The (oversimplified, not-quite-accurate) difference is that you'll use ES6 modules when writing for the browser, and commonjs modules when writing nodejs scripts.

First, we'll look at ES6 modules.

### Part 1: Namespacing

Take a look at the files in `Exercises/Ex1`. We have an entrypoint called `index.ts`, and two files `game1.ts` and `game2.ts` that are acting as libraries. Typescript is currently giving us an error because `game1.ts` and `game2.ts` have functions with overlapping names. By default, typescript files are not modules, and they all exist in the global namespace.

- Run `npm install` and `npm run ex1` in the `Exercises` folder, and take a look at the emitted javascript files. (In this exercise we've disabled the `--noEmitOnError` option, so we can still look at the js output even though there are compile errors)  
  Apart from adding `"use strict"` to the files, typescript hasn't really done anything to them yet.

In order to turn our script files into modules, they must have at least one `import` or `export` statement. For this first part we're only interested in the namespacing, though, so we don't have anything we want to export yet.

- We can work around this for now, though. Try adding `export {}` to the bottom of `game1.ts` and `game2.ts`.
- Run `npm run ex1` again, and examine the newly generated js.

Our generated code has now been compiled to [AMD ("Asynchronous module definition")](https://requirejs.org/docs/whyamd.html) modules. We can see how namespacing has been implemented using a function - within the function body, we know that any parameter names refer to exact objects we control. And names inside the function stay scoped to that function - no names will accidentally escape unless we explicitly return them.

- In `tsconfig.json`, try changing the `"module": "amd"` option to some other values (like `"umd"` or `"System"`) and compare the results.  
  Note how the generated code relies on some external function (like `define` or `System.register`) to actually define the modules. This function acts like a DI container in C# code: we'll need something external to register all our modules and join them together.

### Part 2: Importing from other modules

Take a look at `index.ts`. It's now erroring because there isn't a `playGame` function in scope any more - making `game1.ts` and `game2.ts` modules took `playGame` out of the global scope.

- Change `game1.ts` and `game2.ts` so they export their `playGame` functions. ( Take a look at https://www.typescriptlang.org/docs/handbook/modules.html#export for syntax help )
- Change `index.ts` so that it imports a `playGame` function from each file.  
  We still can't have two different functions called `playGame` in scope - we'll either need to rename the imports, or scope the functions somehow. Take a look at https://www.typescriptlang.org/docs/handbook/modules.html#import-a-single-export-from-a-module for options, and pick the one you like best.
- Take a look at the newly-generated javascript for these files. Note how the dependencies get exported from `game1.js` and `game2.js`, and how the generated function in `index.js` injects those dependencies.

### Part 3: Default exports

Since we're only exporting a single function for each game file, it also makes sense to try using a default export ( https://www.typescriptlang.org/docs/handbook/modules.html#default-exports ). This doesn't change much, except we now use a different syntax for importing: `import playGame1 from './game1'` instead of `import { playGame } from './game1'`. Default imports don't have a pre-defined name, which makes them easier to rename if necessary (as in this exercise) but can lead to more inconsistent code.

- Try quickly changing the `Ex1` folder over to using default imports. Which style do you prefer?

### Part 4: Making it work in a browser

Even though typescript has compiled our code, it requires additional compilation steps before it can be easily used in a browser. This is for a couple of reasons:

- While [most browsers](https://caniuse.com/es6-module) now support loading ES6 modules directly, support is not universal, and [comes with a bunch of caveats](https://david-gilbertson.medium.com/es6-modules-in-the-browser-are-they-ready-yet-715ca2c94d09).
- Even where modules are supported, they tend to be the wrong size - we want to avoid the browser making hundreds of network requests for each of the files in our codebase.

We need to use a "bundler" tool to compile our individual `.js` files into one big output bundle. If you're familiar with classic C build pipelines, it might help to think of modules as object files and bundling as the linker step.

There are many javascript bundlers available, on a rotating lifecyle: [grunt](https://gruntjs.com/), [gulp](https://gulpjs.com/) and [browserify](https://browserify.org/) are on the way out, [webpack](https://webpack.js.org/) is the current standard choice and [parcel](https://parceljs.org/) is the up-and-coming contender. Expect this to all change again in a few years.

Parcel and webpack provide many additional features on top of simple bundling, such as integrating the typescript compiler into the build process or running an in-memory development server with hot reloading. We don't have enough time to dive into all these features, though (they could fill up an entire course just by themselves!) so for this exercise we'll use a simpler alternative called [rollup.js](https://rollupjs.org/guide/en/):

- In `tsconfig.json`, change the generated module format to `"ES6"` and run the typescript build again.  
  This means typescript will keep the files as ES6 modules, rather than transforming them into AMD or UMD module functions. This is necessary since Rollup takes the ES6 module format as input.
- In the `Ex1` folder, run `npx rollup --file bundle.js index.js`.  
  This tells Rollup to read `index.js`, process its dependencies, and then bundle them into an output file called `bundle.js`.
- Take a look at the generated `bundle.js` contents.  
  Note how the three files have been merged into one, and the namespace issues have been resolved by renaming some of the functions.

## Exercise 2: Modules with Node.js and `require` (Optional)

If you're only writing code for node scripts rather than the browser, then the situation is a bit different and a bit simpler:

- The syntax is different - you'll generally use `require()` and `module.exports` instead of `import` and `export` (although newer versions of node [do support the ES6 syntax](https://stackoverflow.com/a/45854500))
- You don't need a separate bundling step, since node has module resolution built in.

For building a node app, there are a few steps. **It's fine if you want to skip these, since there aren't many interesting differences.**

- In the Ex1 folder, run `npm install @types/node --save-dev`. This will provide types for the `require` function and `module` object, along with all the other APIs that node provides. You'll learn more about `@types` packages later in this unit.
- In `tsconfig.json`, set the `"module"` option to `"commonjs"`.
- We still need to use the `import` and `export` keywords to let typescript know that individual files are modules, so we end up using a strange hybrid of ES6 and node module syntax:
  - Instead of `const foo = require("./library");`, write `import foo = require("./library")`
  - Instead of `module.exports = { exportedFunc }`, write `export = { exportedFunc }`
- The `import`/`export` syntax will be transformed back into the standard node syntax in the generated javascript.

## Exercise 3: Interfaces

- Analyze Ex3.ts
- Can we try to take a slice of a beer?
- Let's make the code more type safe by describing functions expectations from their parameters using [interfaces](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#interfaces).
- Declare `IFood`, `IBeverage` and `IPizza` interfaces. Change the three functions to take corresponding types and make sure that the existing calls for `beer` and `margheritta` are still working.
- Can `IPizza` be a `salad`? If so, then improve the interface definition to restrict its `type`?
- What's your opinion on making `takeSlice` a part of the `IPizza` interface?

## Exercise 4: Classes

### Part A - Classes or Interfaces?

"The decision to use a class or an interface truly depends on our use case: type-checking only, implementation details (typically via creating a new instance), or even both! We can use classes for type-checking and the underlying implementation - whereas we cannot with an interface. "
[From ultimatecourses.com](https://ultimatecourses.com/blog/classes-vs-interfaces-in-typescript#:~:text=A%20class%20is%20a%20blueprint,implementation%20nor%20initialisation%20for%20them.)

### Part B - Class construction

- Ex4.ts contains a basic class definition for `Dog`.
- Create an interface called `IDog` that represents the same thing as `Dog`.
- Create an instance of `IDog` and also print it's name.
- In the generated JavaScript what is the difference between `IDog` and `Dog`?

Extend `Dog` to include a private variable for owner name.
Can you access the owner name in TypeScript?
Are there any differences between ownerName and name in the generated JavaScript?

### Part C - Using classes

Define a type called `AllDogs` that is either `Dog` or `IDog`.
Create a function called `printDogName` that takes a single `AllDogs` input and prints to the console the dog name.

Add a function called `wag` to the `Dog` class that outputs the text:

```js
`${this.name} wags its tail for ${this.ownerName}`;
```

Call `wag` on `fiddo`

### Part D - Type checking

Create a new class `PizzaDog` that extends `Dog` and instantiate a new instance of `PizzaDog` called `pizza`.  
Is `pizza` an `instanceof` `PizzaDog`?  
Is `pizza` and `instanceof` `Dog`?

Remove the extends and copy `Dog`'s constructor to `PizzaDog`.
Does this change the previous two answers?

If you had used two separate interfaces for `IDog` with the same definition would you be able to distinguish them?

## Exercise 5: Type defintions for JavaScript

In the real world, where we write production code, we might want to include a library from NPM to allow ourselves to write and maintain less code ourselves. If this library is written in JavaScript, you can still get all the TypeScript goodies by installing a type definition alongside it.

- Open Ex5/Ex5.ts
- Note the `console.log` statement
- Run Ex5 and confirm that TypeScript is unhappy with us: `Cannot find module 'os' or its corresponding type declarations.`
- Also note that (at least Visual Studio Code) doesn't give any intellisenese if you try to browse `os`' members with `CTRL`+`Space`. Horrible dev experience!

This can all be solved by installing the appropritate typings: `npm install --save-dev @types/node`

- Run the above statement, then re-run the example.
- Note that you get output.
- Pick something else from `os` and output that!

A lot of packages nowadays come with TypeScript bindings (see ex. [boxen](https://www.npmjs.com/package/boxen) and [chalk](https://www.npmjs.com/package/chalk), but for those who don't, there's likely a binding available via [Definitely Typed](https://github.com/DefinitelyTyped/DefinitelyTyped).

The naming standard is `@types/`_original package name_

Since we're using these types during development (not runtime), we always install them with `-D | --save-dev`
