using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.User;
using MediatR;

namespace CleanArchitecture.Application.Auth.Commands.Register;

public record RegisterCommand : IRequest<AuthDto>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public string ConfirmPassword { get; init; } = null!;
    public int DepartmentId { get; init; }
}

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthDto>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    private readonly IUserAuthenticationService _userAuthenticationService;
    private readonly IMapper _mapper;

    public RegisterCommandHandler(IIdentityService identityService, IApplicationDbContext context, IUserAuthenticationService userAuthenticationService, IMapper mapper)
    {
        _identityService = identityService;
        _context = context;
        _userAuthenticationService = userAuthenticationService;
        _mapper = mapper;
    }

    public async Task<AuthDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
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

        var retUser = _mapper.Map<ApplicationUserDto>(entity);

        return !await _userAuthenticationService.ValidateUserAsync(request.UserName, request.Password)
            ? throw new NotFoundException(request.UserName)
            : new AuthDto { Token = await _userAuthenticationService.CreateTokenAsync(), User = retUser };
    }
}
