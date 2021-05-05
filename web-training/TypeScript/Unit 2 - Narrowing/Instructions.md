# Brainstorming
- Enums, union types and discriminated unions
- Null/undefined-value narrowing - Array.find()
- Union type narrowing
- Type predicate functions  




# Union Types
TypeScript’s type system allows you to build new types out of existing ones using a large variety of operators. The first way to combine types you might see is a _union type_. A union type is type formed from two or more other types, representing values that may be _any one_ of those types. We refer to each of these types as the union’s _members_.

Let’s write a function that can operate on strings or numbers:

```ts
function printId(id: number | string) {
  console.log("Your ID is: " + id);
}
```

- Write three function invocations, passing a number, a string and an object
- Change the function to instead `console.log(id.toUpperCase())`

_Narrow_ the union with code, the same as you would in JavaScript without type annotations:

```ts
function printId(id: number | string) {
  if (typeof id === "string") {
    // In this branch, id is of type 'string'
    console.log(id.toUpperCase());
  } else {
    // Here, id is of type 'number'
    console.log(id);
  }
}
```

- Write a function that welcomes people. The function accepts `string[] | string` and outputs either a single greeting **Welcome lone traveller _name_** or a combined greeting **Hello _Name 1_, _Name 2_ ... and _Name N_!**. Hint: There's an `Array.isArray` method.


# Type Aliases and Interfaces

We’ve been using object types and union types by writing them directly in type annotations. This is convenient, but it’s common to want to use the same type more than once and refer to it by a single name.

A _type alias_ is exactly that - a name for any type. The syntax for a type alias is:

```ts
type Point = {
  x: number;
  y: number;
};

// Example usage
function printCoord(pt: Point) {
  console.log("The coordinate's x value is " + pt.x);
  console.log("The coordinate's y value is " + pt.y);
}

printCoord({ x: 100, y: 100 });
```

Note that aliases are _only_ aliases - you cannot use type aliases to create different/distinct “versions” of the same type.

- Review the [differences between Type Aliases and Interfaces](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#differences-between-type-aliases-and-interfaces)

# Type Assertions

Sometimes you will have information about the type of a value that TypeScript can’t know about.

For example, if you’re using document.getElementById, TypeScript only knows that this will return some kind of HTMLElement, but you might know that your page will always have an HTMLCanvasElement with a given ID.

In this situation, you can use a type assertion to specify a more specific type:

```ts
const myCanvas = document.getElementById("main_canvas") as HTMLCanvasElement;
```

Like a type annotation, type assertions are removed by the compiler and won’t affect the runtime behavior of your code.

# Feed the pets - instanceof and type predicate functions

- Check index.ts.
- Change the Feed function to output the pet's name. Why the name field is accesible in this context?
- Use instanceof to feed the dogs.
- Are both dogs being fed? If not, then why?
- Use a type predicate functions to feed Fluffy.




