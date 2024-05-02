using Application.Commons.Interfaces;
using Domain.Founders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Founders;
public class GetFounderListQueryHandler : IRequestHandler<GetFounderListQuery, IEnumerable<Founder>>
{
    private readonly IFounderRepository _founderRepository;

    public GetFounderListQueryHandler(IFounderRepository founderRepository)
    {
        _founderRepository = founderRepository;
    }

    public Task<IEnumerable<Founder>> Handle(GetFounderListQuery request, CancellationToken cancellationToken)
    {
        return _founderRepository.GetAllAsync(cancellationToken);
    }
}
