   using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class QuestionDto : IMapFrom<Question>
{
    public QuestionDto()
    {
        Options = new List<OptionDto>();
    }

    public int Id { get; set; }

    public int QuizId { get; set; }

    public string Description { get; set; } = null!;

    public IList<OptionDto> Options { get; set; }
}
