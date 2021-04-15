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