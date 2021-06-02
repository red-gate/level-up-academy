# Type manipulation

## Exercise 1: Generics (Alex)

- Generic functions
- - constraints

## Exercise 2: `keyof`, `typeof` and Indexed Access Types

The `keyof` operator takes an object type and produces a string or numeric literal union of its keys:

- `keyof { x: number; y: number }` is `'x' | 'y'` because the object's keys/properties/fields can either be **x** or **y** (e.g. `myObject[x] = myObject.y + 1`)
- `keyof { [n: number]: unknown }` is `number` because that's the restriction we set (e.g. `const someValue = myArray[1]`)
- `keyof { [key: string]: boolean}` is `number | string`. Numeric indexes will be coerced into strings (e.g. `myMap['magic-feature-enabled'] = true`)

These will come in handy in the next Unit, when we discuss _Mapped Types_. Here's a quick preview:

A mapped type is a generic type which uses a unicon created via a keyof to iterate through the keys/fields/properties of one type to create another:

Given `type OptionsFlags<Type> = { [Property in keyof Type]: boolean; }`

And `type FeatureFlags = { darkMode: () => void; newUserProfile: () => void; }`

Then `type FeatureOptions = OptionsFlags<FeatureFlags>` is `type FeatureOptions { darkMode: boolean; newUserProfile: boolean }`

- Run through the code examples in Ex2 and make sure you understand the concepts.

To look up the type of a specific property of a containing type, we use an *indexed access type*:

`type PlaceName = MyGeoPosition['name']`

- What union would the following produce and why? `type RiddleMeThis = MyGeoPosition['name' | 'longitude']`

## Exercise 3: Conditional types (Mark)

### Part A: Conditional types

Conditional types are cool because they let us implement basic logic at the type level. You'll likely not use them much in your own code, but they show up a lot when typing various library functions.

One common pattern for a function is to operate on defined values, but pass through `null` values unchanged. Take a look at `returnsNullIfNull.ts`, where we implement this pattern with two different function overloads. We can implement the same idea in a more generic way by using conditional types. Take a look at `oneOrMany.ts` to see the idea.

- TODO: more detail

### Part B: Pattern matching on types

For our own code, conditional types become much more practical when combined with the `infer` keyword. We can match against different types and extract part of the result.

In `api.ts` we have some duplication between the contents of a `createApi` method and an `Api` type. We use the `Api` type to make sure that a mock Api implementation contains all the correct methods. However, this is redundant: instead of defining the `Api` type explicitly, we can define it as "whatever the `createApi` method returns". This means we can remove the amount of boilerplate when adding a new method to the api.

- Create a `MyReturnType<T>` conditional type which, if `T` is a function, produces the return type of `T`
- Compare it to the actual `ReturnType<T>` type in typescript's standard libary
- Use `MyReturnType<T>` to define the `Api` type as whatever `createApi` returns.
- TODO: more detail

## Exercise 4: Intersection types

### Part A

- Create an interface to represent `ICat` with two variables:
  - `name` of type `string`
  - `numberOfLives` of type `number`
- Create a variable `kitty` that is an `ICat`

### Part B

- Tame animals are animals that have an owner.
- Create an interface called `ITame` to represent this
- Create a type called `TameCat` that is both `ICat` and `ITame`
- Adjust `kitty` to be a `TameCat`
- Do something similar for `fiddo`

### Part C

- Create a function `PrintTameCat` that outputs:
  `` console.log(`${cat.name} is a cat owned by ${cat.ownerName}`);``
- `fiddo` has all the properties used in `PrintTameCat` - Can you pass `fiddo` to this function?

### Part D

- Create a type `TameAnimal` that is anything tame with a name
- Create a function `PrintTameAnimal` that takes in a `TameAnimal`
- Can you use this function on both `kitty` and `fiddo`?
- Is there a difference between your definition of `PrintTameAnimal` and this one:

```ts
function PrintTameAnimal(tameAnimal: { name: string; ownerName: string }) {
  console.log(
    `${tameAnimal.name} is an animal owned by ${tameAnimal.ownerName}`
  );
}
```

### Part E

- Convert `IDog` and `ICat` to a discriminated union `Animal`:

```ts
type Animal =
  | {
      type: "Dog";
      name: string;
      goodBoy: boolean;
    }
  | {
      type: "Cat";
      name: string;
      numberOfLives: number;
    };
```

- Create a type definition for `Dog` that is an `Animal` with value `"Dog"` for the `type` property. This is using an intersection type to select from a union
- Fix the definition of `PrintDog` to take a `Dog`
- Use a Type Assertion to convert fiddo to a `Dog`
- Remove or comment out everything not needed for `PrintDog` to run and run the program
