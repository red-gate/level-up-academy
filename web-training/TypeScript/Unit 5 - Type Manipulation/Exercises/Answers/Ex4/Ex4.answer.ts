
interface IDog {
    name: string;
    goodBoy: boolean;
}

function PrintDog(dog: IDog){
    console.log(`${dog.name} ${dog.goodBoy ? "is" : "is not"} a good boy`);
}

const fiddo = {
    name: "Fiddo",
    goodBoy: true 
}

PrintDog(fiddo);

interface ICat {
    name: string;
    numberOfLives: number;
}

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

function PrintTameAnimal(tameAnimal: {name: string, ownerName: string}){
    console.log(`${tameAnimal.name} is an animal owned by ${tameAnimal.ownerName}`);
}

var kitty = {
    name: "kitty",
    numberOfLives: 9,
    ownerName: 'Piers'
};

PrintTameCat(kitty);

var puppy = {
    name: "Lexi",
    goodBoy: false,
    ownerName: 'Piers'
};

PrintTameCat(puppy);
PrintTameAnimal(puppy);
PrintTameAnimal(kitty);
