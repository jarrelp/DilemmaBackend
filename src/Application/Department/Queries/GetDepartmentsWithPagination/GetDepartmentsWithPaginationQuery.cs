using AutoMapper;
using AutoMapper.QueryableExtensions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Application.Common.Models;
using MediatR;

namespace CleanArchitecture.Application.Departments.Queries.GetDepartmentsWithPagination;

public record GetDepartmentsWithPaginationQuery : IRequest<PaginatedList<DepartmentDto>>
{
    public int? DepartmentId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetDepartmentsWithPaginationQueryHandler : IRequestHandler<GetDepartmentsWithPaginationQuery, PaginatedList<DepartmentDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetDepartmentsWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<DepartmentDto>> Handle(GetDepartmentsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        var ret = new PaginatedList<DepartmentDto>();
        ret = request.DepartmentId == null
            ? await _context.Departments
            .OrderBy(x => x.Name)
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize)
            : await _context.Departments
            .Where(x => x.Id == request.DepartmentId)
            .OrderBy(x => x.Name)
            .ProjectTo<DepartmentDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
        return ret;
    }
}
