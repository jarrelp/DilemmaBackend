using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionSkillDto : IMapFrom<OptionSkill>
{
    public int Id { get; set; }

    public int OptionId { get; set; }

    public string Description { get; set; } = null!;

    public int SkillLevel { get; set; }

    public SkillDto Skill { get; set; } = null!;

    public void Mapping(Profile profile)
    {
        profile.CreateMap<OptionSkill, OptionSkillDto>()
            .ForMember(d => d.SkillLevel, opt => opt.MapFrom(s => (int)s.SkillLevel));
    }
}
