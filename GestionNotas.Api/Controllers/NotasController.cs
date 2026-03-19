using GestionNotas.Api.Application.Common.Models;
using GestionNotas.Application.Transactions.Nota.Commands;
using GestionNotas.Api.Domain.Entities;
using GestionNotas.Domain.Interfaces.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using GestionNotas.Application.Transactions.Nota.Queries;

namespace GestionNotas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NotasController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetNotasQuery(), cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new GetNotaByIdQuery(id), cancellationToken);

            if (!result.Exitoso)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<int>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post(
            [FromBody] CreateNotaCommand command,
            CancellationToken cancellationToken)
        {
            var resultado = await mediator.Send(command, cancellationToken);

            if (!resultado.Exitoso)
                return UnprocessableEntity(resultado);

            return CreatedAtAction(
                nameof(GetById),
                new { id = resultado.Data },
                resultado);
        }

        [HttpPut]
        public async Task<IActionResult> Put(UpdateNotaCommand command, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(command, cancellationToken);

            if (!result.Exitoso)
                return UnprocessableEntity(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var result = await mediator.Send(new DeleteNotaCommand(id), cancellationToken);

            if (!result.Exitoso)
                return NotFound(result);

            return Ok(result);
        }
    }
}
