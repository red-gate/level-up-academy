# Type manipulation, part 2

## Exercise 1: Mapped types

In the last unit, we introduced ourselves to the `keyof` operator. Now, we will unleash its full potential! Mapped types build on the syntax for index signatures, which are used to declare the types of properties which has not been declared ahead of time.

- Create a `FeatureFlags` type by applying `Options<T>` to the `Features` type.
- Create an readonly version of `UserAccount` by applying `Immutable<T>` to it.
- Create a type where no property is optional by applying `Concrete<T>` to `UserAccount`.
- Create a lazilly-evaluated version of `UserAccount` by applying `Lazy<T>` to it.
- Create a new type where the `id` property is excluded, by applying `ExcludeId<T>` to `UserAccount`.
- Create a new type where you apply `Lazy<T>`, `Concrete<T>` and `ExcludeId<T>` to `UserAccount`

## Exercise 2: Template literal types

Template literal types are a new Typescript feature which adds a lot more typing power around stringly-typed code. They are primarily useful for typing existing Javascript APIs, but can be useful in new code as well.

Previously we've seen how string literal types let us specify specific string values as part of a type. Template literal types go further by letting us poke around the contents of the string itself!

The syntax for template literal types is based on template strings in Javascript:

```ts
// template string:
console.log(`One plus one is: ${1 + 1}`);

// template literal type:
type Two = "2";
type Maths = `One plus one is: ${Two}`;
```

(Doing maths in the type system is actually possible, but that's a topic for another time ;)

Union types get expanded when they're interpolated:

```ts
type Greetings = `Hello, ${"world" | "friends"}!`;
// ^ Equivalent to "Hello, world!" | "Hello, friends!"
```

- Take a look at `Ex2/ex2.ts`
- Try filling in the type definitions for `CssBorderString` and `EventHandlers<T>` types.

Bonus: you can do some crazy type-level programming with template literal types, including [a fully-functional SQL database and query language](https://github.com/codemix/ts-sql)! Check out [Typescript Awesome Template Literal Types](https://github.com/ghoullier/awesome-template-literal-types) for more examples.

## Exercise 3: Mixins

### Part A.

Look at `ex3.ts` and ensure you understand the types presented within.

### Part B - Constructor definitions

- Mixins are like partial class definitions that can be merged onto other classes
- To do this, we first need to declare a type to represent a constructor like this:

```ts
type AnimalConstructor = new (...args: any[]) => {};
```

- Does this make sense?
- Why can't this be:

```ts
type AnimalConstructor = new (name: string) => {};
```

### Part C - Your first mixin

- Add the follow code to `ex3.ts`:

```ts
function Own<TBase extends AnimalConstructor>(Base: TBase) {
  return class Owned extends Base {
    // Mixins may not declare private/protected properties
    //	so ES2020 private fields are the best we have
    _ownerName = "";

    setOwner(owner: string) {
      this._ownerName = owner;
    }

    get owner(): string {
      return this._ownerName;
    }
  };
}
const OwnedCat = Own(Cat);
```

- Can you make a new OwnedCat and set its owner?
- Can you own a Platypus?
- Alter `AnimalConstructor` to have a generic constraing:

```ts
type AnimalConstructor<T = Animal> = new (...args: any[]) => T;
```

### Part D - Exercise time

- Can you create a new mixin called `Trainable` that adds a boolean property `trained` to Animals?
- Can you create a cat called `oscar` that is owned by `Sami` and is trained?

### Part E - Why?

- Discuss why you might want to use mixins

## Exercise 4: Putting it all together

A general issue with Typescript is that it does not add any runtime type information. We get a ton of static typing power at compile time, but very little ability to reflect on types when the code is actually running.

This is particularly an issue when typescript code has to deal with with foreign input that might not be typed correctly - for example, a JSON response from a web API.

Fortunately, there's a workaround for these kinds of issues, using the advanced types we've learned in the past two units. The trick is to define runtime validation code using some carefully-crafted functions, and then our advanced types to extract the static types from the validator definitions. This way we ensure that the runtime and static type definitions always stay in sync.

- Take a look through `Ex4/runtimeValidation.ts` to see how union types, generics, conditional types, mapped types and template literal types are all used together.

Note: If you want to do this in real code, you should probably use `io-ts` - a proper library with much more features than we've shown off here.

## See also

- [Utility Types](https://www.typescriptlang.org/docs/handbook/utility-types.html) contains a lot of commonly used goodies. Check 'em out!
