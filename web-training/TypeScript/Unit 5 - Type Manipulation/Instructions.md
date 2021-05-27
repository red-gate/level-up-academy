# Type manipulation

## Generics (Alex)
- Generic functions
- - constraints


## Keyof, Typeof, Indexed Access Types (Sami)

The `keyof` operator takes an object type and produces a string or numeric literal union of its keys:
- `keyof { x: number; y: number }` is `'x' | 'y'` because the object's keys/properties/fields can either be **x** or **y** (e.g. `myObject[x] = myObject.y + 1`)
- `keyof { [n: number]: unknown }` is `number` because that's the restriction we set (e.g. `const someValue = myArray[1]`)
- `keyof { [key: string]: boolean}` is `number | string` because JavaScript allows you to index objects by numeric index and we also said that we wanted to index our object by name (e.g. `myMap['magic-feature-enabled'] = true`)

## Conditional types (Mark)


- Intersection types (Piers)
- - combining types `type X = Person & Serializable & Loggable`
- - picking members from a union `type X = FarmAnimal & { type: 'cow' }`
