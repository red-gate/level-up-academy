const fraction = require ('./ex1.js');

describe('Fraction', function () {
  test.each([[1, 2, 0.5]])('Fraction(%i, %i).toNumber()', 
    (num, den, expected) => {
      expect (new fraction.Fraction(num, den).toNumber()).toBe(expected);
    })

  // Add more test cases for toNumber() test
  // Add a new test for toString() with several test cases  
});
