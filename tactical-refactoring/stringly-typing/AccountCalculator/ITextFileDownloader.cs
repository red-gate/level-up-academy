using System.Threading;
using System.Threading.Tasks;

namespace AccountCalculator
{
    public interface ITextFileDownloader
    {
        public Task<string> Download(string uri, CancellationToken cancellationToken);
    }
}
