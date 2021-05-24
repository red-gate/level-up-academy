class Dog {
    constructor(public name: string, public goodBoy: boolean, private ownerName: string){}

    wag(){
        console.log(`${this.name} wags its tail for ${this.ownerName}`);
    }
}


interface IDog {
    name: string;
    goodBoy: boolean;
}


const fiddo = new Dog("Fiddo", true, "piers");
console.log(fiddo.name);

const iFiddo = {name: "iFiddo", goodBoy: true};
console.log(iFiddo.name);


type AllDogs = IDog | Dog;
function printDogName(dog: AllDogs){
    console.log(dog.name);
}

printDogName(fiddo);
printDogName(iFiddo);

fiddo.wag();

class PizzaDog {
    constructor(public name: string, public goodBoy: boolean, private ownerName: string){}
}
const pizza = new PizzaDog("pizza", true, "piers");

console.log(pizza instanceof PizzaDog);
console.log(pizza instanceof Dog);