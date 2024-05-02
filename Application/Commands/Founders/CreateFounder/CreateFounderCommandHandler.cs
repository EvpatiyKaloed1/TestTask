using Application.Commons.Interfaces;
using Domain.Clients.Exeptions;
using Domain.Common.ValueObjects;
using Domain.Founders;
using MediatR;

namespace Application.Commands.Founders.CreateFounder;

public class CreateFounderCommandHandler : IRequestHandler<CreateFounderCommand, Founder>
{
    private readonly IFounderRepository _founderRepository;

    public CreateFounderCommandHandler(IFounderRepository repository)
    {
        _founderRepository = repository;
    }

    public async Task<Founder> Handle(CreateFounderCommand request, CancellationToken cancellationToken)
    {
        var founder = await _founderRepository.GetFounderByInnAsync(request.Inn, cancellationToken);
        if (founder != null)
        {
            throw new InnTakenException(request.Inn);
        }

        var newFounder = new Founder(request.Inn, request.FounderFull, new Dates(DateTime.Now));

        return await _founderRepository.CreateFounderAsync(newFounder, cancellationToken);
    }
}