using Octapull.Domain.Identity;

namespace Octapull.Application.Abstractions
{
    public interface ITokenService
    {
        public string CreateToken(User user);
    }
}
