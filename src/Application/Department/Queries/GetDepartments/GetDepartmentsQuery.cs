using System.Linq;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartments;

public record GetDepartmentQuery : IRequest<List<DepartmentDto>>;

public class GetDepartmentsWithPaginationQueryHandler : IRequestHandler<GetDepartmentQuery, List<DepartmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DepartmentDto>> Handle(GetDepartmentQuery request, CancellationToken cancellationToken)
    {
        return await _context.Departments
            .OrderBy(x => x.Name)
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
    }
}
