const fraction = require ('./ex3.js');

describe('Fraction', function () {
  test('toNumber() works for 1/2 with mocks', async () => {
    const expectedValue = 0.5;

    const onErrorMock = jest.fn();

    const mockApi = {
      divide: jest.fn((a, b) => expectedValue)
    }   

    const a = new fraction.Fraction(1, 2, mockApi);
    expect(await a.toNumber(onErrorMock)).toBe(expectedValue);
    expect(mockApi.divide).toHaveBeenCalledTimes(1);
    expect(mockApi.divide.mock.calls[0][0]).toBe(1);
    expect(mockApi.divide.mock.calls[0][1]).toBe(2);
    expect(onErrorMock).toHaveBeenCalledTimes(0);
  });

  test('toNumber() calls error callbacks', async () => {
    const mockApi = {
      divide: () => { throw new Error("Error"); }
    }

    const onErrorMock = jest.fn();

    const a = new fraction.Fraction(1, 2, mockApi);
    expect(await a.toNumber(onErrorMock)).toBe(Number.NaN);
    expect(onErrorMock).toHaveBeenCalledTimes(1);
  });
});