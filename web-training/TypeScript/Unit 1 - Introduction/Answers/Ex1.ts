function Greet(name: string, n: number): string {
    return `Hello ${`${name} `.repeat(n)}`
}

console.log(Greet("Piers", 5));

function Fibonacci(n: number): number {
    if(n < 2){
        return n;
    }
    return Fibonacci(n - 1) + Fibonacci(n - 2);
}

console.log(Fibonacci(7));

function Fibonnaci(index: number, previousValues?: number[]): number {
    if(index === 0) return 0;
    if(index === 1) return 1;
    if(!previousValues){
        previousValues = Array(index + 1).fill(-1);
    }
    console.log(previousValues);
    if(previousValues[index] !== -1){
        console.log("Found old value" + previousValues[index]);
        return previousValues[index];
    }
    var num = Fibonnaci(index - 1, previousValues) + Fibonnaci(index - 2, previousValues);
    previousValues[index] = num;
    return num;
}

console.log(Fibonnaci(8));