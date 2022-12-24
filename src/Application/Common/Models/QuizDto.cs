using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class QuizDto : IMapFrom<Quiz>
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public bool Active { get; set; }
}
