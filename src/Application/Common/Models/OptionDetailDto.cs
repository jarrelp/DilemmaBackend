using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionDetailDto : OptionDto
{
    public OptionDetailDto()
    {
        OptionSkills = new List<OptionSkillDto>();
    }

    public IList<OptionSkillDto> OptionSkills { get; set; }
}
