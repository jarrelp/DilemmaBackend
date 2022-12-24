using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class ApplicationUserDto : IMapFrom<ApplicationUser>
{
    public string Id { get; set; } = null!;

    public string UserName { get; set; } = null!;

    public string Department { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ApplicationUser, ApplicationUserDto>()
            .ForMember(d => d.Department, opt => opt.MapFrom(s => s.Department.Name ?? ""));
    }
}
