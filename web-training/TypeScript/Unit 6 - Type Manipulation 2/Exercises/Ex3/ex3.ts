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