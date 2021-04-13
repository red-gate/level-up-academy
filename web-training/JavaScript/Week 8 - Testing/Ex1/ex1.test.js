const fraction = require ('./ex1.js');

describe('Fraction', function () {

  test('toNumber() works for 1/2', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a.toNumber()).toBe(0.5);  
  });

  // Add a single toNumber() test for 1/3
  test('toNumber() works for 1/3', () => {
    const a = new fraction.Fraction(1, 3);
    expect(a.toNumber()).toBeCloseTo(0.33333333, 8);
  });
  
  // Add a single toString() test that asserts the ToString of a fraction is in fractional form
  test('toString() works for 1/2', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a.toString()).toBe("1/2");
  });
  
  // Test that that constructed fraction has correct properties ({ numerator, denominator })
  test('numerator and denominator are as expected', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a).toMatchObject({numerator:1, denominator:2});
  });
  
  // Test that the fraction constructors throws an exception when a non-numeric parameter is passed to it
  describe ('when passed invalid input', () => {
      test('throws when numerator is not a number', () => {
        expect(() => new fraction.Fraction('invalid', 2)).toThrow("Expected the numerator to be an integer number");
      });
      test('throws when denominator is not a number', () => {
        expect(() => new fraction.Fraction(1, 'invalid')).toThrow("Expected the denominator to be an integer number");
      });
  });
});
