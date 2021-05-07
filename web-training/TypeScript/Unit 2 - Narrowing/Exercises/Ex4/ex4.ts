interface Cat {
    name: string;
    numberOfLives: number;
}

class Dog {
    name: string;
    isAGoodBoy: boolean;

    constructor(name: string, isAGoodBoy: boolean) {
        this.name = name;
        this.isAGoodBoy = isAGoodBoy;
    }
}

interface TeddyBear {
    name: string;
    isStuffed: true;
}

type Pet = Cat | Dog | TeddyBear;

function FeedCat(cat: Cat) {
    console.log(`${cat.name} was given cat food`);
}

function FeedDog(dog: Dog) {
    console.log(`${dog.name} was given dog food`);
}

function Feed(pet: Pet) {
    FeedCat(pet);
    FeedDog(pet);
}

Feed({
    name: "Fluffy",
    numberOfLives: 7
});

Feed({
    name: "Buddy",
    isAGoodBoy: true
});

Feed({
    name: "Baloo",
    isStuffed: true
});