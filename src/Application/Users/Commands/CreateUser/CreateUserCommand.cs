using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.User;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.CreateUser;

public record CreateUserCommand : IRequest<ApplicationUserDto>
{
    public string UserName { get; init; } = null!;
    public string Password { get; init; } = null!;
    public int DepartmentId { get; init; }
}

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, ApplicationUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IIdentityService identityService, IApplicationDbContext context, IMapper mapper)
    {
        _identityService = identityService;
        _context = context;
        _mapper = mapper;
    }

    public async Task<ApplicationUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
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

        var ret = _mapper.Map<ApplicationUserDto>(entity);

        return ret;
    }
}
