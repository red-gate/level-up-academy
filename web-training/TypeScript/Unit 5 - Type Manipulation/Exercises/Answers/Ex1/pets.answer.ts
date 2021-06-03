export interface Pet {
    name: string;
    owner: string;
}

export class Dog implements Pet {
    name: string;
    owner: string;
    goodBoy: boolean;

    constructor (name: string, owner: string, goodBoy: boolean) {
        this.name = name;
        this.owner = owner;
        this.goodBoy = goodBoy;
    }
}

export class Cat implements Pet {
    name: string;
    owner: string;
    numberOfLives: number;

    constructor (name: string, owner: string, numberOfLives: number) {
        this.name = name;
        this.owner = owner;
        this.numberOfLives = numberOfLives;
    }
}