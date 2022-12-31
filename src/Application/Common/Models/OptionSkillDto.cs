using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionSkillDto : IMapFrom<OptionSkill>
{
    public string Id { get; set; } = null!;

    public int SkillLevel { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OptionSkill, OptionSkillDto>()
            .ForMember(d => d.Id, opt => opt.MapFrom(s => s.OptionId + "-" + s.SkillId))
            .ForMember(d => d.SkillLevel, opt => opt.MapFrom(s => (int)s.SkillLevel));
    }
}
