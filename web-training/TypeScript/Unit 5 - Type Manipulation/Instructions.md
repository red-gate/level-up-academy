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



## Exercise 3: Conditional types (Mark)


## Exercise 4: Intersection types (Piers)

- combining types `type X = Person & Serializable & Loggable`
- picking members from a union `type X = FarmAnimal & { type: 'cow' }`
