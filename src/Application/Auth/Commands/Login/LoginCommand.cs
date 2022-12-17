﻿using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.User;
using MediatR;

namespace CleanArchitecture.Application.Auth.Commands.Login;

public record LoginCommand : IRequest<string>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public int DepartmentId { get; init; }
}

public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;

    public LoginCommandHandler(IIdentityService identityService, IApplicationDbContext context)
    {
        _identityService = identityService;
        _context = context;
    }

    public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var departmentEntity = await _context.Departments
            .FindAsync(new object[] { request.DepartmentId }, cancellationToken);

        if (departmentEntity == null)
        {
            throw new NotFoundException(nameof(Department), request.DepartmentId);
        }

        var result = await _identityService.CreateUserAsync(request.UserName, request.Password, request.DepartmentId);

        var entity = await _identityService.GetUserAsync(result.UserId);

        entity.AddDomainEvent(new UserCreatedEvent(entity));

        return result.UserId;
    }
}
