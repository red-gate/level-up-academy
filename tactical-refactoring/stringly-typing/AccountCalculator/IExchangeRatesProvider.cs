using System.Collections.Generic;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public interface IExchangeRatesProvider
    {
        Task<IEnumerable<ExchangeRateRecord>> GetExchangeRates();
    }
}
