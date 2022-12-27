using AutoMapper;
using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Events.Result;
using MediatR;

namespace CleanArchitecture.Application.Results.Commands.CreateResult;

public record CreateResultCommand : IRequest<ResultDto>
{
    public int QuizId { get; set; }

    public string ApplicationUserId { get; set; } = null!;

    public IList<int> AnswerIds { get; init; } = new List<int>();
}

public class CreateResultCommandHandler : IRequestHandler<CreateResultCommand, ResultDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;

    public CreateResultCommandHandler(IApplicationDbContext context, IIdentityService identityService, IMapper mapper)
    {
        _context = context;
        _identityService = identityService;
        _mapper = mapper;
    }

    public async Task<ResultDto> Handle(CreateResultCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await _identityService.GetUserAsync(request.ApplicationUserId);

        if (userEntity == null)
        {
            throw new NotFoundException(nameof(ApplicationUser), request.ApplicationUserId);
        }

        var quizEntity = await _context.Quizzes
            .FindAsync(new object[] { request.QuizId }, cancellationToken);

        if (quizEntity == null)
        {
            throw new NotFoundException(nameof(Quiz), request.QuizId);
        }

        var answers = new List<Option>();
        foreach(var item in request.AnswerIds)
        {
            answers.Add(_context.Options.Where(x => x.Id == item).First());
        }

        var entity = new Domain.Entities.Result
        {
            Quiz = quizEntity,
            ApplicationUser = userEntity,
            Answers = answers
        };

        var resultEntity = await _identityService.AddUserResultAsync(userEntity, entity);

        if(!resultEntity.Result.Succeeded)
        {
            throw new NotFoundException();
        }

        entity.AddDomainEvent(new ResultCreatedEvent(entity));

        /*_context.Results.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);*/

        var result = _mapper.Map<ResultDto>(entity);

        return result;
    }
}
