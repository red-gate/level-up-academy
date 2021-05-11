# Unit 3 - Functions

## The Handbook
https://www.typescriptlang.org/docs/handbook/2/functions.html

Functions

- Excercise 1: Function types (Alex)
- - Passing functions as variables
- Excercise 2: Return types (Mark)
- - inferrence, void vs. never and exhaustive switch checking:
- - With return types (simple case):

```ts
type Product = {};
type FarmAnimal =
  | {
      type: "cow";
      milk: () => Product;
    }
  | {
      type: "chicken";
      collectEggs: () => Product;
    };

function sixAmRoutine(animal: FarmAnimal): Product {
  switch (animal.type) {
    case "chicken":
      return animal.collectEggs();
    case "cow":
      return animal.milk();
  }
}
```

- - Without return types, using the `never` type:

```ts
type FarmAnimal =
  | {
      type: "cow";
      milk: () => void;
    }
  | {
      type: "chicken";
      collectEggs: () => void;
    };

function sixAmRoutine(animal: FarmAnimal) {
  switch (animal.type) {
    case "chicken":
      animal.collectEggs();
      break;
    case "cow":
      animal.milk();
      break;
    default:
      assertUnreachable(animal);
  }
}

function assertUnreachable(x: never): never {
  throw "this line shouldn't ever be reached!";
}
```

## Excercise 3: Optional and Overloads

### Part 1
Read Ex3.ts and explain what is contained within

### Part 2
Write a function called `FeedCat` that takes as argument a single `Cat` and optionally a single `CatFood`.
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
- food that is either `CatFood` or `DogFood`
Can you force feed kitty with dogfood now?

### Part 5
What if we want a single implementation that causes only cats to be fed catfood and dogs to be fed dogfood? Write this function called `Feed` and feed both `kitty` and `fiddo`

- Excercise 4: Destructuring (Sami)
