using CleanArchitecture.Application.Common.Mappings;

namespace CleanArchitecture.Application.Common.Models;

public class ResultDto : IMapFrom<Domain.Entities.Result>
{
    public ResultDto()
    {
        Answers = new List<OptionDto>();
    }

    public int Id { get; set; }

    public int QuizId { get; set; }

    public string ApplicationUserId { get; set; } = null!;

    public IList<OptionDto> Answers { get; set; }
}
