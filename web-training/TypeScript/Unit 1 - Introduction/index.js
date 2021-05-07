"use strict";
exports.__esModule = true;
var fraction_1 = require("./Examples/fraction");
var MathApi = /** @class */ (function () {
    function MathApi() {
    }
    MathApi.prototype.divide = function (num, den) {
        return num / den;
    };
    return MathApi;
}());
var f = new fraction_1.Fraction(3, 4, { divide: function (num, den) {
        return num / den;
    } });
console.log(f.toNumber(console.log));
