using System.Collections.Generic;
using System.Threading.Tasks;
using AccountCalculator.Domain;

namespace AccountCalculator
{
    public interface IPurchasesReader
    {
        Task<IEnumerable<Purchase>> ReadPurchases();
    }
}
