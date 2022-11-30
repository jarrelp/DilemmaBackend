using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Common.Models;

public class OptionDto : IMapFrom<Option>
{
    public int Id { get; set; }

    public int QuestionId { get; set; }

    public string Description { get; set; } = null!;
}
