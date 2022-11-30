using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class ApplicationUserDto : IMapFrom<ApplicationUser>
{
    public ApplicationUserDto()
    {
        Results = new List<ResultDto>();
    }

    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int DepartmentId { get; set; }

    public IList<ResultDto> Results { get; set; }
}
