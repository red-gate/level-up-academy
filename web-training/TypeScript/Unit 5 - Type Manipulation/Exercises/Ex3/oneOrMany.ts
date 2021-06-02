function isEven(n: number): boolean;
function isEven(n: number[]): boolean[];
function isEven(n: number | number[]): boolean | boolean[] {
  if (Array.isArray(n)) {
    return n.map((x) => isEven(x));
  }
  return n % 2 === 0;
}

type ArrayOrSingle<T1> = T1 extends any[] ? boolean[] : boolean;

function isEven2<T extends number | number[]>(n: T): ArrayOrSingle<T> {
  if (Array.isArray(n)) {
    // HACK: this cast is required because of a typescript limitation: https://github.com/microsoft/TypeScript/issues/33912
    return n.map((x) => x % 2 === 0) as ArrayOrSingle<T>;
  }

  // TODO: why is this cast necessary?
  return ((n as number) % 2 === 0) as ArrayOrSingle<T>;
}

// check the inferred types of `isEven` below - they should be `boolean` or `boolean[]`
// as appropriate, but not both at the same time

console.log(isEven(3));
console.log(isEven([1, 2, 3]));

console.log(isEven2(3));
console.log(isEven2([1, 2, 3]));
