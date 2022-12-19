using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using Azure.Core;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchitecture.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IUserClaimsPrincipalFactory<ApplicationUser> _userClaimsPrincipalFactory;
    private readonly IAuthorizationService _authorizationService;
    private readonly IApplicationDbContext _context;

    public IdentityService(
        UserManager<ApplicationUser> userManager,
        IUserClaimsPrincipalFactory<ApplicationUser> userClaimsPrincipalFactory,
        IAuthorizationService authorizationService,
        IApplicationDbContext context)
    {
        _userManager = userManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;
        _authorizationService = authorizationService;
        _context = context;
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

        return user.UserName;
    }

    public async Task<ApplicationUser> GetUserAsync(string userId)
    {
        var user = await _userManager.Users.Include(x => x.Results).Include(x => x.Department).FirstAsync(u => u.Id == userId);

        return user;
    }

    public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
    {
        var user = await _userManager.Users.Include(x => x.Results).Include(x => x.Department).FirstAsync(u => u.UserName == userName);

        return user;
    }

    public async Task<List<ApplicationUser>> GetAllUsersAsync()
    {
        var users = await _userManager.Users.Include(x => x.Results).Include(x => x.Department).ToListAsync();

        List<ApplicationUser> ret = new List<ApplicationUser>();
        ret.AddRange(users);

        return ret;
    }

    public async Task<List<ApplicationUser>> GetAllUsersByDepartmentAsync(int departmentId)
    {
        var users = await GetAllUsersAsync();

        List<ApplicationUser> ret = new List<ApplicationUser>();
        if (users.Any())
        {
            foreach(var item in users)
            {
                if(item.DepartmentId == departmentId)
                {
                    ret.Add(item);
                }
            }
        }

        return ret;
    }

    public async Task<List<Domain.Entities.Result>> GetUserResults(string userId)
    {
        var user = await _userManager.Users.Include(x => x.Results).Include(x => x.Department).FirstAsync(u => u.Id == userId);

        var ret = new List<Domain.Entities.Result>();
        foreach(var item in user.Results)
        {
            ret.Add(item);
        }
        return ret;
    }

    public async Task<(Application.Common.Models.Result Result, string UserId)> CreateUserAsync(string userName, string password, int departmentId)
    {
        var user = new ApplicationUser
        {
            UserName = userName
        };

        var entity = await _context.Departments
            .FindAsync(new object[] { departmentId });

        if (entity == null)
        {
            throw new NotFoundException(nameof(Department), departmentId);
        }
        else
        {
            user.Department = entity;
        }

        var result = await _userManager.CreateAsync(user, password);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<bool> IsInRoleAsync(string userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> AuthorizeAsync(string userId, string policyName)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        if (user == null)
        {
            return false;
        }

        var principal = await _userClaimsPrincipalFactory.CreateAsync(user);

        var result = await _authorizationService.AuthorizeAsync(principal, policyName);

        return result.Succeeded;
    }

    public async Task<Application.Common.Models.Result> DeleteUserAsync(string userId)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null ? await DeleteUserAsync(user) : Application.Common.Models.Result.Success();
    }

    public async Task<Application.Common.Models.Result> DeleteUserAsync(ApplicationUser user)
    {
        var result = await _userManager.DeleteAsync(user);

        return result.ToApplicationResult();
    }

    public async Task<(Application.Common.Models.Result Result, string UserId)> EditUserAsync(string id, string userName, string password, int departmentId)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), id);
        }
        else
        {
            user.Id = id;
            user.UserName = userName;
            user.DepartmentId = departmentId;
        }

        user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, password);

        var result = await _userManager.UpdateAsync(user);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Application.Common.Models.Result Result, string UserId)> DeleteUserDepartmentAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), userId);
        }
        else
        {
            user.Department = null;
        }

        var result = await _userManager.UpdateAsync(user);

        return (result.ToApplicationResult(), user.Id);
    }

    public async Task<(Application.Common.Models.Result Result, string UserId)> AddUserResultAsync(ApplicationUser user, Domain.Entities.Result resultModel)
    {
        var entity = await _userManager.FindByIdAsync(user.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), user.Id);
        }

        entity.Results.Add(resultModel);

        var result = await _userManager.UpdateAsync(entity);

        return (result.ToApplicationResult(), entity.Id);
    }
}
