"use strict";
exports.__esModule = true;
exports.Fraction = void 0;
var Fraction = /** @class */ (function () {
    function Fraction(num, den, mathApi) {
        this.numerator = num;
        this.denominator = den;
        this.mathApi = mathApi;
    }
    Fraction.prototype.toNumber = function (onError) {
        try {
            return this.mathApi.divide(this.numerator, this.denominator);
        }
        catch (error) {
            if (onError) {
                onError("Error when fetching " + this.numerator + "/" + this.denominator + ": " + error);
            }
            return Number.NaN;
        }
    };
    Fraction.prototype.toString = function () {
        return this.numerator + "/" + this.denominator;
    };
    return Fraction;
}());
exports.Fraction = Fraction;
