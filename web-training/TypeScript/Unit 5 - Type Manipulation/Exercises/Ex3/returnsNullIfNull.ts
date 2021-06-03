// A basic version of processString always returns `string | null`
function processString(input: string | null): string | null {
  return input != null ? `Hello ${input}!` : null;
}

// It's not inaccurate, but it is annoying in cases where we know the result:

console.log(processString("kitty").toUpperCase()); // this should be safe
console.log(processString(null).toUpperCase()); // this is unsafe!

// As we've seen before, we can improve the type of `processString` using
// specific function overloads for the null and not - null cases.
// Try adding these overloads to the above function:
//
//     function processString(input: string): string;
//     function processString(input: null): null;
//

// That works, but is a bit verbose. How can we use conditional types on the
// return value as a more generic solution ?
// Hint: try defining a `IsNullIfNull<T>` type which is null if `T` is null,
// and `T` otherwise.
