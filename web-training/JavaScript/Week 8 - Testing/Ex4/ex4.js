const fetch = require("node-fetch");

exports.Fraction = function(num, den) {
    if (typeof(num) !== "number" || !Number.isInteger(num)) {
        throw new Error("Expected the numerator to be an integer number");
    }

    if (typeof(den) !== "number" || !Number.isInteger(den)) {
        throw new Error("Expected the denominator to be an integer number");
    }

    return {
    numerator: num,
    denominator: den,
    toNumber: async () => {
        const response = await fetch(`https://api.mathjs.org/v4/?expr=${num}/${den}`);
        return response.json();
    },    
    toString: () => {
        return `${this.numerator}/${this.denominator}`;
    }}
}