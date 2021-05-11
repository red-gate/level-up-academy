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

- Excercise 3 (Piers):
- - Optional parameters
- - Overloads
- Excercise 4: Destructuring (Sami)
