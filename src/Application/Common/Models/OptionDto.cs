using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionDto : IMapFrom<Option>
{
    public OptionDto()
    {
        OptionSkills = new List<OptionSkillDto>();
    }

    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public int QuestionId { get; set; }

    public int ResultId { get; set; }

    public IList<OptionSkillDto> OptionSkills { get; set; }
}
