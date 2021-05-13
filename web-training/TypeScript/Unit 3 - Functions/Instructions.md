# Unit 3 - Functions

## The Handbook

https://www.typescriptlang.org/docs/handbook/2/functions.html

## Exercise 1: Function types (Alex)

- Passing functions as variables

## Exercise 2: Making sure we return from functions

Most of the time, we can just let typescript infer the return values of our functions:

```ts
const hello = () => "hello, world!";
//    ^ inferred type: () => string
```

However, there are a few situations where specifying an explicit return type is useful: to enforce that the function always returns something.

This most commonly becomes an issue with switch statements. In other languages, switch statements are often seen as an antipattern, and a common source of bugs when some cases are not implemented.

However, in typescript we can fix one of the biggest issues with switch statements: by use type checking features to make sure that they cover all cases.

### Part 1

Take a look at `Ex2/WithReturnValue.ts`, and the `getProductFromAnimal` function in particular. This function isn't currently handling all types of `FarmAnimal`.

- What error is typescript reporting at the moment? What is the function's inferred return type?
- Try changing the function to have an explicit return type. What error does typescript produce now?  
  This error message is a little confusing - can you explain it?
- Try changing the function to handle the missing case.

### Part 2

What happens if the function doesn't return a value? How can we make sure that switch statements are exhaustive in that case?

For this task we'll need to learn about [a special type called `never`](https://www.typescriptlang.org/docs/handbook/2/functions.html#never). One way to think about types is in terms of the number of possible values they have:

- types like `string` and `object` have many values, while `boolean` has two values
- types like `"hello, world"`, `true`, `null` and `undefined` each have a single value
- `never` is a type with `zero` values!

`never` is useful in a couple of situations here:

- We can use `never` as a function return type to indicate that the function always throws rather than returning a value.
- We can assert that a variable's type should be `never` if we've already narrowed down all other possibilities.

Have a look at `Ex2/WithoutReturnValue.ts`. Note we have a similar `FarmAnimal` type now, but the per-animal functions no longer return values.

- Are any errors being produced with the current code? Would you expect some?
- How can we use `assertUnreachable` to ensure an error happens here? Make sure the error goes away once the `processAnimal` function is complete.

### Bonus discussion:

Thinking about types in terms of the number of possible values again (their 'cardinality'):

- Unions are sometimes called 'sum' types, and structs/interfaces/records called 'product' types. Why is this?
- Given that `never` has a cardinality of zero, what's the implication for a type like `string | never`?

## Exercise 3: Optional and Overloads

### Part 1

Read Ex3.ts and explain what is contained within

### Part 2

Write a function called `FeedCat` that takes as argument a single `Cat` and [optionally](https://www.typescriptlang.org/docs/handbook/2/functions.html#optional-parameters) a single `CatFood`.

- This should output some text that differs if catfood is provided
- Feed kitty with and without catfood

### Part 3

Write a function called `FeedEither` that takes three arguments:

- a single `Pet`
- An optional `CatFood`
- An optional `DogFood`

Can you feed kitty with dogfood?

### Part 4

Write a function called `ForceFeed` that takes two parameters:

- A single `Pet`
- [food that is either](https://www.typescriptlang.org/docs/handbook/2/everyday-types.html#union-types) `CatFood` or `DogFood`

Can you force feed kitty with dogfood now?

### Part 5

What if we want a single implementation that causes only cats to be fed catfood and dogs to be fed dogfood? Write this function called `Feed` and feed both `kitty` and `fiddo`

[Function overloads](https://www.typescriptlang.org/docs/handbook/2/functions.html#function-overloads)

## Exercise 4: Spread operator, object destructuring and rest parameters

This is a reasoning exercise. Open up the code in Ex4 and look at it together:

### The Spread Operator

- Observe the differences in output from line 5 and 8. This is the _spread operator_ in action.
- On line 18, note how we can use the spread operator to spread an object and replace one of its members.

### Object destructuring and rest parameters

The function declaration on line 20 features object destructuring and rest parameters.

- Try to reason about what's going on, given the output generated from line 24.

### Super advanced technology

- Given what you've leared above and your experiences with other programming languages, explain the `apply` function on line 26.
