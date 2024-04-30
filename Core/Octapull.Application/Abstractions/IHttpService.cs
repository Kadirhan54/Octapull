namespace Octapull.Application.Abstractions
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> SendAsync(HttpResponseMessage request, CancellationToken cancellationToken);
    }
}
