using Application.Commons.Interfaces;
using Domain.Clients.Dto;
using Domain.Clients.Exeptions;
using Domain.Founders;
using Mapster;
using MediatR;

namespace Application.Commands.Founders.UpdateFounder;

public sealed class UpdateFounderCommandHanlder : IRequestHandler<UpdateFounderCommand, Founder>
{
    private readonly IFounderRepository _repository;

    public UpdateFounderCommandHanlder(IFounderRepository repository)
    {
        _repository = repository;
    }

    public async Task<Founder> Handle(UpdateFounderCommand request, CancellationToken cancellationToken)
    {
        var founder = await _repository.GetFounderByIdAsync(request.Id, cancellationToken);
        if (founder == null)
        {
            throw new NotFoundException(request.Id);
        }

        founder.Update(request.Adapt<UpdateFounderDto>());
        await _repository.UpdateAsync(founder, cancellationToken);

        return founder;
    }
}