# Type manipulation, part 2

## Exercise 1: Mapped types

In the last unit, we introduced ourselves to the `keyof` operator. Now, we will unleash its full potential! Mapped types build on the syntax for index signatures, which are used to declare the types of properties which has not been declared ahead of time.

- Create a `FeatureFlags` type by applying `Options<T>` to the `Features` type.
- Create an readonly version of `UserAccount` by applying `Immutable<T>` to it.
- Create a type where no property is optional by applying `Concrete<T>` to `UserAccount`.
- Create a lazilly-evaluated version of `UserAccount` by applying `Lazy<T>` to it.
- Create a new type where the `id` property is excluded, by applying `ExcludeId<T>` to `UserAccount`.
- Create a new type where you apply `Lazy<T>`, `Concrete<T>` and `ExcludeId<T>` to `UserAccount`

## Exercise: Introduction to Template literal types
   
## Exercise 4: Mixins

### Part A.

Look at ex4.ts and ensure you understand the types presented within.

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

- Add the follow code to `ex4.ts`:

```ts
function Own<TBase extends AnimalConstructor>(Base: TBase) {
    return class Owned extends Base {
		// Mixins may not declare private/protected properties
		//	so ES2020 private fields are the best we have
        _ownerName = "";

        setOwner(owner: string){
            this._ownerName = owner;
        }

        get owner(): string {
            return this._ownerName;
        }
    }
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


## Putting it all together (Mark et. al)
The complete example

## See also
- [Utility Types](https://www.typescriptlang.org/docs/handbook/utility-types.html) contains a lot of commonly used goodies. Check 'em out!
