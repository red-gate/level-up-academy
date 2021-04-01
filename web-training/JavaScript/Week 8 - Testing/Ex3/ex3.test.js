const fraction = require ('./ex3.js');

describe('Fraction', function () {

  test('toNumber() works for 1/2', async () => {
    const a = new fraction.Fraction(1, 2);
    expect(await a.toNumber()).toBe(0.5);  
  });

  // Try changing this test so that it mocks out the fetch call
  // Add an assertion to check that your mock was called
});
