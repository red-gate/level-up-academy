const fetch = require("node-fetch");

exports.MathApi = function() {
    return {
        divide: async (num, den) => {
            const response = await fetch(`https://api.mathjs.org/v4/?expr=${num}/${den}`);
            return response.json();
        },
        
    }
} 

exports.Fraction = function(num, den, mathApi) {
    if (typeof(num) !== "number" || !Number.isInteger(num)) {
        throw new Error("Expected the numerator to be an integer number");
    }

    if (typeof(den) !== "number" || !Number.isInteger(den)) {
        throw new Error("Expected the denominator to be an integer number");
    }

    return {
    numerator: num,
    denominator: den,
    toNumber: async (onError) => {
        try
        {
            return await mathApi.divide(num, den);
        }
        catch (error)
        {
            if (onError) {
                onError(`Error when fetching ${num}/${den}: ${error}`);
            }
            return Number.NaN;
        }
    },    
    toString: () => {
        return `${this.numerator}/${this.denominator}`;
    }}
}