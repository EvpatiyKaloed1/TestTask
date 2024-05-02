using Application.Commons.Interfaces;
using MediatR;

namespace Application.Commands.Founders.DeleteFounder;

public sealed class DeleteFounderCommandHandler : IRequestHandler<DeleteFounderCommand>
{
    private readonly IFounderRepository _repository;

    public DeleteFounderCommandHandler(IFounderRepository repository)
    {
        _repository = repository;
    }

    public async Task Handle(DeleteFounderCommand request, CancellationToken cancellationToken)
    {
        await _repository.DeleteAsync(request.Id, cancellationToken);
    }
}