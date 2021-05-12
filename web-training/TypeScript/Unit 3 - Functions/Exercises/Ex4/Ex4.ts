function apply<T>(fn: (value: T) => void, ...values: T[]) {
    values.forEach(v => fn(v));
}

apply(console.log, "I", "like", "candies");

// Given an array
const question = ["To", "be", "or", "not", "to", "be"]

// Print each individual value
console.log(...question)

// Print the whole array
console.log(question)

// Given an object
const snackOptions = {
    lemons: 'sour',
    melons: 'sweet',
    crisps: 'savoury',
}

// When life gives you lemons, make lemonade
console.log({...snackOptions, lemons: 'lemonade' });

function onlyInterestedInFruits({ lemons, melons } : { lemons: string, melons: string }, ...similarities: string[]) {
    console.log(`Melons are ${melons}, lemons are ${lemons}; both ${similarities.join(" and ")}!`);
}

console.log(onlyInterestedInFruits(snackOptions, "can be yellow", "are oval"));