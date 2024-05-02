using Domain.Founders;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries.Founders;
public sealed class GetFounderListQuery : IRequest<IEnumerable<Founder>>;
