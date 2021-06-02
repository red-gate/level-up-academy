# Type manipulation

## Exercise 1: Generics (Alex)
- Generic functions
- - constraints


## Exercise 2: Keyof, Typeof, Indexed Access Types (Sami)

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

## Exercise 3: Conditional types (Mark)


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
``` console.log(`${cat.name} is a cat owned by ${cat.ownerName}`);```
- `fiddo` has all the properties used in `PrintTameCat` - Can you pass `fiddo` to this function?

### Part D
- Create a type `TameAnimal` that is anything tame with a name
- Create a function `PrintTameAnimal` that takes in a `TameAnimal`
- Can you use this function on both `kitty` and `fiddo`?
- Is there a difference between your definition of `PrintTameAnimal` and this one:
```ts
function PrintTameAnimal(tameAnimal: {name: string, ownerName: string}){
    console.log(`${tameAnimal.name} is an animal owned by ${tameAnimal.ownerName}`);
}
```