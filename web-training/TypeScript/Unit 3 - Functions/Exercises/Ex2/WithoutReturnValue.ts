type FarmAnimal =
  | { type: "cow"; milk: () => void }
  | { type: "chicken"; collectEggs: () => void }
  | { type: "angoranRabbit"; collectWool: () => void };

function processAnimal(animal: FarmAnimal) {
  switch (animal.type) {
    case "chicken":
      animal.collectEggs();
      break;
    case "cow":
      animal.milk();
      break;
  }
}

export function sixAmRoutine(animals: FarmAnimal[]) {
  animals.forEach((a) => processAnimal(a));
}

function assertUnreachable(x: never): never {
  throw "this line shouldn't ever be reached!";
}
