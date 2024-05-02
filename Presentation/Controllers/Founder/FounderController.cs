using Application.Commands.Founders.CreateFounder;
using Application.Commands.Founders.DeleteFounder;
using Application.Commands.Founders.UpdateFounder;
using Application.Queries;
using Application.Queries.Founders;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Founder.Dto;
using Swashbuckle.AspNetCore.Annotations;

namespace Presentation.Controllers.Founder;

[ApiController]
[Route("api/founder")]
public class FounderController : Controller
{
    private readonly ISender _sender;

    public FounderController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("create")]
    [SwaggerOperation(Summary = "Создание нового учредителя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Учредитель успешно создан", typeof(Domain.Founders.Founder))]
    public async Task<IActionResult> CreateFounderAsync([FromBody] CreateFounderDto createFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(createFounderDto.Adapt<CreateFounderCommand>(), token));
    }

    [HttpPatch("update")]
    [SwaggerOperation(Summary = "Обновление учредителя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Учредитель успешно обновлен", typeof(Domain.Founders.Founder))]
    public async Task<IActionResult> UpdateAsync([FromBody] UpdateFounderDto updateFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(updateFounderDto.Adapt<UpdateFounderCommand>(), token));
    }

    [HttpDelete]
    [SwaggerOperation(Summary = "Удаление учредителя")]
    [SwaggerResponse(StatusCodes.Status200OK, "Учредитель успешно удален")]
    public async Task DeleteFounderAsync([FromQuery] Guid Id, CancellationToken cancellationToken)
    {
        await _sender.Send(new DeleteFounderCommand(Id), cancellationToken);
    }

    [HttpGet("list")]
    [SwaggerOperation(Summary = "Получение списка всех учредителей")]
    [SwaggerResponse(StatusCodes.Status200OK, "", typeof(IEnumerable<Domain.Founders.Founder>))]
    public async Task<IActionResult> ClientsListAsync(CancellationToken token)
    {
        return Json(await _sender.Send(new GetFounderListQuery(), token));
    }
}