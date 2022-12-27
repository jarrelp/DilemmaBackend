using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using CleanArchitecture.Application.Common.Mappings;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Questions.Queries.GetQuestionsByActiveQuiz;
public class ActiveQuestionDto : IMapFrom<Question>
{
    public ActiveQuestionDto() 
    {
        Options = new List<ActiveOptionDto>();
    }
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public IList<ActiveOptionDto> Options { get; set; }
}

public class ActiveOptionDto : IMapFrom<Option>
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;
}
