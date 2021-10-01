using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AccountCalculator
{
    public interface IJsonDownloader
    {
        public Task<string> Download(string uri, CancellationToken cancellationToken);
    }

    public class JsonDownloader : IJsonDownloader
    {
        private static readonly HttpClient HttpClient = new ();

        public async Task<string> Download(string uri, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var responseMessage = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseContentRead, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            return await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}
