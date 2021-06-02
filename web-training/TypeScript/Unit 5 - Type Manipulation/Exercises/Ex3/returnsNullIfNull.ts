function processString(input: string): string;
function processString(input: null): null;
function processString(input: string | null): string | null {
  return input != null ? `Hello ${input}!` : null;
}

console.log(processString("kitty").toUpperCase()); // safe
console.log(processString(null).toUpperCase()); // unsafe!
