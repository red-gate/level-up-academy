exports.Fraction = function(num, den) {
    if (typeof(num) !== "number" || !Number.isInteger(num)) {
        throw new Error("Expected the numerator to be an integer number");
    }

    if (typeof(den) !== "number" || !Number.isInteger(den)) {
        throw new Error("Expected the denominator to be an integer number");
    }

    this.numerator = num;
    this.denominator = den;

    this.toNumber = () => {
        return this.numerator / this.denominator;
    }

    this.toString = () => {
        return `${this.numerator}/${this.denominator}`;
    }
}