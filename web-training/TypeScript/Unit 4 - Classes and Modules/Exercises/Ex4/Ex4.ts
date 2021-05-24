class Dog {
    constructor(public name: string, public goodBoy: boolean, private ownerName: string){}

    wag(){
        console.log(`${this.name} wags its tail for ${this.ownerName}`);
    }
}


const fiddo = new Dog("Fiddo", true, "piers");
console.log(fiddo.name);