const list = ["Piers", "Sami", "Mark", "Alex"];

function FindByFirstLetter(letter: string) {
    return list.find(name => name[0] == letter);
}

function Greet(name: string) {
    console.log(`Hello ${name}!`);
}

Greet(FindByFirstLetter("P"));