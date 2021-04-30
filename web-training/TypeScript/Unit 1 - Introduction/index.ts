import {IMathApi, Fraction} from "./Examples/fraction"
class MathApi implements IMathApi {
    divide(num: number, den: number): number {
        return num / den;
    }
}

let f = new Fraction(3, 4, { divide: (num, den) => {
    return num/den;
}});
console.log(f.toNumber(console.log));