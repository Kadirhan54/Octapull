using Octapull.Application.Dtos.Account.Request;
using Octapull.Application.Dtos.Account.User;
using Octapull.Application.Models;
using Octapull.Domain.Identity;
using System.Security.Claims;

namespace Octapull.Application.Interfaces
{
    public interface IIdentityService
    {
        Task<string?> GetUserNameAsync(string userId);

        //Task<ApplicationUser?> GetCurrentUserAsync(ClaimsPrincipal claimsPrincipal);

        Task<ApplicationUser?> GetUserByIdAsync(string userId);

        Task<ApplicationUser?> GetUserByUserNameAsync(string userName);

        Task<ApplicationUser?> GetUserByEmailAsync(string email);

        Task<bool> IsInRoleAsync(string userId, string role);

        Task<bool> AuthorizeAsync(string userId, string policyName);

        Task<bool> SignInUser(ApplicationUser user, string password);

        Task<(Result Result, string UserId)> CreateUserAsync(CreateUserRequestDto createUserRequestDto);

        Task<Result> DeleteUserAsync(string userId);
    }
}
