using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IIdentityService
{
    Task<string> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Models.Result Result, string UserId)> CreateUserAsync(string userName, string password, int departmentId);

    Task<Models.Result> DeleteUserAsync(string userId);

    Task<List<Domain.Entities.Result>> GetUserResults(string userId);

    Task<ApplicationUser> GetUserAsync(string userId);

    Task<List<ApplicationUser>> GetAllUsersAsync();
}
