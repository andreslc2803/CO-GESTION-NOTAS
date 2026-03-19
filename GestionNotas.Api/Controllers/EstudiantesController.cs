using GestionNotas.Application.Transactions.Estudiante.Commands;
using GestionNotas.Application.Transactions.Estudiante.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GestionNotas.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudiantesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get(CancellationToken ct)
            => Ok(await mediator.Send(new GetEstudiantesQuery(), ct));

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken ct)
            => Ok(await mediator.Send(new GetEstudianteByIdQuery(id), ct));

        [HttpPost]
        public async Task<IActionResult> Post(CreateEstudianteCommand cmd, CancellationToken ct)
            => Ok(await mediator.Send(cmd, ct));

        [HttpPut]
        public async Task<IActionResult> Put(UpdateEstudianteCommand cmd, CancellationToken ct)
            => Ok(await mediator.Send(cmd, ct));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
            => Ok(await mediator.Send(new DeleteEstudianteCommand(id), ct));
    }
}
