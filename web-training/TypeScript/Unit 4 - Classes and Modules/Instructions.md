## Exercise 1: import, export, default (Mark)

- Export/import functions from one file to another
- Use default
- Resolve a naming conflict  

- https://www.typescriptlang.org/docs/handbook/2/modules.html#modules-in-typescript
- Adding `import` or `export` to a typescript file makes it a module
- Modules are namespaced
  - Typescript will complain about duplicate definitions between files that are not modules
  - You can add `export {}` if you just want the namespace without exporting something
- Export styles:  
  `export default function hello[...]` is used like `import hello from './hello'`  
  `export function hello[...] ` or `function hello[...]; export { hello }` is used like `import { hello } from './hello'`  
  Style choice: export each item individually, or put one big `export { ... }` statement at the bottom of the file.
- Typescript can also import and export types. This has no impact on the compiled result, but provides namespacing for types.
  - Note: typescript can sometimes be confusing - the same name might refer to both a value and a type. This happens most often with class declarations, but can happen with other types too. Types and values have separate namespaces (TODO: check details here)

## Exercise 2: require (Mark)

- Export/import functions from one file to another
- Explore Module Output Options
- Discuss bundling (we already did it in JS script a bit)
    
- https://www.typescriptlang.org/docs/handbook/2/modules.html#es-module-syntax-with-commonjs-behavior
- Typescript also supports the commonjs / node syntax for modules, using `module.exports = { ... }` and `const foo = require(...)`
- TypeScript Module Output Options
  - https://www.typescriptlang.org/docs/handbook/2/modules.html#typescripts-module-output-options
  - Why is a bundler necessary?
  - https://mattallan.me/posts/modern-javascript-without-a-bundler/

## Exercise 3: interfaces (Alex)

- Declare, extend and use
- What can we delcare in an interface
- Duck typing
   
## Exercise 4: Classes (Piers)

### Part A - Classes or Interfaces?

"The decision to use a class or an interface truly depends on our use case: type-checking only, implementation details (typically via creating a new instance), or even both! We can use classes for type-checking and the underlying implementation - whereas we cannot with an interface. "
[From ultimatecourses.com](https://ultimatecourses.com/blog/classes-vs-interfaces-in-typescript#:~:text=A%20class%20is%20a%20blueprint,implementation%20nor%20initialisation%20for%20them.)

### Part B - Class construction

- Ex4.js contains a basic class definition for `Dog`.
- Create an interface called `IDog` that represents the same thing as `Dog`.
- Create an instance of `IDog` and also print it's name.
- In the generated JavaScript what is the difference between `IDog` and `Dog`?

Extend `Dog` to include a private variable for owner name.
Can you access the owner name in TypeScript?
Are there any differences between ownerName and name in the generated JavaScript?

### Part C - Using classes

Define a type called `AllDogs` that is either `Dog` or `IDog`.
Create a function called `printDogName` that takes a single `AllDogs` input and prints to the console the dog name

Add a function called `wag` to the `Dog` class that outputs the text:
```js
`${this.name} wags its tail for ${this.ownerName}`
```
Call `wag` on `fiddo`

### Part D - Type checking

Create a new class `PizzaDog` that extends `Dog` and instantiate a new instance of `PizzaDog` called `pizza`
Is `pizza` an `instanceof` `PizzaDog`?
Is `pizza` and `instanceof` `Dog`?

Remove the extends and copy `Dog`'s constructor to `PizzaDog`.
Does this change the previous two answers?

If you had used two separate interfaces for with the same definition would you be able to distinguish them?

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
