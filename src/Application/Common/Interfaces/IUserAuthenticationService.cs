using System.Security.Claims;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Interfaces;

public interface IUserAuthenticationService
{
    Task<bool> ValidateUserAsync(string userName, string password);

    Task<string> CreateTokenAsync();
}
