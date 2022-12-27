using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class QuestionDto : IMapFrom<Question>
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
}
