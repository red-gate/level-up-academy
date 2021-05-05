// interface Cat {
//     name: string;
//     numberOfLives: number;
// }

// class Dog {
//     name: string;
//     isAGoodBoy: boolean;

//     constructor(name: string, isAGoodBoy: boolean) {
//         this.name = name;
//         this.isAGoodBoy = isAGoodBoy;
//     }
// }

// interface TeddyBear {
//     name: string;
//     isStuffed: true;
// }

// type Pet = Cat | Dog | TeddyBear;

// function FeedCat(cat: Cat) {
//     console.log(`${cat.name} was given cat food`);
// }

// function FeedDog(dog: Dog) {
//     console.log(`${dog.name} was given dog food`);
// }

// function isCat(pet: Pet): pet is Cat {
//     return typeof (pet as Cat).numberOfLives === "number"; 
// }

// function Feed(pet: Pet) {
//     if (pet instanceof Dog) {
//         FeedDog(pet);
//     }
//     else if (isCat(pet)) {
//         FeedCat(pet);
//     };    
// }

// Feed({
//     name: "Fluffy",
//     numberOfLives: 7
// });

// Feed(new Dog("Rex", true));

// Feed({
//     name: "Buddy",
//     isAGoodBoy: false
// });

// Feed({
//     name: "Baloo",
//     isStuffed: true
// });