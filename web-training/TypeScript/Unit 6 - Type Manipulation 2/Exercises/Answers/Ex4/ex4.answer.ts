class Animal {
    constructor(public name: string){}
}

class Cat extends Animal {
    constructor(public name: string, public numberOfLives: number){
        super(name);
    }
}

class Dog extends Animal {
    constructor(public name: string, public goodBoy: boolean){
        super(name);
    }
}

class Platypus {

}

const beast = new Cat("Meow", 9); // Must be its name, what it said when I asked it nicely

/*
If you define a constructor function in a mixin class, it must have a single rest parameter of type any[].
The reason for this is that the mixin should not be tied to a specific class with known constructor parameters;
therefore the mixin should accept an arbitrary number of arbitrary values as constructor parameters.
All of the parameters are passed to the constructor of Base, and then the mixin does its thing.
In our case, it initializes the tag property.
*/
// [From](https://mariusschulz.com/blog/mixin-classes-in-typescript)
type AnimalConstructor<T = Animal> = new (...args: any[]) => T;

function Own<TBase extends AnimalConstructor>(Base: TBase) {
    return class Owned extends Base {
        _ownerName = "";

        setOwner(owner: string){
            this._ownerName = owner;
        }

        get owner(): string {
            return this._ownerName;
        }
    }
}

function Trainable<TBase extends AnimalConstructor>(Base: TBase) {
    return class Traned extends Base {
        _trained = false;
        train() {
            this._trained = true;
        }
        get trained(): boolean {
            return this._trained;
        }
    }
}

const OwnedCat = Own(Cat);
const OwnedDog = Own(Dog);
const OwnedPlatypus = Own(Platypus); // Can't own a platypus!

const felix = new OwnedCat("Felix", 9);
felix.setOwner("Piers");


const TrainedCat = Trainable(Cat);
const TrainedOwnedCat = Trainable(Own(Cat));

const oscar = new TrainedOwnedCat("oscar", 9);
oscar.setOwner("Sami");
oscar.train();
