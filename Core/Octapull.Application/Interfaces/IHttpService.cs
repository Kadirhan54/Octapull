namespace Octapull.Application.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
