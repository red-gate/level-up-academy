# Brainstorming

- Enums, union types and discriminated unions
- Null/undefined-value narrowing - Array.find()

# Union Types

TypeScript’s type system allows you to build new types out of existing ones using a large variety of operators. The first way to combine types you might see is a _union type_. A union type is type formed from two or more other types, representing values that may be _any one_ of those types. We refer to each of these types as the union’s _members_.

## Exercise 1 - making implicit conversions explicit

Let’s write a function that can operate on strings or numbers:

```ts
function printId(id: number | string) {
  console.log("Your ID is: " + id);
}
```

- Write three function invocations, passing a number, a string and an object
- Try changing the function to print `id.toUpperCase()` instead of just `id`. What error do you get?
- You'll need to narrow the type of id to either `number` or `string` using the `typeof` operator, the same as you would in Javascript.

## Exercise 2 - array or not

- Write a function that welcomes people. The function accepts `string[] | string` and outputs either a single greeting **Welcome lone traveller _name_** or a combined greeting **Hello _Name 1_, _Name 2_ ... and _Name N_!**. Hint: There's an `Array.isArray` method.

## Exercise 3 - instanceof and type predicate functions

- Check ex3.ts.
- Change the Feed function to output the pet's name. Why the name field is accesible in this context?
- Use instanceof to feed the dogs.
- Are both dogs being fed? If not, then why?
- Use a type predicate functions to feed Fluffy.

# Discriminated unions

So far, we've distinguished between different members of a union by inspecting certain specific properties that we know exist in each case. This works well enough, but it can get a little awkward.

Discriminated unions are a pattern in typescript (and other languages) where each member of the union shares a common property that can be used to distinguish members from each other. For example, we can write a type like this:

```ts
type Pet =
  | {
      type: "cat"; // Note how we're using a literal string as a type here
      name: string;
      numberOfLives: number;
    }
  | {
      type: "dog";
      name: string;
      isAGoodBoy: boolean;
    };
```

Since the 'type' property is common to all members of the Pet union, we can check it for arbitrary `Pet`s. And once we check the value of the type property, typescript can narrow down which kind of Pet it is.

## Exercise 4 - pets, again

- Adapt exercise 3 to use discriminated unions. Try using a common property ('type', 'kind' or similar) to distinguish the union members instead of custom type guards.

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

Be careful with type assertions! In some cases, Typescript will produce type errors for incorrect assertions:

```ts
const foo = null as string;
//          ^^ Conversion of type 'null' to type 'string' may be a mistake because neither type sufficiently overlaps with the other. If this was intentional, convert the expression to 'unknown' first.
```

but in other cases, Typescript will happily believe you:

```ts
interface Api {
  function1: () => void;
  function2: () => void;
}

const mockApi = { function1: () => {} } as Api;
// Compiles, but fails at runtime:
mockApi.function2();
```

In general, avoid using type assertions unless strictly necessary.
