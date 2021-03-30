const fraction = require ('./ex1.js');

describe('Fraction', function () {
  test('toNumber() works for 1/2', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a.toNumber()).toBe(0.5);   
  });

  test('toNumber() works for 1/3', () => {
    const a = new fraction.Fraction(1, 3);
    expect(a.toNumber()).toBeCloseTo(0.33333, 5);   
  });

  test('toString() works for 1/2', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a.toString()).toBe("1/2");
  });
  
  test('initializes numerator and denominator correclty', () => {
    const a = new fraction.Fraction(1, 2);
    expect(a).toMatchObject({numerator: 1, denominator: 2});
  });

  test('throws when initiliazed with non-numeric ', () => {
    const a = new fraction.Fraction(1, 2);
    expect(() => new fraction.Fraction("hello", "world")).toThrow("Expected the numerator to be an integer number");
  });
});
