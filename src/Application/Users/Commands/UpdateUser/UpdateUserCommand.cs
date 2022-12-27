using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.User;
using MediatR;

namespace CleanArchitecture.Application.Users.Commands.UpdateUser;

public record UpdateUserCommand : IRequest<ApplicationUserDto>
{
    public string Id { get; init; } = null!;

    public string UserName { get; init; } = null!;

    public string Password { get; init; } = null!;

    public int DepartmentId { get; init; }
}

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, ApplicationUserDto>
{
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UpdateUserCommandHandler(IApplicationDbContext context, IIdentityService identityService, IMapper mapper)
    {
        _context = context;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<ApplicationUserDto> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _identityService.GetUserAsync(request.Id);

        if (entity == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), request.Id);
        }

        var departmentEntity = await _context.Departments
            .FindAsync(new object[] { request.DepartmentId }, cancellationToken);

        if (departmentEntity == null)
        {
            throw new NotFoundException(nameof(Department), request.DepartmentId);
        }

        var result = await _identityService.EditUserAsync(request.Id, request.UserName, request.Password, request.DepartmentId);        

        var newEntity = await _identityService.GetUserAsync(result.UserId);

        newEntity.AddDomainEvent(new UserCreatedEvent(newEntity));

        var ret = _mapper.Map<ApplicationUserDto>(newEntity);

        return ret;
    }
}
