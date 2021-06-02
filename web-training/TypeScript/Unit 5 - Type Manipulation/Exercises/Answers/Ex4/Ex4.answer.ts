
type Animal = {
    type: "Dog",
    name: string,
    goodBoy: boolean
  } | {
    type: "Cat",
    name: string,
    numberOfLives: number
  }

type Dog = Animal & {type: "Dog"}
function PrintDog(dog: Dog){
    console.log(`${dog.name} ${dog.goodBoy ? "is" : "is not"} a good boy`);
}

const fiddo = {
    name: "Fiddo",
    goodBoy: true 
} as Dog

PrintDog(fiddo);

/*
interface ITame {
    ownerName: string;
}

type TameCat = ICat & ITame
type TameAnimal = ITame & {name: string}

function PrintTameCat(cat: TameCat){
    console.log(`${cat.name} is a cat owned by ${cat.ownerName}`);
}

function PrintTameAnimal(tameAnimal: TameAnimal){
    console.log(`${tameAnimal.name} is an animal owned by ${tameAnimal.ownerName}`);
}

// Doesn't rely on an external types or interfaces so wouldn't be affected by changing
// requirements to these (For example, adding registered address to ITame)
function PrintTameAnimal1(tameAnimal: {name: string, ownerName: string}){
    console.log(`${tameAnimal.name} is an animal owned by ${tameAnimal.ownerName}`);
}

var kitty = {
    name: "kitty",
    numberOfLives: 9,
    ownerName: 'Piers'
};

// PrintTameCat(kitty);

var puppy = {
    name: "Lexi",
    goodBoy: false,
    ownerName: 'Piers'
};

// PrintTameCat(puppy);
PrintTameAnimal(puppy);
PrintTameAnimal(kitty);
*/
