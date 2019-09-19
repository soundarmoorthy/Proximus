using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Proximus
{
    public class RetryHandler : DelegatingHandler
    {
        // Strongly consider limiting the number of retries - "retry forever" is
        // probably not the most user friendly way you could respond to "the
        // network cable got pulled out."
        private const int MaxRetries = 3;

        Predicate<HttpResponseMessage> predicate;
        public RetryHandler(Predicate<HttpResponseMessage> predicate)
            : base(new HttpClientHandler())
        {
            this.predicate = predicate;
        }

        public TimeSpan DefaultTimeout { get; set; } = TimeSpan.FromSeconds(100);

        protected override async Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            HttpResponseMessage response = null;
            for (int i = 0; i < MaxRetries; i++)
            {
                using (var cts = GetCancellationTokenSource(request, cancellationToken))
                {
                    try
                    {
                        response = await base.SendAsync(request, cts?.Token ?? cancellationToken);
                        if (success(response))
                        {
                            return response;
                        }
                        else
                            throw new Exception(response.ReasonPhrase);

                    }
                    catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
                    {
                        Console.WriteLine("Timeout! Initiating Retry");
                        Thread.Sleep(3000);
                        continue;
                    }
                }
            }
            throw new Exception("Max retry exceeded for google maps!");
        }

        private CancellationTokenSource GetCancellationTokenSource(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            var timeout = request.GetTimeout() ?? DefaultTimeout;
            if (timeout == Timeout.InfiniteTimeSpan)
            {
                // No need to create a CTS if there's no timeout
                return null;
            }
            else
            {
                var cts = CancellationTokenSource
                    .CreateLinkedTokenSource(cancellationToken);
                cts.CancelAfter(timeout);
                return cts;
            }
        }

        private bool success(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode && symanticSuccess(response);
        }

        private bool symanticSuccess(HttpResponseMessage response)
        {
            if (predicate != null)
                return this.predicate(response);
            else
                return true;
        }
    }
}
