using Application.Commands.Clients.CreateClient;
using Application.Commands.Clients.UpdateClient;
using Application.Queries;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Client.Dto;

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
    public async Task<IActionResult> CreateClientAsync([FromBody] CreateClientDto createDto, CancellationToken token)
    {
        return Json(await _sender.Send(createDto.Adapt<CreateClientCommand>(), token));
    }

    [HttpPut("update")]
    public async Task<IActionResult> UpdateClientAsync([FromBody] UpdateClientDto updateDto, CancellationToken token)
    {
        return Json(await _sender.Send(updateDto.Adapt<UpdateClientCommand>(), token));
    }

    [HttpPatch("add-founder")]
    public async Task<IActionResult> AddFounderAsync([FromBody] AddFounderDto addFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(new AddFoundersCommand(addFounderDto.ClientId, addFounderDto.Founders), token));
    }

    [HttpGet("list")]
    public async Task<IActionResult> ClientsListAsync([FromBody] UpdateClientDto updateDto, CancellationToken token)
    {
        return Json(await _sender.Send(updateDto.Adapt<GetClientListQuery>(), token));
    }

    [HttpDelete("delete-founder")]
    public async Task<IActionResult> DeleteFounderAsync([FromBody] AddFounderDto addFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(new DeleteFoundersCommand(addFounderDto.ClientId, addFounderDto.Founders), token));
    }

    [HttpDelete("id:guid")]
    public async Task DeleteClientAsync([FromRoute] Guid id, CancellationToken token)
    {
        await _sender.Send(new DeleteClientCommand(id), token);
    }
}