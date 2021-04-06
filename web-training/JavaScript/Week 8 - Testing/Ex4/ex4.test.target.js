const fraction = require ('./ex4.js');
const fetch = require("node-fetch");

jest.mock('node-fetch');

describe('Fraction', function () {
  test('toNumber() works for 1/2 with mocks', async () => {
    const resp = {json: () => 0.5};
    fetch.mockResolvedValue(resp);

    const a = new fraction.Fraction(1, 2);
    expect(await a.toNumber()).toBe(0.5);
    expect(fetch).toHaveBeenCalledTimes(3);
  });
});