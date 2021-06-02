type IsNullIfNull<T> = T extends null ? null : T;

function processString<T extends string | null>(input: T): IsNullIfNull<T> {
  // HACK: this cast is required because of a typescript limitation: https://github.com/microsoft/TypeScript/issues/33912
  return (input != null ? `Hello ${input}!` : null) as IsNullIfNull<T>;
}

console.log(processString("kitty").toUpperCase()); // safe
console.log(processString(null).toUpperCase()); // unsafe!
