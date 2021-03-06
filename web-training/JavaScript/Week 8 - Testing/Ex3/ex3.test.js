const ex3 = require ('./ex3.js');

describe('Fraction', function () {

  test('toNumber() works for 1/2', async () => {
    const onError = (error) => console.log(error);    
    const a = new ex3.Fraction(1, 2, new ex3.MathApi());
    expect(await a.toNumber(onError)).toBe(0.5); 
  });

  // Try changing this test so that it mocks out the fetch call
  // Add an assertion to check that your mock was called
});
