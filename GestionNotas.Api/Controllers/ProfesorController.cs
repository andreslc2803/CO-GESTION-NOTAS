using GestionNotas.Application.Transactions.Profesor.Commands;
using GestionNotas.Application.Transactions.Profesor.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionNotas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfesorController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
            => Ok(await mediator.Send(new GetProfesoresQuery(), ct));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
            => Ok(await mediator.Send(new GetProfesorByIdQuery(id), ct));

        [HttpPost]
        public async Task<IActionResult> Post(CreateProfesorCommand cmd, CancellationToken ct)
            => Ok(await mediator.Send(cmd, ct));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateProfesorCommand cmd, CancellationToken ct)
            => Ok(await mediator.Send(cmd, ct));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
            => Ok(await mediator.Send(new DeleteProfesorCommand(id), ct));
    }
}
