using MediatR;
using Microsoft.AspNetCore.Mvc;
using QuantumWorld.Infrastructure.Commands.Users;

namespace QuantumWorld.Api.Controllers;

public class MessagesController : ApiControllerBase
{
    private readonly IMediator _mediator;

    public MessagesController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpDelete("{id}/{username}")]
    public async Task<IActionResult> Delete(DeleteMessage request)
    {
        await _mediator.Send(request);
        return NoContent();
    }
}


