using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AccountCalculator
{
    public class TextFileDownloader : ITextFileDownloader
    {
        private static readonly HttpClient HttpClient = new ();

        public async Task<string> Download(Uri uri, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var responseMessage = await HttpClient.GetAsync(uri, HttpCompletionOption.ResponseContentRead, cancellationToken);

            cancellationToken.ThrowIfCancellationRequested();
            return await responseMessage.Content.ReadAsStringAsync(cancellationToken);
        }
    }
}