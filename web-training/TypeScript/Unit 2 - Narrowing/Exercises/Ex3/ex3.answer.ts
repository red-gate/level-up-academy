// const list = ["Piers", "Sami", "Mark", "Alex"];

// function FindByFirstLetter(letter: string) {
//     return list.find(name => name[0] == letter);
// }

// function Greet(name: string) {
//     console.log(`Hello ${name}!`);
// }

// const nameToGreet = FindByFirstLetter("P");

// if (nameToGreet) {
//     Greet(nameToGreet);
// }

// Alternately, we could change FindByFirstLetter to throw if a match is not found:
// function FindByFirstLetter(letter: string) {
//     const result = list.find(name => name[0] == letter);
//
//     // At this point `result` has type `string | undefined`
//     if (result === undefined) {
//         throw new Error(`Couldn't find a name starting with the letter ${letter}!`);
//     }
//
//     // Since this code is unreachable if the result was undefined, the type of `result` is now just `string`
//     return result;
// }
