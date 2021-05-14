type Product = {};
type ShippingBin = { deposit(products: Product[]): void };
type FarmAnimal =
  | { type: "cow"; milk: () => Product }
  | { type: "chicken"; collectEggs: () => Product }
  | { type: "angoranRabbit"; collectWool: () => Product };

function getProductFromAnimal(animal: FarmAnimal): Product {
  switch (animal.type) {
    case "chicken":
      return animal.collectEggs();
    case "cow":
      return animal.milk();
    case "angoranRabbit":
      return animal.collectWool();
  }
}

export function sixAmRoutine(animals: FarmAnimal[], shippingBin: ShippingBin) {
  const products = animals.map((a) => getProductFromAnimal(a));
  shippingBin.deposit(products);
}
