using Octapull.Application.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Octapull.Persistence.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            try
            {
                // Send the HTTP request and return the response
                return await _httpClient.SendAsync(request, cancellationToken);
            }
            catch (Exception ex)
            {
                // Handle any exceptions (e.g., network errors)
                // You might want to log the exception or handle it differently based on your requirements
                throw new HttpRequestException("An error occurred while sending the HTTP request.", ex);
            }
        }
    }
}
