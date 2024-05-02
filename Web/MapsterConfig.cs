using Application.Commands.Clients.CreateClient;
using Application.Commands.Clients.UpdateClient;
using Application.Commands.Founders.CreateFounder;
using Application.Commands.Founders.UpdateFounder;
using Domain.Clients;
using Domain.Clients.ValueObjects;
using Domain.Common.ValueObjects;
using Domain.Founders;
using Domain.Founders.ValueObjects;
using Infrastructure.Dto;
using Mapster;
using MapsterMapper;
using Presentation.Controllers.Client.Dto;
using Presentation.Controllers.Founder.Dto;
using System.Reflection;

namespace Web;

public static class MapsterConfig
{
    public static IServiceCollection RegisterMapster(this IServiceCollection services)
    {
        TypeAdapterConfig<CreateClientDto, CreateClientCommand>
        .ForType()
        .MapWith(x => new CreateClientCommand(new ClientName(x.Name), new Inn(x.Inn), x.Type, x.Founders));

        TypeAdapterConfig<Client, ClientDto>
        .ForType()
        .MapWith(x => new ClientDto
        {
            Id = x.Id,
            Name = x.Name.Name,
            Inn = x.Inn.InnValue,
            Type = x.Type,
            Created = x.Date.Created.ToUniversalTime(),
            Updated = x.Date.Updated != null ? x.Date.Updated.Value.ToUniversalTime() : null,
            Founders = x.Founders.Adapt<List<FounderDto>>()
        });

        TypeAdapterConfig<Founder, FounderDto>
        .ForType()
        .MapWith(x => new FounderDto
        {
            Inn = x.Inn.InnValue,
            FirstName = x.Name.FirstName,
            LastName = x.Name.LastName,
            SurName = x.Name.SurName,
            Created = x.Date.Created.ToUniversalTime(),
            Updated = x.Date.Updated != null ? x.Date.Updated.Value.ToUniversalTime() : null,
            Id = x.Id
        });

        TypeAdapterConfig<Presentation.Controllers.Client.Dto.UpdateClientDto, UpdateClientCommand>
        .ForType()
        .MapWith(x => new UpdateClientCommand(x.Id, new Inn(x.Inn), x.Type, new ClientName(x.Name)));

        TypeAdapterConfig<UpdateClientCommand, Domain.Clients.Dto.UpdateClientDto>
        .ForType()
        .MapWith(x => new Domain.Clients.Dto.UpdateClientDto(x.Inn, x.Type, x.Name));

        TypeAdapterConfig<ClientDto, Client>
        .ForType()
        .MapWith(x => new Client(new Inn(x.Inn), x.Type, new Dates(x.Created, x.Updated), new ClientName(x.Name), x.Id, x.Founders.Adapt<IEnumerable<Founder>>()));

        TypeAdapterConfig<FounderDto, Founder>
         .ForType()
         .MapWith(x => new Founder(new Inn(x.Inn), new FounderFullName(x.FirstName, x.LastName, x.SurName), new Dates(x.Created, x.Updated), x.Id));

        TypeAdapterConfig<CreateFounderDto, CreateFounderCommand>
         .ForType()
         .MapWith(x => new CreateFounderCommand(new Inn(x.Inn), new FounderFullName(x.FirstName, x.LastName, x.SurName)));

        TypeAdapterConfig<Presentation.Controllers.Founder.Dto.UpdateFounderDto, UpdateFounderCommand>
        .ForType()
        .MapWith(x => new UpdateFounderCommand(x.Id, x.Inn != null ? new Inn(x.Inn) : null, x.FirstName, x.LastName, x.SurName));

        TypeAdapterConfig<UpdateFounderCommand, Domain.Clients.Dto.UpdateFounderDto>
        .ForType()
        .MapWith(x => new Domain.Clients.Dto.UpdateFounderDto(x.Inn, x.FirstName, x.LastName, x.SurName));
        TypeAdapterConfig.GlobalSettings.Scan(Assembly.GetExecutingAssembly());

        var typeAdapterConfig = TypeAdapterConfig.GlobalSettings;
        typeAdapterConfig.Scan(Assembly.GetExecutingAssembly());
        var mapperConfig = new Mapper(typeAdapterConfig);

        services.AddSingleton<IMapper>(mapperConfig);
        return services;
    }
}