using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Entities;
using MediatR;

namespace CleanArchitecture.Application.Options.Commands.UpdateOption;

public record UpdateOptionCommand : IRequest
{
    public int Id { get; init; }

    public string Description { get; init; } = null!;
}

public class UpdateOptionCommandHandler : IRequestHandler<UpdateOptionCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateOptionCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Options
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Option), request.Id);
        }

        entity.Description = request.Description;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
