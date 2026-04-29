using BobsCorn.Application.Corn.BuyCorn;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace BobsCorn.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CornController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CornController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("buy")]
        public async Task<IActionResult> BuyCorn(CancellationToken cancellationToken)
        {
            var clientId = Request.Headers["X-Client-Id"].FirstOrDefault();
            
            var command = new BuyCornCommand(clientId ?? string.Empty);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                if (result.RetryAfterSeconds.HasValue)
                {
                    Response.Headers["Retry-After"] = result.RetryAfterSeconds.Value.ToString();
                }

                return StatusCode(StatusCodes.Status429TooManyRequests, result);
            }
            
            return Ok(result);
        }
    }
}
