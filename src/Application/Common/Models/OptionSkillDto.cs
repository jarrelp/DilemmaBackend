using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionSkillDto : IMapFrom<OptionSkill>
{
    public string Option { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int SkillLevel { get; set; }

    public string Skill { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OptionSkill, OptionSkillDto>()
            .ForMember(d => d.Option, opt => opt.MapFrom(s => s.Option.Description))
            .ForMember(d => d.Skill, opt => opt.MapFrom(s => s.Skill.Name))
            .ForMember(d => d.SkillLevel, opt => opt.MapFrom(s => (int)s.SkillLevel));
    }
}
