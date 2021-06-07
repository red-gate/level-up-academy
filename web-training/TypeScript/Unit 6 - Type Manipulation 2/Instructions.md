# Type manipulation, part 2

## Exercise 1: Mapped types (Sami)

In the last unit, we introduced ourselves to the `keyof` operator. Now, we will unleash its full potential! Mapped types build on the syntax for index signatures, which are used to declare the types of properties which has not been declared ahead of time.

- Create a `FeatureFlags` type by applying `Options<T>` to the `Features` type.
- Create an readonly version of `UserAccount` by applying `Immutable<T>` to it.
- Create a type where no property is optional by applying `Concrete<T>` to `UserAccount`.
- Create a lazilly-evaluated version of `UserAccount` by applying `Lazy<T>` to it.
- Create a new type where the `id` property is excluded, by applying `ExcludeId<T>` to `UserAccount`.
- Create a new type where you apply `Lazy<T>`, `Concrete<T>` and `ExcludeId<T>` to `UserAccount`

## Exercise 2: Utility types (Alex)
  - Cover a few types and mention other options  

## Exercise 3: Template literal types (Mark)
  - Event name example?
   
## Exercise 4: Mixins (Piers)
  - Simple example
