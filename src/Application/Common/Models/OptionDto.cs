using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionDto : IMapFrom<Option>
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Question { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<Option, OptionDto>()
            .ForMember(d => d.Question, opt => opt.MapFrom(s => s.Question.Description ?? ""));
    }
}
