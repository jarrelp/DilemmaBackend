using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class ResultDto : IMapFrom<Domain.Entities.Result>
{
    public string Quiz { get; set; } = null!;

    public string User { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Domain.Entities.Result, ResultDto>()
            .ForMember(d => d.Quiz, opt => opt.MapFrom(s => s.Quiz.Description))
            .ForMember(d => d.User, opt => opt.MapFrom(s => s.ApplicationUser.UserName));
    }
}
