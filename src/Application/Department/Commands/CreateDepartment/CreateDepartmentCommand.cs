using AutoMapper;
using AutoMapper.Configuration;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Department;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Departments.Commands.CreateDepartment;

public record CreateDepartmentCommand : IRequest<DepartmentDto>
{
    public string Name { get; init; } = null!;
}

public class CreateDepartmentCommandHandler : IRequestHandler<CreateDepartmentCommand, DepartmentDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CreateDepartmentCommandHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DepartmentDto> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
    {
        var entity = new Department
        {
            Name = request.Name
        };

        entity.AddDomainEvent(new DepartmentCreatedEvent(entity));

        _context.Departments.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        var result = _mapper.Map<DepartmentDto>(entity);

        return result;
    }
}
