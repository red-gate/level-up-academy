const fraction = require ('./ex2.js');

describe('Fraction', function () { 
  test.each([[1, 2, 0.5], [2, 1, 2]])('Fraction(%i, %i).toNumber()', 
    (num, den, expected) => {
      expect (new fraction.Fraction(num, den).toNumber()).toBe(expected);
    })

  test.each([[1, 2, "1/2"], [2, 1, "2/1"]])('Fraction(%i, %i).toString()', 
    (num, den, expected) => {
      expect (new fraction.Fraction(num, den).toString()).toBe(expected);
    })
});
