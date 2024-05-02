using Application.Commands.Founders.CreateFounder;
using Application.Commands.Founders.DeleteFounder;
using Application.Commands.Founders.UpdateFounder;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.Controllers.Founder.Dto;

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
    public async Task<IActionResult> CreateFounderAsync([FromBody] CreateFounderDto createFounderDto, CancellationToken token)
    {
        return Json(await _sender.Send(createFounderDto.Adapt<CreateFounderCommand>(), token));
    }

    [HttpDelete("id:guid")]
    public async Task DeleteFounderAsync([FromRoute] Guid Id)
    {
        await _sender.Send(new DeleteFounderCommand(Id));
    }

    [HttpPatch("update")]
    public async Task<IActionResult> UpdateAsync([FromBody] Dto.UpdateFounderDto updateFounderDto, CancellationToken token)
    {
        return Json(updateFounderDto.Adapt<UpdateFounderCommand>(), token);
    }
}