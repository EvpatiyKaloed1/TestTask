using Application.Commands.Clients.AddFounders;
using Application.Commands.Clients.CreateClient;
using Application.Commands.Clients.DeleteClient;
using Application.Commands.Clients.DeleteFounders;
using Application.Commands.Clients.UpdateClient;
using Application.Queries;
using Application.Queries.Clients;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Client.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers.Client;

[ApiController]
[Route("api/clients")]
public class ClientController : Controller
{
    private readonly ISender _sender;

    public ClientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Создание нового клиента")]
    [SwaggerResponse(StatusCodes.Status200OK, "Клиент успешно создан", typeof(Domain.Clients.Client))]
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientDto createDto, CancellationToken token)
    {
        return Json(await _sender.Send(createDto.Adapt<CreateClientCommand>(), token));
    }

    [HttpPut("update")]
    [SwaggerOperation(Summary = "Обновление клиента")]
    [SwaggerResponse(StatusCodes.Status200OK, "Клиент успешно обновлен", typeof(Domain.Clients.Client))]
    public async Task<IActionResult> UpdateClientAsync([FromBody] UpdateClientDto updateDto, CancellationToken token)
    {
        return Json(await _sender.Send(updateDto.Adapt<UpdateClientCommand>(), token));
    }

    [HttpPatch("add-founder")]
    [SwaggerOperation(Summary = "Добавление учредителей к клиенту")]
    [SwaggerResponse(StatusCodes.Status200OK, "Учредители успешно добавлены", typeof(Domain.Clients.Client))]
    public async Task<IActionResult> AddFounderAsync([FromBody] AddFounderDto addFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(new AddFoundersCommand(addFounderDto.ClientId, addFounderDto.Founders), token));
    }

    [HttpDelete("delete-founder")]
    [SwaggerOperation(Summary = "Удаление учредителей клиента")]
    [SwaggerResponse(StatusCodes.Status200OK, "Учредители успешно удалены", typeof(Domain.Clients.Client))]
    public async Task<IActionResult> DeleteFounderAsync([FromBody] AddFounderDto addFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(new DeleteFoundersCommand(addFounderDto.ClientId, addFounderDto.Founders), token));
    }

    [HttpGet("list")]
    [SwaggerOperation(Summary = "Получение списка всех клиентов")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IEnumerable<Domain.Clients.Client>))]
    public async Task<IActionResult> ClientsListAsync(CancellationToken token)
    {
        return Json(await _sender.Send(new GetClientListQuery(), token));
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Удаление клиента")]
    [SwaggerResponse(StatusCodes.Status200OK, "Клиент успешно удален")]
    public async Task DeleteClientAsync([FromQuery]Guid id, CancellationToken token)
    {
        await _sender.Send(new DeleteClientCommand(id), token);
    }
}