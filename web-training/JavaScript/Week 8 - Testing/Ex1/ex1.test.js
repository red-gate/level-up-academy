const fraction = require ('./ex1.js');

describe('Fraction', function () {

  test('toNumber() works for 1/2', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a.toNumber()).toBe(0.5);  
  });

  // Add a single toNumber() test for 1/3
  // Add a single toString() test that asserts the ToString of a fraction is in fractional form
  // Test that that constructed fraction has correct properties ({ numerator, denominator })
  // Test that the fraction constructors throws an exception when a non-numeric parameter is passed to it
});
